using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
	public class SiteSqlDAL
	{
		private readonly string connectionString;

		public SiteSqlDAL(string databaseConnectionString)
		{
			connectionString = databaseConnectionString;
		}

		public IList<Site> GetSites(string campgroundId)
		{
			List<Site> siteList = new List<Site>();

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand("SELECT * FROM site WHERE site.campground_id = @campground_id;", conn);
					cmd.Parameters.AddWithValue("@campground_id", campgroundId);

					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						Site site = new Site();

						site.SiteId = Convert.ToInt32(reader["site_id"]);
						site.CampgroundId = Convert.ToInt32(reader["campground_id"]);
						site.SiteNumber = Convert.ToInt32(reader["site_number"]);
						site.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
						site.IsAccessible = Convert.ToBoolean(reader["accessible"]);
						site.MaxRVLength = Convert.ToInt32(reader["max_rv_length"]);
						site.HasUtilities = Convert.ToBoolean(reader["utilities"]);

						siteList.Add(site);
					}
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return siteList;
		}


		public IList<Campground> GetCampgrounds(string parkId)
		{
			List<Campground> campgroundList = new List<Campground>();

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand("SELECT * FROM campground WHERE campground.park_id = @park_id;", conn);
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
