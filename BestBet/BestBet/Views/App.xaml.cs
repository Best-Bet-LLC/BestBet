﻿using System;
using System.Collections.Generic;
using System.IO;
using BestBet.Data;
using BestBet.Services;
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
                return new List<string>() { "DraftKings", "Unibet", "PointsBet (US)", "BetOnline.ag", "Betfair", "BetRivers", "Bookmaker", "Bovada", "FanDuel", "GTbets", "Intertops", "LowVig.ag", "MyBookie.ag", "William Hill (US)" };
            }
            set
            {
                bookmakersList = value;
            }
        }


        public App()
        {
            InitializeComponent();
            //FlowListView.Init();
            DependencyService.Register<OddsAPIInterface, OddsAPI>();
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
