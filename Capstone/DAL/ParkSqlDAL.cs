using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
	public class ParkSqlDAL
	{
		private readonly string connectionString;



		public ParkSqlDAL(string databaseConnectionString)
		{
			connectionString = databaseConnectionString;
		}

		public IList<Park> GetParks()
		{
			List<Park> parkList = new List<Park>();

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					SqlCommand cmd = new SqlCommand("SELECT * FROM park", conn);
					SqlDataReader reader = cmd.ExecuteReader();

					while (reader.Read())
					{
						Park park = new Park();

						park.ParkId = Convert.ToInt32(reader["park_id"]);
						park.Name = Convert.ToString(reader["name"]);
						park.Location = Convert.ToString(reader["location"]);
						park.EstablishedDate = Convert.ToDateTime(reader["establish_date"]);
						park.Area = Convert.ToInt32(reader["area"]);
						park.AnnualVisitCount = Convert.ToInt32(reader["visitors"]);
						park.Description = Convert.ToString(reader["description"]);

						parkList.Add(park);
					}
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return parkList;

		}

		public Park GetParkInfo(string parkId)
		{
			Park park = new Park();
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand("SELECT * FROM park WHERE park_id = @park_id", conn);
					cmd.Parameters.AddWithValue("@park_id", parkId);

					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						

						park.ParkId = Convert.ToInt32(reader["park_id"]);
						park.Name = Convert.ToString(reader["name"]);
						park.Location = Convert.ToString(reader["location"]);
						park.EstablishedDate = Convert.ToDateTime(reader["establish_date"]);
						park.Area = Convert.ToInt32(reader["area"]);
						park.AnnualVisitCount = Convert.ToInt32(reader["visitors"]);
						park.Description = Convert.ToString(reader["description"]);
					}
				}

			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}

			return park;
		}





	}
}
