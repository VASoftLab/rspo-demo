using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SQLite;

using System.Globalization;

using Kepware.ClientAce.OpcDaClient;

using OPCCore;

namespace RSPODemo
{
    public partial class FormMain : Form
    {
        ApplicationSettings appSettings; // Объект для хранения настроек приложения        
        OPCControl _OPCControl; // Объект дла работы с ОРС сервером
        List<OPCTag> TAG; // Список объектов типа OPCTag { Имя_тега, Тип_данных }
        
        private string _PathToDB = "dbmonitoring.db";
        private string _ConnectionString;
        private SQLiteConnection _SqliteConnection;

        #region TAG Values Holder - Глобальные переменные для хранения текущих значений тегов
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
            // Создаем экзепляр объекта для работы с настройками приложения
            appSettings = new ApplicationSettings();
            // Создаем список объектов типа OPCTag и заполняем его из файла TagList.csv
            TAG = new List<OPCTag>();
            FillTAGList();

            // Создаем объект для работы с ОРС сервером
            _OPCControl = new OPCControl();
            // Подписываемся на события от сервера
            // Изменение состояния сервера и Изменение состояния данных
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
                    
                    if (itemName == "Simulation Examples.Functions.Ramp1")
                    {
                        // Запоминаем текущее значение в глобальной переменной
                        _Functions_Ramp1 = item.Value.ToString().ToDouble();
                        // Передаем текущее значение для отображения на пользовательском компоненте
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
            String fileTagList = "TagList.csv"; // Имя файла с информацией о тегах
            if (System.IO.File.Exists(fileTagList) == true) // Проверка на существование файла
            {
                // Считываем содержимое файла в массив string[]
                TAG_LIST_WITH_TYPES = System.IO.File.ReadAllLines(fileTagList);
                String[] data; // Буферный массив
                foreach (String T in TAG_LIST_WITH_TYPES)
                {
                    data = T.Split(','); // Разделяем компоненты строки разделенные запятыми на отдельные компоненты массива
                    if ((data[1].ToLower() == "double") || (data[1] == "System.Double"))
                        TAG.Add(new OPCTag() { Name = data[0], Type = OPCTagType.Double });
                    if ((data[1].ToLower() == "boolean") || (data[1] == "System.Boolean"))
                        TAG.Add(new OPCTag() { Name = data[0], Type = OPCTagType.Boolean });
                    if ((data[1].ToLower() == "string") || (data[1] == "System.String"))
                        TAG.Add(new OPCTag() { Name = data[0], Type = OPCTagType.String });
                    if ((data[1].ToLower() == "long") || (data[1] == "System.Long"))
                        TAG.Add(new OPCTag() { Name = data[0], Type = OPCTagType.Long });
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

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Загрузка настроек из файла
            appSettings.Load();

            // Отображаем текущие настройка на форме
            textBoxHostName.Text = appSettings.HostName;
            textBoxPortNumber.Text = appSettings.PortNumber.ToString();
            textBoxServerIdentifier.Text = appSettings.ServerIdentifier.ToString();

            // Инициализация пользовательских компонент
            OPC1.Initialization("OPC I", "Information");
            OPC2.Initialization("OPC II", "Information");
            OPC3.Initialization("OPC III", "Information");
            OPC4.Initialization("OPC IV", "Information");

            _ConnectionString = String.Format("Data Source={0};Version=3;", _PathToDB);
            _SqliteConnection = new SQLiteConnection(_ConnectionString);
            _SqliteConnection.Open();
        }

        private void buttonOPCSave_Click(object sender, EventArgs e)
        {
            // Сохранение текущих настроек в файл
            appSettings.HostName = textBoxHostName.Text;

            int PortNumber = 0;
            Int32.TryParse(textBoxPortNumber.Text, out PortNumber);
            appSettings.PortNumber = PortNumber;

            appSettings.ServerIdentifier = textBoxServerIdentifier.Text;
            appSettings.Save();
        }

        private void buttonOPCConnect_Click(object sender, EventArgs e)
        {
            // Соединение с сервером
            if (_OPCControl.DAServer.ServerState == ServerState.DISCONNECTED)
            {
                // Connection string - адрес сервера для подключения
                String UrlString = String.Format("opcda://{0}/{1}/", appSettings.HostName, appSettings.ServerIdentifier);
                // Установка адреса
                _OPCControl.SetURL(UrlString);
                // Соединение с сервером - true - команда установки соединения
                _OPCControl.ConnectOPCServer(true);

                // Проверяем, удалось ли установить соединение
                if (_OPCControl.IsConnected == false)
                {
                    MessageBox.Show("OPC server connection problem!\nCheck OPC connection settings.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Определяем кол-во тегов, на которые удалось подписаться
                int subscribedCount = _OPCControl.SubscribeData(TAG);

                // Вывод диагностического сообщения
                MessageBox.Show(String.Format("Subscribed {0} from {1} tags", subscribedCount, TAG.Count), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Устанавливаем признак изменения состояния подписки
                _OPCControl.ModifySubscription(true);
                // Асинхронное чтение - не используется
                //_OPCControl.ReadSynchronously(TAGVALUE);
            }
            else
            {
                // Отключение от сервера
                if (_OPCControl.DAServer.ServerState == ServerState.CONNECTED)
                {
                    // Получаем подтверждение отключения от сервера
                    if (MessageBox.Show("Are you sure you want to disconnect?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        try
                        {
                            if ((_OPCControl != null) && (_OPCControl.IsConnected == true))
                            {
                                // Отписываемся от данных
                                _OPCControl.UnSubscribeData();
                                // Выставляем флаг изменения подписка
                                _OPCControl.ModifySubscription(true);
                                // Разрываем соединение - false - команда на отключение от сервера
                                _OPCControl.ConnectOPCServer(false);
                            }
                        }
                        catch (Exception E) // Обработчик исключительной ситуации
                        {
                            String S = E.ToString(); // appLog.WriteLog(E.ToString());
                        }
                    }
                }
            }
            // Обновляем информационное сообщение в строке статуса
            if (_OPCControl.DAServer.ServerState == ServerState.CONNECTED)
            {
                buttonOPCConnect.Text = "DISCONNECT";
                // timerMain.Enabled = true;
                timerMain.Start();
            }
            else
            {
                buttonOPCConnect.Text = "CONNECT";
                // timerMain.Enabled = false;
                timerMain.Stop();
                MessageBox.Show("Disconnected from OPC server", "OPC server connection status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Сохраняем настройки в файл
            appSettings.Save();

            if (_SqliteConnection.State == ConnectionState.Open)
                _SqliteConnection.Close();

            // Проверяем, установлено ли соединение с сервером, если установлено - разрываем соединение перед закрытием формы
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

        private void timerMain_Tick(object sender, EventArgs e)
        {

            Application.DoEvents();

            if (_SqliteConnection.State == ConnectionState.Closed)
                return;

            Int32 _DateUnix = (Int32)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            try
            {
                using (SQLiteCommand sqliteCommand = _SqliteConnection.CreateCommand())
                {
                    sqliteCommand.CommandText = "INSERT INTO [tblData] (TimeStamp, Ramp1, Random1, Sine1) VALUES (@pTimeStamp, @pRamp1, @pRandom1, @pSine1)";
                    sqliteCommand.CommandType = CommandType.Text;
                    sqliteCommand.Parameters.Add(new SQLiteParameter("@pTimeStamp", _DateUnix));
                    sqliteCommand.Parameters.Add(new SQLiteParameter("@pRamp1", _Functions_Ramp1));
                    sqliteCommand.Parameters.Add(new SQLiteParameter("@pRandom1", _Functions_Random1));
                    sqliteCommand.Parameters.Add(new SQLiteParameter("@pSine1", _Functions_Sine1));

                    sqliteCommand.ExecuteNonQuery();
                }
            }
            catch { }
            

            Application.DoEvents();
        }
    }
    // Вспомогательные клас для преобразования строки в различные типы данных
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
