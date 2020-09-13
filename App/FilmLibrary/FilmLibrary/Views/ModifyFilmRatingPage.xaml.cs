using System;
using FilmLibrary.Models;
using Xamarin.Forms;

namespace FilmLibrary.Views
{
    /**
     * Code Behind Class: ModifyFilmRatingPage
     * Responsible for modifying film rating
     * for FilmLibrary App
    **/
    public partial class ModifyFilmRatingPage : ContentPage
    {
        // Constructor
        public ModifyFilmRatingPage(Film filmInfo)
        {
            InitializeComponent();
            filmInfo.Rating = String.Empty;
        }

        /**
         * Method: OnModifyButtonClicked
         * Responsible for defining the behavior of
         * modify button click event
        **/
        async void OnModifyButtonClicked(object sender, EventArgs e)
        {
            var filmInfo = (Film)BindingContext;

            // Validation checks
            if (filmInfo.Rating.ToString().Length == 0 || String.IsNullOrWhiteSpace(filmInfo.Rating.ToString()))
            {
                await DisplayAlert("Error", "\nMust enter film new rating", "OK");
            }
            else if (filmInfo.IsInvalidRating(filmInfo.Rating))
            {
                await DisplayAlert("Error", "\nMust enter a valid film new rating", "OK");
            }
            else
            {
                string filmUpdateStatus = await App.filmLibraryManager.UpdateFilmTaskAsync(filmInfo);
                await DisplayAlert("Film Update Status", "\n" + filmUpdateStatus, "OK");

                // Go to the Main Page if the update is successful
                // otherwise, go back to ViewFilms Page
                if (filmUpdateStatus.Equals("Success!"))
                {
                    await Navigation.PopToRootAsync();
                }
                else
                {
                    await Navigation.PopAsync();
                }
            }
        }
    }
}
