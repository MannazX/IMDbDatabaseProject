// See https://aka.ms/new-console-template for more information
using IMDbImportData;
using Microsoft.Data.SqlClient;
using System;
using System.Diagnostics.Metrics;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

Console.WriteLine("IMDb Import");
List<TitleModel> movies = new List<TitleModel>();
List<GenreModel> genres = new List<GenreModel>();

foreach (string movie in File.ReadLines("C:/temp/title.basics.tsv").Skip(1).Take(10))
{
	string[] parts = movie.Split('\t');
	string[] genreParts = new string[2];
	if (parts.Length == 9)
	{
		Console.WriteLine(movie);
		movies.Add(new TitleModel(parts));
		string genreString = parts[8];
		try
		{
			string[] genreArr = genreString.Split(",");
			foreach (string genre in genreArr)
			{
				genreParts[0] = parts[0];
				genreParts[1] = genre;
				genres.Add(new GenreModel(genreParts));
			}
		}
		catch (Exception ex)
		{
			genreParts[0] = parts[0];
			genreParts[1] = parts[8];
			genres.Add(new GenreModel(genreParts));
		}
	}
	else
	{
		Console.WriteLine("Invalid line: " + movie);
	}
}

//foreach (TitleModel movie in movies)
//{
//	Console.WriteLine(movie);
//}

//foreach (GenreModel genre in genres)
//{
//	Console.WriteLine(genre);
//}

IInserter inserter = new BulkInserter();
SqlConnection sqlConn = new SqlConnection(Secret.connectionString);
sqlConn.Open();
inserter.InsertTitles(movies, sqlConn);
inserter.InsertGenres(genres, sqlConn);
sqlConn.Close();
