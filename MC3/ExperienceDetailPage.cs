using System;

using Xamarin.Forms;
using System.Collections.Generic;

namespace MC3
{
	public class ExperienceDetailPage : ContentPage
	{
		public ExperienceDetailPage (Experience exp, List<User> users, DemoRepository rep)
		{
			StackLayout comp = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Children = {
					new Label { Text = "Company Name:", FontAttributes = FontAttributes.Bold, FontSize = 18 },
					new Label { Text = exp.OrganizationName, FontAttributes = FontAttributes.Bold, FontSize = 18 }
				}
			};
			StackLayout position = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Children = {
					new Label { Text = "Position:", FontSize = 18 },
					new Label { Text = exp.Title, FontSize = 18 }
				}
			};
			StackLayout timeFrame = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Children = {
					new Label { Text = "Time Frame:", FontSize = 18 },
					new Label { Text = exp.TimeFrame.ToString(), FontSize = 18 }
				}
			};

			ListView listViewContacts = new ListView {
				// Source of data items.
				ItemsSource = users,

				// Define template for displaying each item.
				ItemTemplate = new DataTemplate (() => {
					return new ContactCell ();
				}),
				HasUnevenRows = true

			};

			listViewContacts.ItemTapped += delegate(object sender, ItemTappedEventArgs e) {
				User usr = e.Item as User;
				UserProfilePage profPage = UserProfilePage.CreatePage(usr, rep, false);
				ParentView.Navigation.PushAsync(profPage);
				listViewContacts.SelectedItem = null;

			};

			Content = new StackLayout { 
				BackgroundColor = Color.FromHex("#FFFFFF"),
				Children = {
					comp,
					position,
					timeFrame,
					new BoxView { BackgroundColor= Color.FromHex("#ffd32f2f"), HeightRequest= 3 },
					new Label { Text = "Contacts:", FontSize = 15, XAlign = TextAlignment.Center},
					new BoxView { BackgroundColor= Color.FromHex("#ffd32f2f"), HeightRequest= 3 },
					listViewContacts
				}
			};
		}

		public static ExperienceDetailPage CreatePage(Experience exp , DemoRepository rep) {
			List<User> users = rep.getUsersWithIdList (exp.studentIds);
			return new ExperienceDetailPage(exp, users,rep);
		}
	}

	public class ContactCell : ViewCell
	{
		public ContactCell()
		{
			StackLayout cellWrapper = new StackLayout () { BackgroundColor = Color.FromHex("#FFFFFF"),
				Padding = 5 };

			Label name = new Label () {
				FontSize = 20,
				FontAttributes = FontAttributes.Bold,
			};
			name.SetBinding(Label.TextProperty, "Name");

			Label major = new Label () {
				FontSize = 15,
			};
			major.SetBinding(Label.TextProperty, "Major");

			Label classYear = new Label () {
				FontSize = 15,
				FontAttributes = FontAttributes.Bold,
			};
			classYear.SetBinding(Label.TextProperty, "ClassYear");
			cellWrapper.Children.Add (name);
			cellWrapper.Children.Add (major);
			cellWrapper.Children.Add (classYear);

			View = cellWrapper;
		}
	}
}


