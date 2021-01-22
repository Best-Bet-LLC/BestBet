using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestBet.ViewModels;
using BestBet.Views;
using SQLite;

using Sharpnado.Presentation.Forms.CustomViews;
using Sharpnado.Tabs;
using Sharpnado.HorizontalListView.RenderedViews;
using Xamarin.Forms;
using System.Windows.Input;
using BestBet.Models;

namespace BestBet.Views
{
    
    public partial class TabbedMainPage : ContentPage, IAnimatableReveal
    {
        public bool Animate { get; set; }

        public TabbedMainPage()
        {
            if (App.region == null)
            {
                App.region = "us";
            }
            if (App.sport == null)
            {
                App.sport = "upcoming";
            }
            InitializeComponent();

            updateDB();
            var t = Task.Run(() =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () => {

                HorizontalListView.PreRevealAnimationAsync = async (viewCell) =>
                {
                    viewCell.View.Opacity = 0;

                    if (HorizontalListView.ListLayout == HorizontalListViewLayout.Vertical)
                    {
                        viewCell.View.RotationX = 90;
                    }
                    else
                    {
                        viewCell.View.RotationY = -90;
                    }
                };

                HorizontalListView.RevealAnimationAsync = async (viewCell) =>
                {
                    await viewCell.View.FadeTo(1);

                    if (HorizontalListView.ListLayout == HorizontalListViewLayout.Vertical)
                    {
                        await viewCell.View.RotateXTo(0);
                    }
                    else
                    {
                        await viewCell.View.RotateYTo(0);
                    }
                };



                HorizontalListViewHot.PreRevealAnimationAsync = async (viewCell) =>
                {
                    viewCell.View.Opacity = 0;

                    if (HorizontalListViewHot.ListLayout == HorizontalListViewLayout.Vertical)
                    {
                        viewCell.View.RotationX = 90;
                    }
                    else
                    {
                        viewCell.View.RotationY = -90;
                    }
                };

                HorizontalListViewHot.RevealAnimationAsync = async (viewCell) =>
                {
                    await viewCell.View.FadeTo(1);

                    if (HorizontalListViewHot.ListLayout == HorizontalListViewLayout.Vertical)
                    {
                        await viewCell.View.RotateXTo(0);
                    }
                    else
                    {
                        await viewCell.View.RotateYTo(0);
                    }
                };

                HorizontalListViewSports.PreRevealAnimationAsync = async (viewCell) =>
                {
                    viewCell.View.Opacity = 0;

                    if (HorizontalListViewSports.ListLayout == HorizontalListViewLayout.Vertical)
                    {
                        viewCell.View.RotationX = 90;
                    }
                    else
                    {
                        viewCell.View.RotationY = -90;
                    }
                };

                HorizontalListViewSports.RevealAnimationAsync = async (viewCell) =>
                {
                    await viewCell.View.FadeTo(1);

                    if (HorizontalListViewSports.ListLayout == HorizontalListViewLayout.Vertical)
                    {
                        await viewCell.View.RotateXTo(0);
                    }
                    else
                    {
                        await viewCell.View.RotateYTo(0);
                    }
                };
                });
            });

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

                    if (isPresent == false)
                    {
                        await App.Database.SaveBookAsync(new Book(book));
                    }
                }
            }

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
            await viewModel.getSports();
            await viewModel.getUpcomingMatches();
        }



        async void CollectionView_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            //string previous = (e.PreviousSelection.FirstOrDefault() as Sport)?.Name;
            if (e.CurrentSelection.Count > 0)
            {

                if (App.previousSport != null)
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

        public void launchMatchPage(Match matchIn)
        {
            Navigation.PushModalAsync(new MatchesPage(matchIn));
        }
    }
}

