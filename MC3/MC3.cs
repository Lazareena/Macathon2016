using System;

using Xamarin.Forms;

namespace MC3
{
	public class App : Application
	{
		public Button _button;
		public StackLayout _loginPage;
		public DemoRepository _demoRep;
		public App ()
		{
			_button = new Button
			{
				Text = "Login",
				BorderRadius = 20,
				StyleId = "LoginButton"
			};
					

			Image mainLogo = new Image { Aspect = Aspect.AspectFit };
			ImageSource loopLogo = ImageSource.FromResource ("MC3.SharedResources.Loop2.jpeg");
			mainLogo.Source = loopLogo;

			Image join = new Image { Aspect = Aspect.AspectFit };
			ImageSource joinImage = ImageSource.FromResource ("MC3.SharedResources.JoinButton.jpg");
			join.Source = joinImage;

			Image login = new Image { Aspect = Aspect.AspectFit };
			ImageSource loginImage = ImageSource.FromResource ("MC3.SharedResources.LoginButton.jpg");
			login.Source = loginImage;

			var loginTapped = new TapGestureRecognizer ();
			loginTapped.Tapped += delegate {
				_demoRep = new DemoRepository();
				Page experienceFeed = MainTabPage.CreateMainPage(_demoRep);
//				MainPage.Navigation.PushAsync(experienceFeed);
				Application.Current.MainPage = new NavigationPage(experienceFeed);
			};
			login.GestureRecognizers.Add (loginTapped);

			Grid grid = new Grid {
				VerticalOptions = LayoutOptions.FillAndExpand,
				RowDefinitions = {
					new RowDefinition { Height = new GridLength(100)},
				},
				ColumnDefinitions = {
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
				}
			};

			grid.Children.Add (join, 0, 0);
			grid.Children.Add (login,1,0);

			_loginPage = new StackLayout {
				VerticalOptions = LayoutOptions.Center,
				Children = {
					mainLogo,
					grid
				}
			};

			// The root page of your application
			ContentPage mainPage = new ContentPage {
				BackgroundColor = Color.FromHex("#FFFFFF"),
				Content = _loginPage
			};
			MainPage = new NavigationPage (mainPage);
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

