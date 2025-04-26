using System;
using System.Text;
using System.Collections.Generic;

namespace ProyectoPhone
{

    public static class PhoneDecoder
    {
        private static void Main(string[] args)
        {
            /*Console.WriteLine("Hello World!");
            Console.WriteLine(Convert("33#"));          // "E"
            Console.WriteLine(Convert("227*#"));       // "B"
            Console.WriteLine(Convert("4433555 555666#")); // "HELLO"
            Console.WriteLine(Convert("4466655520778833082555#")); // "HOLA QUE TAL"
            Console.WriteLine(Convert("8 88777444666*664#")); // "TURING" (asumo que era el objetivo)
        */}
        // Mapa de teclas: ¡Ojo con el '7' que tiene 4 letras!
        private static readonly Dictionary<char, string> TecladoViejo = new()
        {
            {'2', "ABC"},  // El 2 es el más usado, va primero
            {'3', "DEF"},
            {'4', "GHI"},  
            {'5', "JKL"},
            {'6', "MNO"},
            {'7', "PQRS"}, // RSPQ? No, el orden original es importante
            {'8', "TUV"},
            {'9', "WXYZ"}, // WXYZ es fácil de recordar
            {'0', " "},    // El espacio es útil
            {'1', "&'("}   // Casi nadie usa esto, pero por si las moscas
        };

        public static string Decodificar(string input)
        {
            if (string.IsNullOrEmpty(input)) 
            {
                return ""; // Nada que hacer aquí
            }

            var mensaje = new StringBuilder();
            int posicion = 0;

            try 
            {
                while (posicion < input.Length && input[posicion] != '#')
                {
                    char teclaActual = input[posicion];
                    
                    // Backspace es caso especial
                    if (teclaActual == '*')
                    {
                        if (mensaje.Length > 0)
                        {
                            mensaje.Length--; // Adiós último carácter
                        }
                        posicion++;
                        continue;
                    }

                    // Los espacios son pausas, los ignoramos
                    if (teclaActual == ' ')
                    {
                        posicion++;
                        continue;
                    }

                    // ¿Es una tecla válida?
                    if (TecladoViejo.ContainsKey(teclaActual))
                    {
                        var (nuevoPosicion, letra) = ProcesarTecla(input, posicion);
                        mensaje.Append(letra);
                        posicion = nuevoPosicion;
                    }
                    else
                    {
                        posicion++; // Tecla no válida, la saltamos
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"¡Ups! Algo falló: {ex.Message}");
                // Pero seguimos adelante igual
            }

            return mensaje.ToString();
        }

        private static (int nuevoPosicion, char letra) ProcesarTecla(string input, int posicionInicial)
        {
            char tecla = input[posicionInicial];
            int vecesPresionado = ContarPulsaciones(input, posicionInicial);
            string letrasDisponibles = TecladoViejo[tecla];

            // Esto evita IndexOutOfRange si hay muchas pulsaciones
            int indiceLetra = (vecesPresionado - 1) % letrasDisponibles.Length;
            char letraSeleccionada = letrasDisponibles[indiceLetra];

            return (posicionInicial + vecesPresionado, letraSeleccionada);
        }

        private static int ContarPulsaciones(string input, int posicion)
        {
            char teclaOriginal = input[posicion];
            int contador = 0;

            while (posicion < input.Length && input[posicion] == teclaOriginal)
            {
                contador++;
                posicion++;
            }

            return contador;
        }
    }
}
