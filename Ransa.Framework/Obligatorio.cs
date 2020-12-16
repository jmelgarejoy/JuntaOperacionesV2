using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ransa.Framework
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Obligatorio : Attribute
    {
        public bool Requerido { get; set; }
        public int LongitudMinima { get; set; }
        public int LongitudMaxima { get; set; }

        public Obligatorio(bool requerido, int longitudMinima = 0, int longitudMaxima = 0)
        {
            Requerido = requerido;
            LongitudMinima = longitudMinima;
            LongitudMaxima = longitudMaxima;
        }
    }
}
