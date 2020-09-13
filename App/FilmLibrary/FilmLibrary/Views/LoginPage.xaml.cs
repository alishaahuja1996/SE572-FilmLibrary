using System;
using FilmLibrary.Models;
using Xamarin.Forms;

namespace FilmLibrary.Views
{
    /**
     * Code Behind Class: LoginPage
     * Responsible for taking login related input
     * for FilmLibrary App
    **/
    public partial class LoginPage : ContentPage
    {
        // Constructor
        public LoginPage()
        {
            InitializeComponent();
        }

        /**
         * Method: OnSubmitButtonClicked
         * Responsible for defining the behavior of
         * submit button click event
        **/
        async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            var account = (LoginAccount)BindingContext;

            // Validation Checks
            if (account.Username.Length == 0 || String.IsNullOrWhiteSpace(account.Username))
            {
                await DisplayAlert("Error", "\nMust enter Username", "OK");
            }
            else if (App.filmLibraryManager.IsInvalidValue(account.Username))
            {
                await DisplayAlert("Error", "\nMust enter a valid Username", "OK");
            }
            else
            {
                string loginStatus = await App.filmLibraryManager.SaveLoginTaskAsync(account);
                await DisplayAlert("Login Status", "\n" + loginStatus, "OK");
                await Navigation.PopAsync();
            }
        }
    }
}