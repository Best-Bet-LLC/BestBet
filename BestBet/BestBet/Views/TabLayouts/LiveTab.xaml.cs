using System;
using System.Collections.Generic;
using BestBet.ViewModels;
using Xamarin.Forms;

namespace BestBet.Views.TabLayouts
{
    public partial class LiveTab : ContentView
    {
        public LiveTab()
        {
            InitializeComponent();
        }

        void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new FilterBooksModal());
        }

        async void home_Clicked(System.Object sender, System.EventArgs e)
        {

            if (App.previousSport != null)
            {
                App.previousSport.IsSelected = false;
            }
            var viewModel = (SportsViewModel)((ImageButton)sender).BindingContext;

            App.sport = "upcoming";
            await viewModel.getSports();
            await viewModel.getUpcomingMatches();
        }
    }
}
