using System;

namespace TrackerG5
{
    class Tracker
    {
        private static Tracker instance;

        uint idUser;
        uint idSession;

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


        public void Init() 
        { 
            //el id del usuario
            /*
             * if(archivoExiste) 
             *  id = load(archivo)
             * else
             *  id = generateHashUsuario
             * */
            //el id de sesion

            //evento de inicio de sesion
            
        }

        public void End()
        {

            //evento de fin de inicio de sesion

            //guarda el id del usuario en disco
        }
    }
}
