// See https://aka.ms/new-console-template for more information
using IMDbImportData;
using IMDbImportData.Models;
using Microsoft.Data.SqlClient;
using IMDbImportData.Inserters;
using System;
using System.Diagnostics.Metrics;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;

Console.WriteLine("IMDb Import");
List<TitleModel> movies = new List<TitleModel>();
List<GenreModel> genres = new List<GenreModel>();
List<NameModel> names = new List<NameModel>();
List<ProfessionModel> professions = new List<ProfessionModel>();
List<NameTitleModel> nameTitles = new List<NameTitleModel>();
List<CrewDirectorModel> directors = new List<CrewDirectorModel>();
List<CrewWriterModel> writers = new List<CrewWriterModel>();
int numberOfRows = 10000;
void ReadInTitleBasics()
{
	foreach (string movie in File.ReadLines("C:/temp/title.basics.tsv").Skip(1).Take(numberOfRows))
	{
		string[] parts = movie.Split('\t');
		string[] genreParts = new string[2];
		if (parts.Length == 9)
		{
			//Console.WriteLine(movie);
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
}

void ReadInNameBasics()
{
	foreach (string name in File.ReadLines("C:/temp/name.basics.tsv").Skip(1).Take(numberOfRows))
	{
		string[] parts = name.Split("\t");
		string[] professionParts = new string[2];
		string[] nameTitleParts = new string[2];
		if (parts.Length == 6)
		{
			//Console.WriteLine(name);
			names.Add(new NameModel(parts));
			string professionString = parts[4];
			string titlesString = parts[5];
			try
			{
				string[] professionArr = professionString.Split(",");
				string[] titlesArr = titlesString.Split(",");
				if ((professionArr.Length > 1 || titlesArr.Length > 1) && (!professionString.Equals("\\N") || !titlesArr.Equals("\\N")))
				{
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
				else
				{
					throw new Exception("Split Prefix Missing");
				}
			}
			catch (Exception ex)
			{
				if (parts[4].Equals("\\N") || parts[5].Equals("\\N"))
				{
					continue;
				}
				else
				{
					professionParts[0] = parts[0];
					professionParts[1] = parts[4];
					nameTitleParts[0] = parts[0];
					nameTitleParts[1] = parts[5];
					professions.Add(new ProfessionModel(professionParts));
					nameTitles.Add(new NameTitleModel(nameTitleParts));
				}
			}
		}
		else
		{
			Console.WriteLine("Invalid line: " + name);
		}
	}
}
void ReadInTitleCrews()
{
	foreach (string crew in File.ReadLines("C:/temp/title.crew.tsv").Skip(1).Take(numberOfRows))
	{
		string[] parts = crew.Split("\t");
		string[] directorParts = new string[2];
		string[] writerParts = new string[2];
		if (parts.Length == 3)
		{
			//Console.WriteLine(crew);
			string directorString = parts[1];
			string writerString = parts[2];
			try
			{
				string[] directorArr = directorString.Split(",");
				string[] writerArr = writerString.Split(",");
				if ((directorArr.Length > 1) || (writerArr.Length > 1))
				{
					if (writerString.Equals("\\N")) 
					{
						foreach (string director in directorArr)
						{
							directorParts[0] = parts[0];
							directorParts[1] = director;
							directors.Add(new CrewDirectorModel(directorParts));
						}
					}
					else if (directorString.Equals("\\N"))
					{
						foreach (string writer in writerArr)
						{
							writerParts[0] = parts[0];
							writerParts[1] = writer;
							writers.Add(new CrewWriterModel(writerParts));
						}
					}
					else
					{
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
				}
				else
				{
					throw new Exception("Exception caught, jump to catch");
				}
			}
			catch (Exception ex)
			{
				if (!parts[1].Equals("\\N"))
				{
					directorParts[0] = parts[0];
					directorParts[1] = parts[1];
					directors.Add(new CrewDirectorModel(directorParts));
				}
				if (!parts[2].Equals("\\N"))
				{
					writerParts[0] = parts[0];
					writerParts[1] = parts[2];
					writers.Add(new CrewWriterModel(writerParts));
				}
				continue;
			}
		}
		else
		{
			Console.WriteLine("Invalid line: " + crew);
		}
	}
}

Stopwatch sw = new Stopwatch();

sw.Start();
//ReadInTitleBasics();
//ReadInNameBasics();
ReadInTitleCrews();
sw.Start();
Console.WriteLine("Time Elapsed to Read in Data: " + sw.ElapsedMilliseconds + " ms");


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

IInserter inserter = new PreparedInserter();
SqlConnection sqlConn = new SqlConnection(Secret.connectionString);
sw.Start();
sqlConn.Open();
//inserter.InsertTitles(movies, sqlConn);
//inserter.InsertGenres(genres, sqlConn);
//inserter.InsertNames(names, sqlConn);
//inserter.InsertProfessions(professions, sqlConn);
//inserter.InsertNameTitles(nameTitles, sqlConn);
//inserter.InsertCrewDirectors(directors, sqlConn);
inserter.InsertCrewWriters(writers, sqlConn);
sqlConn.Close();
sw.Stop();

Console.WriteLine("Elapsed milliseconds to Insert Data: " + sw.ElapsedMilliseconds + " ms");
