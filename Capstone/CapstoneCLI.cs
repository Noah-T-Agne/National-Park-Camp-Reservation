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

				}
				else if (mainMenuSelection.ToLower() == "q")
				{
					QuitApplication();
					break;
				}

				Console.Clear();
				PrintTitle();
				Console.WriteLine();
				PrintHeader();
				PrintMenu();
			}
		}

		private void QuitApplication()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine("Goodbye");

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
			Console.Write(">>  ");
		}


		/// <summary>
		/// Display the list of parks
		/// </summary>
		/// <returns>The park id selected by user</returns>
		private int DisplayAllParks()
		{
			int parkSelection = 0;
			while (true)
			{
				ParkSqlDAL dal = new ParkSqlDAL(DatabaseConnection);
				IList<Park> parks = dal.GetParks();
				

				if (parks.Count > 0)
				{
					Console.WriteLine("Please select the national park that you wish to visit.");
					foreach (Park park in parks)
					{
						Console.WriteLine(park.ParkId.ToString().PadRight(10) + park.Name.PadRight(40));
					}
					Console.Write(">>  ");
					parkSelection = CLIHelper.GetInteger(Console.ReadLine());
					if (parkSelection <= parks.Count && parkSelection > 0)
					{
						break;
					}
					Console.Clear();
				}
				else
				{
					Console.WriteLine("**** SOLD TO PRIVATE CORPORATION-TEDDY ROOSEVELT SPINNING IN GRAVE ****");
				}
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
				int parkInfoId = CLIHelper.GetInteger(Console.ReadLine());

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
					int viewCampgroundsMenuSelection = CLIHelper.GetInteger(Console.ReadLine());

					if (viewCampgroundsMenuSelection == 1)
					{
						Console.Clear();
						campgrounds = DisplayCampgroundInfo(parkId);
						campgroundId = SelectValidCampground(parkId, campgrounds);
						break;
					}
				}
				else if (parkInfoId == 2)
				{
					campgrounds = DisplayCampgroundInfo(parkId);
					campgroundId = SelectValidCampground(parkId, campgrounds);
					break;
				}
				else if (parkInfoId == 3)
				{
					break;
				}
			}
		}

		/// <summary>
		/// Displays list of available campgrounds for selected park
		/// </summary>
		/// <param name="parkId"></param>
		/// <returns>The list of campgrounds available</returns>
		private IList<Campground> DisplayCampgroundInfo(int parkId)
		{
			CampgroundSqlDAL dal = new CampgroundSqlDAL(DatabaseConnection);
			IList<Campground> campgrounds = dal.GetCampgrounds(parkId);
			Console.Clear();
			if (campgrounds.Count > 0)
			{
				Console.WriteLine("CAMPGROUNDS");
				Console.WriteLine("ID".PadRight(10) + "Name".PadRight(40) + "Open".ToString().PadRight(10) + "Close".ToString().PadRight(10) + "Daily Fee".ToString().PadRight(10));


				foreach (Campground campground in campgrounds)
				{
					Console.WriteLine((Convert.ToInt32(campground.CampgroundId)).ToString().PadRight(10) + campground.Name.PadRight(40) + campground.OpenMonth.ToString().PadRight(10) + campground.CloseMonth.ToString().PadRight(10) + campground.DailyFee.ToString("C2").PadRight(10));

				}
			}
			else
			{
				Console.WriteLine("**** NO CAMPGROUNDS TO SHOW ****");
			}
			return campgrounds;
		}

		/// <summary>
		/// User will choose between available campgrounds at selected park by campground id
		/// </summary>
		/// <param name="parkId"></param>
		/// <param name="campgrounds"></param>
		/// <returns>campground id selected by user</returns>
		private int SelectValidCampground(int parkId, IList<Campground> campgrounds)
		{
			int campgroundId;
			while (true)
			{
				Console.Write("Which campground (enter 0 to cancel)?: ");
				campgroundId = CLIHelper.GetInteger(Console.ReadLine());

				decimal dailyFee = 0;
				bool indexExists = false;
				foreach (var campground in campgrounds)
				{
					if (campground.CampgroundId == campgroundId)
					{
						indexExists = true;
						dailyFee = campground.DailyFee;
						break;
					}
				}

				if (campgroundId == 0 || !indexExists)
				{
					break;
				}
				else if (indexExists)
				{
					DateTime[] desiredReservationDates = SearchForCampgroundReservation(campgroundId, campgrounds);
					int[] sitePair = DisplayAvailableSites(parkId, campgroundId);
					if (sitePair[1] == 0)
					{
						break;
					}
					Reservation successfulReservation = CheckAvailableReservation(parkId, campgroundId, sitePair[0], sitePair[1], desiredReservationDates);
					if (successfulReservation.ReservationId != 0)
					{
						Console.WriteLine($"The reservation has been made and Confirmation ID is: {successfulReservation.ReservationId}");
						Console.WriteLine($"The total cost of your stay is {((Convert.ToDecimal((successfulReservation.EndDate - successfulReservation.StartDate).TotalDays) + 1) * dailyFee).ToString("C2")}");
						Console.WriteLine("Thank you for Camping! press any key to return to Main Menu");
						Console.ReadKey();
						break;
					}
				}
			}
			return campgroundId;
		}

		/// <summary>
		/// Checks the entered requested date range against existing reservations for that selected site at the selected campground at the selected park
		/// </summary>
		/// <param name="parkId"></param>
		/// <param name="campgroundId"></param>
		/// <param name="siteNumber"></param>
		/// <param name="desiredReservationDates"></param>
		/// <returns>Reservation at the selected site at the selected campground at the selected park</returns>
		private Reservation CheckAvailableReservation(int parkId, int campgroundId, int siteId, int siteNumber, DateTime[] desiredReservationDates)
		{
			ReservationSqlDAL dal = new ReservationSqlDAL(DatabaseConnection);
			IList<Reservation> reservations = dal.GetReservations(parkId, campgroundId, siteNumber);
			Reservation successfulReservation = new Reservation();
			bool isValidReservation = true;

			foreach (var reservation in reservations)
			{
				if ((desiredReservationDates[0] >= reservation.StartDate && desiredReservationDates[0] <= reservation.EndDate) || (desiredReservationDates[1] >= reservation.StartDate && desiredReservationDates[1] <= reservation.EndDate))
				{
					isValidReservation = false;
					Console.WriteLine("Reservation already booked during all or part of this date range. Please select another date range.");
					break;
				}
			}
			if (isValidReservation)
			{
				Console.Write("Please Enter Reservation Name: ");
				string reservationName = Console.ReadLine();
				successfulReservation = dal.AddReservation(siteId, reservationName, desiredReservationDates);
			}
			return successfulReservation;
		}

		/// <summary>
		/// Collects requested date range for reservation
		/// </summary>
		/// <param name="campgroundId"></param>
		/// <param name="campgrounds"></param>
		/// <returns>Array of datetime representing requested arrival date and requested departure date</returns>
		private DateTime[] SearchForCampgroundReservation(int campgroundId, IList<Campground> campgrounds)
		{
			DateTime[] desiredReservationDates = new DateTime[2];
			Campground currentCampground = new Campground();

			foreach (var campground in campgrounds)
			{
				if (campgroundId == campground.CampgroundId)
				{
					currentCampground = campground;
					break;
				}
			}


			while (true)
			{
				Console.WriteLine();
				Console.WriteLine("What is the arrival date (YYYY-MM-DD) ?:");
				Console.Write("Year (YYYY): ");
				int arrivalYear = CLIHelper.GetInteger(Console.ReadLine());
				Console.Write("Month (MM): ");
				int arrivalMonth = CLIHelper.GetInteger(Console.ReadLine());
				Console.Write("Day (DD): ");
				int arrivalDay = CLIHelper.GetInteger(Console.ReadLine());

				DateTime arrivalDate = new DateTime(arrivalYear, arrivalMonth, arrivalDay);
				desiredReservationDates[0] = arrivalDate;

				Console.WriteLine();

				Console.WriteLine("What is the departure date?:");
				Console.Write("Year (YYYY): ");
				int departureYear = CLIHelper.GetInteger(Console.ReadLine());
				Console.Write("Month (MM): ");
				int departureMonth = CLIHelper.GetInteger(Console.ReadLine());
				Console.Write("Day (DD): ");
				int departureDay = CLIHelper.GetInteger(Console.ReadLine());

				DateTime departureDate = new DateTime(departureYear, departureMonth, departureDay);
				desiredReservationDates[1] = departureDate;

				bool validDateRange = CheckDateRange(currentCampground, desiredReservationDates);

				if (validDateRange)
				{
					break;
				}
				Console.WriteLine("Invalid Date Entered, Please Try Again");
			}

			return desiredReservationDates;
		}

		/// <summary>
		/// Checks date range from user against open and close date range of campground and its associated sites
		/// </summary>
		/// <param name="currentCampground"></param>
		/// <param name="desiredReservationDates"></param>
		/// <returns>The open status of the campground for the requested date range</returns>
		private bool CheckDateRange(Campground currentCampground, DateTime[] desiredReservationDates)
		{
			bool validDateRange = false;
			if (desiredReservationDates[0].Month >= currentCampground.OpenMonth && desiredReservationDates[0].Month <= currentCampground.CloseMonth && desiredReservationDates[1].Month >= currentCampground.OpenMonth && desiredReservationDates[1].Month <= currentCampground.CloseMonth)
			{
				validDateRange = true;
			}
			return validDateRange;
		}

		/// <summary>
		/// Displays the sites available at the selected campground at the selected park
		/// </summary>
		/// <param name="parkId"></param>
		/// <param name="campgroundId"></param>
		/// <returns>Site number selected by user</returns>
		private int[] DisplayAvailableSites(int parkId, int campgroundId)
		{

			
			int siteNumber = 0;
			int siteId = 0;
			int[] sitePair = new int[2] { siteId, siteNumber };
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

				siteNumber = CLIHelper.GetInteger(Console.ReadLine());
				bool validSite = false;
				foreach (var site in sites)
				{
					if (siteNumber == site.SiteNumber)
					{
						validSite = true;
						siteId = site.SiteId;
						sitePair[0] = siteId;
						sitePair[1] = site.SiteNumber;
						break;
					}
				}
				if (validSite || siteNumber == 0)
				{
					break;
				}
			}
			return sitePair;
		}
	}
}

