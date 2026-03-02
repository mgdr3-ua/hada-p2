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

        //evento público
        public event EventHandler<EventArgs> eventoFinPartida;
        //constructor
        public Tablero(int tamTablero, List<Barco> barcos)
        {
            TamTablero = tamTablero;
            this.barcos = barcos ?? throw new ArgumentNullException(nameof(barcos));

            coordenadasDisparadas = new List<Coordenada>();
            coordenadasTocadas = new List<Coordenada> (); 
            barcosEliminados = new List<Barco> ();
            casillasTablero = new Dictionary<Coordenada, string> ();

            //unir eventos tocado/hundido de cada barco
            foreach (Barco b in this.barcos)
            {
                b.eventoTocado += cuandoEventoTocado;
                b.eventoHundido += cuandoEventoHundido;
            }

            //inicializar tablero
            inicializaCasillasTablero();
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

            //colocar barcos (nombre_barcos)
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
            tablero += Environment.NewLine;
            tablero += "CASILLAS TABLERO" + Environment.NewLine;
            tablero += "-----" + Environment.NewLine;

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
            salida += "Coordenadas disparadas: ";
            foreach (Coordenada cd in coordenadasDisparadas)
                salida += cd.ToString() + " ";
            salida += Environment.NewLine;

            //coordenadas tocadas
            salida += "Coordenadas tocadas: ";
            foreach (Coordenada ct in coordenadasTocadas)
                salida += ct.ToString() + " ";
            salida += Environment.NewLine;

            //tablero
            salida += DibujarTablero();

            return salida;
        }

        //MANJEADORES (privados)
        
        //maneja el evento tocado
        private void cuandoEventoTocado(object sender, TocadoArgs e)
        {
            //actualiza la casilla tocada en el tablero
            if (estaDentroTablero(e.coordenadaImpacto) && casillasTablero.ContainsKey(e.coordenadaImpacto))
            {
                casillasTablero[e.coordenadaImpacto] = e.nombre + "_T";
            }

            //registrar coordenada tocada sin repetidos
            bool existe = false;
            foreach (Coordenada ct in coordenadasTocadas)
            {
                if (ct.Equals(e.coordenadaImpacto))
                {
                    existe = true;
                    break;
                }
            }

            if (!existe)
                coordenadasTocadas.Add(new Coordenada(e.coordenadaImpacto));

            //mensaje pedido
            Console.WriteLine($"TABLERO: Barco [{e.nombre}] tocado en Coordenada: [{e.coordenadaImpacto}]");

        }

        //maneja el evento hundido
        private void cuandoEventoHundido(object snder, HundidoArgs e)
        {
            //mensaje pedido
            Console.WriteLine($"TABLERO: Barco [{e.nombre}] hundido !!");

            //marcar barco como eliminado sin repetidos
            Barco barcoHundido = null;
            foreach (Barco b in barcos)
            {
                if (b.Nombre == e.nombre)
                {
                    barcoHundido = b;
                    break;
                }
            }

            if (barcoHundido != null && !barcosEliminados.Contains(barcoHundido))
                barcosEliminados.Add(barcoHundido);

            //si todos hundidos -> lanzar evento fin de partida 
            if (barcosEliminados.Count == barcos.Count)
            {
                eventoFinPartida?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
