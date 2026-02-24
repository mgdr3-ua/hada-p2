using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Coordenada
    {
        private int _fila;
        private int _columna;

        // Propiedades públicas con campo de respaldo (0..9)
        public int Fila
        {
            get => _fila;
            set
            {
                if (value < 0 || value > 9)
                    throw new ArgumentOutOfRangeException(nameof(Fila), "Fila debe estar entre 0 y 9.");
                _fila = value;
            }
        }

        public int Columna
        {
            get => _columna;
            set
            {
                if (value < 0 || value > 9)
                    throw new ArgumentOutOfRangeException(nameof(Columna), "Columna debe estar entre 0 y 9.");
                _columna = value;
            }
        }

        // 4 constructores requeridos
        public Coordenada()
        {
            Fila = 0;
            Columna = 0;
        }

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

        public Coordenada(Coordenada coordenada)
        {
            if (coordenada == null) throw new ArgumentNullException(nameof(coordenada));
            Fila = coordenada.Fila;
            Columna = coordenada.Columna;
        }

        // Métodos requeridos
        public override string ToString() => $"({Fila},{Columna})";

        public override int GetHashCode() =>
            Fila.GetHashCode() ^ Columna.GetHashCode();

        public override bool Equals(object obj) =>
            obj is Coordenada c && Fila == c.Fila && Columna == c.Columna;

        public bool Equals(Coordenada coordenada) =>
            coordenada != null && Fila == coordenada.Fila && Columna == coordenada.Columna;
    }
}
