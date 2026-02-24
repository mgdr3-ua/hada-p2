using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hada_p2
{
    public class Barco
    {   
        //Propiedades
        public Dictionary<Coordenada, string> coordenadasBarco { get; private set; }
        public string Nombre { get; private set; }
        public int NumDanyos { get; private set; }

        // Eventos
        public event EventHandler<TocadoArgs> eventoTocado;
        public event EventHandler<HundidoArgs> eventoHundido;
    }
}
