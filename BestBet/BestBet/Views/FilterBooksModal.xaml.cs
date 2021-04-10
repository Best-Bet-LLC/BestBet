using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BestBet.Views
{
    public partial class FilterBooksModal : ContentPage
    {
        public FilterBooksModal()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            booksCollectionView.ItemsSource = await App.Database.GetBooksAsync();
        }

        
        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            //var book = (List<Book>)((Button)sender).BindingContext;
            //await App.Database.SaveBookAsync(book);
            await Navigation.PopModalAsync();
        }

        async void CheckBox_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            try { 
                var book = (Book)((CheckBox)sender).BindingContext;
                await App.Database.SaveBookAsync(book);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
