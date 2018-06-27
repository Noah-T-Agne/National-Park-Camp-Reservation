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
		const string Command_Quit = "q";
		const string DatabaseConnection = @"Data Source=.\SQLExpress;Initial Catalog=Campground;Integrated Security=True";

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
					GetAllParks();
					DisplayQuit();
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
		private void DisplayParkInfo(string input)
		{
			Console.Clear();
			ParkSqlDAL dal = new ParkSqlDAL(DatabaseConnection);
			Park park = dal.GetParkInfo(input);

			Console.WriteLine("Park Information Screen");
			Console.WriteLine(park.Name);
			Console.WriteLine("Location: ".PadRight(25) + park.Location);
			Console.WriteLine("Established: ".PadRight(25) + park.EstablishedDate);
			Console.WriteLine("Area: ".PadRight(25) + park.Area+" sq km");
			Console.WriteLine("Annual Visitors: ".PadRight(25) + park.AnnualVisitCount);
			Console.WriteLine();
			Console.WriteLine(park.Description);

			Console.ReadKey();


		}
		private void DisplayQuit()
		{
			Console.WriteLine("Q".PadRight(10) + "Quit".PadRight(40));

		}

		private void QuitApplication()
		{
			Console.WriteLine("Thank You for Searching the National Park Database");
		}

		private void GetAllParks()
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

		private void PrintHeader()
		{
			Console.WriteLine("Welcome to Park Campground Site Reservation(Do Da Do Da)");
		}

		private void PrintMenu()
		{
			Console.Clear();
			Console.WriteLine("Main Menu Please type in a command");
			Console.WriteLine(" 1 - Show all Parks");
			Console.WriteLine(" 2 - Show all employees");
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
