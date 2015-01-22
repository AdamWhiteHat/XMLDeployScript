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
		public static void Serialize(string Filename, object obj)
		{
			using (StreamWriter streamWriter = new StreamWriter(Filename))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
				xmlSerializer.Serialize(streamWriter, obj);
			}
		}

		public static object DeSerialize(string Filename, Type type)
		{
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
