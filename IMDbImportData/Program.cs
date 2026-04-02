// See https://aka.ms/new-console-template for more information
using IMDbImportData;
using IMDbImportData.Models;
using Microsoft.Data.SqlClient;
using IMDbImportData.Inserters;
using System;
using System.Diagnostics.Metrics;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

Console.WriteLine("IMDb Import");
List<TitleModel> movies = new List<TitleModel>();
List<GenreModel> genres = new List<GenreModel>();
List<NameModel> names = new List<NameModel>();
List<ProfessionModel> professions = new List<ProfessionModel>();
List<NameTitleModel> nameTitles = new List<NameTitleModel>();
List<CrewDirectorModel> directors = new List<CrewDirectorModel>();
List<CrewWriterModel> writers = new List<CrewWriterModel>();

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

foreach (string crew in File.ReadLines("C:/temp/title.crew.tsv").Skip(1).Take(10))
{
	string[] parts = crew.Split("\t");
	string[] directorParts = new string[2];
	string[] writerParts = new string[2];
	if (parts.Length == 3)
	{
		Console.WriteLine(crew);
		string directorString = parts[1];
		string writerString = parts[2];
		try
		{
			string[] directorArr = directorString.Split(",");
			string[] writerArr = writerString.Split(",");
			foreach (string director in directorArr)
			{
				directorParts[0] = parts[0];
				directorParts[1] = director;
				directors.Add(new CrewDirectorModel(directorParts));
			}
			foreach (string writer in writerArr)
			{
				writerParts[0] = parts[0];
				writerParts[1] = writer;
				writers.Add(new CrewWriterModel(writerParts));
			}
		}
		catch (Exception ex)
		{
			directorParts[0] = parts[0];
			directorParts[1] = parts[1];
			writerParts[0] = parts[0];
			writerParts[1] = parts[2]; 
			directors.Add(new CrewDirectorModel(directorParts));
			writers.Add(new CrewWriterModel(writerParts));
		}
	}
	else
	{
		Console.WriteLine("Invalid line: " + crew);
	}
}

foreach (string name in File.ReadLines("C:/temp/name.basics.tsv").Skip(1).Take(10))
{
	string[] parts = name.Split("\t");
	string[] professionParts = new string[2];
	string[] nameTitleParts = new string[2];
	if (parts.Length == 6)
	{
		Console.WriteLine(name);
		string professionString = parts[4];
		string titlesString = parts[5];
		try
		{
			string[] professionArr = professionString.Split(",");
			string[] titlesArr = titlesString.Split(",");
			foreach (string profession in professionArr)
			{
				professionParts[0] = parts[0];
				professionParts[1] = profession;
				professions.Add(new ProfessionModel(professionParts));
			}
			foreach (string title in titlesArr)
			{
				nameTitleParts[0] = parts[0];
				nameTitleParts[1] = title;
				nameTitles.Add(new NameTitleModel(nameTitleParts));
			}
		}
		catch (Exception ex)
		{
			professionParts[0] = parts[0];
			professionParts[1] = parts[4];
			nameTitleParts[0] = parts[0];
			nameTitleParts[1] = parts[5];
			professions.Add(new ProfessionModel(professionParts));
			nameTitles.Add(new NameTitleModel(nameTitleParts));
		}
	}
	else
	{
		Console.WriteLine("Invalid line: " + name);
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

//foreach (NameModel name in names)
//{
//	Console.WriteLine(name);
//}

//foreach (ProfessionModel profession in professions)
//{
//	Console.WriteLine(profession);
//}

//foreach (NameTitleModel nameTitle in nameTitles)
//{
//	Console.WriteLine(nameTitle);
//}

//foreach (CrewDirectorModel director in directors)
//{
//	Console.WriteLine(director);
//}

//foreach (CrewWriterModel writer in writers)
//{
//	Console.WriteLine(writer);
//}

IInserter inserter = new BulkInserter();
SqlConnection sqlConn = new SqlConnection(Secret.connectionString);
sqlConn.Open();
inserter.InsertTitles(movies, sqlConn);
inserter.InsertGenres(genres, sqlConn);
sqlConn.Close();
