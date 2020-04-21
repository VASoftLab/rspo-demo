using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Globalization;

using Kepware.ClientAce.OpcDaClient;

using OPCCore;

namespace RSPODemo
{
    public partial class FormMain : Form
    {
        ApplicationSettings appSettings;
        Dictionary<string, string> TAGVALUE;
        OPCControl _OPCControl;
        List<OPCTag> TAG;

        #region TAG Values Holder
        double _Functions_Ramp1 = 0.0;
        double _Functions_Ramp2 = 0.0;
        double _Functions_Ramp3 = 0.0;
        double _Functions_Ramp4 = 0.0;

        double _Functions_Random1 = 0.0;
        double _Functions_Random2 = 0.0;
        double _Functions_Random3 = 0.0;
        double _Functions_Random4 = 0.0;

        double _Functions_Sine1 = 0.0;
        double _Functions_Sine2 = 0.0;
        double _Functions_Sine3 = 0.0;
        double _Functions_Sine4 = 0.0;
        #endregion

        public FormMain()
        {
            InitializeComponent();
            appSettings = new ApplicationSettings();

            TAGVALUE = new Dictionary<string, string>();
            TAG = new List<OPCTag>();
            FillTAGList();

            _OPCControl = new OPCControl();
            _OPCControl.SubscribeToOPCDAServerEvents(DAServer_StateChanged, DAServer_DataChanged);
        }

        #region OPC SERVER METHODS
        public void DAServer_StateChanged(int clientHandle, ServerState state)
        {
            object[] SSCevHndArray = new object[2];
            SSCevHndArray[0] = clientHandle;
            SSCevHndArray[1] = state;
            BeginInvoke(new DaServerMgt.ServerStateChangedEventHandler(ServerStateChanged), SSCevHndArray);
        }
        public void DAServer_DataChanged(int clientSubscription, bool allQualitiesGood, bool noErrors, ItemValueCallback[] ItemValues)
        {
            object[] DCevHndlrArray = new object[4];
            DCevHndlrArray[0] = clientSubscription;
            DCevHndlrArray[1] = allQualitiesGood;
            DCevHndlrArray[2] = noErrors;
            DCevHndlrArray[3] = ItemValues;
            BeginInvoke(new DaServerMgt.DataChangedEventHandler(DataChanged), DCevHndlrArray);
        }
        public void ServerStateChanged(int clientHandle, ServerState state)
        {
            try
            {
                switch (state)
                {
                    case ServerState.ERRORSHUTDOWN:
                        toolStripStatusLabelOPCConnection.Text = "OPC: Server is shutting down"; // The server is shutting down
                        break;
                    case ServerState.ERRORWATCHDOG:
                        toolStripStatusLabelOPCConnection.Text = "OPC: Server connection has been lost"; // Server connection has been lost
                        break;
                    case ServerState.CONNECTED:
                        toolStripStatusLabelOPCConnection.Text = "OPC: Server is connected"; // Server is connected
                        break;
                    case ServerState.DISCONNECTED:
                        toolStripStatusLabelOPCConnection.Text = "OPC: Server is disconnected"; // Server is disconnected
                        break;
                }
            }
            catch { }
        }
        public void DataChanged(int clientSubscription, bool allQualitiesGood, bool noErrors, ItemValueCallback[] ItemValues)
        {
            DateTime itemTimeStamp = DateTime.Now;
            String itemName = String.Empty;

            foreach (ItemValueCallback item in ItemValues)
            {
                if (_OPCControl.IDTAG.ContainsKey((int)item.ClientHandle))
                {
                    itemName = _OPCControl.IDTAG[(int)item.ClientHandle];
                    if (TAGVALUE.ContainsKey(itemName))
                        TAGVALUE[itemName] = item.Value.ToString();

                    if (itemName == "Simulation Examples.Functions.Ramp1")
                    {
                        _Functions_Ramp1 = item.Value.ToString().ToDouble();
                        OPC1.RAMP = _Functions_Ramp1;
                    }
                    if (itemName == "Simulation Examples.Functions.Ramp2")
                    {
                        _Functions_Ramp2 = item.Value.ToString().ToDouble();
                        OPC2.RAMP = _Functions_Ramp2;
                    }
                    if (itemName == "Simulation Examples.Functions.Ramp3")
                    {
                        _Functions_Ramp3 = item.Value.ToString().ToDouble();
                        OPC3.RAMP = _Functions_Ramp3;
                    }
                    if (itemName == "Simulation Examples.Functions.Ramp4")
                    {
                        _Functions_Ramp4 = item.Value.ToString().ToDouble();
                        OPC4.RAMP = _Functions_Ramp4;
                    }

                    if (itemName == "Simulation Examples.Functions.Random1")
                    {
                        _Functions_Random1 = item.Value.ToString().ToDouble();
                        OPC1.RAND = _Functions_Random1;
                    }
                    if (itemName == "Simulation Examples.Functions.Random2")
                    {
                        _Functions_Random2 = item.Value.ToString().ToDouble();
                        OPC2.RAND = _Functions_Random2;
                    }
                    if (itemName == "Simulation Examples.Functions.Random3")
                    {
                        _Functions_Random3 = item.Value.ToString().ToDouble();
                        OPC3.RAND = _Functions_Random3;
                    }
                    if (itemName == "Simulation Examples.Functions.Random4")
                    {
                        _Functions_Random4 = item.Value.ToString().ToDouble();
                        OPC4.RAND = _Functions_Random4;
                    }

                    if (itemName == "Simulation Examples.Functions.Sine1")
                    {
                        _Functions_Sine1 = item.Value.ToString().ToDouble();
                        OPC1.SINE = _Functions_Sine1;
                    }
                    if (itemName == "Simulation Examples.Functions.Sine2")
                    {
                        _Functions_Sine2 = item.Value.ToString().ToDouble();
                        OPC2.SINE = _Functions_Sine2;
                    }
                    if (itemName == "Simulation Examples.Functions.Sine3")
                    {
                        _Functions_Sine3 = item.Value.ToString().ToDouble();
                        OPC3.SINE = _Functions_Sine3;
                    }
                    if (itemName == "Simulation Examples.Functions.Sine4")
                    {
                        _Functions_Sine4 = item.Value.ToString().ToDouble();
                        OPC4.SINE = _Functions_Sine4;
                    }
                }
            }
        }
        #endregion

        private void FillTAGList()
        {
            String[] TAG_LIST_WITH_TYPES;
            String fileTagList = "TagList.csv";
            if (System.IO.File.Exists(fileTagList) == true)
            {
                TAG_LIST_WITH_TYPES = System.IO.File.ReadAllLines(fileTagList);
                String[] data;
                foreach (String T in TAG_LIST_WITH_TYPES)
                {
                    data = T.Split(',');
                    if ((data[1].ToLower() == "double") || (data[1] == "System.Double"))
                        TAG.Add(new OPCTag() { Name = data[0], Type = OPCTagType.Double });
                    if ((data[1].ToLower() == "boolean") || (data[1] == "System.Boolean"))
                        TAG.Add(new OPCTag() { Name = data[0], Type = OPCTagType.Boolean });
                    if ((data[1].ToLower() == "string") || (data[1] == "System.String"))
                        TAG.Add(new OPCTag() { Name = data[0], Type = OPCTagType.String });
                    if ((data[1].ToLower() == "long") || (data[1] == "System.Long"))
                        TAG.Add(new OPCTag() { Name = data[0], Type = OPCTagType.Long });

                    TAGVALUE.Add(data[0], String.Empty);
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите закрыть приложение?", "Подтверждение выхода", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            
            OPC1.RAMP = Math.PI;
            OPC1.RAND = rnd.NextDouble();
            OPC1.SINE = Math.Sin(rnd.Next());

            OPC2.RAMP = Math.PI;
            OPC2.RAND = rnd.NextDouble();
            OPC2.SINE = Math.Sin(rnd.Next());

            OPC3.RAMP = Math.PI;
            OPC3.RAND = rnd.NextDouble();
            OPC3.SINE = Math.Sin(rnd.Next());

            OPC4.RAMP = Math.PI;
            OPC4.RAND = rnd.NextDouble();
            OPC4.SINE = Math.Sin(rnd.Next());
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            appSettings.Load();

            textBoxHostName.Text = appSettings.HostName;
            textBoxPortNumber.Text = appSettings.PortNumber.ToString();
            textBoxServerIdentifier.Text = appSettings.ServerIdentifier.ToString();

            OPC1.Initialization("OPC I", "Information");
            OPC2.Initialization("OPC II", "Information");
            OPC3.Initialization("OPC III", "Information");
            OPC4.Initialization("OPC IV", "Information");
        }

        private void buttonOPCSave_Click(object sender, EventArgs e)
        {
            appSettings.HostName = textBoxHostName.Text;

            int PortNumber = 0;
            Int32.TryParse(textBoxPortNumber.Text, out PortNumber);
            appSettings.PortNumber = PortNumber;

            appSettings.ServerIdentifier = textBoxServerIdentifier.Text;
            appSettings.Save();
        }

        private void buttonOPCConnect_Click(object sender, EventArgs e)
        {
            if (_OPCControl.DAServer.ServerState == ServerState.DISCONNECTED)
            {
                String UrlString = String.Format("opcda://{0}/{1}/", appSettings.HostName, appSettings.ServerIdentifier);
                _OPCControl.SetURL(UrlString);
                _OPCControl.ConnectOPCServer(true);

                if (_OPCControl.IsConnected == false)
                {
                    MessageBox.Show("OPC server connection problem!\nCheck OPC connection settings.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int subscribedCount = _OPCControl.SubscribeData(TAG);

                MessageBox.Show(String.Format("Subscribed {0} from {1} tags", subscribedCount, TAG.Count), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _OPCControl.ModifySubscription(true);
                _OPCControl.ReadSynchronously(TAGVALUE);
            }
            else
            {
                if (_OPCControl.DAServer.ServerState == ServerState.CONNECTED)
                {
                    if (MessageBox.Show("Are you sure you want to disconnect?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        try
                        {
                            if ((_OPCControl != null) && (_OPCControl.IsConnected == true))
                            {
                                _OPCControl.UnSubscribeData();
                                _OPCControl.ModifySubscription(true);
                                _OPCControl.ConnectOPCServer(false);
                            }
                        }
                        catch (Exception E)
                        {
                            String S = E.ToString(); // appLog.WriteLog(E.ToString());
                        }
                    }
                }
            }

            if (_OPCControl.DAServer.ServerState == ServerState.CONNECTED)
            {
                buttonOPCConnect.Text = "DISCONNECT";
            }
            else
            {
                buttonOPCConnect.Text = "CONNECT";
                MessageBox.Show("Disconnected from OPC server", "OPC server connection status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            appSettings.Save();

            if (_OPCControl.DAServer.ServerState == ServerState.CONNECTED)
            {
                try
                {
                    if ((_OPCControl != null) && (_OPCControl.IsConnected == true))
                    {
                        _OPCControl.UnSubscribeData();
                        _OPCControl.ModifySubscription(true);
                        _OPCControl.ConnectOPCServer(false);
                    }
                }
                catch { }
            }
        }
    }

    public static class StringConverterHelper
    {
        static readonly NumberFormatInfo provider = new NumberFormatInfo();
        static StringConverterHelper()
        {
            provider.NumberDecimalSeparator = ".";
        }
        public static int ToInteger(this string value)
        {
            int temp = 0;
            Int32.TryParse(value, out temp);
            return temp;
        }
        public static byte ToByte(this string value)
        {
            Byte temp = 0;
            Byte.TryParse(value, out temp);
            return temp;
        }
        public static UInt16 ToUInt16(this string value)
        {
            UInt16 temp = 0;
            UInt16.TryParse(value, out temp);
            return temp;
        }
        public static double ToDouble(this string value)
        {
            double temp = 0;
            Double.TryParse(value, out temp);
            return temp;
        }
        public static bool ToBoolean(this string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return false;
            }
            else
            {
                switch (value.ToLower())
                {
                    case "истина":
                        return true;
                    case "true":
                        return true;
                    case "t":
                        return true;
                    case "1":
                        return true;

                    case "ложь":
                        return false;
                    case "false":
                        return false;
                    case "f":
                        return false;
                    case "0":
                        return false;
                    default:
                        throw new InvalidCastException("You can't cast a weird value to a bool!");
                }
            }
        }
    }
}
