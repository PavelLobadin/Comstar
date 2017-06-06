
using System;
using Comstar.Client.Models;
using Comstar.Client.ViewModels;

using Xamarin.Forms;

namespace Comstar.Client.Views
{
	public partial class ItemDetailPage : ContentPage
	{
		ItemDetailViewModel viewModel;

		// Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
		public ItemDetailPage()
		{
			InitializeComponent();
		}

		public ItemDetailPage(ItemDetailViewModel viewModel)
		{
			InitializeComponent();

			BindingContext = this.viewModel = viewModel;
		}

		async void Save_Clicked(object sender, EventArgs e)
		{
			MessagingCenter.Send(this, "UpdateItem", viewModel.Item);
			await Navigation.PopToRootAsync();
		}
	}
}
