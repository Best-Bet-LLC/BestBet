using System;
using System.Collections.Generic;
using System.IO;
using BestBet.Data;
using BestBet.Services;
using BestBet.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BestBet
{
    public partial class App : Application
    {
        public static string sport { get; set; }
        public static string region { get; set; }
        public static Sport previousSport { get; set; }

        static BooksDB database;

        public static BooksDB Database
        {
            get
            {
                if (database == null)
                {
                    database = new BooksDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Books.db3"));
                }
                return database;

            }
        }


        public static List<string> bookmakersList {
            get
            {
                return new List<string>() { "DraftKings", "Unibet", "PointsBet (US)", "BetOnline.ag", "Betfair", "BetRivers", "Bookmaker", "Bovada", "FanDuel", "GTbets", "Intertops", "LowVig.ag", "MyBookie.ag", "William Hill (US)", "BetMGM", "SugarHouse", "Caesars" };
            }
            set
            {
                bookmakersList = value;
            }
        }


        public App()
        {
            InitializeComponent();
            Sharpnado.HorizontalListView.Initializer.Initialize(true, false);
            Sharpnado.Tabs.Initializer.Initialize(loggerEnable: false, debugLogEnable: false);
            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);
            Sharpnado.TaskLoaderView.Initializer.Initialize(loggerEnable: false);
            
            //FlowListView.Init();
            DependencyService.Register<OddsAPIInterface, OddsAPI>();
            MainPage = new NavigationPage(new SplashScreen());
           
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
