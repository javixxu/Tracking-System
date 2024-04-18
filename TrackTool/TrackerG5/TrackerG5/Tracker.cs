using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
namespace TrackerG5
{
    class Tracker
    {
        private static Tracker instance;


        string idUser;
        string idSession;
        string idUserNameLocation="../ID_USER_TRACKER";

        const int size = 7;


        HashSet<ITrackerAsset> assets = new HashSet<ITrackerAsset>();//lista de assets
        List<TrackerEvent> events = new List<TrackerEvent>();

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

        public TrackerEvent GetNextEvent()
        {
            if (events.Count > 0)
            {
                TrackerEvent e = events[0];
                events.RemoveAt(0);
                return e;
            }
     
            return null;
        }
        public void AddEvent(TrackerEvent e)
        {
            events.Add(e);
            if(events.Count >= size) 
            { 


            }
        }
        public void Init() 
        { 
            idUser = GetUserID();
            idSession = CreateHashID(idUser+DateTime.Now.ToString());
            
            Console.WriteLine("USER ID: " + idUser + " SESSION ID: " + idSession);
            //evento de inicio de sesion
            

            //crear JSON
       
        }

        public void End()
        {

            //evento de fin de inicio de sesion

            //guarda el id del usuario en disco
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
