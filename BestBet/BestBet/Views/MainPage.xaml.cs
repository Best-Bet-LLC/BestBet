using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestBet.ViewModels;
using BestBet.Views;
using Xamarin.Forms;
using SQLite;

namespace BestBet
{
    public partial class MainPage : ContentPage
    {
        //public List<Sport> AllSports { get; set; }

        public MainPage()
        {
            if(App.region == null)
            {
                App.region = "us";
            }
           if(App.sport == null)
            {
                App.sport = "upcoming";
            }
            InitializeComponent();
            updateDB();
            
            //try
            //{
            //    if (Application.Current.Properties["bookmakers"] == null)
            //    {
            //        Application.Current.Properties["bookmakers"] = new List<string>() { "DraftKings", "Unibet", "PointsBet (US)", "BetOnline.ag", "Betfair", "BetRivers", "Bookmaker", "Bovada", "FanDuel", "GTbets", "Intertops", "LowVig.ag", "MyBookie.ag", "William Hill (US)" };
                    
            //    }
            //} catch (KeyNotFoundException keyEx)
            //{
            //    Application.Current.Properties["bookmakers"] = new List<string>() { "DraftKings", "Unibet", "PointsBet (US)", "BetOnline.ag", "Betfair", "BetRivers", "Bookmaker", "Bovada", "FanDuel", "GTbets", "Intertops", "LowVig.ag", "MyBookie.ag", "William Hill (US)" };
            //} catch (Exception ex)
            //{
            //    Console.WriteLine($"crash: {ex.Message}");
            //}

            
            //BindingContext = this;
        }

        private async void updateDB()
        {
            //App.Database.DropTable();
            List<Book> booksInDB = await App.Database.GetBooksAsync();
            if (booksInDB.Count != App.bookmakersList.Count)
            {
                foreach (var book in App.bookmakersList)
                {
                    var isPresent = false;

                    foreach (var bookInDB in booksInDB)
                    {
                        if (bookInDB.name == book)
                        {
                            isPresent = true;
                        }
                    }

                    if(isPresent == false)
                    {
                        await App.Database.SaveBookAsync(new Book(book));
                    }
                }
            }
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void SportTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            Sport tappedSport = (Sport)((CollectionView)sender).SelectedItem;
            App.Current.MainPage.Navigation.PushAsync(new MatchesPage(tappedSport.key, "us"));
            CollectionView collectionView = ((CollectionView)sender);
            collectionView.SelectedItem = null;
        }

        void ListView_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
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
            viewModel.getSports();
            await viewModel.getUpcomingMatches();
        }

        

        async void CollectionView_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            //string previous = (e.PreviousSelection.FirstOrDefault() as Sport)?.Name;
            if (e.CurrentSelection.Count > 0)
            {
                
                if(App.previousSport != null)
                {
                    App.previousSport.IsSelected = false;
                }
                var newSport = (e.CurrentSelection.FirstOrDefault() as Sport);
                newSport.IsSelected = true;
                App.previousSport = newSport;
                App.sport = (e.CurrentSelection.FirstOrDefault() as Sport)?.key;
                var viewModel = (SportsViewModel)((CollectionView)sender).BindingContext;
                ((CollectionView)sender).SelectedItem = null;
                await viewModel.getUpcomingMatches();
                
               
            }
            
            //sportsViewModel.getUpcomingMatches();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //((CollectionView)sender).SelectedItem = null;
        }

        
    }
}
