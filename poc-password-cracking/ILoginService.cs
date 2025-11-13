using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poc_password_cracking;

public interface ILoginService
{

    public string[] AllowedCharacters { get; }

    public int MinCharacters { get; }
    public int MaxCharacters { get; }

    public string Password { get; }

    public bool IsPasswordValid(string username, string password);

}
