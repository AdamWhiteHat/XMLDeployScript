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
			this.Sources = new List<XSource> { Source };
			this.Destinations = Destinations.ToList();
		}
	}
}
