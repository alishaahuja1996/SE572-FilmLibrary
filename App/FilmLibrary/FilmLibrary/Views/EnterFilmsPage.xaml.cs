using System;
using FilmLibrary.Models;
using Xamarin.Forms;

namespace FilmLibrary.Views
{
    /**
     * Code Behind Class: EnterFilmsPage
     * Responsible for taking film related input
     * for FilmLibrary App
    **/
    public partial class EnterFilmsPage : ContentPage
    {
        // Constructor
        public EnterFilmsPage()
        {
            InitializeComponent();
        }

        /**
        * Method: OnSaveButtonClicked
        * Responsible for taking film related user input
        * and saving them on 'Submit' button click event
        */
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var filmInfo = (Film)BindingContext;

            // Validation checks
            if ((filmInfo.Name.Length == 0 || String.IsNullOrWhiteSpace(filmInfo.Name)) &&
                (filmInfo.Rating.Length == 0 || String.IsNullOrWhiteSpace(filmInfo.Rating)))
            {
                await DisplayAlert("Error", "\nMust enter both film title and rating", "OK");
            }
            else if (filmInfo.Name.Length == 0 || String.IsNullOrWhiteSpace(filmInfo.Name))
            {
                await DisplayAlert("Error", "\nMust enter film title", "OK");
            }
            else if (App.filmLibraryManager.IsInvalidValue(filmInfo.Name))
            {
                await DisplayAlert("Error", "\nMust enter a valid film title", "OK");
            }
            else if (filmInfo.Rating.Length == 0 || String.IsNullOrWhiteSpace(filmInfo.Rating))
            {
                await DisplayAlert("Error", "\nMust enter film rating", "OK");
            }
            else if (filmInfo.IsInvalidRating(filmInfo.Rating))
            {
                await DisplayAlert("Error", "\nMust enter a valid film rating", "OK");
            }
            else
            {
                string filmSaveStatus = await App.filmLibraryManager.SaveFilmTaskAsync(filmInfo);
                await DisplayAlert("Film Save Status", "\n" + filmSaveStatus, "OK");
                await Navigation.PopAsync();
            }
        }
    }
}
