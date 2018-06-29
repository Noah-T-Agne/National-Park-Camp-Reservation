using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
	public class CLIHelper
	{
		public static DateTime GetDateTime(string message)
		{
			string userInput = message;
			DateTime dateValue = DateTime.MinValue;

			while (!DateTime.TryParse(userInput, out dateValue))
			{
				Console.WriteLine("Invalid input format. Please try again");
				Console.Write(">>  ");
				userInput = Console.ReadLine();
			}
			

			return dateValue;
		}

		public static int GetInteger(string message)
		{
			string userInput = message;
			int intValue = 0;

			while (!int.TryParse(userInput,out intValue))
			{
				Console.WriteLine("Invalid input format. Please try again");
				Console.Write(">>  ");
				userInput = Console.ReadLine();
			}
			return intValue;
		}


		public static double GetDouble(string message)
		{
			string userInput = String.Empty;
			double doubleValue = 0.0;
			int numberOfAttempts = 0;

			do
			{
				if (numberOfAttempts > 0)
				{
					Console.WriteLine("Invalid input format. Please try again");
				}

				Console.Write(message + " ");
				userInput = Console.ReadLine();
				numberOfAttempts++;
			}
			while (!double.TryParse(userInput, out doubleValue));

			return doubleValue;

		}

		public static bool GetBool(string message)
		{
			string userInput = String.Empty;
			bool boolValue = false;
			int numberOfAttempts = 0;

			do
			{
				if (numberOfAttempts > 0)
				{
					Console.WriteLine("Invalid input format. Please try again");
				}

				Console.Write(message + " ");
				userInput = Console.ReadLine();
				numberOfAttempts++;
			}
			while (!bool.TryParse(userInput, out boolValue));

			return boolValue;
		}

		public static string GetString(string message)
		{
			string userInput = String.Empty;
			int numberOfAttempts = 0;

			do
			{
				if (numberOfAttempts > 0)
				{
					Console.WriteLine("Invalid input format. Please try again");
				}

				Console.Write(message + " ");
				userInput = Console.ReadLine();
				numberOfAttempts++;
			}
			while (String.IsNullOrEmpty(userInput));

			return userInput;
		}
	}
}
