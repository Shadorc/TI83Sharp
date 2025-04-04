namespace TI83Sharp;

public static class LogicHelper
{
    public static bool IsTrue(object value)
    {
        if (value is not TiNumber number)
        {
            throw RuntimeError.DataType;
        }
        return number != (TiNumber)0;
    }

    // TI-Basic uses 1 for true and 0 for false
    public static TiNumber BoolToNumber(bool value)
    {
        return value ? 1 : 0;
    }
}
