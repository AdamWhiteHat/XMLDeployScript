using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XMLDeploySimple
{
	public static class XML
	{
		public static string LineBreak = Environment.NewLine; //"&#xd;";
		//public static string[] LineBreaker = new string[] { LineBreak };
		public static string[] LineSplitter = new string[] { "\n" };

		public static void Serialize(string Filename, object obj)
		{
			if (Filename == null || Filename.Length < 1 || obj == null)
				return;

			using (StreamWriter streamWriter = new StreamWriter(Filename))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
				xmlSerializer.Serialize(streamWriter, obj);
			}
		}

		public static object DeSerialize(string Filename, Type type)
		{
			if (Filename == null || !File.Exists(Filename))
				return null;

			object result = null;
			using (TextReader reader = new StreamReader(Filename, Encoding.UTF8, true))	// StringReader(Filename))
			{
				XmlSerializer serializer = new XmlSerializer(type);
				result = serializer.Deserialize(reader);
			}
			return result;
		}
	}
}
