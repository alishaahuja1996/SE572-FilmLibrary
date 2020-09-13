using System.Collections.Generic;
using System.Threading.Tasks;
using FilmLibrary.Models;

namespace FilmLibrary.AppDataManager
{
    /**
     * Class: FilmLibraryManager
     * Responsible for managing
     * the overall Film Library App
    **/
    public class FilmLibraryManager
    {
        // Attribute
		RestService restService;

        // Constructor
		public FilmLibraryManager(RestService service)
        {
			restService = service;
		}

        /**
         * Method: ViewFilmsTaskAsync
         * Responsible to call rest service
         * for viewing films in the App
        **/
        public Task<List<Film>> ViewFilmsTaskAsync()
        {
            return restService.ViewFilmsInfoAsync();
        }

        /**
         * Method: SaveLoginTaskAsync
         * Responsible to call rest service
         * for saving login info in the App
        **/
        public Task<string> SaveLoginTaskAsync(LoginAccount info)
		{
            return restService.SaveLoginInfoAsync(info);
		}

        /**
         * Method: SaveFilmTaskAsync
         * Responsible to call rest service
         * for saving film info in the App
        **/
        public Task<string> SaveFilmTaskAsync(Film info)
        {
            return restService.SaveFilmInfoAsync(info);
        }

        /**
         * Method: SaveFilmTaskAsync
         * Responsible to call rest service
         * for updating film info in the App
        **/
        public Task<string> UpdateFilmTaskAsync(Film info)
        {
            return restService.UpdateFilmInfoAsync(info);
        }

        /**
         * Helper Method: IsInvalidValue
         * Responsible to check for invalid input value characters
         * on user input. (used for filmTitle and username)
        **/
        public bool IsInvalidValue(string inputValue)
        {
            bool isInvalid = false;

            List<string> invalidChars = new List<string>() { ",", ".", "/", "\\", ";", ":", "<", ">", "?", "[", "]", "@", "!", "#", "$", "%", "^", "*" };

            // Check invalid case: if only single invalid character is entered
            if (inputValue.Length == 1)
            {
                foreach (string invalidChar in invalidChars)
                {
                    if (invalidChar.Equals(inputValue))
                    {
                        isInvalid = true;
                        break;
                    }
                }
            }

            return isInvalid;
        }
    }

}
