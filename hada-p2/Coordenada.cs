using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hada_p2
{
    internal class Coordenada
    {
        private int _fila;
        private int _columna;

        // Propiedades con validación (0-9) 
        public int Fila
        {
            get { return _fila; }
            set { if (value >= 0 && value <= 9) _fila = value; }
        }

        public int Columna
        {
            get { return _columna; }
            set { if (value >= 0 && value <= 9) _columna = value; }
        }

        // 4 Constructores requeridos
        public Coordenada() { Fila = 0; Columna = 0; }

        public Coordenada(int fila, int columna)
        {
            Fila = fila;
            Columna = columna;
        }

        public Coordenada(string fila, string columna)
        {
            Fila = int.Parse(fila);
            Columna = int.Parse(columna);
        }

        public Coordenada (Coordenada coordenada)
        {
            Fila = coordenada.Fila;
            Columna = coordenada .Columna;
        }

        // Sobrecargas de métodos de objetos 

    }

}
