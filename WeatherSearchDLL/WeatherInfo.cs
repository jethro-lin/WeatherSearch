using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherSearchDLL
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

    public class Scenery
    {
        public string TypeID;
        public string TypeName;

        public Scenery(string path, string v)
        {
            this.TypeID = path;
            this.TypeName = v;
        }
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
