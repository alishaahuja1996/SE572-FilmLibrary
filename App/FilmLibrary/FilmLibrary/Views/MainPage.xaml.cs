using System;
using FilmLibrary.Models;
using FilmLibrary.Views;
using Xamarin.Forms;

namespace FilmLibrary
{
	/**
     * Code Behind Class: MainPage
     * Responsible for defining the behavior 
     * of Home page for the Film Library App
    **/
	public partial class MainPage : ContentPage
    {
		// Constructor
        public MainPage()
        {
            InitializeComponent();
        }

		/**
        * Method: OnLoginButtonClicked
        * Responsible for navigating to Login Page
        * on 'Login' button click event
        */
		async void OnLoginButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new LoginPage()
			{
				BindingContext = new LoginAccount()
			});
		}

		/**
        * Method: OnEnterFilmsButtonClicked
        * Responsible for navigating to EnterFilms Page
        * on 'EnterFilms' button click event
        */
		async void OnEnterFilmsButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new EnterFilmsPage()
			{
				BindingContext = new Film()
			});
		}

		/**
        * Method: OnViewFilmsButtonClicked
        * Responsible for showing the existing film title and
        * film rating as a list on new 'ViewFilmsPage' page OR
        * give informed message if no films exist in the library
        * on 'ViewFilms' button click event
        */
		async void OnViewFilmsButtonClicked(object sender, EventArgs e)
		{
			var allFilms = await App.filmLibraryManager.ViewFilmsTaskAsync();

			if (allFilms.Count == 0)
			{
				await DisplayAlert("Information", "\nNo films exist in library", "OK");
			}
			else
			{
				await Navigation.PushAsync(new ViewFilmsPage(allFilms));
			}
		}
	}
}
