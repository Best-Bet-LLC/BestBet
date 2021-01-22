using System;
using System.Collections.Generic;
using BestBet.Models;
using BestBet.ViewModels;
using Xamarin.Forms;

namespace BestBet.Views
{
    public partial class MatchesPage : ContentPage
    {
        public MatchesPage()
        {
            //InitializeComponent();
        }

        public MatchesPage(Match matchIn)
        {
            InitializeComponent();
            bool singleMatchViewIn = true;
            BindingContext = new OddsViewModel(matchIn, singleMatchViewIn);
            
        }

        public MatchesPage(string sport, string region)
        {
            App.sport = sport;
            App.region = region;
            InitializeComponent();
            //OddsViewModel oddsViewModel = new OddsViewModel(sport, region);
        }

        void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new FilterBooksModal());
        }

        void home_clicked(System.Object sender, System.EventArgs e)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new FilterBooksModal());
        }
    }
}
