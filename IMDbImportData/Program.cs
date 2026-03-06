// See https://aka.ms/new-console-template for more information
using IMDbImportData;
using Microsoft.Data.SqlClient;
using System;
using System.Diagnostics.Metrics;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

Console.WriteLine("IMDb Import");
List<TitleModel> movies = new List<TitleModel>();

foreach (string movie in File.ReadLines("C:/temp/title.basics.tsv").Skip(1).Take(10))
{
	string[] parts = movie.Split('\t');
	if (parts.Length == 9)
	{
		Console.WriteLine(movie);
		movies.Add(new TitleModel(parts));
	}
	else
	{
		Console.WriteLine("Invalid line: " + movie);
	}
}

foreach (TitleModel movie in movies)
{
	Console.WriteLine(movie);
}

NormalInserter inserter = new NormalInserter();
SqlConnection sqlConn = new SqlConnection();
sqlConn.Open();
inserter.InsertTitles(movies, sqlConn);
sqlConn.Close();
