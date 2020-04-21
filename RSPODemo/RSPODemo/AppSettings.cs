using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;

using OPCCore;

namespace RSPODemo
{
    [Serializable]
    public class ApplicationSettings
    {
        [XmlElement("OPCSpecification")]
        public OPCSpecificationType OPCSpecification { get; set; }

        [XmlElement("HostName")]
        public String HostName { get; set; }

        [XmlElement("PortNumber")]
        public Int32 PortNumber { get; set; }

        [XmlElement("ServerIdentifier")]
        public String ServerIdentifier { get; set; }

        [XmlElement("ConnectionStringOPC")]
        public String ConnectionStringOPC { get; set; }

        [XmlElement("ConnectionStringDB")]
        public String ConnectionStringDB { get; set; }

        [XmlElement("TimerInterval")]
        public Int32 TimerInterval { get; set; }

        public ApplicationSettings()
        {
            OPCSpecification = OPCSpecificationType.OPCDA;
            HostName = "localhost";
            PortNumber = 49320;
            ServerIdentifier = "Kepware.KEPServerEX.V6";
            ConnectionStringOPC = "opcda://localhost/Kepware.KEPServerEX.V6/";
            ConnectionStringDB = String.Empty;
            TimerInterval = 1000;
        }

        public String SerializeToString()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
                StringWriter textWriter = new StringWriter();

                xmlSerializer.Serialize(textWriter, this);
                return textWriter.ToString();
            }
            catch (Exception E)
            {
                return String.Empty;
            }   
        }

        public void DeserializeFromString(String stringData)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
                using (TextReader reader = new StringReader(stringData))
                {
                    ApplicationSettings temp = (ApplicationSettings)xmlSerializer.Deserialize(reader);

                    this.OPCSpecification = temp.OPCSpecification;
                    this.HostName = temp.HostName;
                    this.PortNumber = temp.PortNumber;
                    this.ServerIdentifier = temp.ServerIdentifier;
                    this.ConnectionStringOPC = temp.ConnectionStringOPC;
                    this.ConnectionStringDB = temp.ConnectionStringDB;
                    this.TimerInterval = temp.TimerInterval;
                }
            }
            catch (Exception E)
            {
                ;
            }
        }

        public void Load()
        {
            String configFile = Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), String.Format(@"{0}.config", Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location))));
            if (File.Exists(configFile))
            {
                try
                {
                    String serializedString = System.IO.File.ReadAllText(configFile);
                    this.DeserializeFromString(serializedString);
                }
                catch (Exception E)
                {
                    ;
                }
            }
        }

        public void Save()
        {
            try
            {
                String configFile = Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), String.Format(@"{0}.config", Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location))));

                String serializedString = this.SerializeToString();
                System.IO.File.WriteAllText(configFile, serializedString);
            }
            catch (Exception E)
            {
                ;
            }
        }
    }
}
