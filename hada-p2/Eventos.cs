using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hada_p2
{
    // Información para el evento de tocar un barco
    public class TocadoArgs : EventArgs
    {
        public string nombre {  get; set; }
        public Coordenada coordenadaImpacto { get; set; }

        public TocadoArgs(string nombre, Coordenada coordenadaImpacto) {
        
            this.nombre = nombre;
            this.coordenadaImpacto = coordenadaImpacto;
        }
    }

    // Información para el evento de hundir un barco
    public class HundidoArgs : EventArgs
    {
        public string nombre { get; set; }

        public HundidoArgs(string nombre)
        {
            this.nombre = nombre;
        }
    }
}
