using FilmLibrary.AppDataManager;
using Xamarin.Forms;

namespace FilmLibrary
{
    /**
     * Code Behind Class: App
     * Responsible for defining the overall
     * behavior for FilmLibrary App
    **/
    public partial class App : Application
    {
        // Attributes
        public static FilmLibraryManager filmLibraryManager { get; private set; }

        // Constructor
        public App()
        {
            // Initialize the app components and set the Home to MainPage
            InitializeComponent();
            filmLibraryManager = new FilmLibraryManager(new RestService());
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
