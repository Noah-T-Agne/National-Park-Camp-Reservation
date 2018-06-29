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

		public Reservation AddReservation(int siteId,string reservationName,DateTime[] desiredReservationDates)
		{
			Reservation reservation = new Reservation();
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					SqlCommand cmd = new SqlCommand("INSERT INTO reservation(site_id, name, from_date, to_date,create_date) VALUES(@site_id,@name,@from_date,@to_date,@create_date);", conn);
					cmd.Parameters.AddWithValue("@site_id", siteId);
					cmd.Parameters.AddWithValue("@name", reservationName);
					cmd.Parameters.AddWithValue("@from_date", desiredReservationDates[0]);
					cmd.Parameters.AddWithValue("@to_date", desiredReservationDates[1]);
					cmd.Parameters.AddWithValue("@create_date", DateTime.Today);

					cmd.ExecuteNonQuery();					
					SqlCommand cmd2 = new SqlCommand("SELECT TOP 1* FROM reservation ORDER BY reservation_id DESC;", conn);
					SqlDataReader reader = cmd2.ExecuteReader();
					while (reader.Read())
					{
						reservation.ReservationId = Convert.ToInt32(reader["reservation_id"]);
						reservation.SiteId = Convert.ToInt32(reader["site_id"]);
						reservation.Name = Convert.ToString(reader["name"]);
						reservation.StartDate = Convert.ToDateTime(reader["from_date"]);
						reservation.EndDate = Convert.ToDateTime(reader["to_date"]);
						reservation.CreateDate = Convert.ToDateTime(reader["create_date"]);
						//reservation.DailyFee = Convert.ToDecimal(reader["daily_fee"]);
					}
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return reservation;

		}

		public IList<Reservation> GetReservations(int parkId, int campgroundId,  int siteNumber)
		{
			List<Reservation> reservationList = new List<Reservation>();

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand("SELECT * FROM reservation JOIN site ON site.site_id = reservation.site_id JOIN campground ON campground.campground_id = site.campground_id JOIN park ON park.park_id = campground.park_id WHERE park.park_id = @park_id AND campground.campground_id = @campground_id AND site.site_number = @site_number;", conn);
					cmd.Parameters.AddWithValue("@campground_id", campgroundId);
					cmd.Parameters.AddWithValue("@park_id", parkId);
					cmd.Parameters.AddWithValue("@site_number", siteNumber);

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
						reservation.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
						reservation.IsAccessible = Convert.ToBoolean(reader["accessible"]);
						reservation.MaxRVLength = Convert.ToInt32(reader["max_rv_length"]);
						reservation.HasUtilities = Convert.ToBoolean(reader["utilities"]);
						reservation.CampgroundId = Convert.ToInt32(reader["campground_id"]);
						reservation.DailyFee = Convert.ToDecimal(reader["daily_fee"]);
						reservation.CampgroundName = Convert.ToString(reader["name"]);
						reservation.CampgroundOpenMonth = Convert.ToInt32(reader["open_from_mm"]);
						reservation.CampgroundCloseMonth = Convert.ToInt32(reader["open_to_mm"]);
						reservation.ParkId = Convert.ToInt32(reader["park_id"]);
						reservation.ParkName = Convert.ToString(reader["name"]);
						reservation.ParkLocation = Convert.ToString(reader["location"]);


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
