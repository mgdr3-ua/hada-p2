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
        public Dictionary<Coordenada, string> CoordenadasBarco { get; private set; }
        public string Nombre { get; private set; }
        public int NumDanyos { get; private set; }

        // Eventos
        public event EventHandler<TocadoArgs> eventoTocado;
        public event EventHandler<HundidoArgs> eventoHundido;

        public Barco(string nombre, int longitud, char orientacion, Coordenada coordenadaInicio)
        {
            Nombre = nombre;
            NumDanyos = 0;
            CoordenadasBarco = new Dictionary<Coordenada, string>();
            
            // Calcular coordenadas según orientacion 
            for(int m = 0; m < longitud; m++)
            {
                Coordenada nueva;

                if (orientacion == 'h')
                    nueva = new Coordenada(coordenadaInicio.Fila, coordenadaInicio.Columna + m);
                else 
                    nueva = new Coordenada(coordenadaInicio.Fila + m , coordenadaInicio.Columna);

                CoordenadasBarco.Add(nueva, Nombre);
            }
        }

        public void Disparo(Coordenada c)
        {
            if (CoordenadasBarco.ContainsKey(c)) {

                //Si ya estaba tocada ("_T"), no incrementa daños
                if (!CoordenadasBarco[c].EndsWith("_T"))
                {
                    CoordenadasBarco[c] = Nombre + "_T"; // Acá actualizamos etiqueta :)
                    NumDanyos++;

                    // Lanzar evento Tocado
                    if (eventoTocado != null)
                        eventoTocado(this, new TocadoArgs(Nombre, c));

                    // Verififcar si se ha hundido 
                    if (hundido() && eventoHundido != null)
                        eventoHundido(this, new HundidoArgs(Nombre));
                }
        }
    }

        public bool hundido()
        {
            //Si todas las etiquetas tienen el sufijo _T, está hundido :(
            return CoordenadasBarco.Values.All(v => v.EndsWith("_T"));
        }

        public override string ToString()
        {
            string result = $"[{Nombre}] - DAÑOS: [{NumDanyos}] - HUNDIDO: [{hundido()}] - COORDENADAS: ";
            foreach (var par in CoordenadasBarco)
            {
                result += $"[{par.Key} : {par.Value}] ";

            }
            return result;
        }
    }
}
