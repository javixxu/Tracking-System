using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TrackerG5
{
    class Tracker
    {
        private static Tracker instance;
        string idUser;
        string idSession;
        string idUserNameLocation= "../Tracking System/Assets/Scripts/TRACKERG5/Data/ID_USER_TRACKER";
        string resultLocation= "../Tracking System/Assets/Scripts/TRACKERG5/Data/RESULT";

        const int size = 7;

        IPersistence persistence;
        ISerializer serializer;
        HashSet<ITrackerAsset> assets = new HashSet<ITrackerAsset>();//lista de assets
  
        public enum serializeType { Json};
        public enum persistenceType { Disc };

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
            if(!File.Exists(idUserNameLocation))
            {
                File.WriteAllText(idUserNameLocation, CreateHashID(DateTime.Now.ToString()+new Random().Next()));
            }

            return File.ReadAllText(idUserNameLocation);
        }


        public void AddEvent(TrackerEvent e)
        {
            e.Id = CreateHashID(idUser + DateTime.Now.ToString());
            e.IdUser = idUser;
            e.IdSession = idSession;
            e.Timestamp = DateTime.Now;

            persistence.Send(e);
        }

        public void Init(serializeType sT, persistenceType pT) 
        { 
            idUser = GetUserID();
            idSession = CreateHashID(idUser+DateTime.Now.ToString() + "tracker");
            
            Console.WriteLine("USER ID: " + idUser + " SESSION ID: " + idSession);
            //evento de inicio de sesion
            
            switch(sT)
            {
                case serializeType.Json:
                    serializer = new JsonSerializer();
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

            AddEvent(new LoginEvent());

        }

        public void End()
        {
            //evento de fin de inicio de sesion
            AddEvent(new LogoutEvent());
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
    }
}
