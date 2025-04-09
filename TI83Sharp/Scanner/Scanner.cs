using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace TI83Sharp;

public class Scanner
{
    private const char DECIMAL_SEP = '.';
    private const char EOF_CHAR = '\0';

    private static readonly Dictionary<string, Token> _reservedKeywords = GetReservedKeywords();
    private static readonly List<char> _reservedChars = GetReservedChars();

    private readonly IOutput _output;
    private readonly string _source;
    private readonly List<Token> _tokens;

    private int _pos;
    private char _currentChar;
    private int _line;

    private static Dictionary<string, Token> GetReservedKeywords()
    {
        var reservedKeywords = new Dictionary<string, Token>();
        var tokenTypes = TokenType.GetTokenTypesByCategory(TokenTypeCategory.ReservedWord);
        foreach (var tokenType in tokenTypes)
        {
            var lexeme = tokenType.Lexeme;
            reservedKeywords[lexeme] = new Token(tokenType, lexeme);
        }
        return reservedKeywords;
    }

    private static List<char> GetReservedChars()
    {
        var reservedChars = new List<char>();
        var tokenTypes = TokenType.GetTokenTypesByCategory(TokenTypeCategory.SingleChar);
        foreach (var tokenType in tokenTypes)
        {
            reservedChars.Add(tokenType.Lexeme[0]);
        }
        return reservedChars;
    }

    private static string NormalizeSource(string source)
    {
        var delvarRegex = new Regex(@"DelVar ([θA-Z])(?!:)(?!\s*\n\s*:)", RegexOptions.Compiled);

        // Normalize line endings to LF     
        source = source.Replace("\r\n", "\n");
        // Normalize MINUS SIGN to HYPHEN-MINUS
        source = source.Replace('−', '-');
        // Add colon after DelVar command if absent because this is the only chainable command
        source = delvarRegex.Replace(source, "DelVar $1:");

        var normalizedSource = new StringBuilder();
        var normalizedLine = new StringBuilder();

        foreach (var line in source.Split('\n'))
        {
            normalizedLine.Clear();
            normalizedLine.Append(line.TrimStart());

            // Add colons to the beginning of the line if not present
            if (normalizedLine.Length == 0 || normalizedLine[0] != ':')
            {
                normalizedLine.Insert(0, ':');
            }

            normalizedSource.AppendLine(normalizedLine.ToString());
        }
        return normalizedSource.ToString();
    }

    public Scanner(IOutput output, string source)
    {
        _output = output;
        _source = NormalizeSource(source);
        _tokens = new List<Token>();

        _pos = 0;
        _currentChar = _source.Length == 0 ? EOF_CHAR : _source[_pos];
        _line = 1;
    }

    private SyntaxError Error(string message)
    {
        return new SyntaxError(message, _line);
    }

    public void ScanTokens(List<Token> tokens)
    {
        _tokens.Clear();

        while (!IsAtEOF())
        {
            ScanToken();
        }

        AddToken(TokenType.Eof);

        tokens.AddRange(_tokens);
    }

    public void ScanToken()
    {
        if (_currentChar == ' ' || _currentChar == '\t' || _currentChar == '\r' || _currentChar == '\n')
        {
            Advance();
        }
        else if (LastTokenType() == TokenType.Lbl || LastTokenType() == TokenType.Goto)
        {
            LblName();
        }
        // Valid numbers: '5' '5.2' '.2'
        else if (char.IsNumber(_currentChar) || _currentChar == DECIMAL_SEP && char.IsNumber(Peek()))
        {
            Number();
        }
        else if (_currentChar == '"')
        {
            String();
        }
        // Matrix IDs need to be processed before single chars because '[' is a reserved char
        else if (_currentChar == '[' && Environment.MatrixIDs.Contains(Peek()) && Peek(2) == ']')
        {
            MatrixID();
        }
        else if (TokenType.TryGet(TokenTypeCategory.SingleChar, _currentChar.ToString(), out var singleCharTokenType))
        {
            Advance();
            AddToken(singleCharTokenType);
        }
        else
        {
            ID();
        }
    }

    private void AddToken(TokenType type, string? lexeme = null, object? literal = null)
    {
        _tokens.Add(new Token(type, lexeme ?? type.Lexeme, literal, _line));
    }

    private char Peek(int offset = 1)
    {
        int peekPos = _pos + offset;
        if (peekPos >= _source.Length)
        {
            return EOF_CHAR;
        }

        return _source[peekPos];
    }

    private void Advance()
    {
        Debug.Assert(_currentChar != EOF_CHAR, $"{nameof(Advance)} cannot be called if the EOF has been reached");

        if (_currentChar == '\n')
        {
            _line++;
        }

        _pos++;
        _currentChar = IsAtEOF() ? EOF_CHAR : _source[_pos];
    }

    private void LblName()
    {
        var lexemeBuilder = new StringBuilder();
        while (!IsAtEndOfStatement())
        {
            if (!Environment.AllowedLabelNameChars.Contains(_currentChar))
            {
                throw Error("Label name must only contain letters (including θ) and numbers 0 to 9.");
            }

            lexemeBuilder.Append(_currentChar);
            Advance();
        }

        var lexeme = lexemeBuilder.ToString();
        if (lexeme.Length < 1 || lexeme.Length > 2)
        {
            throw Error("Label name must be 1 or 2 characters long.");
        }

        AddToken(TokenType.LblName, lexeme);
    }

    private void Number()
    {
        var lexemeBuilder = new StringBuilder();
        while (char.IsNumber(_currentChar))
        {
            lexemeBuilder.Append(_currentChar);
            Advance();
        }

        if (_currentChar == DECIMAL_SEP)
        {
            // Leading decimal point
            if (lexemeBuilder.Length == 0)
            {
                lexemeBuilder.Append('0');
            }

            lexemeBuilder.Append(DECIMAL_SEP);
            Advance();

            while (char.IsNumber(_currentChar))
            {
                lexemeBuilder.Append(_currentChar);
                Advance();
            }
        }

        var lexeme = lexemeBuilder.ToString();
        var literal = float.Parse(lexeme, CultureInfo.InvariantCulture);
        AddToken(TokenType.Number, lexeme, literal);
    }

    public void String()
    {
        var lexemeBuilder = new StringBuilder(_currentChar);

        // First double-quote
        Advance();

        while (_currentChar != '"' && !IsAtEOF() && !IsAtNewLine() && _currentChar != '→')
        {
            lexemeBuilder.Append(_currentChar);
            Advance();
        }

        // Closing quotes are optional
        if (_currentChar == '"')
        {
            lexemeBuilder.Append('"');
            Advance();
        }

        var lexeme = lexemeBuilder.ToString();
        var literal = lexeme.Replace("\"", "");
        AddToken(TokenType.String, lexeme, literal);
    }

    private void MatrixID()
    {
        var strBuilder = new StringBuilder();
        for (int i = 0; i < 3; ++i)
        {
            strBuilder.Append(_currentChar);
            Advance();
        }

        var lexeme = strBuilder.ToString();
        AddToken(TokenType.MatrixId, lexeme);
    }

    private void ID()
    {
        var lexemeBuilder = new StringBuilder();

        while (!IsAtEndOfStatement() && !char.IsWhiteSpace(_currentChar) && !_reservedChars.Contains(_currentChar))
        {
            lexemeBuilder.Append(_currentChar);
            Advance();
        }

        var lexeme = lexemeBuilder.ToString();

        if (_reservedKeywords.TryGetValue(lexeme, out var token))
        {
            AddToken(token.Type, lexeme);
            return;
        }

        if (Environment.Consts.ContainsKey(lexeme))
        {
            AddToken(TokenType.ConstId, lexeme);
            return;
        }

        if (lexeme.Length == 1 && Environment.NumberIDs.Contains(lexeme[0]))
        {
            AddToken(TokenType.NumberId, lexeme);
            return;
        }

        if (Environment.StrIDs.Contains(lexeme))
        {
            AddToken(TokenType.StringId, lexeme);
            return;
        }

        if (lexeme.StartsWith(Environment.LIST_NAME_START) || Environment.ListIDs.Contains(lexeme))
        {
            CheckListID(lexeme);
            AddToken(TokenType.ListId, lexeme);
            return;
        }

        if (IsImplicitVarMult(lexeme))
        {
            ImplicitVarMult(lexeme);
            return;
        }

        if (_currentChar == '(')
        {
            lexeme += _currentChar;
            Advance();
        }

        if (Environment.CommandNames.Contains(lexeme))
        {
            AddToken(TokenType.CommandId, lexeme);
            return;
        }

        if (Environment.FunctionNames.Contains(lexeme))
        {
            AddToken(TokenType.FunctionId, lexeme);
            return;
        }

        throw Error(lexeme);
    }

    // TODO: Custom lists that do not need LIST_NAME_START
    private void CheckListID(string lexeme)
    {
        if (Environment.ListIDs.Contains(lexeme))
        {
            return;
        }

        var startNameIdx = lexeme.StartsWith(Environment.LIST_NAME_START) ? 1 : 0;
        if (lexeme.Length < 1 + startNameIdx || lexeme.Length > 5 + startNameIdx)
        {
            throw Error($"List name '{lexeme}' must be between one and five characters.");
        }

        if (!Environment.NumberIDs.Contains(lexeme[startNameIdx]))
        {
            throw Error($"List name '{lexeme}' must start with a letter or theta.");
        }

        for (int i = startNameIdx + 1; i < lexeme.Length; i++)
        {
            if (!Environment.NumberIDs.Contains(lexeme[startNameIdx]) && !char.IsNumber(lexeme[i]))
            {
                throw Error($"List name '{lexeme}' must be comprised of any combination of capital letters and numbers and theta.");
            }
        }
    }

    private static bool IsImplicitVarMult(string lexeme)
    {
        // Check if the lexeme is a multiplication of several number IDs (e.g. AB for A*B)
        foreach (var c in lexeme)
        {
            if (!Environment.NumberIDs.Contains(c))
            {
                return false;
            }
        }
        return true;
    }

    private void ImplicitVarMult(string lexeme)
    {
        for (int i = 0; i < lexeme.Length; i++)
        {
            char c = lexeme[i];
            AddToken(TokenType.NumberId, c.ToString());
            if (i + 1 < lexeme.Length)
            {
                AddToken(TokenType.Mult);
            }
        }
    }

    private bool IsAtEOF()
    {
        return _pos >= _source.Length;
    }

    private bool IsAtNewLine()
    {
        return _currentChar == '\r' || _currentChar == '\n';
    }

    private bool IsAtEndOfStatement()
    {
        return IsAtEOF() || IsAtNewLine() || _currentChar == ':';
    }

    private TokenType? LastTokenType()
    {
        return _tokens.Count > 0 ? _tokens[^1].Type : null;
    }
}

