using System;
using System.IO;
using org.mariuszgromada.math.mxparser;

namespace Algorithm_Nelder_Mead
{
    class Program
    {
        public static string[] ReadInformationFromFile()
        {
            string[] s = new string[4];
            StreamReader f = new StreamReader(@"C:\Users\Alina\Desktop\универ\6 семестр\Методы оптимизации\ЛР1 2.0\Algorithm Nelder-Mead\TestFile.txt");
            for(int i = 0; i < 4; i++)
            {
                s[i] = f.ReadLine();
            }
            return s;
        }

        public static string[] ReadInformationFromConsole()
        {
            string[] s = new string[4];
            Console.WriteLine("Введите альфа (обычно 1):");
            s[0] = Console.ReadLine();//коэффициент отражения
            Console.WriteLine("Введите бета (обычно 0,5):");
            s[1] = Console.ReadLine();//коэффициент сжатия
            Console.WriteLine("Введите гамма (обычно 2):");
            s[2] = Console.ReadLine();//коэффициент растяжения
            Console.WriteLine("Введите функцию (пример ввода f(x1,x2)=3*x1+x2^2):");
            s[3] = Console.ReadLine();
            return s;
        }
        static void Main()
        {
            bool isCallSuccessful = License.iConfirmNonCommercialUse("Alina"); //"лицензия" для библиотеки
            
            string [] inform = ReadInformationFromConsole();
            //string[] inform = ReadInformationFromFile();
            Point ans = NelderMead.Run(inform[3], double.Parse(inform[0]), double.Parse(inform[1]), double.Parse(inform[2]));

            Console.Write(NelderMead.WriteAnswer());
            
        }
    }
}
