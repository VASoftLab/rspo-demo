using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPCCore
{
    public enum OPCTagType : int
    {
        Unknown = 0,
        Boolean = 1,
        Word    = 2,
        Short   = 3,
        DWord   = 4,
        Long    = 5,
        Float   = 6,
        Double  = 7,
        String  = 8,
        Char    = 9,
        Date    = 10
    }
}
