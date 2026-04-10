using Microsoft.Data.SqlClient;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IMDbImportData
{
	public class SecureUserDataProtocol
	{
		public void SearchMovieTitles(string title)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(Secret.connectionString))
				{
					conn.Open();
					using (SqlCommand cmd = new SqlCommand("SProc_SearchMovieTitles", conn))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.AddWithValue("@Title", title);
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							while (reader.Read())
							{
								Console.WriteLine($"Title: {reader["PrimaryTitle"]}");
							}
						}
					}
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine("Query Error: " + ex.Message);
			}
		}

		public void SearchNames(string name)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(Secret.connectionString))
				{
					conn.Open();
					using (SqlCommand cmd = new SqlCommand("SProc_SearchNames", conn))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.AddWithValue("@Name", name);
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							while (reader.Read())
							{
								Console.WriteLine($"Name: {reader["PrimaryName"]}");
							}
						}
					}
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine("Query Error: " + ex.Message);
			}
		}

		public void AddMovieTitle(string type, string primTitle, string origTitle, bool isAdult, int? startYear, int? endYear, int? runTime)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(Secret.connectionString))
				{
					conn.Open();

					using (SqlCommand cmd = new SqlCommand("SProc_AddMovieTitle", conn))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						SqlCommand countTitles = new SqlCommand("SELECT COUNT(*) FROM Titles", conn);
						int tConst = Convert.ToInt32(countTitles.ExecuteScalar()) + 1;
						cmd.Parameters.AddWithValue("@TConst", tConst);
						cmd.Parameters.AddWithValue("@Type", type);
						cmd.Parameters.AddWithValue("@PrimaryTitle", primTitle);
						cmd.Parameters.AddWithValue("@OriginalTitle", origTitle);
						cmd.Parameters.AddWithValue("@IsAdult", Convert.ToInt32(isAdult));
						cmd.Parameters.AddWithValue("@StartYear", startYear);
						cmd.Parameters.AddWithValue("@EndYear", endYear);
						cmd.Parameters.AddWithValue("@RunTime", runTime);
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							if (reader.Read())
							{
								Console.WriteLine($"Movie Title Added with ID: {tConst}");
							}
						}
					}
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine("Query Error: " + ex.Message);
			}
		}

		public void UpdateMovieTitle(int tConst, string type, string primTitle, string origTitle, bool isAdult, int? startYear, int? endYear, int? runTime)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(Secret.connectionString))
				{
					conn.Open();

					using (SqlCommand cmd = new SqlCommand("SProc_UpdateMovieTitle", conn))
					{
						cmd.CommandType = CommandType.StoredProcedure;

						cmd.Parameters.AddWithValue("@TConst", tConst);
						cmd.Parameters.AddWithValue("@Type", type);
						cmd.Parameters.AddWithValue("@PrimaryTitle", primTitle);
						cmd.Parameters.AddWithValue("@OriginalTitle", origTitle);
						cmd.Parameters.AddWithValue("@IsAdult", Convert.ToInt32(isAdult));
						cmd.Parameters.AddWithValue("@StartYear", startYear);
						cmd.Parameters.AddWithValue("@EndYear", endYear);
						cmd.Parameters.AddWithValue("@RunTime", runTime);
						cmd.ExecuteNonQuery();

						Console.WriteLine($"Movie with ID: {tConst} - Updated");
					}
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine("Query Error: " + ex.Message);
			}
		}

		public void AddName(string name, int? birthYear, int? deathYear)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(Secret.connectionString))
				{
					conn.Open();

					using (SqlCommand cmd = new SqlCommand("SProc_AddName", conn))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						SqlCommand countNames = new SqlCommand("SELECT COUNT(*) FROM Names", conn);
						int nConst = Convert.ToInt32(countNames.ExecuteScalar()) + 1;
						cmd.Parameters.AddWithValue("@NConst", nConst);
						cmd.Parameters.AddWithValue("@PrimaryName", name);
						cmd.Parameters.AddWithValue("@BirthYear", birthYear);
						cmd.Parameters.AddWithValue("@DeathYear", deathYear);
						cmd.ExecuteNonQuery();
					}
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine("Query Error: " + ex.Message);
			}
		}

		public void DeleteMovieTitle(int tConst)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(Secret.connectionString))
				{
					conn.Open();

					using (SqlCommand cmd = new SqlCommand("SProc_DeleteMovieTitle", conn))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.AddWithValue("@TConst", tConst);
						cmd.ExecuteNonQuery();
						Console.WriteLine($"Movie with {tConst} Deleted");
					}
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine("Query Error: " + ex.Message);
			}
		}
	}
}
