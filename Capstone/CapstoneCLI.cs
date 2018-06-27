using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone
{
	public class CapstoneCLI
	{
		const string Command_GetAllParks = "1";

		const string Command_GetAssociatedCampgrounds = "1";
		const string Command_SearchForReservation = "2";

		const string Command_Quit = "q";
		const string DatabaseConnection = @"Data Source=.\SQLExpress;Initial Catalog=Campground;Integrated Security=True";


		/// <summary>
		/// Runs the console line interface for National Park Reservations
		/// </summary>
		public void RunCLI()
		{
			PrintHeader();
			PrintMenu();

			while (true)
			{
				string command = Console.ReadLine();

				Console.Clear();
				command = command.ToLower();
				if (command == Command_GetAllParks)
				{
					DisplayAllParks();
					//DisplayQuit();
					string input = Console.ReadLine();
					if(input =="1"|| input == "2"|| input == "3")
					{
						DisplayParkInfo(input);
					}
				}
				else if (command == Command_Quit)
				{
					QuitApplication();
					break;
				}

				PrintMenu();
			}
		}

		/// <summary>
		/// Displays information for a selected park
		/// </summary>
		/// <param name="parkId">Numerical ID of a national park</param>
		private void DisplayParkInfo(string parkId)
		{
			while (true)
			{
				Console.Clear();
				ParkSqlDAL dal = new ParkSqlDAL(DatabaseConnection);
				Park park = dal.GetParkInfo(parkId);

				Console.WriteLine("Park Information Screen");
				Console.WriteLine(park.Name);
				Console.WriteLine("Location: ".PadRight(25) + park.Location);
				Console.WriteLine("Established: ".PadRight(25) + park.EstablishedDate);
				Console.WriteLine("Area: ".PadRight(25) + park.Area + " sq km");
				Console.WriteLine("Annual Visitors: ".PadRight(25) + park.AnnualVisitCount);
				Console.WriteLine();
				Console.WriteLine(park.Description);
				Console.WriteLine();

				SelectOptionFromParkInfoMenu(parkId);
			}
		}

		private void SelectOptionFromParkInfoMenu(string parkId)
		{
			Console.WriteLine("Select a Command:");
			Console.WriteLine("  1) View Campgrounds");
			Console.WriteLine("  2) Search for Reservation");
			Console.WriteLine("  3) Return to Previous Screen");

			string command = Console.ReadLine();

			Console.Clear();
			command = command.ToLower();
			if (command == Command_GetAssociatedCampgrounds)
			{
				DisplayAllCampgrounds(parkId);
				Console.WriteLine();
				Console.WriteLine("Press any key to return to the Park Information Screen.");
				Console.ReadKey();
			}
			else if (command == Command_SearchForReservation)
			{
				IList<Campground> campgroundList = DisplayAllCampgrounds(parkId);
				DisplayAvailableSites(campgroundList);
			}
		}


		//=========================================================
		//=========================================================
		//=========================================================
		private void DisplayAvailableSites(IList<Campground> campgrounds)
		{
			Console.Write("Which campground (enter 0 to cancel)?:  ");
			string campgroundId = Console.ReadLine();

			Console.WriteLine("What is the arrival date?:");
			Console.Write("Year (YYYY): ");
			string arrivalYear = Console.ReadLine();
			Console.Write("Month (MM): ");
			string arrivalMonth = Console.ReadLine();
			Console.Write("Day (DD): ");
			string arrivalDay = Console.ReadLine();

			Console.WriteLine();

			Console.WriteLine("What is the departure date?:");
			Console.Write("Year (YYYY): ");
			string departureYear = Console.ReadLine();
			Console.Write("Month (MM): ");
			string departureMonth = Console.ReadLine();
			Console.Write("Day (DD): ");
			string departureDay = Console.ReadLine();

			SiteSqlDAL dal = new SiteSqlDAL(DatabaseConnection);
			IList<Site> sites = dal.GetSites(campgroundId);

			if (sites.Count > 0)
			{
				Console.WriteLine("Site Number".ToString().PadRight(15) + "Max. Occupancy".ToString().PadRight(15) + "Accessible?".ToString().PadRight(15) + "Max RV Length".ToString().PadRight(15) + "Utility".ToString().PadRight(15) + "Cost".ToString());
				foreach (Site site in sites)
				{
					// START HERE TOMORROW. THIS DOES NOT WORK**********************************************************8
					Console.WriteLine(site.SiteNumber.ToString().PadRight(15) + site.MaxOccupancy.ToString().PadRight(15) + site.IsAccessible.ToString().PadRight(15) + site.MaxRVLength.ToString().PadRight(15) + site.HasUtilities.ToString().PadRight(15) + campgrounds[Convert.ToInt32(campgroundId) - 1].DailyFee.ToString("C2"));
				}
			}
			else
			{
				Console.WriteLine("**** SOLD TO PRIVATE CORPORATION-TEDDY ROOSEVELT SPINNING IN GRAVE ****");

			}
			Console.ReadKey();
		}
		//=========================================================
		//=========================================================
		//=========================================================

		private void DisplayQuit()
		{
			Console.WriteLine("Q".PadRight(10) + "Quit".PadRight(40));

		}

		private void QuitApplication()
		{
			Console.WriteLine("Thank You for Searching the National Park Database");
		}

		private void DisplayAllParks()
		{
			ParkSqlDAL dal = new ParkSqlDAL(DatabaseConnection);
			IList<Park> parks = dal.GetParks();
			if (parks.Count > 0)
			{
				foreach (Park park in parks)
				{
					Console.WriteLine(park.ParkId.ToString().PadRight(10) + park.Name.PadRight(40));
				}
			}
			else
			{
				Console.WriteLine("**** SOLD TO PRIVATE CORPORATION-TEDDY ROOSEVELT SPINNING IN GRAVE ****");

			}
		}

		private IList<Campground> DisplayAllCampgrounds(string parkId)
		{
			CampgroundSqlDAL dal = new CampgroundSqlDAL(DatabaseConnection);
			IList<Campground> campgrounds = dal.GetCampgrounds(parkId);
			if (campgrounds.Count > 0)
			{
				foreach (Campground campground in campgrounds)
				{
					Console.WriteLine(campground.CampgroundId.ToString().PadRight(10) + campground.Name.PadRight(40) + campground.OpenMonth.ToString().PadRight(40) + campground.CloseMonth.ToString().PadRight(40) + campground.DailyFee.ToString("C2").PadRight(40));
				}
			}
			else
			{
				Console.WriteLine("**** SOLD TO PRIVATE CORPORATION-TEDDY ROOSEVELT SPINNING IN GRAVE ****");

			}

			return campgrounds;
		}

		private void PrintHeader()
		{
			Console.WriteLine("Welcome to Park Campground Site Reservation(Do Da Do Da)");
		}

		private void PrintMenu()
		{
			Console.Clear();
			Console.WriteLine("Main Menu Please type in a command");
			Console.WriteLine(" 1 - Show all Parks");
			//Console.WriteLine(" 2 - Show all employees");
			//Console.WriteLine(" 3 - Employee search by first and last name");
			//Console.WriteLine(" 4 - Get employees without projects");
			//Console.WriteLine(" 5 - Get all projects");
			//Console.WriteLine(" 6 - Create Department");
			//Console.WriteLine(" 7 - Update Department Name");
			//Console.WriteLine(" 8 - Create Project");
			//Console.WriteLine(" 9 - Assign Employee to Project");
			//Console.WriteLine("10 - Remove Employee from Project");

			Console.WriteLine(" Q - Quit");
			Console.WriteLine();

		}

	}
}
