using System;

using Xamarin.Forms;

namespace MC3
{
	public class MainTabPage : TabbedPage
	{
		private static TheExperienceFeed _experienceFeed;
		private static UserProfilePage _userPage;
		public MainTabPage (TheExperienceFeed experienceFeed, UserProfilePage userPage)
		{
			Children.Add (experienceFeed);
			Children.Add (userPage);
		}

		public static Page CreateMainPage(DemoRepository rep) {
		    _experienceFeed = TheExperienceFeed.CreateExperienceFeed(rep);
			User currUser = rep.getCurrentUser ();
			_userPage = UserProfilePage.CreatePage (currUser, rep, true);
			MainTabPage tabPage = new MainTabPage (_experienceFeed, _userPage);
			return tabPage;
		}

	}
}


