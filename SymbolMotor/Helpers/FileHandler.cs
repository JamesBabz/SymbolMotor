using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SymbolMotor.Helpers
{
    public static class FileHandler
    {
        /// <summary>
        ///     Serializes an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializableObject">The object to serialize</param>
        /// <param name="fileName">The name of the file where the serialized object should be saved to</param>
        public static void SerializeObject<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null) return;

            try
            {
                var xmlDocument = new XmlDocument();
                var serializer = new XmlSerializer(serializableObject.GetType());
                using (var stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: " + ex.Message);
            }
        }

        /// <summary>
        ///     Deserializes an xml file into an object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">The file to desrialize</param>
        /// <returns>The deserialized object</returns>
        public static T DeSerializeObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return default(T);

            var objectOut = default(T);
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);

                var xmlString = xmlDocument.OuterXml;
                using (var read = new StringReader(xmlString))
                {
                    var outType = typeof(T);
                    var serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: " + ex.Message);
            }
            return objectOut;
        }

        public static List<string[]> LoadFromTxt(string path)
        {
            var loadedTags = new List<string[]>();

            if (!File.Exists(path))
            {
                Console.WriteLine("File does not exist");
               return null;
            }
            using (TextReader tr = new StreamReader(path))
            {
                tr.ReadLine(); //Skips first row
                while (tr.Peek() >= 0)
                {
                    var line = tr.ReadLine();
                    if (line == null) continue;
                    var properties = line.Split(',').Select(p => p.Trim()).ToArray();
                    loadedTags.Add(properties);
                }
            }
            return loadedTags;
        }
    }
}
