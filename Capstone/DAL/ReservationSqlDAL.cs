using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
	public class ReservationSqlDAL
	{
		private readonly string connectionString;

		public ReservationSqlDAL(string databaseConnectionString)
		{
			connectionString = databaseConnectionString;
		}

		public void AddReservation()
		{

		}

		public IList<Reservation> GetReservations(int campgroundId, int parkId, int siteId)
		{
			List<Reservation> reservationList = new List<Reservation>();

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand("SELECT * FROM reservation JOIN site ON site.site_id = reservation.site_id JOIN campground ON campground.campground_id = site.campground_id JOIN park ON park.park_id = campground.park_id WHERE park.park_id = @park_id AND campground.campground.id = @campground.id AND site.site_id = @site_id;", conn);
					cmd.Parameters.AddWithValue("@campground_id", campgroundId);
					cmd.Parameters.AddWithValue("@park_id", parkId);
					cmd.Parameters.AddWithValue("@site_id", siteId);

					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						Reservation reservation = new Reservation();

						reservation.ReservationId = Convert.ToInt32(reader["reservation_id"]);
						reservation.SiteId= Convert.ToInt32(reader["site_id"]);
						reservation.Name = Convert.ToString(reader["name"]);
						reservation.StartDate = Convert.ToDateTime(reader["from_date"]);
						reservation.EndDate = Convert.ToDateTime(reader["to_date"]);
						reservation.CreateDate = Convert.ToDateTime(reader["create_date"]);
						reservation.SiteNumber = Convert.ToInt32(reader["site_number"]);

						reservationList.Add(reservation);
					}
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return reservationList;
		}

	}
}
