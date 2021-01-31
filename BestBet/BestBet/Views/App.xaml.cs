using System;
using System.Collections.Generic;
using System.IO;
using Amazon;
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
            AWSConfigs.AWSRegion = "us-east-1";
            //// Initialize the Amazon Cognito credentials provider
            //CognitoAWSCredentials credentials = new CognitoAWSCredentials(
            //    "us-east-1:055d7332-9e1c-405b-8579-c82e13576ad6", // Identity pool ID
            //    AWSConfigs.AWSRegion // Region
            //);

            //// Example for |MA|
            //analyticsManager = MobileAnalyticsManager.GetOrCreateInstance(
            //  credentials,
            //  AWSConfigs.AWSRegion, // Region
            //  APP_ID // app id
            //);
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
