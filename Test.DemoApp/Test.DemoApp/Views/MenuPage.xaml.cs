using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Test.DemoApp.Forms.Views
{
	public partial class MenuPage : ContentPage
	{
		public MenuPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
		}
	}
}
