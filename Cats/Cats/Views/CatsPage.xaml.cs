using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cats.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class CatsPage : ContentPage
    {
        public CatsPage()
        {
            InitializeComponent();
            ListViewCats.ItemSelected += ListViewCats_ItemSelected;
        }

        private async void ListViewCats_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var SelectedCat = e.SelectedItem as Models.Cat;
            if (SelectedCat != null)
            {
                await Navigation.PushAsync(new Views.DetailsPage(SelectedCat));
                ListViewCats.SelectedItem = null;
            }
        }


        /*
        [XamlCompilation(XamlCompilationOptions.Compile)]
        public partial class CatsPage : ContentPage
        {
            public CatsPage()
            {
                InitializeComponent();
                BindingContext = new CatsPageViewModel(); //this; // new ContentPage();
            }
        }

        class CatsPageViewModel : INotifyPropertyChanged
        {

            public CatsPageViewModel()
            {
                IncreaseCountCommand = new Command(IncreaseCount);
            }

            int count;

            string countDisplay = "You clicked 0 times.";
            public string CountDisplay
            {
                get { return countDisplay; }
                set { countDisplay = value; OnPropertyChanged(); }
            }

            public ICommand IncreaseCountCommand { get; }

            void IncreaseCount() =>
                CountDisplay = $"You clicked {++count} times";


            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                */
    }
}
