using Hada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Hada
{

   public class Tablero
   {
        
        private int tamTablero;

        //propiedad pública
        public int TamTablero
        {
            get { return tamTablero; }
            set 
            {
                if (value < 4 || value > 9)
                    throw new ArgumentOutOfRangeException(nameof(TamTablero), "El tamaño mínimo del tablero es 4, y el máximo 9.");

                tamTablero = value;
            }

        }

        //propiedades privadas
        private List<Coordenada> coordenadasDisparadas;
        private List<Coordenada> coordenadasTocadas;
        private List<Barco> barcos;
        private List<Barco> barcosEliminados;
        private Dictionary<Coordenada, string> casillasTablero;

        //constructor
        public Tablero(int tamTablero, List<Barco> barcos)
        {
            TamTablero = tamTablero;
            this.barcos = barcos ?? throw new ArgumentNullException(nameof(barcos));

            coordenadasDisparadas = new List<Coordenada>();
            coordenadasTocadas = new List<Coordenada> (); 
            barcosEliminados = new List<Barco> ();
            casillasTablero = new Dictionary<Coordenada, string> ();
        }

        //metodo privado inicializaCasillasTablero()
        private void inicializaCasillasTablero()
        {
            casillasTablero.Clear ();

            //todo a agua
            for(int fila = 0; fila < TamTablero; fila++) 
            {
                for (int col = 0; col < TamTablero; col++) 
                {
                    casillasTablero.Add(new Coordenada(fila, col), "AGUA");
                }
            }

            //colocar barcos
            foreach(Barco b in barcos)
            {
                foreach(var par in b.CoordenadasBarco) 
                {
                    if (casillasTablero.ContainsKey(par.Key))
                        casillasTablero[par.Key] = b.Nombre; //estado nombre_barco
                }
            }
        }

        private bool estaDentroTablero(Coordenada c)
        {
            return c != null &&
                c.Fila >= 0 && c.Fila < TamTablero &&
                c.Columna >= 0 && c.Columna < TamTablero;
        }

        //metodo publico
        public void Disparar(Coordenada c)
        {
            if(!estaDentroTablero(c))
            {
                Console.WriteLine($"La coordenada {c} está fuera de las dimensiones del tablero.");
                return;
            }

            //registrar disparos
            coordenadasDisparadas.Add(new Coordenada(c));

            //comprobar impacto en barcos
            foreach (Barco b in barcos)
                b.Disparo(c);
        }

        public string DibujarTablero()
        {
            string tablero = "";

            for(int fila = 0; fila < TamTablero; fila++) 
            { 
                for(int col = 0; col < TamTablero; col++) 
                {
                    Coordenada coord = new Coordenada(fila, col);
                    tablero += casillasTablero[coord] + "]";
                }

                tablero += Environment.NewLine;
            }

            return tablero;

        }

        public override string ToString()
        {
            string salida = "";

            //informacion de los barcos
            foreach (Barco b in barcos)
                salida += b.ToString() + Environment.NewLine;

            //coordenadas disparadas
            salida += "Coordenadas Disparadas: ";
            foreach (Coordenada cd in coordenadasDisparadas)
                salida += cd.ToString() + " ";
            salida += Environment.NewLine;

            //coordenadas tocadas
            salida += "Coordenadas Tocadas: ";
            foreach (Coordenada ct in coordenadasTocadas)
                salida += ct.ToString() + " ";
            salida += Environment.NewLine;

            //tablero
            salida += DibujarTablero();

            return salida;
        }
    }
}
