
static char[,] GenerarOceano(char[,] oceano)
{
    for (int x = 0; x < oceano.GetLength(0); x++)
    {
        for (int y = 0; y < oceano.GetLength(1); y++)
        {
            oceano[x, y] = 'O';
        }
        
    }
    return oceano;
}
static void MostrarOceano(char[,] oceano)
{
    Console.Write(" ");
    for (int k = 0; k < oceano.GetLength(0); k++)
    {
        Console.Write($" {k}");
    }
    Console.WriteLine();
    for (int i = 0; i < oceano.GetLength(0); i++)
    {
        Console.Write($"{i} ");
        for (int j = 0; j < oceano.GetLength(1); j++)
        {
            Console.Write($"{oceano[i, j]} ");
        };
        Console.WriteLine();
    }
}

static char[,] GenerarBarcos(char[,] oceano, int tamBarco, char tipo, int cantidad)
{
    Random random = new Random();
    int posX, posY, direccion, dirfila, dircolumna, c=0;

    do
    {

        do
        {
            posX = random.Next(0, oceano.GetLength(0));
            posY = random.Next(0, oceano.GetLength(1));
            direccion = random.Next(0, 4);
            dirfila = 0;
            dircolumna = 0;
            switch (direccion)
            {
                case 0:
                    dircolumna = 1;
                    break;
                case 1:
                    dircolumna = -1;
                    break;
                case 2:
                    dirfila = 1;
                    break;
                case 3:
                    dirfila = -1;
                    break;
                default:
                    break;
            }

        } while (EspacioValido(oceano, posX, posY, dirfila, dircolumna, tamBarco) == false);
        for (int i = 0; i < tamBarco; i++)
        {
            oceano[posX, posY] = tipo;
            posX += dirfila;
            posY += dircolumna;
        }
        c++;
    } while (cantidad == c);
 
    return oceano;
}

static bool EspacioValido(char[,] oceano, int posX, int posY, int dirfila, int dircolumna, int tambarco)
{
    for (int i = 0; i < tambarco; i++)
    {
        if (oceano.GetLength(0) <= posX|| oceano.GetLength(1) <= posY|| oceano[posX, posY] != 'O')
        {
            
            return false;
        }

        posX += dirfila;
        posY += dircolumna;

        if (posX<0||posY<0)
        {
            return false;
        }
    }
    return true;
}

static char[,] Disparo(char[,]oceano, char[,] oceanoUsuario)
{
    uint posX, posY;

    Console.WriteLine("Ingrese la Fila que desea atacar");
    uint.TryParse(Console.ReadLine(), out posX);

    Console.WriteLine("Ingrese la Columna que desea atacar");
    uint.TryParse(Console.ReadLine(), out posY);

    if (oceano[posX, posY] == 'O' || oceanoUsuario[posX,posY] == 'X' )
    {
        Console.WriteLine("Disparo Fallido");
        oceanoUsuario[posX, posY] = 'X';
    }
    else
    {
        Console.WriteLine("¡ACIERTO!");
        oceanoUsuario[posX, posY] = oceano[posX, posY];
    }
    
    return oceanoUsuario;
}

static void Traduccion()
{
    Console.Write("O = Oceano (Espacio Vacio)");
    Console.Write("\tX = Espacio Golpeado");
    Console.Write("\tA = Porta Aviones");
    Console.Write("\tF = Fragata");
    Console.Write("\tN = Navio\n");
}

//////////////////////////////////////////////////////////

char[,] oceano, oceanoUsuario;
int tamaño, dificultad, cont, puntos = 0;

Console.WriteLine("Ingrese el tamaño que desea usar para el OCEANO");
int.TryParse(Console.ReadLine(), out tamaño);

if (tamaño == 0||tamaño<0)
{
    Console.WriteLine("Tamaño INVALIDO, generando el OCEANO con un tamaño de 9");
    tamaño = 9;
}
oceano = new char[tamaño, tamaño];
oceanoUsuario = new char[tamaño, tamaño];

Console.WriteLine("Ingrese la diicultad que desea usar");
Console.WriteLine("1.Facil (25 Disparos)\n2.Intermedia (20 Disparos)\n3.Dificil (15 Disparos)");
int.TryParse(Console.ReadLine(), out dificultad);

if (dificultad < 1 || dificultad > 3)
{
    Console.WriteLine("Dificultad Invalida, Inciando con la dificultad Intermedia");
    dificultad = 2;
    Console.ReadLine();
}

switch (dificultad)
{
    default:
        cont = 20;
        break;
    case 1:
        cont = 25;
        break;
    case 2:
        cont = 20;
        break;
    case 3: 
        cont = 15;
        break;
}

GenerarOceano(oceano);
GenerarOceano(oceanoUsuario);

GenerarBarcos(oceano, 3, 'A', 1);
GenerarBarcos(oceano, 2, 'F', 2);
GenerarBarcos(oceano, 1, 'N', 3);
Console.Clear();

for (int i = 0; i < cont; i++)
{
    Traduccion();
    MostrarOceano(oceanoUsuario);
    
    Console.WriteLine($"Quedan {cont-i} disparos");
    oceanoUsuario = Disparo(oceano,oceanoUsuario);
    
    Console.ReadLine();
    Console.Clear();
    
}

foreach (char i in oceanoUsuario)
{
    switch (i)
    {
        case 'N':
            puntos += 1;
            break;
        case 'F':
            puntos += 2;
            break;
        case 'A':
            puntos += 3;
            break;
        default:
            break;
    }

}

Console.WriteLine("Fin del Juego");
Console.WriteLine($"Punteo Final = Puntos({puntos})*Dificultad({dificultad}) = {puntos * dificultad}");