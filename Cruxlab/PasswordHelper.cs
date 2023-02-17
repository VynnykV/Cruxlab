using System.Text.RegularExpressions;

namespace Cruxlab;

public static class PasswordHelper
{
    private static readonly Regex _rowFormat= new(@"^[^\s]\s\d+-\d+:\s[^\s]+$");
    
    public static bool ContainsSymbol(
        string password,
        char symbol,
        uint minMatch,
        uint maxMatch)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentNullException(password, "Password is Empty");
        }

        var count = password.Count(x => x == symbol);
        return count >= minMatch && count <= maxMatch;
    }

    public static (string password, char symbol, uint minMatch, uint maxMatch) ParseRow(string row)
    {
        if (string.IsNullOrEmpty(row))
        {
            throw new ArgumentNullException(row, "Row is Empty");
        }

        if (!_rowFormat.IsMatch(row))
        {
            throw new InvalidOperationException("Row format is Incorrect");
        }

        var data = row.Split(' ');
        var symbol = char.Parse(data[0]);
        var range = data[1]
            .Trim(':')
            .Split('-');
        var minMatch = uint.Parse(range[0]);
        var maxMatch = uint.Parse(range[1]);
        var password = data[2];

        return (password, symbol, minMatch, maxMatch);
    }
}