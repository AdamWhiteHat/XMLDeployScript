using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XMLDeploySimple
{
    public class XSource
    {
		[XmlAttribute]
		public string Name { get; set; }
		[XmlAttribute]
		public string Location { get; set; }

		public XSource() { }
		public XSource(string Name, string Location)
		{
			this.Name = Name;
			this.Location = Location;
		}
	}
}
