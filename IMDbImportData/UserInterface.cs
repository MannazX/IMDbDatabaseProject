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
				string title;
				string name;
				int year;
				int id;

				try
				{
					switch (options)
					{
						case "1.1":
							Console.WriteLine("Enter movie title: ");
							title = Console.ReadLine() ?? "";
							userProtocol.SearchMovieTitles(title);
							break;
						case "1.2":
							Console.WriteLine("Enter movie title: ");
							title = Console.ReadLine() ?? "";
							Console.WriteLine("Enter movie year: ");
							year = Convert.ToInt32(Console.ReadLine());
							userProtocol.AddMovieTitle(title, year);
							break;
						case "1.3":
							Console.WriteLine("Enter perName: ");
							name = Console.ReadLine() ?? "";
							break;
						case "1.4":
							Console.WriteLine("Enter Movie ID: ");
							id = Convert.ToInt32(Console.ReadLine());
							Console.WriteLine("Enter Updated Movie Title");
							title = Console.ReadLine() ?? "";
							Console.WriteLine("Enter Updated Movie Year");
							year = Convert.ToInt32(Console.ReadLine());
							userProtocol.UpdateMovieTitle(id, title, year);
							break;
						case "2.1":
							Console.WriteLine("Enter persons name: ");
							name = Console.ReadLine() ?? "";
							userProtocol.SearchNames(name);
							break;
						case "2.2":
							Console.WriteLine("Enter persons ID: ");
							id = Convert.ToInt32(Console.ReadLine());
							userProtocol.DeleteMovieTitle(id);
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
