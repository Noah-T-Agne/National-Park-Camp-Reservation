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
		// DECLARE DATABASE CONNECTION
		const string DatabaseConnection = @"Data Source=.\SQLExpress;Initial Catalog=Campground;Integrated Security=True";

		/// <summary>
		/// Runs the console line interface for National Park Reservations
		/// </summary>
		public void RunCLI()
		{
			PrintTitle();
			Console.WriteLine();
			PrintHeader();
			PrintMenu();

			while (true)
			{
				string mainMenuSelection = Console.ReadLine();

				Console.Clear();
				mainMenuSelection = mainMenuSelection.ToLower();
				if (mainMenuSelection == "1")
				{
					int parkSelection = DisplayAllParks();
					DisplayParkInfo(parkSelection);

					//string input = Console.ReadLine();
					//if (input == "1" || input == "2" || input == "3")
					//{
					//	DisplayParkInfo(input);
					//}
				}
				//else if (command == Command_Quit)
				//{
				//	QuitApplication();
				//	break;
				//}

				Console.Clear();
				PrintMenu();
			}
		}

		private void PrintTitle()
		{
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine(@"   .syyyyyyyyyyyyyyyyyyyyyyyyyyyys.	  __  __ ___ ________  ___  __  __ ___ __     ");
			Console.WriteLine(@"   m+           \    /           +m	  ||\ ||// \\| || ||| // \\ ||\ ||// \\||     ");
			Console.WriteLine(@"   M-            \  /            -M	  ||\\||||=||  ||  ||((   ))||\\||||=||||     ");
			Console.WriteLine(@"   M-            /MM\            -M	  || \|||| ||  ||  || \\_// || \|||| ||||==|| ");
			Console.WriteLine(@"   M-           /mMMm\           -M	  ____  ___ ____ __ ___  ");
			Console.WriteLine(@"   M-          /mMMMMm\          -M	  || \\// \\|| \\|| ///   ");
			Console.WriteLine(@"   M-         /mMMMMMMm\         -M	  ||_//||=||||_//||<< 	  ");
			Console.WriteLine(@"   M-        /mMMMMMMMMm\        -M	  ||   || |||| \\|| \\\   ");
			Console.WriteLine(@"   M-       /mMMMMMMMMMMm\       -M	   ___ ___ ___  _______  __ ________ ____");
			Console.WriteLine(@"   M-      /mMMMMMMMMMMMMm\      -M	  //  // \\||\\//|||| \\(( \||| || |||	");
			Console.WriteLine(@"   M-     /mMMMMMMMMMMMMMMm\     -M	 ((   ||=|||| \/ ||||_// \\ ||  ||  ||==");
			Console.WriteLine(@"   M-    /mMMMMVVVVVVVVMMMMm\    -M	  \\__|| ||||    ||||   \_))||  ||  ||___");
			Console.WriteLine(@"   M-   /mMMMMM|      |MMMMMm\   -M	  ____  ____ __  ________ __ __ ___ ________  ___  __  __     ");
			Console.WriteLine(@"   M-  /mMMMMMM|      |MMMMMMm\  -M	  || \\||   (( \||   || \\|| ||// \\| || ||| // \\ ||\ ||     ");
			Console.WriteLine(@"   M-./mMMMMMMM|______|MMMMMMMm\.-M	  ||_//||==  \\ ||== ||_//\\ //||=||  ||  ||((   ))||\\||     ");
			Console.WriteLine(@"   N+............................+N	  || \\||___\_))||___|| \\ \V/ || ||  ||  || \\_// || \||     ");
			Console.WriteLine(@"   .syyyyyyyyyyyyyyyyyyyyyyyyyyyys.      ");
			Console.ResetColor();
		}

		private void PrintHeader()
		{
			Console.WriteLine("Welcome to Park Campground Site Reservation(Do Da Do Da)");
		}

		private void PrintMenu()
		{
			Console.WriteLine("Main Menu Please type in a command");
			Console.WriteLine(" 1 - Show all Parks");
			Console.WriteLine(" Q - Quit");
			Console.WriteLine();
		}

		private int DisplayAllParks()
		{
			ParkSqlDAL dal = new ParkSqlDAL(DatabaseConnection);
			IList<Park> parks = dal.GetParks();
			int parkSelection = 0;

			if (parks.Count > 0)
			{
				Console.WriteLine("Please select the national park that you wish to visit.");
				foreach (Park park in parks)
				{
					Console.WriteLine(park.ParkId.ToString().PadRight(10) + park.Name.PadRight(40));
				}
				parkSelection = Convert.ToInt32(Console.ReadLine());
			}
			else
			{
				Console.WriteLine("**** SOLD TO PRIVATE CORPORATION-TEDDY ROOSEVELT SPINNING IN GRAVE ****");
			}

			return parkSelection;
		}

		/// <summary>
		/// Displays information for a selected park
		/// </summary>
		/// <param name="parkId">Numerical ID of a national park</param>
		private void DisplayParkInfo(int parkId)
		{
			while (true)
			{
				Console.Clear();
				ParkSqlDAL dal = new ParkSqlDAL(DatabaseConnection);
				Park park = dal.GetParkInfo(parkId);

				Console.WriteLine("Park Information Screen");
				Console.WriteLine(park.Name + " National Park");
				Console.WriteLine("Location: ".PadRight(25) + park.Location);
				Console.WriteLine("Established: ".PadRight(25) + park.EstablishedDate);
				Console.WriteLine("Area: ".PadRight(25) + park.Area + " sq km");
				Console.WriteLine("Annual Visitors: ".PadRight(25) + park.AnnualVisitCount);
				Console.WriteLine();
				Console.WriteLine(park.Description);
				Console.WriteLine();

				Console.WriteLine("Select a Command:");
				Console.WriteLine("  1) View Campgrounds");
				Console.WriteLine("  2) Search for Reservation");
				Console.WriteLine("  3) Return to Previous Screen");
				Console.Write(">> ");
				int parkInfoId = Convert.ToInt32(Console.ReadLine());

				int campgroundId = 0;
				IList<Campground> campgrounds;

				if (parkInfoId == 1)
				{
					DisplayCampgroundInfo(parkId);
					Console.WriteLine();
					Console.WriteLine("Select a Command");
					Console.WriteLine("  1) Search for Available Reservation");
					Console.WriteLine("  2) Return to Previous Screen");
					Console.Write(">> ");
					int viewCampgroundsMenuSelection = Convert.ToInt32(Console.ReadLine());

					if (viewCampgroundsMenuSelection == 1)
					{
						Console.Clear();
						campgrounds = DisplayCampgroundInfo(parkId);
						campgroundId = SelectValidCampground(parkId, campgrounds);
						//DisplayAvailableSites(parkId, campgroundId);
					}
				}
				else if (parkInfoId == 2)
				{
					campgrounds = DisplayCampgroundInfo(parkId);
					campgroundId = SelectValidCampground(parkId, campgrounds);
					
					//DisplayAvailableSites(parkId, campgroundId);
				}
				else if (parkInfoId == 3)
				{
					break;
				}

				if (campgroundId != 0)
				{
					SearchForCampgroundReservation();
				}

				//DisplayCampgroundInfo(parkId, campgroundId);
			}
		}

		private IList<Campground> DisplayCampgroundInfo(int parkId)
		{
			CampgroundSqlDAL dal = new CampgroundSqlDAL(DatabaseConnection);
			IList<Campground> campgrounds = dal.GetCampgrounds(parkId);
			Console.Clear();
			if (campgrounds.Count > 0)
			{
				Console.WriteLine("CAMPGROUNDS");
				Console.WriteLine("ID".PadRight(10) + "Name".PadRight(30) + "Open".ToString().PadRight(10) + "Close".ToString().PadRight(10) + "Daily Fee".ToString().PadRight(10));


				foreach (Campground campground in campgrounds)
				{
					Console.WriteLine((Convert.ToInt32(campground.CampgroundId)).ToString().PadRight(10) + campground.Name.PadRight(30) + campground.OpenMonth.ToString().PadRight(10) + campground.CloseMonth.ToString().PadRight(10) + campground.DailyFee.ToString("C2").PadRight(10));

				}
			}
			else
			{
				Console.WriteLine("**** NO CAMPGROUNDS TO SHOW ****");
			}
			return campgrounds;
		}

		private int SelectValidCampground(int parkId, IList<Campground> campgrounds)
		{
			int campgroundId;
			while (true)
			{
				Console.Write("Which campground (enter 0 to cancel)?: ");
				campgroundId = Convert.ToInt32(Console.ReadLine());

				bool indexExists = false;
				foreach (var campground in campgrounds)
				{
					if (campground.CampgroundId == campgroundId)
					{
						indexExists = true;
						break;
					}
				}

				if (campgroundId == 0)
				{
					break;
				}
				else if (indexExists)
				{
					DisplayAvailableSites(parkId, campgroundId);
				}
			}
			return campgroundId;
		}

		private DateTime[] SearchForCampgroundReservation()
		{
			DateTime[] desiredReservationDates = new DateTime[2];

			Console.WriteLine();
			Console.WriteLine("What is the arrival date?:");
			Console.Write("Year (YYYY): ");
			string arrivalYear = Console.ReadLine();
			Console.Write("Month (MM): ");
			string arrivalMonth = Console.ReadLine();
			Console.Write("Day (DD): ");
			string arrivalDay = Console.ReadLine();

			DateTime arrivalDate = new DateTime(Convert.ToInt32(arrivalYear), Convert.ToInt32(arrivalMonth), Convert.ToInt32(arrivalDay));
			desiredReservationDates[0] = arrivalDate;

			Console.WriteLine();

			Console.WriteLine("What is the departure date?:");
			Console.Write("Year (YYYY): ");
			string departureYear = Console.ReadLine();
			Console.Write("Month (MM): ");
			string departureMonth = Console.ReadLine();
			Console.Write("Day (DD): ");
			string departureDay = Console.ReadLine();

			DateTime departureDate = new DateTime(Convert.ToInt32(departureYear), Convert.ToInt32(departureMonth), Convert.ToInt32(departureDay));
			desiredReservationDates[1] = departureDate;

			return desiredReservationDates;
		}

		private void DisplayAvailableSites(int parkId, int campgroundId)
		{
			while (true)
			{
				SiteSqlDAL dal = new SiteSqlDAL(DatabaseConnection);
				IList<Site> sites = dal.GetSites(parkId, campgroundId);

				if (sites.Count > 0)
				{
					Console.WriteLine("Site Number".ToString().PadRight(15) + "Max. Occupancy".ToString().PadRight(15) + "Accessible?".ToString().PadRight(15) + "Max RV Length".ToString().PadRight(15) + "Utility".ToString().PadRight(15) + "Cost".ToString());
					foreach (Site site in sites)
					{
						Console.WriteLine(site.SiteNumber.ToString().PadRight(15) + site.MaxOccupancy.ToString().PadRight(15) + site.IsAccessible.ToString().PadRight(15) + site.MaxRVLength.ToString().PadRight(15) + site.HasUtilities.ToString().PadRight(15) + site.DailyFee.ToString("C2"));
					}
				}
				else
				{
					Console.WriteLine("**** SOLD TO PRIVATE CORPORATION-TEDDY ROOSEVELT SPINNING IN GRAVE ****");

				}
				Console.WriteLine();
				Console.Write("Which site should be reserved (enter 0 to cancel)? ");

				Console.ReadLine();
				//DisplayAllReservations(sites);
			}
		}
	}
}

//		private void DisplayAllReservations(IList<Site> sites)
//		{
//			string siteSelection = Console.ReadLine();
//			while (true)
//			{
//				if (siteSelection == "0")
//				{
//					break;
//				}
//				else
//				{
//					Console.Write("What name should the reservation be made under? ");
//					string reservationName = Console.ReadLine();

//					ReservationSqlDAL dal = new ReservationSqlDAL(DatabaseConnection);
//					IList<Reservation> reservations = dal.GetReservations(sites[Convert.ToInt32(siteSelection) - 1].SiteId);
//					if (sites.Count > 0)
//					{
//						Console.Clear();
//						Console.WriteLine("Reservation Id".ToString().PadRight(15) + "Site Id".ToString().PadRight(15) + "Name".ToString().PadRight(15) + "Check In".ToString().PadRight(15) + "Check Out".ToString().PadRight(15) + "Book Date".ToString());
//						foreach (Reservation reservation in reservations)
//						{
//							Console.WriteLine(reservation.ReservationId.ToString().PadRight(15) + reservation.SiteId.ToString().PadRight(15) + reservation.Name.ToString().PadRight(15) + reservation.StartDate.ToString().PadRight(15) + reservation.EndDate.ToString().PadRight(15) + reservation.CreateDate.ToString() + reservation.SiteNumber.ToString());
//						}
//					}
//					else
//					{
//						Console.WriteLine("**** SOLD TO PRIVATE CORPORATION-TEDDY ROOSEVELT SPINNING IN GRAVE ****");

//					}
//					Console.ReadKey();

//				}
//			}
//		}

//		
//		}

