using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IMDbImportData.Models
{
	public class NameModel
	{
		public int NConst { get; set; }
		public string PrimaryName { get; set; }
		public int? BirthYear { get; set; }
		public int? DeathYear { get; set; }

		public NameModel(string[] nameInfo)
		{
			NConst = Convert.ToInt32(nameInfo[0].Substring(2));
			PrimaryName = nameInfo[1];
			BirthYear = ParseNullableInt(nameInfo[3]);
			DeathYear = ParseNullableInt(nameInfo[4]);
		}

		private int? ParseNullableInt(string value)
		{
			if (int.TryParse(value, out int result))
			{
				return result;
			}
			return null;
		}

		public override string ToString()
		{
			return NConst + " - " + PrimaryName;
		}
	}
}
