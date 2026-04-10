using Microsoft.Data.SqlClient;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

		public void AddMovieTitle(string title, int year)
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
						int tConst = Convert.ToInt32(countTitles) + 1;
						cmd.Parameters.AddWithValue("@TConst", tConst);
						cmd.Parameters.AddWithValue("@Title", title);
						cmd.Parameters.AddWithValue("@Year", year);
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							if (reader.Read())
							{
								int newTConst = Convert.ToInt32(reader["TConst"]);
								Console.WriteLine($"Movie Title Added with ID: {newTConst}");
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

		public void UpdateMovieTitle(int tConst, string title, int year)
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
						cmd.Parameters.AddWithValue("@Title", title);
						cmd.Parameters.AddWithValue("@Year", year);

						cmd.ExecuteNonQuery();

						Console.WriteLine($"Movie {tConst} Updated");
					}
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine("Query Error: " + ex.Message);
			}
		}

		public void AddName(string name, int year)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(Secret.connectionString))
				{
					conn.Open();

					using (SqlCommand cmd = new SqlCommand("SProc_AddName", conn))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.AddWithValue("@PrimaryName", name);
						cmd.Parameters.AddWithValue("@BirthYear", year);
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
