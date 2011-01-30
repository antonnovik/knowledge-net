using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace Knowledge.KIF.Converter.util {

    public class IOUtils {

        public static XmlDocument readXml(string fileName) {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(fileName, settings);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            return doc;
        }

        public static string readText(string fileName) {
            string result = null;
            using (StreamReader sr = new StreamReader(fileName))
            {
                result = sr.ReadToEnd();
                sr.Close();
            }
            return result;
        }

        public static string validatePath(string path) {
            string result = StringUtils.EMPTY_STRING;
            try {
                new FileInfo(path);
            } catch (Exception ex) {
                result = ex.Message;
            }
            return result;
        }

        public static void createOrRewriteFile(string filePath) {
            File.Create(filePath).Close();//TODO: need an sol
        }

        public static bool isPathExists(string filePath) {
            return File.Exists(filePath);
        }

    }
}
