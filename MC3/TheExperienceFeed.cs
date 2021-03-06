﻿using System;

using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace MC3
{
	public class TheExperienceFeed : ContentPage
	{
		public ListView _listView;
		public DemoRepository _rep;
		private SearchBar _searchBar;
		private Image _addButton;
		public TheExperienceFeed (List<Experience> experiences, DemoRepository rep)
		{
			_listView = new ListView
			{
				// Source of data items.
				ItemsSource = experiences,

				// Define template for displaying each item.
				ItemTemplate = new DataTemplate(() =>
					{
						return new ExperienceCell();
					}),
				HasUnevenRows = true

			};
			_listView.ItemTapped += delegate(object sender, ItemTappedEventArgs e) {
				Experience exp = e.Item as Experience;
				ExperienceDetailPage detailPage = ExperienceDetailPage.CreatePage(exp, rep);
				ParentView.Navigation.PushAsync(detailPage);
				_listView.SelectedItem = null;
			}; 

			_searchBar = new SearchBar () { Placeholder = "Search by Organization or Experience" };

			_searchBar.TextChanged += (sender, e) => {

				if (string.IsNullOrWhiteSpace(e.NewTextValue))
					_listView.ItemsSource = experiences;
				else
				{
					var searchString = e.NewTextValue.ToLower();

					_listView.ItemsSource = experiences.Where(i => i.OrganizationName.ToLower().Contains(searchString) ||
						(i.Title.ToLower().Contains(searchString)));
				}
			};

			_addButton = new Image { Aspect = Aspect.AspectFit };
			ImageSource addButtonImage = ImageSource.FromResource ("MC3.SharedResources.AddButton3.png");
			_addButton.Source = addButtonImage;

			var addTapped = new TapGestureRecognizer ();
			addTapped.Tapped += async delegate {
				var expPage = AddExperiencePage.CreatePage(rep);
				await Navigation.PushAsync(expPage);
			};
				
			_addButton.GestureRecognizers.Add (addTapped);

			Title = "Main Feed";
			Content = new StackLayout {
				BackgroundColor = Color.FromHex("#FFFFFF"),
				Children = {
					_searchBar,
					_listView,
					_addButton
				}
			};
		}

		public static TheExperienceFeed CreateExperienceFeed (DemoRepository dataRep) {
			List<Experience> experiences = dataRep.getExperiences();
			return new TheExperienceFeed (experiences, dataRep);
		}
			
	}

	public class ExperienceCell : ViewCell
	{
		public ExperienceCell()
		{

		}
		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged ();
			dynamic c = BindingContext;
			var experience = (Experience)c;

			//instantiate each of our views
			StackLayout cellWrapper = new StackLayout () { Padding = 5, BackgroundColor = Color.FromHex("#FFFFFF") };

			Label organization = new Label () {
				FontSize = 20,
				FontAttributes = FontAttributes.Bold,
			};
			organization.SetBinding(Label.TextProperty, "OrganizationName");

			BoxView underline = new BoxView { HeightRequest = 3, BackgroundColor= Color.FromHex ("ffb2ebf2"), WidthRequest= organization.Width };

			Label exp = new Label { };
			exp.SetBinding (Label.TextProperty, "Title");

			Image logoforTime = new Image { Aspect = Aspect.AspectFit };
			ImageSource snowflake = ImageSource.FromResource ("MC3.SharedResources.Snowflake4.png");
			ImageSource sun = ImageSource.FromResource ("MC3.SharedResources.SunCopy.png");
			ImageSource books = ImageSource.FromResource ("MC3.SharedResources.Books2.png");


			if (experience.TimeFrame == TimePeriod.JTerm) {
				logoforTime.Source = snowflake;
			} 
			if (experience.TimeFrame == TimePeriod.Summer) {
				logoforTime.Source = sun;
			}
			if (experience.TimeFrame == TimePeriod.SchoolYear) {
				logoforTime.Source = books;
			}


			Grid grid = new Grid {
				VerticalOptions = LayoutOptions.FillAndExpand,
				RowDefinitions = {
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Auto },
				},
				ColumnDefinitions = {
					new ColumnDefinition { Width = new GridLength (30) },
					new ColumnDefinition { Width = new GridLength (300) },
				}
			};

			grid.Children.Add (organization, 1,0);
			grid.Children.Add (underline, 1, 1);
			grid.Children.Add (exp, 1, 2);
			grid.Children.Add (logoforTime,0,1,0,3);

			cellWrapper.Children.Add (grid);
			View = cellWrapper;
		}
	}

}
