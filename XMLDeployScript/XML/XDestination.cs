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
    public class XDestination
    {
		[XmlAttribute]
		public string Name { get; set; }
		[XmlText]
		public string Locations { get; set; }

		public List<string> GetLocations()
		{
			return this.Locations.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();
        }

		public XDestination() { }
		public XDestination(string Name,params string[] Locations)
		{
			this.Name = Name;
			this.Locations = Locations.Aggregate((a,b) => string.Format("{1}{0}{2}",Environment.NewLine,a,b));
		}
	}
}
