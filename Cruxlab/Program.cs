using Cruxlab;

var filePath = Path.Combine(Directory.GetCurrentDirectory(), "passwords.txt");

if (File.Exists(filePath))
{
    var count = 0;
    var data = File.ReadAllLines(filePath);
    foreach (var row in data)
    {
        try
        {
            var res = PasswordHelper.ParseRow(row);
            if (PasswordHelper.ContainsSymbol(res.password, res.symbol, res.minMatch, res.maxMatch))
            {
                count++;
            }
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine($"row - {row} is in wrong format");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    Console.WriteLine($"Result: {count}");
}

