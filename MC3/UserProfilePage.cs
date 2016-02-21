using System;

using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace MC3
{
	public class UserProfilePage : ContentPage
	{
		private ListView _listViewExperience;
		private Image _addButton;

		public UserProfilePage (User user, DemoRepository rep, bool isRoot)
		{
			_listViewExperience = new ListView 
			{
				// Source of data items.
				ItemsSource = user.Experiences,

				// Define template for displaying each item.
				ItemTemplate = new DataTemplate(() =>
					{
						return new ExperienceCellUserPage();
					}),
				HasUnevenRows = true,

					
			};
			_addButton = new Image { Aspect = Aspect.AspectFit };
			ImageSource addButtonImage = ImageSource.FromResource ("MC3.SharedResources.AddButton3.png");
			_addButton.Source = addButtonImage;

			var addTapped = new TapGestureRecognizer ();
			addTapped.Tapped += async delegate {
				var expPage = AddExperiencePage.CreatePage(rep);
				await ParentView.Navigation.PushAsync(expPage);
			};
			_addButton.GestureRecognizers.Add (addTapped);

			Title = "Profile";
			StackLayout content = new StackLayout { 
				BackgroundColor = Color.FromHex("#FFFFFF"),
//				BackgroundColor = Color.FromHex("ffb2ebf2"),
				Children = {
					new Label { Text = user.Name, FontSize = 20, FontAttributes = FontAttributes.Bold, XAlign = TextAlignment.Center },
					new Label { Text = user.Major + ", " + user.ClassYear, FontSize= 18, XAlign = TextAlignment.Center }, 
					new Label { Text = user.Email, FontSize =18, XAlign = TextAlignment.Center },
					new Label { Text = user.PhoneNumber, FontSize =18, XAlign = TextAlignment.Center },
					new BoxView { BackgroundColor= Color.FromHex("#ffd32f2f"), HeightRequest= 3 },
					new Label { Text = "Experiences:", FontSize =18, XAlign = TextAlignment.Center },
					new BoxView { BackgroundColor= Color.FromHex("#ffd32f2f"), HeightRequest= 3 },
					_listViewExperience
				}
			};

			if (isRoot) {
				content.Children.Add (_addButton);
			}
			Content = content;
		}

		public static UserProfilePage CreatePage(User user, DemoRepository rep, bool isRoot) {
			return new UserProfilePage (user, rep, isRoot);
		}
	}

	public class ExperienceCellUserPage : ViewCell
	{
		public ExperienceCellUserPage()
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

			Label exp = new Label { FontSize = 15};
			exp.SetBinding (Label.TextProperty, "Title");

			Label paid = new Label { FontSize = 15 };
			if (experience.Paid) {
				paid.Text = "Paid";
			} else {
				paid.Text = "Unpaid";
			}

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

			grid.Children.Add (organization, 1, 0);
			grid.Children.Add (exp, 1, 1);
			grid.Children.Add (paid, 1, 2);
			grid.Children.Add (logoforTime, 0, 1, 0, 3);

			cellWrapper.Children.Add (grid);
			View = cellWrapper;
		}
	}
}


