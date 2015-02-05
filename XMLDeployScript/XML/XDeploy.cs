using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml.Resolvers;
using System.Xml.XmlConfiguration;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace XMLDeploySimple
{
	[XmlRoot("Deploy")]
    public class XDeploy
	{
		[XmlElement("Build")]
		public List<XBuild> Builds { get; set; }

		public XDeploy() { }
		public XDeploy(params XBuild[] Builds)
		{
			if (Builds == null || Builds.Length < 1)
				return;

			this.Builds = Builds.ToList();
		}
	}

	public class XBuild
	{
		[XmlElement("Source")]
		public List<XSource> Sources { get; set; }
		[XmlElement("Destination")]
		public List<XDestination> Destinations { get; set; }

		public XBuild() { }

		public XBuild(XSource Source, params XDestination[] Destinations)
		{
			if (Source == null || Destinations == null || Destinations.Length < 1)
				return;

			this.Sources = new List<XSource> { Source };
			this.Destinations = Destinations.ToList();
		}

		public XBuild(List<XSource> Sources, params XDestination[] Destinations)
		{
			if (Sources == null || Sources.Count < 1 || Destinations == null || Destinations.Length < 1)
				return;

			this.Sources = Sources;
			this.Destinations = Destinations.ToList();
		}
	}
}
