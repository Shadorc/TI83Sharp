using System.Diagnostics.CodeAnalysis;

namespace TI83Sharp;

public class TokenType
{
    private static readonly Dictionary<TokenTypeCategory, Dictionary<string, TokenType>> s_tokenTypes = new Dictionary<TokenTypeCategory, Dictionary<string, TokenType>>();

    // Single character
    public static TokenType Store { get; } = new TokenType(TokenTypeCategory.SingleChar, "→");
    public static TokenType Colon { get; } = new TokenType(TokenTypeCategory.SingleChar, ":");
    public static TokenType Comma { get; } = new TokenType(TokenTypeCategory.SingleChar, ",");
    public static TokenType LeftParentheses { get; } = new TokenType(TokenTypeCategory.SingleChar, "(");
    public static TokenType RightParentheses { get; } = new TokenType(TokenTypeCategory.SingleChar, ")");
    public static TokenType LeftCurlyBracket { get; } = new TokenType(TokenTypeCategory.SingleChar, "{");
    public static TokenType RightCurlyBracket { get; } = new TokenType(TokenTypeCategory.SingleChar, "}");
    public static TokenType LeftSquareBracket { get; } = new TokenType(TokenTypeCategory.SingleChar, "[");
    public static TokenType RightSquareBracket { get; } = new TokenType(TokenTypeCategory.SingleChar, "]");
    public static TokenType Plus { get; } = new TokenType(TokenTypeCategory.SingleChar, "+");
    public static TokenType Minus { get; } = new TokenType(TokenTypeCategory.SingleChar, "-");
    public static TokenType Mult { get; } = new TokenType(TokenTypeCategory.SingleChar, "*");
    public static TokenType Div { get; } = new TokenType(TokenTypeCategory.SingleChar, "/");
    public static TokenType Pow { get; } = new TokenType(TokenTypeCategory.SingleChar, "^");
    public static TokenType Square { get; } = new TokenType(TokenTypeCategory.SingleChar, "²");
    public static TokenType Cube { get; } = new TokenType(TokenTypeCategory.SingleChar, "³");
    public static TokenType TenPower { get; } = new TokenType(TokenTypeCategory.SingleChar, "ᴇ");
    public static TokenType Equal { get; } = new TokenType(TokenTypeCategory.SingleChar, "=");
    public static TokenType NotEqual { get; } = new TokenType(TokenTypeCategory.SingleChar, "≠");
    public static TokenType Greater { get; } = new TokenType(TokenTypeCategory.SingleChar, ">");
    public static TokenType GreaterEqual { get; } = new TokenType(TokenTypeCategory.SingleChar, "≥");
    public static TokenType Less { get; } = new TokenType(TokenTypeCategory.SingleChar, "<");
    public static TokenType LessEqual { get; } = new TokenType(TokenTypeCategory.SingleChar, "≤");

    // Reserved word
    public static TokenType If { get; } = new TokenType(TokenTypeCategory.ReservedWord, "If");
    public static TokenType Then { get; } = new TokenType(TokenTypeCategory.ReservedWord, "Then");
    public static TokenType Else { get; } = new TokenType(TokenTypeCategory.ReservedWord, "Else");
    public static TokenType While { get; } = new TokenType(TokenTypeCategory.ReservedWord, "While");
    public static TokenType Repeat { get; } = new TokenType(TokenTypeCategory.ReservedWord, "Repeat");
    public static TokenType For { get; } = new TokenType(TokenTypeCategory.ReservedWord, "For");
    public static TokenType End { get; } = new TokenType(TokenTypeCategory.ReservedWord, "End");
    public static TokenType Lbl { get; } = new TokenType(TokenTypeCategory.ReservedWord, "Lbl");
    public static TokenType Goto { get; } = new TokenType(TokenTypeCategory.ReservedWord, "Goto");
    public static TokenType And { get; } = new TokenType(TokenTypeCategory.ReservedWord, "and");
    public static TokenType Or { get; } = new TokenType(TokenTypeCategory.ReservedWord, "or");
    public static TokenType Xor { get; } = new TokenType(TokenTypeCategory.ReservedWord, "xor");

    // Misc
    public static TokenType String { get; } = new TokenType(TokenTypeCategory.Misc, "String");
    public static TokenType Number { get; } = new TokenType(TokenTypeCategory.Misc, "Number");
    public static TokenType ConstId { get; } = new TokenType(TokenTypeCategory.Misc, "ConstId");
    public static TokenType NumberId { get; } = new TokenType(TokenTypeCategory.Misc, "NumberId");
    public static TokenType StringId { get; } = new TokenType(TokenTypeCategory.Misc, "StringId");
    public static TokenType ListId { get; } = new TokenType(TokenTypeCategory.Misc, "ListId");
    public static TokenType MatrixId { get; } = new TokenType(TokenTypeCategory.Misc, "MatrixId");
    public static TokenType CommandId { get; } = new TokenType(TokenTypeCategory.Misc, "CommandId");
    public static TokenType FunctionId { get; } = new TokenType(TokenTypeCategory.Misc, "FunctionId");
    public static TokenType LblName { get; } = new TokenType(TokenTypeCategory.Misc, "LblId");
    public static TokenType Eof { get; } = new TokenType(TokenTypeCategory.Misc, "EOF");

    private static void Register(TokenType tokenType)
    {
        if (!s_tokenTypes.TryGetValue(tokenType.Category, out var tokenTypes))
        {
            tokenTypes = new Dictionary<string, TokenType>();
            s_tokenTypes[tokenType.Category] = tokenTypes;
        }

        tokenTypes[tokenType.Lexeme] = tokenType;
    }

    public static List<TokenType> GetTokenTypesByCategory(TokenTypeCategory category)
    {
        return new List<TokenType>(s_tokenTypes[category].Values);
    }

    public static bool TryGet(TokenTypeCategory category, string representation, [MaybeNullWhen(false)] out TokenType tokenType)
    {
        if (s_tokenTypes.TryGetValue(category, out var tokenTypes))
        {
            return tokenTypes.TryGetValue(representation, out tokenType);
        }

        tokenType = null;
        return false;
    }

    public readonly TokenTypeCategory Category;
    public readonly string Lexeme;

    public TokenType(TokenTypeCategory category, string lexeme)
    {
        Category = category;
        Lexeme = lexeme;

        Register(this);
    }

    public override string ToString()
    {
        return $"TokenType({Lexeme}, {Category})";
    }
}
