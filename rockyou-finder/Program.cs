// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Security.Cryptography;

Console.WriteLine("Hello, World!");

var file = "rockyou.txt";

var passwordToMatch = "bestpasshass";
var passwordToMatchHash = SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(passwordToMatch));

var timer = new Stopwatch();
timer.Start();

using var reader = new StreamReader(file);
string? line;
while ((line = reader.ReadLine()) != null)
{
    var hash = SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(line));
    if (Enumerable.SequenceEqual(passwordToMatchHash, hash))
    {
        Console.WriteLine($"Password found: {line}");
        return;
    }
}

Console.WriteLine("Password not found.");
timer.Stop();

var time = timer.ElapsedMilliseconds;
Console.WriteLine($"Time taken: {time}");