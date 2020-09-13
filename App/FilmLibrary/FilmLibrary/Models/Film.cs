using System;
using Newtonsoft.Json;

namespace FilmLibrary.Models
{
    /**
     * Class: Film
     * Responsible for defining Film 
     * for the Film Library App
    **/
    public class Film
    {
        // Attributes
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("rating")]
        public string Rating { get; set; }

        // Constructor
        public Film()
        {
            Name = String.Empty;
            Rating = String.Empty;
        }

        /**
        * Helper Method: isInvalidRating
        * Responsible for film rating
        * validation checks
        */
        public bool IsInvalidRating(string value)
        {
            bool isInvalid;
            int result;

            // Check if user input is integer
            if (int.TryParse(value, out result))
            {
                // Check if user input is valid integer range
                if (result < 1 || result > 5)
                {
                    isInvalid = true;
                }
                else
                {
                    isInvalid = false;
                }
            }
            else
            {
                isInvalid = true;
            }

            return isInvalid;
        }
    }
}
