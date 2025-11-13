using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poc_password_cracking;

public class BasicLoginService : ILoginService
{
    public string[] AllowedCharacters => [
        "a","b","c","d","e","f","g","h","i","j","k","l","m",
        "n","o","p","q","r","s","t","u","v","w","x","y","z",
        "A","B","C","D","E","F","G","H","I","J","K","L","M",
        "N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
    ];

    public int MinCharacters => 4;
    public int MaxCharacters => 7;

    public string Password { get; private set; }

    public BasicLoginService()
    {
        var passwordLength = Random.Shared.Next(MinCharacters, MaxCharacters + 1);

        Password = string.Empty;
        for (int i = 0; i < passwordLength; i++)
        {
            var index = Random.Shared.Next(0, AllowedCharacters.Length);
            Password += AllowedCharacters[index];
        }
    }

    public bool IsPasswordValid(string username, string password)
    {
        return password == Password;
    }
}
