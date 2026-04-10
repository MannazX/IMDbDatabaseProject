using IMDbImportData.Inserters;
using IMDbImportData.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbImportData
{
	public class ReadData
	{
		public List<TitleModel> Movies { get; set; }
		public List<TitleGenreModel> Genres { get; set; }
		public List<NameModel> Names { get; set; }
		public List<ProfessionModel> Professions { get; set; }
		public List<NameTitleModel> NameTitles { get; set; }
		public List<CrewDirectorModel> Directors { get; set; }
		public List<CrewWriterModel> Writers { get; set; }
		public int NumberOfRows { get; set; }

		public ReadData(int numberOfRows)
		{
			NumberOfRows = numberOfRows;
			Movies = new List<TitleModel>();
			Genres = new List<TitleGenreModel>();
			Names = new List<NameModel>();
			Professions = new List<ProfessionModel>();
			NameTitles = new List<NameTitleModel>();
			Directors = new List<CrewDirectorModel>();
			Writers = new List<CrewWriterModel>();
		}
		public void ReadInTitleBasics()
		{
			Console.WriteLine("IMDb Read In Movie Title Data");
			foreach (string movie in File.ReadLines("C:/temp/title.basics.tsv").Skip(1).Take(NumberOfRows))
			{
				string[] parts = movie.Split('\t');
				string[] genreParts = new string[2];
				if (parts.Length == 9)
				{
					//Console.WriteLine(movie);
					Movies.Add(new TitleModel(parts));
					string genreString = parts[8];
					try
					{
						string[] genreArr = genreString.Split(",");
						foreach (string genre in genreArr)
						{
							genreParts[0] = parts[0];
							genreParts[1] = genre;
							Genres.Add(new TitleGenreModel(genreParts));
						}
					}
					catch (Exception ex)
					{
						genreParts[0] = parts[0];
						genreParts[1] = parts[8];
						Genres.Add(new TitleGenreModel(genreParts));
					}
				}
				else
				{
					Console.WriteLine("Invalid line: " + movie);
				}
			}
		}

		public void ReadInNameBasics()
		{
			Console.WriteLine("IMDb Read In Name Data");

			foreach (string name in File.ReadLines("C:/temp/name.basics.tsv").Skip(1).Take(NumberOfRows))
			{
				string[] parts = name.Split("\t");
				string[] professionParts = new string[2];
				string[] nameTitleParts = new string[2];
				if (parts.Length == 6)
				{
					//Console.WriteLine(name);
					Names.Add(new NameModel(parts));
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
								Professions.Add(new ProfessionModel(professionParts));
							}
							foreach (string title in titlesArr)
							{
								nameTitleParts[0] = parts[0];
								nameTitleParts[1] = title;
								NameTitles.Add(new NameTitleModel(nameTitleParts));
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
							Professions.Add(new ProfessionModel(professionParts));
							NameTitles.Add(new NameTitleModel(nameTitleParts));
						}
					}
				}
				else
				{
					Console.WriteLine("Invalid line: " + name);
				}
			}
		}

		public void ReadInTitleCrews()
		{
			Console.WriteLine("IMDb Read In Crew Data");

			foreach (string crew in File.ReadLines("C:/temp/title.crew.tsv").Skip(1).Take(NumberOfRows))
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
									Directors.Add(new CrewDirectorModel(directorParts));
								}
							}
							else if (directorString.Equals("\\N"))
							{
								foreach (string writer in writerArr)
								{
									writerParts[0] = parts[0];
									writerParts[1] = writer;
									Writers.Add(new CrewWriterModel(writerParts));
								}
							}
							else
							{
								foreach (string director in directorArr)
								{
									directorParts[0] = parts[0];
									directorParts[1] = director;
									Directors.Add(new CrewDirectorModel(directorParts));
								}
								foreach (string writer in writerArr)
								{
									writerParts[0] = parts[0];
									writerParts[1] = writer;
									Writers.Add(new CrewWriterModel(writerParts));
								}
							}
						}
						else
						{
							throw new Exception("Special case caught, jump to catch");
						}
					}
					catch (Exception ex)
					{
						if (!parts[1].Equals("\\N"))
						{
							directorParts[0] = parts[0];
							directorParts[1] = parts[1];
							Directors.Add(new CrewDirectorModel(directorParts));
						}
						if (!parts[2].Equals("\\N"))
						{
							writerParts[0] = parts[0];
							writerParts[1] = parts[2];
							Writers.Add(new CrewWriterModel(writerParts));
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

		public void ReadInsertAllTables()
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			ReadInTitleBasics();
			ReadInNameBasics();
			ReadInTitleCrews();
			sw.Stop();
			Console.WriteLine("Time Elapsed to Read in Data: " + sw.ElapsedMilliseconds + " ms");

			IInserter inserter = new PreparedInserter();
			SqlConnection sqlConn = new SqlConnection(Secret.connectionString);
			sw.Start();
			sqlConn.Open();

			inserter.InsertTitles(Movies, sqlConn);
			inserter.InsertGenres(Genres, sqlConn);
			inserter.InsertNames(Names, sqlConn);
			inserter.InsertProfessions(Professions, sqlConn);
			inserter.InsertNameTitles(NameTitles, sqlConn);
			inserter.InsertCrewDirectors(Directors, sqlConn);
			inserter.InsertCrewWriters(Writers, sqlConn);
			sqlConn.Close();
			sw.Stop();

			Console.WriteLine("Elapsed milliseconds to Insert Data: " + sw.ElapsedMilliseconds + " ms");
		}
	}
}
