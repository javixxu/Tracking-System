using Assets.Scripts.TRACKERG5;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace TrackerG5
{
    class Tracker
    {
        private static Tracker instance;
        string idUser;
        string idSession;
        string resultLocation = "";

        const int size = 7;

        IPersistence persistence;
        ISerializer serializer;
        HashSet<ITrackerAsset> assets = new HashSet<ITrackerAsset>();//lista de assets

        public enum serializeType { Json, Csv, Yaml };
        public enum persistenceType { Disc };
        public enum eventType { StartGame, Endgame, Death, LoseShield };

        Tracker() { }
        public static Tracker Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Tracker();
                }
                return instance;
            }
        }

        private string GetUserID()
        {
            string machineName = Environment.MachineName;
            string macAddress = GetMacAddress();
            string nameUser = machineName + macAddress;

            SHA256 sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(nameUser));

            return new Guid(hashBytes.Take(16).ToArray()).ToString("N");
        }

        private string GetMacAddress()
        {
            string macAddress = NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault();

            if (string.IsNullOrEmpty(macAddress))
                macAddress = "00:00:00:00:00:00";

            return macAddress;
        }

        public void AddEvent(eventType eventT, Dictionary<string, string> eventParameters = null)
        {

            TrackerEvent e = null;
            switch (eventT)
            {
                case eventType.StartGame:
                    e = new StartGameEvent();
                    break;
                case eventType.Endgame:
                    e = new EndGameEvent();
                    break;
                case eventType.Death:
                    e = new DeathEvent();
                    break;
                case eventType.LoseShield:
                    e = new LoseShieldEvent();
                    break;
                default:
                    break;
            }
            e.Id = CreateHashID(idUser + DateTime.Now.ToString());
            e.IdUser = idUser;
            e.IdSession = idSession;
            e.Timestamp = getTimeStamp();
            e.SetParamns(eventParameters);

            persistence.Send(e);
        }

        public void Init(serializeType sT, persistenceType pT)
        {
            idUser = GetUserID();
            idSession = CreateHashID(idUser + DateTime.Now.ToString() + "tracker");

            Console.WriteLine("USER ID: " + idUser + " SESSION ID: " + idSession);
            //evento de inicio de sesion
            switch (sT)
            {
                case serializeType.Json:
                    serializer = new JsonSerializer();
                    resultLocation = "../Tracking System/Assets/Scripts/TRACKERG5/Data/RESULT.json";
                    break;
                case serializeType.Csv:
                    serializer = new CsvSerializer();
                    resultLocation = "../Tracking System/Assets/Scripts/TRACKERG5/Data/RESULT.csv";
                    break;
                case serializeType.Yaml:
                    serializer = new YamlSerializer();
                    resultLocation = "../Tracking System/Assets/Scripts/TRACKERG5/Data/RESULT.yaml";
                    break;
                default:
                    throw new Exception("Serializacion no valida");
            }

            switch (pT)
            {
                case persistenceType.Disc:
                    persistence = new FilePersistence(serializer, resultLocation, size);
                    break;
                default:
                    throw new Exception("Persistencia no valida");

            };

            LoginEvent e = new LoginEvent();
            e.Id = CreateHashID(idUser + DateTime.Now.ToString());
            e.IdUser = idUser;
            e.IdSession = idSession;
            e.Timestamp = getTimeStamp();

            persistence.Send(e);

        }

        public void End()
        {
            //evento de fin de inicio de sesion
            LogoutEvent e = new LogoutEvent();
            e.Id = CreateHashID(idUser + DateTime.Now.ToString());
            e.IdUser = idUser;
            e.IdSession = idSession;
            e.Timestamp = getTimeStamp();

            persistence.Send(e);

            persistence.EndSession();
        }

        private string CreateHashID(string blockchain)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(blockchain);
            bytes = sha256.ComputeHash(bytes);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }

        private long getTimeStamp()
        {
            return (DateTime.Now.Ticks - new DateTime(1970, 1, 1).Ticks) / TimeSpan.TicksPerMillisecond;
        }
    }
}
