using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{


    public class Time
    {
        public string C;
        public string E;
    }

    public class CName
    {
        public string C;
        public string E;
    }

    public class Country
    {
        public string ID;
        public string Area;
        public string TID;
        public CName Name;
    }

    public class Town
    {
        public string ID;
        public string RID;
        public string Tide;
        public CName Name;
    }

    public class NatScenic
    {
        public string PID;
        public string CID;
        public string TID;
        public CName Name;
    }
}
