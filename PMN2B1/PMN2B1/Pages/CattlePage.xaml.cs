using PMN2B1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PMN2B1.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CattlePage : ContentPage
    {
        //public IList<Cattle> Cattles { get; private set; }
        readonly IList<Cattle> Cattles = new ObservableCollection<Cattle>();

        int pickerSpecieSelectedIndex = 0;
        int pickerSexSelectedIndex = 0;

        public CattlePage()
        {
            BindingContext = Cattles;
            InitializeComponent();

            stackLayoutCattle.IsVisible = false;

            /*
            Cattles = new List<Cattle>();
            Cattles.Add(new Cattle
            {
                Identifier = "Mimosa",
                Specie = Specie.Leiteiro,
                BirthDate = new DateTime(2019, 3, 1),
                Sex = Sex.Fêmea,
                //Weight = 14.69 * 20 // 14.69 = 1 @
            });

            Cattles.Add(new Cattle
            {
                Identifier = "Bandido",
                Specie = Specie.Corte,
                BirthDate = new DateTime(2019, 4, 3),
                Sex = Sex.Macho,
                //Weight = 14.69 * 22 // 14.69 = 1 @
            });

            Cattles.Add(new Cattle
            {
                Identifier = "Estrela",
                Specie = Specie.Leiteiro,
                BirthDate = new DateTime(2018, 4, 3),
                Sex = Sex.Fêmea,                
                //Weight = 14.69 * 30 // 14.69 = 1 @
            });

            Cattles.Add(new Cattle
            {
                Identifier = "Prisma",
                Specie = Specie.Leiteiro,
                BirthDate = new DateTime(2019, 12, 20),
                Sex = Sex.Fêmea,                
                //Weight = 14.69 * 12 // 14.69 = 1 @
            });
            */
            OnRefresh();
        }

        async void OnRefresh()
        {
            /*
            var cattleCollection = await App.CattleRepository.GetAllCattleAsync();
            
            foreach (Cattle cattle in cattleCollection)
            {
                if (cattles.All(c => c.Identifier != c.Identifier))
                    cattles.Add(cattle);
            }
            */

            listViewCattle.ItemsSource = await App.CattleRepository.GetAllCattleAsync();
        }

        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Cattle selectedItem = e.SelectedItem as Cattle;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Cattle tappedItem = e.Item as Cattle;
            listViewCattle.IsVisible = false;
            ButtonAdd.IsVisible = false;
            stackLayoutCattle.IsVisible = true;

            identifier.Text = tappedItem.Identifier;
            specie.SelectedIndex = 1;
            sex.SelectedIndex = 1;
            birthdate.Date = tappedItem.BirthDate.Date;
        }

        void OnButtonAddClicked(object sender, EventArgs e)
        {
            listViewCattle.IsVisible = false;
            ButtonAdd.IsVisible = false;
            stackLayoutCattle.IsVisible = true;

            birthdate.MaximumDate = DateTime.Today;
            sex.SelectedIndex = 0;
            specie.SelectedIndex = 0;
        }

        async void OnButtonSaveClicked(object sender, EventArgs e)
        {
            listViewCattle.IsVisible = true;
            ButtonAdd.IsVisible = true;
            stackLayoutCattle.IsVisible = false;


            if (!string.IsNullOrWhiteSpace(identifier.Text))
            {
                await App.CattleRepository.SaveCattleAsync(new Cattle
                {                   
                    Identifier = identifier.Text,
                    Specie = (Specie)pickerSpecieSelectedIndex,
                    BirthDate = birthdate.Date,
                    Sex = (Sex)pickerSexSelectedIndex,                    
                    //Weight = double.Parse(weight.Text),
                });

                identifier.Text = string.Empty;
                birthdate.Date = DateTime.Today;
                listViewCattle.ItemsSource = await App.CattleRepository.GetAllCattleAsync();
            }
        }
        
        void OnPickerSpecieSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            pickerSpecieSelectedIndex = picker.SelectedIndex;
        }
        
        void OnPickerSexSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            pickerSexSelectedIndex = picker.SelectedIndex;
        }

        void OnButtonCancelClicked(object sender, EventArgs e)
        {
            listViewCattle.IsVisible = true;
            ButtonAdd.IsVisible = true;
            stackLayoutCattle.IsVisible = false;

            identifier.Text = string.Empty;
            specie.SelectedIndex = 0;
            birthdate.Date = DateTime.Today;
            sex.SelectedIndex = 0;
        }
    }
}