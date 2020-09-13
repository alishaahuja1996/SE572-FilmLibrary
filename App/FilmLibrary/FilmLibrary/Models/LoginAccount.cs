using System;
using Newtonsoft.Json;

namespace FilmLibrary.Models
{
    /**
     * Class: LoginAccount
     * Responsible for defining Login 
     * for the Film Library App
    **/
    public class LoginAccount
    {
        // Attributes
        [JsonProperty("username")]
        public string Username { get; set; }

        // Constructor
        public LoginAccount()
        {
            Username = String.Empty;
        }
    }
}
