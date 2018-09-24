using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace RCP.Common.Tools
{
    public class Loader
    {
        private static readonly object s_syncRoot = new object();
        private static readonly string s_configurationDirectory = AppDomain.CurrentDomain.BaseDirectory +
                                                                  @"Config\";

        public static T Load<T>(string fileName, object syncObject = null)
        {
            T result = default(T);
            object syncRoot = (syncObject != null) ? syncObject : s_syncRoot;

            lock (syncRoot)
            {
                try
                {
                    result = LoadCore<T>(fileName);
                }
                catch (Exception e)
                {

                }
            }

            return result;
        }

        private static T LoadCore<T>(string filePath)
        {
            T result = default(T);
            if (File.Exists(filePath))
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    Type typeParameterType = typeof(T);
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    var reader = new XmlTextReader(stream);
                    result = (T)serializer.Deserialize(reader);
                }
            }
            return result;
        }

        public static bool Save<T>(string filename, T ob, object syncObject = null, bool ensureDirectory = true)
        {
            object syncRoot = (syncObject != null) ? syncObject : s_syncRoot;

            lock (syncRoot)
            {
                try
                {
                    if (ensureDirectory)
                        EnsureConfigurationDirectory();

                    using (FileStream fs = new FileStream(filename + ".bac", FileMode.Create, FileAccess.ReadWrite, FileShare.Read, 4096,
                        FileOptions.WriteThrough))
                    {
                        XmlSerializer ser = new XmlSerializer(typeof(T));

                        ser.Serialize(fs, ob);
                    }

                    if (File.Exists(filename))
                        File.Replace(filename + ".bac", filename, filename + ".old");
                    else
                        File.Copy(filename + ".bac", filename);

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        private static void EnsureConfigurationDirectory()
        {
            if (!Directory.Exists(s_configurationDirectory))
                Directory.CreateDirectory(s_configurationDirectory);
        }
    }
}
