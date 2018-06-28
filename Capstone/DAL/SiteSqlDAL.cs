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
		// PROPERTY
		private readonly string connectionString;

		// CONSTRUCTOR
		public SiteSqlDAL(string databaseConnectionString)
		{
			connectionString = databaseConnectionString;
		}

		// METHOD

		/// <summary>
		/// Queries a list of sites, filtered by campground ID
		/// </summary>
		/// <param name = "campgroundId" >The campground's ID</ param >
		/// < param name="parkId">The park's ID</param>
		/// <returns>A filtered list of sites based on campground and park</returns>
		public IList<Site> GetSites(int parkId, int campgroundId)
		{
			List<Site> siteList = new List<Site>();

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand("SELECT * FROM site JOIN campground ON campground.campground_id = site.campground_id JOIN park ON park.park_id = campground.park_id WHERE park.park_id = @park_id AND campground.campground_id = @campground_id;", conn);
					cmd.Parameters.AddWithValue("@campground_id", campgroundId);
					cmd.Parameters.AddWithValue("@park_id", parkId);

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
						site.DailyFee = Convert.ToDecimal(reader["daily_fee"]);
						site.CampgroundName = Convert.ToString(reader["name"]);
						site.CampgroundOpenMonth = Convert.ToInt32(reader["open_from_mm"]);
						site.CampgroundCloseMonth = Convert.ToInt32(reader["open_to_mm"]);
						site.ParkId = Convert.ToInt32(reader["park_id"]);
						site.ParkName = Convert.ToString(reader["name"]);
						site.ParkLocation = Convert.ToString(reader["location"]);

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
	}
}
