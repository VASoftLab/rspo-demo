using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kepware.ClientAce.OpcDaClient;

namespace OPCCore
{
    public class OPCControl
    {
        private string URL;
        private int KeepAliveTime;
        private bool RetryAfterConnectionError;
        private bool RetryInitialConnection;
        private string ClientName = "OPCClient";

        public DaServerMgt DAServer = new DaServerMgt();
        private ConnectInfo connectInfo = new ConnectInfo();
        private Boolean isOPCConnectionFiled = false;

        private ItemIdentifier[] itemIdentifiers;

        private int clientHandle = 1;
        private int clientSubscription = 1;
        private int serverSubscription = 0;

        public int UpdateRate = 1000;

        public Dictionary<int, string> IDTAG;

        public bool IsConnected { get { return DAServer.IsConnected; } }

        public OPCControl()
        {
            KeepAliveTime = 1000;
            RetryAfterConnectionError = true;
            RetryInitialConnection = false;
            ClientName = "OPCClient";
            URL = "opcda://localhost/Kepware.KEPServerEX.V6/";
        }

        public OPCControl(int KeepAliveTime, bool RetryAfterConnectionError, bool RetryInitialConnection, string ClientName, string URL)
        {
            this.KeepAliveTime = KeepAliveTime;
            this.RetryAfterConnectionError = RetryAfterConnectionError;
            this.RetryInitialConnection = RetryInitialConnection;
            this.ClientName = ClientName;
            this.URL = URL;
        }
        public void ConnectOPCServer(bool connectToOPC)
        {
            if (connectToOPC)
            {
                connectInfo.LocalId = "en";
                connectInfo.KeepAliveTime = KeepAliveTime;
                connectInfo.RetryAfterConnectionError = RetryAfterConnectionError;
                connectInfo.RetryInitialConnection = RetryInitialConnection;
                connectInfo.ClientName = ClientName;

                try
                {
                    if (DAServer.IsConnected == false)
                        DAServer.Connect(URL, clientHandle, ref connectInfo, out isOPCConnectionFiled);
                }
                catch { }
            }
            else
            {
                try
                {
                    if (DAServer.IsConnected == true)
                        DAServer.Disconnect();
                }
                catch { }
            }
        }
        public int SubscribeData(List<OPCTag> TAG)
        {
            bool active = false;
            int updateRate = UpdateRate;
            Single deadBand = 0;
            int revisedUpdateRate;

            int clientHandleUNQ = 0;
            int index = 0;

            itemIdentifiers = new ItemIdentifier[TAG.Count];

            if ((IDTAG != null) && (IDTAG.Count > 0))
                IDTAG.Clear();
            else
                IDTAG = new Dictionary<int, string>();

            foreach (OPCTag tag in TAG)
            {
                itemIdentifiers[index] = new ItemIdentifier();
                itemIdentifiers[index].ClientHandle = clientHandleUNQ;
                itemIdentifiers[index].DataType = Type.GetType(tag.Type.ToString());
                itemIdentifiers[index].ItemName = tag.Name;
                IDTAG.Add(clientHandleUNQ, tag.Name);
                index++;
                clientHandleUNQ++;
            }

            try
            {
                DAServer.Subscribe(clientSubscription, active, updateRate, out revisedUpdateRate, deadBand, ref itemIdentifiers, out serverSubscription);
                int faultCounter = 0;
                for (int itemIndex = 0; itemIndex < IDTAG.Count; itemIndex++)
                {
                    if (itemIdentifiers[itemIndex].ResultID.Succeeded == false)
                        faultCounter++;
                }
                return IDTAG.Count - faultCounter;
            }
            catch { return 0; }
        }
        public void UnSubscribeData()
        {
            try
            {
                if ((serverSubscription != 0) && (itemIdentifiers.Count() > 0))
                {
                    DAServer.SubscriptionRemoveItems(serverSubscription, ref itemIdentifiers);
                    IDTAG.Clear();
                }
            }
            catch { };
        }
        public void ModifySubscription(bool action)
        {
            DAServer.SubscriptionModify(serverSubscription, action);
        }
        public void SubscribeToOPCDAServerEvents(DaServerMgt.ServerStateChangedEventHandler DAServer_StateChanged, DaServerMgt.DataChangedEventHandler DAServer_DataChanged)
        {
            // DAServer.ReadCompleted += new Kepware.ClientAce.OpcDaClient.DaServerMgt.ReadCompletedEventHandler(DAServer_ReadCompleted);
            // DAServer.WriteCompleted += new Kepware.ClientAce.OpcDaClient.DaServerMgt.ReadCompletedEventHandler(DAServer_WriteCompleted);
            DAServer.ServerStateChanged += new Kepware.ClientAce.OpcDaClient.DaServerMgt.ServerStateChangedEventHandler(DAServer_StateChanged);
            DAServer.DataChanged += new DaServerMgt.DataChangedEventHandler(DAServer_DataChanged);
        }

        public void ReadSynchronously(Dictionary<string, string> TAGVALUE)
        {
            if (DAServer.ServerState == ServerState.CONNECTED)
            {
                DateTime itemTimeStamp = DateTime.Now;
                ItemIdentifier[] OPCItems = new ItemIdentifier[IDTAG.Count];

                for (int i = 0; i < IDTAG.Count; i++)
                {
                    OPCItems[i] = new ItemIdentifier();
                    OPCItems[i].ItemName = IDTAG[i];
                    OPCItems[i].ClientHandle = IDTAG.ElementAt(i).Key;
                }

                int maxAge = 0;
                ItemValue[] OPCItemValues = null;
                DAServer.Read(maxAge, ref OPCItems, out OPCItemValues);

                if (OPCItemValues == null)
                    return;

                String itemName = String.Empty;

                for (int i = 0; i < OPCItems.Length; i++)
                {
                    if ((OPCItems[i] == null) || (OPCItemValues[i] == null))
                        continue;

                    if (OPCItems[i].ResultID.Succeeded & OPCItemValues[i].Quality.IsGood)
                    {
                        itemName = OPCItems[i].ItemName;
                        ///////////////////////////////////////////////////////
                        // TODO ALL TAGS SHOULD BE TRANSFORMED TO UPPER
                        if (itemName.ToUpper() == "PlantDO.PLCB.Common.IsDOMasterInOperation".ToUpper())
                            itemName = "PlantDO.PLCB.Common.IsDOMasterInOperation".ToUpper();
                        ///////////////////////////////////////////////////////
                        if (TAGVALUE.ContainsKey(itemName))
                            TAGVALUE[itemName] = OPCItemValues[i].Value.ToString();
                    }
                }
            }
        }

        public ReturnCode WriteSingleTagValue(String TagName, String TagValue)
        {
            if (DAServer.ServerState == ServerState.CONNECTED)
            {
                int clientHandle = 1;
                int transactionHandle = 1;

                ItemIdentifier[] itemIdentifiers = new ItemIdentifier[1];
                ItemValue[] itemValues = new ItemValue[1];

                itemIdentifiers[0] = new ItemIdentifier();
                itemIdentifiers[0].ItemName = TagName;
                itemIdentifiers[0].ClientHandle = clientHandle;

                itemValues[0] = new ItemValue();
                itemValues[0].Value = TagValue;

                try
                {
                    return DAServer.WriteAsync(transactionHandle, ref itemIdentifiers, itemValues);
                }
                catch { return ReturnCode.ITEMERROR; }
            }
            else
                return ReturnCode.ITEMERROR;
        }

        public void WriteAsync(Dictionary<string, string> TAG_TO_OPC)
        {
            ItemIdentifier[] itemIdentifiersLocalDO = new ItemIdentifier[TAG_TO_OPC.Count];
            ItemValue[] itemValuesDO = new ItemValue[TAG_TO_OPC.Count];
            int clientHandleDO = 0;
            Random rnd = new Random();
            int transactionHandleUNQ = rnd.Next();

            for (int ind = 0; ind < TAG_TO_OPC.Count; ind++)
            {
                itemIdentifiersLocalDO[ind] = new ItemIdentifier();
                itemIdentifiersLocalDO[ind].ItemName = TAG_TO_OPC.ElementAt(ind).Key;
                itemIdentifiersLocalDO[ind].ClientHandle = clientHandleDO++;
                itemValuesDO[ind] = new ItemValue();
                itemValuesDO[ind].Value = TAG_TO_OPC.ElementAt(ind).Value;
            }

            try
            {
                ReturnCode returnCode = DAServer.WriteAsync(transactionHandleUNQ, ref itemIdentifiersLocalDO, itemValuesDO);
            }
            catch { }
        }

        public void SetURL(String URL)
        {
            this.URL = URL;
        }

        public bool TestConnection()
        {
            bool result = false;
            if (String.IsNullOrEmpty(URL))
                return false;
            try
            {
                Kepware.ClientAce.OpcDaClient.ConnectInfo connectInfo = new Kepware.ClientAce.OpcDaClient.ConnectInfo();
                Kepware.ClientAce.OpcDaClient.DaServerMgt DAserver = new Kepware.ClientAce.OpcDaClient.DaServerMgt();
                Boolean connectFailed = false;

                connectInfo.LocalId = "en";
                connectInfo.KeepAliveTime = KeepAliveTime;
                connectInfo.RetryAfterConnectionError = RetryAfterConnectionError;
                connectInfo.RetryInitialConnection = RetryInitialConnection;
                connectInfo.ClientName = "OPCClient";

                Int32 clientHandle = 1;

                DAserver.Connect(URL, clientHandle, ref connectInfo, out connectFailed);
                result = !connectFailed;
                DAserver.Disconnect();
                return result;
            }
            catch { return false; }
        }
    }
}
