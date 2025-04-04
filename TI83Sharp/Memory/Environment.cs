
using System.Reflection;

namespace TI83Sharp;

public class Environment
{
    public const char LIST_NAME_START = '∟';
    // Need to be different than 'Ans' to differentiate from function
    public const string ANS_VALUE = "AnsValue";

    public static readonly Dictionary<string, TiNumber> Consts = new Dictionary<string, TiNumber>()
    {
        { "π", (TiNumber)3.1415926535898f },
        { "e", (TiNumber)2.718281828459f }
    };
    public static readonly List<char> AllowedLabelNameChars = "θABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToList();
    public static readonly List<char> NumberIDs = "θABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();
    public static readonly List<string> StrIDs = new List<string>() { "Str0", "Str1", "Str2", "Str3", "Str4", "Str5", "Str6", "Str7", "Str8", "Str9" };
    public static readonly List<string> ListIDs = new List<string> { "L₁", "L₂", "L₃", "L₄", "L₅", "L₆" };
    public static readonly List<char> MatrixIDs = "ABCDEFGHIJ".ToList();

    private static readonly List<Callable> Callables = CreateCallables();
    public static readonly List<string> CommandNames = GetNames<Command>();
    public static readonly List<string> FunctionNames = GetNames<Function>();

    private static List<Callable> CreateCallables()
    {
        var callables = new List<Callable>();

        var types = Assembly.GetExecutingAssembly().GetTypes();
        foreach (var type in types)
        {
            if (!typeof(Callable).IsAssignableFrom(type) || type.IsAbstract)
            {
                continue;
            }

            var callable = (Callable)Activator.CreateInstance(type)!;
            callables.Add(callable);
        }

        return callables;
    }

    private static List<string> GetNames<T>() where T : Callable
    {
        var names = new List<string>();
        foreach (var callable in Callables)
        {
            if (typeof(T).IsAssignableFrom(callable.GetType()))
            {
                names.Add(callable.Name);
            }
        }

        return names;
    }

    public static Callable GetCallableByName(string name)
    {
        foreach (var callable in Callables)
        {
            if (callable.Name == name)
            {
                return callable;
            }
        }

        throw new ArgumentException($"Function '{name}' does not exist.");
    }

    private readonly Dictionary<string, object> _values;

    public Environment()
    {
        _values = new Dictionary<string, object>();

        // Register built-in consts
        foreach ((var name, var value) in Consts)
        {
            Set(name, value);
        }

        // Set number variables to 0
        foreach (var name in NumberIDs)
        {
            Set(name.ToString(), (TiNumber)0);
        }

        // Register built-in functions and commands
        foreach (var callable in Callables)
        {
            Set(callable.Name, callable);
        }

        Set(ANS_VALUE, (TiNumber)0);
    }

    public bool Exists(string name)
    {
        return _values.ContainsKey(name);
    }

    public T Get<T>(string name)
    {
        if (!_values.TryGetValue(name, out var value))
        {
            throw RuntimeError.Undefined;
        }
        return (T)value;
    }

    public void Set(string name, object value)
    {
        _values[name] = value;
    }

    public void Delete(string name)
    {
        if (name.Length == 1 && NumberIDs.Contains(name[0]))
        {
            Set(name, (TiNumber)0);
        }
        else
        {
            _values.Remove(name);
        }
    }

    public void ClearList(string name)
    {
        ((TiList)_values[name]).Clear();
    }

    public void ClearAllLists()
    {
        foreach (var value in _values.Values)
        {
            if (value is TiList list)
            {
                list.Clear();
            }
        }
    }
}
