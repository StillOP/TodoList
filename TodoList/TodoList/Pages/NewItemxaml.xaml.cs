using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoList.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewItemxaml : ContentPage
	{
		public NewItemxaml ()
		{
			InitializeComponent ();
		}

        private  async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Item item = new Item();

            if(Text.Text != "" && Description.Text != "")
            {
                item.text = Text.Text;
                item.description = Description.Text;
                item.isDone = false;
                item.priority = 2;
                item.createdDate = DateTime.Now.ToLongDateString();
                item.doneDate = DateTime.MinValue.ToLongDateString();
            }

            var result = await Services.ItemsService.SaveItem(item);
            await DisplayAlert("Alerte d'ajout", result, "X");
            await Navigation.PopModalAsync();
        }
    }
}