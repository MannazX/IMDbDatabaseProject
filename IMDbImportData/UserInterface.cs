using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace IMDbImportData
{
	public class UserInterface
	{
		private SecureUserDataProtocol userProtocol = new SecureUserDataProtocol();

		public void Execute()
		{
			while (true)
			{
				Console.WriteLine("\nIMDB UI Options");
				Console.WriteLine("1.1 - Search Movie\n" +
				"1.2 - Add Movie\n" +
				"1.3 - Update Movie\n" +
				"1.4 - Delete Movie\n" +
				"2.1 - Search Name\n" +
				"2.2 - Add Name\n" + 
				"3 - Exit System");

				string options = Console.ReadLine() ?? "";
				string type;
				string primTitle;
				string origTitle;
				bool isAdult;
				int? startYear;
				int? endYear;
				int? runTime;
				string name;
				int? birthYear;
				int? deathYear;
				string input;

				try
				{
					switch (options)
					{
						case "1.1":
							Console.WriteLine("Enter movie title: ");
							primTitle = Console.ReadLine() ?? "";
							userProtocol.SearchMovieTitles(primTitle);
							break;
						case "1.2":
							Console.WriteLine("Enter movie type: ");
							type = Console.ReadLine() ?? "";
							Console.WriteLine("Enter movie primary title: ");
							primTitle = Console.ReadLine() ?? "";
							Console.WriteLine("Enter movie original title: ");
							input = Console.ReadLine() ?? "";
							origTitle = !input.Equals(primTitle) ? input : primTitle;
							Console.WriteLine("Is the movie rated adult? (y/n): ");
							input = Console.ReadLine() ?? "";
							isAdult = input.ToLower()[0].Equals("y") ? true : false;
							Console.WriteLine("Enter movie starting year: ");
							try
							{
								startYear = Convert.ToInt32(Console.ReadLine());
							}
							catch (Exception ex)
							{
								startYear = null; 
							}
							Console.WriteLine("Enter movie ending year: ");
							try
							{
								endYear = Convert.ToInt32(Console.ReadLine());
							}
							catch (Exception ex)
							{
								endYear = null;
							}
							Console.WriteLine("Enter movie runtime: ");
							try
							{
								runTime = Convert.ToInt32(Console.ReadLine());
								if (runTime <= 0)
								{
									runTime = null;
								}
							}
							catch (Exception ex)
							{
								runTime = null;
							}
							userProtocol.AddMovieTitle(type, primTitle, origTitle, isAdult, startYear, endYear, runTime);
							break;
						case "1.3":
							Console.WriteLine("Enter movie ID: ");
							int tConst = Convert.ToInt32(Console.ReadLine());
							Console.WriteLine("Enter updated type: ");
							type = Console.ReadLine() ?? "";
							Console.WriteLine("Enter updated primary title: ");
							primTitle = Console.ReadLine() ?? "";
							Console.WriteLine("Enter updated original title: ");
							input = Console.ReadLine() ?? "";
							origTitle = !input.Equals(primTitle) ? input : primTitle;
							Console.WriteLine("Is the movie updated to adult rating? (y/n): ");
							input = Console.ReadLine() ?? "";
							isAdult = input.ToLower()[0].Equals("y") ? true : false;
							Console.WriteLine("Enter updated starting year: ");
							try
							{
								startYear = Convert.ToInt32(Console.ReadLine());
							}
							catch (Exception ex)
							{
								startYear = null;
							}
							Console.WriteLine("Enter updated ending year: ");
							try
							{
								endYear = Convert.ToInt32(Console.ReadLine());
							}
							catch (Exception ex)
							{
								endYear = null;
							}
							Console.WriteLine("Enter updated runtime: ");
							try
							{
								runTime = Convert.ToInt32(Console.ReadLine());
								if (runTime <= 0)
								{
									runTime = null;
								}
							}
							catch (Exception ex)
							{
								runTime = null;
							}
							userProtocol.UpdateMovieTitle(tConst, type, primTitle, origTitle, isAdult, startYear, endYear, runTime);
							break;
						case "1.4":
							Console.WriteLine("Enter movie ID: ");
							tConst = Convert.ToInt32(Console.ReadLine());
							userProtocol.DeleteMovieTitle(tConst);
							break;
						case "2.1":
							Console.WriteLine("Enter persons name: ");
							name = Console.ReadLine() ?? "";
							userProtocol.SearchNames(name);
							break;
						case "2.2":
							Console.WriteLine("Enter persons name: ");
							name = Console.ReadLine() ?? "";
							Console.WriteLine("Enter persons birth year: ");
							try
							{
								birthYear = Convert.ToInt32(Console.ReadLine());
							}
							catch (Exception ex)
							{
								birthYear = null;
							}
							Console.WriteLine("Enter persons death year: ");
							try
							{
								deathYear = Convert.ToInt32(Console.ReadLine());
							}
							catch (Exception ex)
							{
								deathYear = null;
							}
							break;
						case "3": return;
						default:
							Console.WriteLine("You did not enter one of the options");
							break;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}
	}
}
