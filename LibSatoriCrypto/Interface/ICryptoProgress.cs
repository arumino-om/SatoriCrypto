using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibSatoriCrypto.Interface
{
    public interface ICryptoProgress
    {
        public int Max { get; set; }
        public int Now { get; set; }
    }
}
