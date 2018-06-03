using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TodoList.ViewModels;

namespace TodoList.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemDetailPage : ContentPage
	{
		public ItemDetailPage(Item item)
		{
			InitializeComponent ();
            BindingContext = new Item();
            BindingContext = item;

            if(!item.isDone)
            {
                var button = new Button
                {
                    Text = "FAIT",
                    
                };
                button.Clicked += Button_Clicked;

                Details.Children.Add(button);
            }
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Services.ItemsService.ItemIsDone(BindingContext as Item);
        }
    }
}