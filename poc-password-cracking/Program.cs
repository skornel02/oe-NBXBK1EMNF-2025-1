// See https://aka.ms/new-console-template for more information
using System.Runtime.CompilerServices;
using poc_password_cracking;

Console.WriteLine("Cracking password!");

ILoginService simpleLoginService = new BasicLoginService();

Console.WriteLine($"The password to crack is: {simpleLoginService.Password}");

Console.WriteLine();
Console.WriteLine($"The allowed characters are: {string.Join(", ", simpleLoginService.AllowedCharacters)}");

for (var charsToTest = simpleLoginService.MinCharacters; 
    charsToTest <= simpleLoginService.MaxCharacters; 
    charsToTest++)
{
    Console.WriteLine();
    Console.WriteLine($"Trying passwords with {charsToTest} characters...");

    var totalCombinations = (long)Math.Pow(simpleLoginService.AllowedCharacters.Length, charsToTest);
    Console.WriteLine($"Total combinations to try: {totalCombinations:N0}");

    var sw = System.Diagnostics.Stopwatch.StartNew();

    foreach (var password in PasswordGenerator.GeneratePassword(charsToTest, simpleLoginService.AllowedCharacters))
    {
        //Console.WriteLine(password);

        if (simpleLoginService.IsPasswordValid("user", password))
        {
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine($"Password cracked! The password is: {password}");
            Console.WriteLine($"Time taken: {sw.Elapsed}");
            return;
        }
    }

    sw.Stop();
}