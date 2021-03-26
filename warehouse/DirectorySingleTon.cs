using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse
{
    public class DirectorySingleTon
    {
        private static DirectorySingleTon _instance = null;

        private DirectorySingleTon()
        {
            DataBase = new Dictionary<string, Product>();
        }

        public static DirectorySingleTon Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DirectorySingleTon();
                }
                return _instance;
            }
        }

        public Dictionary<string, Product> DataBase;
    }
}
