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
		public static bool SaveFile(string Filename, XDeploy DeployScript)
		{
			XML.Serialize(Filename, DeployScript);

			if (File.Exists(Filename))
				return true;
			else
				return false;
		}

		public static XDeploy LoadFile(string Filename)
		{
			XDeploy contents = (XDeploy)XML.DeSerialize(Filename,typeof(XDeploy));

			if (contents != null)
			{
				return contents;
			}

			return new XDeploy();
		}

		public static bool Deploy(string Filename)
		{
			XDeploy build = LoadFile(Filename);
			return Deploy(build);
		}

        public static bool Deploy(XDeploy Deploy)
		{
			if (Deploy == null || Deploy.Builds == null || Deploy.Builds.Count < 1)
				return false;

			List<string> deployedFiles = new List<string>();
			foreach (XBuild Build in Deploy.Builds)
			{
				if (Build == null || Build.Sources.Count < 1)
					continue;
				
				foreach (XSource source in Build.Sources)
				{
					string[] sourceLocations = source.Locations;

					if (sourceLocations == null || sourceLocations.Length < 1)
						continue;

					foreach (string sourcePath in sourceLocations)
					{
						string sourceFilename = Path.GetFileName(sourcePath);

						List<XDestination> sourceDestinations = Build.Destinations.Where(d => d.Name.Contains(source.Name)).ToList();
						List<string> destinationLocations = sourceDestinations.SelectMany( d => d.Locations ).ToList();

						foreach (string destinationPath in destinationLocations)
						{
							string destinationFilename =  Path.Combine(destinationPath, sourceFilename);
							File.Copy(sourcePath, destinationFilename, true);
							deployedFiles.Add(destinationFilename);
						}
					}
				}
			}

			if (deployedFiles.Count < 1)
				return false;

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
