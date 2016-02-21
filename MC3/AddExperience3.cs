using System;

using Xamarin.Forms;
using System.Collections.Generic;

namespace MC3
{
	public class AddExperience3 : ContentPage
	{
		private Picker _pickertimeframe;
		private Picker _pickerPaid;
		private Button _save;

		public AddExperience3 (Organization org, Experience exp, DemoRepository rep)
		{
			Title = "Add Experience";
			_pickertimeframe = new Picker () { Title= "Choose Time Frame" };
			_pickertimeframe.Items.Add ("Summer");
			_pickertimeframe.Items.Add ("SchoolYear");
			_pickertimeframe.Items.Add ("JTerm");

			_pickerPaid = new Picker () { Title= "Paid/Unpaid" };
			_pickerPaid.Items.Add ("Paid");
			_pickerPaid.Items.Add ("Unpaid");

			_save = new Button { Text = "Save" };
			_save.Clicked += async delegate {
				if (_pickerPaid.SelectedIndex < 0 || _pickertimeframe.SelectedIndex < 0) {
					await DisplayAlert("Error", "Fill in all required fields", "Okay");
					return;
				}
				if (exp.TimeFrame.ToString() == _pickertimeframe.Items[_pickertimeframe.SelectedIndex] && 
					(((_pickerPaid.SelectedIndex == 0 ) && exp.Paid ) || (_pickerPaid.SelectedIndex == 1) && !exp.Paid ))   {

					if (exp.studentIds.Contains(rep.getCurrentUser().UserId)) {
						await DisplayAlert("Error", "You have added this position", "Okay");
						return;
					}

					exp.studentIds.Add (rep.getCurrentUser().UserId);
				} else {
					bool paid = (_pickerPaid.SelectedIndex == 0);
					TimePeriod tp;
					if (_pickertimeframe.SelectedIndex == 0) {
						tp = TimePeriod.Summer;
					} else if (_pickertimeframe.SelectedIndex == 1) {
						tp = TimePeriod.SchoolYear;
					} else {
						tp = TimePeriod.JTerm;
					}

					rep.addNewExperience(exp.Title, exp.OrganizationName, exp.OrganizationId, paid, tp); 
				}
				await Navigation.PopAsync();
			};

			Content = new StackLayout { 
				BackgroundColor = Color.FromHex("#FFFFFF"),
				Children = {
					new Label { Text = org.Name, FontSize = 30, XAlign = TextAlignment.Center },
					new Label { Text = exp.Title, FontSize = 20, XAlign = TextAlignment.Center },
					_pickertimeframe,
					_pickerPaid,
					_save
				}
			};
		}
	}
}


