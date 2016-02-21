using System;

using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace MC3
{
	public class AddExperiencePage : ContentPage
	{	
		public ListView _organizationList;
		public SearchBar _search;

		public AddExperiencePage (List<Organization> orgs, DemoRepository rep)
		{
			Title = "Add Experience";

			_organizationList = new ListView
			{
				// Source of data items.
				ItemsSource = orgs,

				// Define template for displaying each item.
				ItemTemplate = new DataTemplate(() =>
					{
						return new OrganizationCell();
					}),
				HasUnevenRows = true

			};

			_organizationList.ItemTapped += async delegate(object sender, ItemTappedEventArgs e) {
				Organization org = e.Item as Organization;
				AddExperiencePage2 addExp = new AddExperiencePage2(org, rep);
				await ParentView.Navigation.PushAsync(addExp);
				Navigation.RemovePage (this);
			};

			_search = new SearchBar () { Placeholder = "Search Organization" };

			_search.TextChanged += (sender, e) => {

				if (string.IsNullOrWhiteSpace(e.NewTextValue))
					_organizationList.ItemsSource = orgs;
				else
				{
					var searchString = e.NewTextValue.ToLower();

					_organizationList.ItemsSource = orgs.Where(i => i.Name.ToLower().Contains(searchString));				}
			};

			Content = new StackLayout { 
				BackgroundColor = Color.FromHex("#FFFFFF"),
				Children = {
					_search,
					_organizationList
				}
			};
		}

		public static AddExperiencePage CreatePage(DemoRepository rep) {
			List<Organization> orgs = rep.getOrganizations();
			return new AddExperiencePage (orgs, rep);
		}
	}
	
						public class OrganizationCell : ViewCell
						{
							public OrganizationCell()
							{
								StackLayout cellWrapper = new StackLayout () { Padding = 5 };

								Label organization = new Label () {
									FontSize = 20,
									FontAttributes = FontAttributes.Bold,
								};
								organization.SetBinding(Label.TextProperty, "Name");
								
								cellWrapper.Children.Add (organization);

								View = cellWrapper;
							}
						}


}


