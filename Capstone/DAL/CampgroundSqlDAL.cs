using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
	public class CampgroundSqlDAL
	{
		// PROPERTY
		private readonly string connectionString;

		// CONSTRUCTOR
		public CampgroundSqlDAL(string databaseConnection)
		{
			connectionString = databaseConnection;
		}

		// METHOD

		/// <summary>
		/// Queries a list of campgrounds, filtered by selected park
		/// </summary>
		/// <param name="parkId">The unique identifier of a national park</param>
		/// <returns>The campgrounds of the selected park</returns>
		public IList<Campground> GetCampgrounds(int parkId)
		{
			List<Campground> campgroundList = new List<Campground>();

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand("SELECT * FROM campground JOIN park ON park.park_id = campground.park_id WHERE park.park_id = @park_id;", conn);
					cmd.Parameters.AddWithValue("@park_id", parkId);

					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						Campground campground = new Campground();

						campground.CampgroundId = Convert.ToInt32(reader["campground_id"]);
						campground.ParkId = Convert.ToInt32(reader["park_id"]);
						campground.Name = Convert.ToString(reader["name"]);
						campground.OpenMonth = Convert.ToInt32(reader["open_from_mm"]);
						campground.CloseMonth = Convert.ToInt32(reader["open_to_mm"]);
						campground.DailyFee = Convert.ToDecimal(reader["daily_fee"]);
						campground.ParkLocation = Convert.ToString(reader["location"]);
						campground.ParkName = Convert.ToString(reader["name"]);

						campgroundList.Add(campground);
					}
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return campgroundList;
		}
	}
}
