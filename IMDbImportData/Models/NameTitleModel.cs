using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbImportData.Models
{
	public class NameTitleModel
	{
		public int NConst { get; set; }
		public int TConst { get; set; }

		public NameTitleModel(string[] nameTitleInfo)
		{
			NConst = Convert.ToInt32(nameTitleInfo[0]);
			TConst = Convert.ToInt32(nameTitleInfo[1]);
		}

		public override string ToString()
		{
			return NConst + " - " + TConst;
		}
	}
}
