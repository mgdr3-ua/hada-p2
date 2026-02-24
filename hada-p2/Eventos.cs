using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hada
{
    public class TocadoArgs : EventArgs
    {
        public string nombre { get; }
        public Coordenada coordenadaImpacto { get; }

        public TocadoArgs(string nombre, Coordenada coordenadaImpacto)
        {
            this.nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            
            this.coordenadaImpacto = coordenadaImpacto ?? throw new ArgumentNullException(nameof(coordenadaImpacto));
        }
    }

    public class HundidoArgs : EventArgs
    {
        public string nombre { get; }
        public HundidoArgs(string nombre){
            this.nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
        }
    }
}