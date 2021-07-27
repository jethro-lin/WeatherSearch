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

    public class Info
    {
        public string Time;
        public string C_T;
        public string C_AT;
        public string F_T;
        public string F_AT;
        public string RH;
        public string Rain;
        public string Sunrise;
        public string Sunset;
    }

    public class Type
    {
        public string C;
        public string link;

        public Type(string _c, string _link)
        {
            this.C = _c;
            this.link = _link;
        }
        public override string ToString()
        {
            return C;
        }
    }
}
