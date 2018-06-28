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
		private readonly string connectionString;

		public CampgroundSqlDAL(string databaseConnection)
		{
			connectionString = databaseConnection;
		}

		public IList<Campground> GetCampgrounds(string parkId)
		{
			List<Campground> campgroundList = new List<Campground>();

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand("SELECT * FROM campground WHERE campground.park_id = @park_id ORDER BY park_id ASC;", conn);
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
