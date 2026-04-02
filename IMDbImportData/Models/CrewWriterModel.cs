using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbImportData.Models
{
	public class CrewWriterModel
	{
		public int TConst { get; set; }
		public int NConst { get; set; }

		public CrewWriterModel(string[] writers)
		{
			TConst = Convert.ToInt32(writers[0]);
			NConst = Convert.ToInt32(writers[1]);
		}

		public override string ToString()
		{
			return TConst + " - " + NConst;
		}
	}
}
