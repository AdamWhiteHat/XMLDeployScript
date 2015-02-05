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
		[XmlElement("Locations")]
		public string _locations { get; set; }
		[XmlIgnore]
		public string[] Locations
		{
			get
			{
				return GetLocations();
			}
			set
			{
				SetLocations(value);
            }
		}

		private string[] GetLocations()
		{
			if (_locations == null || _locations.Length < 1)
				return new string[] { };

			return _locations.Split(XML.LineSplitter, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();
        }

		private void SetLocations(string[] Locations)
		{
			if (Locations == null || Locations.Length < 1)
				return;

			_locations = Locations.Aggregate((a, b) => string.Format("{1}{0}{2}", XML.LineBreak, a, b));
		}

		public XDestination() { }
		public XDestination(string Name,params string[] Locations)
		{
			this.Name = Name;
			SetLocations(Locations);
		}
	}
}
