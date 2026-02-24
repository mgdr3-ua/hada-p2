using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hada
{
    public class Barco
    {
        // Propiedades públicas (escritura privada donde toca)
        public Dictionary<Coordenada, string> CoordenadasBarco { get; private set; }
        public string Nombre { get; private set; }
        public int NumDanyos { get; private set; }

        // Eventos públicos
        public event EventHandler<TocadoArgs> eventoTocado;
        public event EventHandler<HundidoArgs> eventoHundido;

        public Barco(string nombre, int longitud, char orientacion, Coordenada coordenadaInicio)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("Nombre no puede ser vacío.", nameof(nombre));
            if (longitud <= 0)
                throw new ArgumentOutOfRangeException(nameof(longitud), "Longitud debe ser > 0.");
            if (orientacion != 'h' && orientacion != 'v')
                throw new ArgumentException("Orientación debe ser 'h' o 'v'.", nameof(orientacion));
            if (coordenadaInicio == null)
                throw new ArgumentNullException(nameof(coordenadaInicio));

            Nombre = nombre;
            NumDanyos = 0;
            CoordenadasBarco = new Dictionary<Coordenada, string>();

            //Inicializa coordenadas según orientación y longitud
            
            for (int m = 0; m < longitud; m++)
            {
                Coordenada nueva =
                    (orientacion == 'h')
                        ? new Coordenada(coordenadaInicio.Fila, coordenadaInicio.Columna + m)
                        : new Coordenada(coordenadaInicio.Fila + m, coordenadaInicio.Columna);

                // Etiqueta inicial: nombre delbarco
                CoordenadasBarco.Add(nueva, Nombre);
            }
        }

        public void Disparo(Coordenada c)
        {
            if (c == null) return;

            if (!CoordenadasBarco.ContainsKey(c))
                return;

            //Si ya estaba tocada, no incrementa daños ni relanza eventos
          
            if (CoordenadasBarco[c].EndsWith("_T"))
                return;

            // Actualiza etiqueta y daños
            CoordenadasBarco[c] = Nombre + "_T";
            NumDanyos++;

            // Lanza evento tocado
            eventoTocado?.Invoke(this, new TocadoArgs(Nombre, c));

            //Si queda hundido, lanza evento hundido

            if (hundido())
                eventoHundido?.Invoke(this, new HundidoArgs(Nombre));
        }

        public bool hundido()
        {
            //Hundido si todas las etiquetas terminan en _T :(
            return CoordenadasBarco.Values.All(v => v.EndsWith("_T"));
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"[{Nombre}] - DAÑOS: [{NumDanyos}] - HUNDIDO: [{hundido()}] - COORDENADAS: ");

            foreach (var par in CoordenadasBarco)
                sb.Append($"[{par.Key}:{par.Value}] ");

            return sb.ToString();
        }
    }
}