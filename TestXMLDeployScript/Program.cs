using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLDeploySimple;
using System.Reflection;
using System.Reflection.Emit;
using System.Dynamic;
using System.IO;
using System.Threading;

namespace TestXMLDeployScript
{
	internal sealed class Program
	{
		private static void Main(string[] args)
		{
			TestXMLDeploy();
			WaitCountdown(5);
        }

		private static void TestXMLDeploy()
		{
			string filename = "XMLDeployScript.xml";

			if (CreateXML(filename))
				Console.WriteLine("Build generated file success");
			else
			{
				Console.WriteLine("Build generated file Fail");
				return;
			}

			if (XMLDeployScript.DeployScript(filename))
				Console.WriteLine("Deploy success");
			else
			{
				Console.WriteLine("Deploy fail");
				return;
			}

			Console.WriteLine("Deploy.Console passed both tests!");
		}

		private static bool CreateXML(string Filename)
		{
			string buildName = "TestDLL";

			XSource testDLL = new XSource(buildName, @"C:\Users\Adam\AppData\Local\Temp\Deploy\Test.dll");

			List <XDestination> destinations = new List<XDestination>();
			destinations.Add(
				new XDestination(
					buildName, 
					string.Format(
						"{0}\t\t\tC:\\Users\\Adam\\AppData\\Local\\Temp\\Deploy\\Import{0}" +
						"\t\t\tC:\\Users\\Adam\\AppData\\Local\\Temp\\Deploy\\Fix{0}" +
						"\t\t\tC:\\Users\\Adam\\AppData\\Local\\Temp\\Deploy\\Make{0}" +
						"\t\t\tC:\\Users\\Adam\\AppData\\Local\\Temp\\Deploy\\Submit{0}" +
						"\t\t\tC:\\Users\\Adam\\AppData\\Local\\Temp\\Deploy\\Fulfillment{0}" +
						"\t\t\tC:\\Users\\Adam\\AppData\\Local\\Temp\\Deploy\\Preprocess     {0}\t\t",
					Environment.NewLine)
				)
			);

			XDeploy ilinkIntergration = new XDeploy(
					new XBuild(testDLL, destinations.ToArray())
				);

			XML.Serialize(Filename, ilinkIntergration);

			if (File.Exists(Filename))
				return true;

			return false;
        }

		private static void WaitCountdown(int Seconds = 5)
		{
			if (Seconds > 9) Seconds = 9;

			Console.WriteLine(Environment.NewLine);
			Console.Write("The application will now terminate...   ");

			for (int counter = Seconds * 10; counter > 0; counter--)
			{
				if (Console.KeyAvailable)
					return;

				if (counter % 10 == 0)
					Console.Write("\b{0}", (counter / 10));

				Thread.Sleep(100);
			}
		}
	}
}
