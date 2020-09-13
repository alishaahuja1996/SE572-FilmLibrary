using System.Collections.Generic;
using FilmLibrary.Models;
using Xamarin.Forms;

namespace FilmLibrary.Views
{
    /**
     * Code Behind Class: ViewFilmsPage
     * Responsible for showing film information
     * for FilmLibrary App as a 'selectable' listView
    **/
    public partial class ViewFilmsPage : ContentPage
    {
        // Constructor
        public ViewFilmsPage(List<Film> allFilms)
        {
            InitializeComponent();
            // set the 'selectable' listView tag with film information
            listView.ItemsSource = allFilms;
        }

        /**
         * Method: OnFilmSelected
         * Responsible for navigating to ModifyFilmRatingPage
         * with the 'selected film' from the listView
        **/
        async void OnFilmSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var filmInfo = e.SelectedItem as Film;

            await Navigation.PushAsync(new ModifyFilmRatingPage(filmInfo)
            {
                BindingContext = filmInfo
            });
        }
    }
}
