using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XMLDeploySimple
{
	public static class XMLDeployScript
	{
		public static XBuild LoadFile(string Filename)
		{
			XDeploy contents = (XDeploy)XML.DeSerialize(Filename,typeof(XDeploy));

			if (contents != null)
			{
				if (contents.Builds != null)
				{
					return contents.Builds[0];
				}
			}

			return new XBuild();
		}

		public static bool DeployScript(string Filename)
		{
			XBuild build = LoadFile(Filename);

			if (build.Sources.Count < 1)
				return false;

			List<string> deployedFiles = new List<string>();
			foreach (XSource source in build.Sources)
			{
				string sourceFilename = Path.GetFileName(source.Location);

				List<XDestination> sourceDestinations = build.Destinations.Where(d => d.Name.Contains(source.Name)).ToList();
				List<string> locations = sourceDestinations.SelectMany(d => d.GetLocations() ).ToList();

				foreach (string directory in locations)
				{
					string destinationFilename =  Path.Combine(directory, sourceFilename);
					File.Copy(source.Location, destinationFilename,true);
					deployedFiles.Add(destinationFilename);
				}

			}

			bool allExist = true;
			foreach (string file in deployedFiles)
			{
				if (!File.Exists(file))
				{
					allExist = false;
				}
			}
			return allExist;
		}

	}
}
