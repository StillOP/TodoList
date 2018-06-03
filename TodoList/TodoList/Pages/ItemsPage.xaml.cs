using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Services;
using TodoList.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace TodoList.Pages
{
    public class Itemlist
    {
        public ObservableCollection<Item> ItemsCollection { get; set; } = new ObservableCollection<Item>();
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        private ObservableCollection<Item> items = new ObservableCollection<Item>();

        //public ObservableCollection<Item> ItemsCollection { get { return items; } set { items = value; } }

        public ItemsPage()
        {
            InitializeComponent();
            DisplayItems();
            
        }

        private async void DisplayItems()
        {
            items = await ItemsService.GetAllItem();

            Itemlist itemlist = new Itemlist();
            itemlist.ItemsCollection = items;

            BindingContext = new Itemlist();
            BindingContext = itemlist;
           
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (Item)e.SelectedItem;
            await Navigation.PushAsync(new ItemDetailPage(item));
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Xamarin.Forms.NavigationPage(new NewItemxaml()));
        }
    }
}