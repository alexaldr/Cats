using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Cats.Models;
using Xamarin.Forms;

namespace Cats.ViewModels
{
    public class CatsViewModel : INotifyPropertyChanged
    {
        //public Command GetCatsCommand { get; set; }
        //implementa~ção do INotify..
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(
            [System.Runtime.CompilerServices.CallerMemberName]
            string propertyName = null) => 
            PropertyChanged?.Invoke(this, 
            new PropertyChangedEventArgs(propertyName));

        private bool Busy;
        //impedir que refaça algo se o usuário ficar mudando os valores
        public bool IsBusy
        {
            get
            {
                return Busy;
            }
            set
            {
                Busy = value;
                OnPropertyChanged();
                GetCatsCommand.ChangeCanExecute();
            }
        }

        public ObservableCollection<Cat> Cats { get; set; }


        //Construtor
        public CatsViewModel()
        {
            Cats = new ObservableCollection<Models.Cat>();

            GetCatsCommand = new Command(
                async () => await GetCats(), 
                () => !IsBusy
                );
        }

        async Task GetCats()
        {
            if (!IsBusy)
            {
                Exception Error = null;
                try
                {
                    IsBusy = true;
                    //obter dados do repositório
                    var Repository = new Repository();
                    var Items = await Repository.GetCats();
                    //limpar lista atual e carregar nova coleção de itens
                    Cats.Clear();
                    foreach (var Cat in Items)
                    {
                        Cats.Add(Cat);
                    }
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
                finally
                {
                    IsBusy = false;
                }
                //se der erro, mostra mensagem
                if (Error != null)
                {
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                    "Error!", Error.Message, "OK");
                }
            }
            return;
        }

        public Command GetCatsCommand { get; set; }
    }
}
