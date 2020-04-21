using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPCCore
{
    public class OPCTag
    {
        private String _Name;
        private OPCTagType _Type;
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public OPCTagType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public OPCTag()
        {
            _Name = String.Empty;
            _Type = OPCTagType.Double;
        }
        public OPCTag(String Name, OPCTagType Type)
        {
            _Name = Name;
            _Type = Type;
        }
    }
}
