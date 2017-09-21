using System;
using System.IO;
using System.Xml.Serialization;

namespace KeesTalksTech.Assessments.Wehkamp.Store.Utilities.Serialization
{
    public static class XmlSerializationHelper
    {
        /// <summary>
        /// Deserializes content from an XML file into a object.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="xmlFilePath">The file path.</param>
        /// <returns>The data.</returns>
        public static T DeserializeFromFile<T>(string xmlFilePath)
        {
            if(String.IsNullOrEmpty(xmlFilePath))
            {
                throw new ArgumentNullException(nameof(xmlFilePath));
            }

            if (!File.Exists(xmlFilePath))
            {
                throw new FileNotFoundException("XML File not found.", xmlFilePath);
            }

            //could add more exception handling for invalid XML files, bad content,
            //cast exceptions and stuff like that, but it would take the excersise 
            //way too far

            T result;

            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new FileStream(xmlFilePath, FileMode.Open, FileAccess.Read))
            {
                result = (T)serializer.Deserialize(stream);
            }

            return result;
        }

        /// <summary>
        /// Serializes the given object to file.
        /// </summary>
        /// <param name="xmlFilePath">The XML file path.</param>
        /// <param name="obj">The object.</param>
        public static void SerializeToFile(string xmlFilePath, object obj)
        {
            if (String.IsNullOrEmpty(xmlFilePath))
            {
                throw new ArgumentNullException(nameof(xmlFilePath));
            }

            FileStream stream;

            if (!File.Exists(xmlFilePath))
            {
                stream =  File.Create(xmlFilePath);
            }
            else
            {
                stream = new FileStream(xmlFilePath, FileMode.Truncate);
            }

            var serializer = new XmlSerializer(obj.GetType());
            using (stream)
            {
                serializer.Serialize(stream, obj);
            }
        }
    }
}
