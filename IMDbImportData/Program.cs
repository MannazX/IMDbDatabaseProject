// See https://aka.ms/new-console-template for more information
using IMDbImportData;
using System;
using System.Diagnostics.Metrics;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;

Console.WriteLine("IMDB User Interface");
Console.WriteLine("Select an option");
Console.WriteLine("1. Read in Data to Tables\n2. Run Application\n3. Exit System");
Console.WriteLine();

string options = Console.ReadLine() ?? "";

switch (options)
{
	case "1":
		Console.WriteLine("Write the number of rows to insert");
		int numRows = Convert.ToInt32(Console.ReadLine());
		ReadData reader = new ReadData(numRows);
		reader.ReadInsertAllTables();
		break;
	case "2":
		UserInterface ui = new UserInterface();
		ui.Execute();
		break;
	case "3":
		break;
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


