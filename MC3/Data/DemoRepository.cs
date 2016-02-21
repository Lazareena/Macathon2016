using System;
using System.Collections.Generic;
using System.Linq;

namespace MC3
{
	public class DemoRepository
	{
		public DemoRepository ()
		{
		}
			
		private static Experience userZeroExperienceOne = new Experience 
		{ 	
			Id = 0, Title = "Mobile Developer", 
			OrganizationName = "Critical Hits Technologies", 
			OrganizationId = 0, studentIds = new List<int>{0}, 
			TimeFrame = TimePeriod.Summer, 
			Paid= true 
		};
		private static Experience userZeroExperienceTwo = new Experience 
		{ 
			Id = 1, Title = "Garden Program Manager", 
			OrganizationName = "Urban Roots", 
			OrganizationId = 1, studentIds = new List<int>{0}, 
			TimeFrame = TimePeriod.Summer,
			Paid= true 
		};
		private static Experience userOneExperienceOne = new Experience 
		{ 
			Id = 2, Title = "Research Assistant", 
			OrganizationName = "University Of Minnesota", 
			OrganizationId = 2, studentIds = new List<int>{1}, 
			Paid = true, 
			TimeFrame = TimePeriod.JTerm
		};
		private static Experience userOneExperienceTwo = new Experience 
		{ 
			Id = 3, Title = "English As A Second Language", 
			OrganizationName = "University Of Minnesota", 
			OrganizationId = 2, studentIds = new List<int>{1}, 
			Paid = true, 
			TimeFrame = TimePeriod.SchoolYear 
		};
		private static Experience userTwoExperienceOne = new Experience 
		{ 
			Id = 4, Title = "School Outreach Intern", 
			OrganizationName = "Ramsey County Workforce Solutions", 
			OrganizationId = 3, studentIds = new List<int>{2}, 
			Paid = true, 
			TimeFrame = TimePeriod.SchoolYear 
		};
//		private static Experience userThreeExperienceOne = new Experience 
//		{ 
//			Id = 5, Title = "English As A Second Language", 
//			OrganizationName = "University Of Minnesota", 
//			OrganizationId = 2, studentIds = new List<int>{1}, 
//			Paid = true, 
//			TimeFrame = TimePeriod.SchoolYear 
//		};

		private static List<Experience> experiencesUserZero = new List<Experience>() {  userZeroExperienceOne, userZeroExperienceTwo };
		private static List<Experience> experiencesUserOne = new List<Experience>() { userOneExperienceOne, userOneExperienceTwo };
		private static List<Experience> experiencesUserTwo = new List<Experience>() { userTwoExperienceOne };


		private static User userZero = new User() { UserId = 0, Name = "Reena", Major = "Economics", ClassYear = 2016, PhoneNumber = "651-208-8044", Email = "lthaveet@macalester.edu",
			Experiences = experiencesUserZero
		};
		private static User userOne = new User() { UserId = 1, Name = "Eloise", Major = "Neuroscience", ClassYear = 2017, PhoneNumber = "651-208-8044", Email = "lthaveet@macalester.edu",
			Experiences = experiencesUserOne
		};
		private static User userTwo = new User() { UserId = 2, Name = "Kelsey", Major = "Educational Studies", ClassYear = 2016, PhoneNumber = "651-208-8044", Email = "lthaveet@macalester.edu",
			Experiences = experiencesUserTwo
		};

		private static Organization organizationZero = new Organization 
		{ 
			Id = 0, Name = "Critical Hits Technologies", 
			Location = "Seattle", Students = new List<User>() { userOne }, 
			Positions = new List<Experience>() { userZeroExperienceOne } 
		};
		private static Organization organizationOne = new Organization 
		{ 
			Id = 1, Name = "Urban Roots", 
			Location = "Saint Paul", Students = new List<User>() { userZero }, 
			Positions = new List<Experience>() { userZeroExperienceTwo } 
		};
		private static Organization organizationTwo = new Organization 
		{ 
			Id = 2, Name = "University Of Minnesota", Location = "Minneapolis", 
			Students = new List<User>() { userOne }, 
			Positions = new List<Experience>() { userOneExperienceOne, userOneExperienceTwo } 
		};
		private static Organization organizationThree = new Organization 
		{ 
			Id = 3, Name = "Ramsey County Workforce Solutions", Location = "Saint Paul", 
			Students = new List<User>() { userTwo }, 
			Positions = new List<Experience>() { userTwoExperienceOne } 
		};


		private static List<User> _allUsers = new List<User> () { userZero, userOne, userTwo };
		private static List<Experience> _allExperiences = new List<Experience> () { userZeroExperienceOne, userZeroExperienceTwo, userOneExperienceOne, userOneExperienceTwo, userTwoExperienceOne  };
		private static List<Organization> _allOrganizations = new List<Organization> () {organizationZero,  organizationOne, organizationTwo, organizationThree };

		public List<Experience> getExperiences() { return _allExperiences; }
		public List<User> getUsers() { return _allUsers; }
		public List<Organization> getOrganizations() { return _allOrganizations; }
		public User getCurrentUser() { return userZero; }

		public void addNewExperience(string expName, string org, int orgId, bool paid, TimePeriod tf) {
			Experience exp = new Experience { 
				Id = _allExperiences[_allExperiences.Count-1].Id + 1, Title = expName, 
				OrganizationName = org, 
				OrganizationId = orgId, studentIds = new List<int>{ userZero.UserId }, 
				Paid = paid, 
				TimeFrame = tf 
			};

			_allExperiences.Add (exp);
			_allExperiences.OrderBy (i => i.Id).ToList ();
			_allOrganizations.Find (i => i.Id == orgId).Positions.Add (exp);
			userZero.Experiences.Add (exp);
			userZero.Experiences.OrderBy (i => i.Id).ToList ();
		}

		public List<User> getUsersWithIdList(List<int> idList) {
			List<User> userList = new List<User> ();

			foreach (int user in idList) {
				User usr = _allUsers.Find (i => i.UserId == user);
				userList.Add (usr);
			}
			return userList;
		}

	}
}

