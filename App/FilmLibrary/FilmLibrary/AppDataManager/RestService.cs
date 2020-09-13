using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using FilmLibrary.Models;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FilmLibrary.AppDataManager
{
    /**
     * Class: RestService
     * Responsible for managing
     * rest services for Film Library App
    **/
    public class RestService
    {
        // Attributes
        HttpClient Client;
        public static string RestURL = "http://192.168.0.7:8080/api/v1/";
        public List<Film> FilmsInfo { get; private set; }
        public string JWTToken {get; private set;}

        // Constructor
        public RestService()
        {
            Client = new HttpClient();
        }

        /**
         * Method: SaveLoginInfoAsync
         * Responsible for logging in the user to
         * authorize film additions to the App by
         * making POST request and initializining JWT token
        **/
        public async Task<string> SaveLoginInfoAsync(LoginAccount account)
        {
            Uri uri = new Uri(RestURL + "login");
            string loginStatus = "";

            try
            {
                string json = JsonConvert.SerializeObject(account);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await Client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    var jsonResult = JObject.Parse(result);
                    JWTToken = (string)jsonResult["token"];
                    loginStatus = "Success!";
                }
                else
                {
                    loginStatus = "Failed!";
                }
            }
            catch (Exception ex)
            {
                loginStatus = "Internal Error!";
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return loginStatus;
        }

        /**
         * Method: ViewFilmsInfoAsync
         * Responsible for getting all films
         * present in the DB by making GET request
         * and returning a List of films
        **/
        public async Task<List<Film>> ViewFilmsInfoAsync()
        {
            FilmsInfo = new List<Film>();
            Uri uri = new Uri(RestURL + "films");

            try
            {
                HttpResponseMessage response = await Client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    FilmsInfo = JsonConvert.DeserializeObject<List<Film>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return FilmsInfo;
        }

        /**
         * Method: SaveFilmInfoAsync
         * Responsible for saving film information
         * to DB by making POST request to server
         * and returning the film save status
        **/
        public async Task<string> SaveFilmInfoAsync(Film film)
        {
            Uri uri = new Uri(RestURL + "films");
            string filmStatus = "";

            // Validation check to see if the User is logged in and JWT token  exists
            if (String.IsNullOrEmpty(JWTToken))
            {
                filmStatus = "User not authorized to add films. Must Login";
            }
            else
            {
                try
                {
                    string json = JsonConvert.SerializeObject(film);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", JWTToken);
                    
                    HttpResponseMessage response = await Client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        filmStatus = "Success!";
                    }
                    else
                    {
                        filmStatus = "Failed!";
                    }
                }
                catch (Exception ex)
                {
                    filmStatus = "Internal Error";
                    Debug.WriteLine(@"\tERROR {0}", ex.Message);
                }
            }

            return filmStatus;
        }

        /**
         * Method: UpdateFilmInfoAsync
         * Responsible for updating existing film's rating
         * in DB by making PUT request to server
         * and returning the film update status
        **/
        public async Task<string> UpdateFilmInfoAsync(Film film)
        {
            Uri uri = new Uri(RestURL + "modifyRating");
            string filmStatus = "";

            try
            {
                string json = JsonConvert.SerializeObject(film);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await Client.PutAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    filmStatus = "Success!";
                }
                else
                {
                    filmStatus = "Failed!";
                }
            }
            catch (Exception ex)
            {
                filmStatus = "Internal Error!";
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return filmStatus;
        }
    }
}
