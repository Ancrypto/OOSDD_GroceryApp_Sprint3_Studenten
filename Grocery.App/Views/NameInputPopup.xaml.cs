using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using Grocery.App.ViewModels;

namespace Grocery.App.Views;

public partial class NameInputPopup : Popup
{
	public NameInputPopup()
	{
		InitializeComponent();
	}

	private void OnSaveClicked(object sender, EventArgs e)
	{
		Close(NameEntry.Text);
	}
}