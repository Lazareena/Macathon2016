using System;

using Xamarin.Forms;

namespace MC3
{
	public class TheExperienceFeed : ContentPage
	{
		public TheExperienceFeed ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}


