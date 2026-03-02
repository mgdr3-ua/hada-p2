using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Game
    {
        //propiedad privada
        private bool finPartida;

        public Game()
        {
            finPartida = false;
            gameLoop();
        }

        //metodo privado
        private void gameLoop()
        {
            //1. Inicializo barcos (minimo 3) - elegidos para no solapar
            List<Barco> barcos = new List<Barco>();

            //barco(nombre, longitud, orientacion, coordenadaInicio)
            barcos.Add(new Barco("BARCO1", 3, 'h', new Coordenada(0, 0))); //(0,0) (0,1) (0,2)
            barcos.Add(new Barco("BARCO2", 4, 'v', new Coordenada(2, 5))); //(2,5) (3,5) (4,5) (5,5)
            barcos.Add(new Barco("BARCO3", 2, 'h', new Coordenada(7, 2))); //(7,2) (7,3)

            //2. Inicializo tablero
            int tamTablero = 9;
            Tablero tablero = new Tablero(tamTablero, barcos);

            //3. Evento fin de partida del tablero
            tablero.eventoFinPartida += cuandoEventoFinPartida;

            //bucle principal del juego
            while (!finPartida)
            {
                Console.WriteLine("Introduce la coordenada a la que disparar FILA,COLUMNA ('S' para Salir): ");
                string entrada = Console.ReadLine();

                if (entrada == null)
                    continue;

                if(entrada.Equals("s", StringComparison.OrdinalIgnoreCase))
                {
                    finPartida = true;
                    break;
                }

                //comprobamos formato NUMERO, NUMERO
                string[] partes = entrada.Split(',');
                if (partes.Length != 2)
                    continue;

                int fila;
                int columna;
                bool okFila = int.TryParse(partes[0].Trim(), out fila);
                bool okCol = int.TryParse(partes[1].Trim(), out columna);

                if (!okFila || !okCol)
                    continue;

                //ejecuta disparar
                Coordenada c = new Coordenada(fila, columna);
                tablero.Disparar(c);
                
                Console.WriteLine(tablero.ToString());
            }

        }

        //manejador del evento fin de partida
        private void cuandoEventoFinPartida(object sender, EventArgs e)
        {
            Console.WriteLine("PARTIDA FINALIZADA!!");
            finPartida = true;
        }

    }
}
