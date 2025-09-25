using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.App.Views;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using Grocery.Core.Services;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;

namespace Grocery.App.ViewModels
{
    public partial class GroceryListViewModel : BaseViewModel
    {
		[ObservableProperty]
		private ObservableCollection<GroceryList> groceryLists;
		private readonly IGroceryListService _groceryListService;

		public GroceryListViewModel(IGroceryListService groceryListService) 
        {
            Title = "Boodschappenlijst";
            _groceryListService = groceryListService;
            GroceryLists = new(_groceryListService.GetAll());
        }

		[RelayCommand]
		public async Task AddGroceryList()
		{
			var popup = new NameInputPopup();
			var name = await Shell.Current.CurrentPage.ShowPopupAsync(popup);

			if (name is string listName && !string.IsNullOrWhiteSpace(listName))
            {
				GroceryList newList = new GroceryList(_groceryListService.GetAll().Count+1, listName, DateOnly.FromDateTime(DateTime.Now), "#003300", 1);
				_groceryListService.Add(newList);
				GroceryLists.Add(newList);
			}
		}

		[RelayCommand]
        public async Task SelectGroceryList(GroceryList groceryList)
        {
            Dictionary<string, object> paramater = new() { { nameof(GroceryList), groceryList } };
            await Shell.Current.GoToAsync($"{nameof(Views.GroceryListItemsView)}?Titel={groceryList.Name}", true, paramater);
        }

		public override void OnAppearing()
		{
			base.OnAppearing();

			var latestLists = _groceryListService.GetAll();

			GroceryLists.Clear();
			foreach (var list in latestLists)
			{
				GroceryLists.Add(list);
			}
		}

		public override void OnDisappearing()
        {
            base.OnDisappearing();
            GroceryLists.Clear();
        }
    }
}
