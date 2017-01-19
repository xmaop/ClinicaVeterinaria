using System;
using System.Text;
using System.IO;
using System.Xml;	
using System.Collections;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace PetCenter.ExceptionManagement
{
	public class ExceptionXMLPublisher : IExceptionXmlPublisher
	{
		void IExceptionXmlPublisher.Publish(XmlDocument ExceptionInfo, NameValueCollection ConfigSettings)
		{
			string filename;
			if (ConfigSettings != null)
			{
				filename = ConfigSettings["fileName"];
			}
			else
			{
				filename = @"C:\PetCenter.Log.Xml";
			}

			XmlDocument logDoc = new XmlDocument();

			//Create the file if it doesn't exist.
			if (File.Exists(filename) == false)
			{
				using(FileStream f = File.Open(filename,FileMode.Create,FileAccess.Write))
				{
					using(StreamWriter sw = new StreamWriter(f))
					{
						sw.Write("<?xml version='1.0'?><Exceptions></Exceptions>");
						sw.Close();
						f.Close();
					}
				}
			}

			//Add the XML information to the file
			logDoc.Load(filename);
			XmlNode xNode = logDoc.ImportNode(ExceptionInfo.DocumentElement, true);
			logDoc.DocumentElement.AppendChild(xNode);
			File.Delete(filename);
			logDoc.Save(filename);
		}
	}
}