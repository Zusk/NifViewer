using System;
using System.Globalization;

/// <summary>
/// Tiny expression evaluator used for count and condition expressions in nif.xml.
/// Supports simple variable lookups, numeric literals, and single operator expressions
/// with +, -, *, /, ==, !=, &lt;, &gt;, &lt;=, &gt;=.
/// </summary>
public static class NifExpressionEvaluator
{
    public static bool EvaluateCondition(string? expr, NifReadContext ctx)
    {
        if (string.IsNullOrWhiteSpace(expr))
            return true;

        expr = expr.Trim();
        foreach (var op in new[] { "<=", ">=", "==", "!=", "<", ">" })
        {
            int idx = expr.IndexOf(op, StringComparison.Ordinal);
            if (idx > 0)
            {
                string left = expr[..idx].Trim();
                string right = expr[(idx + op.Length)..].Trim();

                long l = EvaluateNumeric(left, ctx);
                long r = EvaluateNumeric(right, ctx);

                return op switch
                {
                    "<" => l < r,
                    ">" => l > r,
                    "<=" => l <= r,
                    ">=" => l >= r,
                    "==" => l == r,
                    "!=" => l != r,
                    _ => true
                };
            }
        }

        // Fall back to treating expression as numeric and checking non-zero
        long value = EvaluateNumeric(expr, ctx);
        return value != 0;
    }

    public static int EvaluateCount(string expr, NifReadContext ctx)
    {
        if (string.IsNullOrWhiteSpace(expr))
            return 1;

        expr = expr.Trim();

        // Simple arithmetic with * or +
        if (expr.Contains('*', StringComparison.Ordinal))
        {
            var parts = expr.Split('*', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            long result = 1;
            foreach (var part in parts)
                result *= EvaluateNumeric(part, ctx);
            return (int)result;
        }

        if (expr.Contains('+', StringComparison.Ordinal))
        {
            var parts = expr.Split('+', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            long result = 0;
            foreach (var part in parts)
                result += EvaluateNumeric(part, ctx);
            return (int)result;
        }

        return (int)EvaluateNumeric(expr, ctx);
    }

    private static long EvaluateNumeric(string expr, NifReadContext ctx)
    {
        expr = expr.Trim();

        if (TryResolveVariable(expr, ctx, out var value))
            return ConvertToInt64(value);

        if (expr.StartsWith("0x", StringComparison.OrdinalIgnoreCase) &&
            long.TryParse(expr.AsSpan(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out long hex))
            return hex;

        if (long.TryParse(expr, NumberStyles.Integer, CultureInfo.InvariantCulture, out long dec))
            return dec;

        return 0;
    }

    private static bool TryResolveVariable(string name, NifReadContext ctx, out object? value)
    {
        return ctx.TryGetValue(name, out value);
    }

    private static long ConvertToInt64(object? value)
    {
        if (value == null)
            return 0;

        return value switch
        {
            byte b => b,
            sbyte sb => sb,
            short s => s,
            ushort us => us,
            int i => i,
            uint ui => ui,
            long l => l,
            ulong ul => unchecked((long)ul),
            bool bl => bl ? 1 : 0,
            _ when long.TryParse(value.ToString(), out var parsed) => parsed,
            _ => 0
        };
    }
}
