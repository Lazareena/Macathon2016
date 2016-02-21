using System;

using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace MC3
{
	public class AddExperiencePage2 : ContentPage
	{
		ListView _experienceList;
		SearchBar _search;
		public AddExperiencePage2 (Organization org, DemoRepository rep)
		{
			Title = "Add Experience";
			List<Experience> exps = org.Positions;
			_experienceList = new ListView
			{
				// Source of data items.
				ItemsSource = exps,

				// Define template for displaying each item.
				ItemTemplate = new DataTemplate(() =>
					{
						return new ExperienceCellAddPage();
					}),
				HasUnevenRows = true

			};

			_experienceList.ItemTapped += async delegate(object sender, ItemTappedEventArgs e) {
				Experience exp = e.Item as Experience;
				AddExperience3 addExp = new AddExperience3(org, exp, rep);
				await ParentView.Navigation.PushAsync(addExp);
				Navigation.RemovePage(this);
			};

			_search = new SearchBar () { Placeholder = "Search Experience" };

			_search.TextChanged += (sender, e) => {

				if (string.IsNullOrWhiteSpace(e.NewTextValue))
					_experienceList.ItemsSource = exps;
				else
				{
					var searchString = e.NewTextValue.ToLower();

					_experienceList.ItemsSource = exps.Where(i => i.Title.ToLower().Contains(searchString));				}
			};

			Content = new StackLayout { 
				BackgroundColor = Color.FromHex("#FFFFFF"),
				Children = {
					_search,
					_experienceList
				}
			};
		}
	}

	public class ExperienceCellAddPage : ViewCell
	{
		public ExperienceCellAddPage()
		{
			StackLayout cellWrapper = new StackLayout () { Padding = 5 };

			Label organization = new Label () {
				FontSize = 20,
				FontAttributes = FontAttributes.Bold,
			};
			organization.SetBinding(Label.TextProperty, "Title");
			cellWrapper.Children.Add (organization);
			View = cellWrapper;
		}
	}


}


