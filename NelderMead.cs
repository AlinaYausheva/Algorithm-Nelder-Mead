using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.mariuszgromada.math.mxparser;

namespace Algorithm_Nelder_Mead
{
    public class NelderMead
    {
        public static double alpha, beta, gamma;
        public static Function f;
        public static int argNum, pointNum;
        public static Point[] points;
        public static Point Xb, Xw, Xg, Xc, Xr, Xs, Xe;
        public static double Fb, Fg, Fw, Fe, Fr, Fs;

        static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        public static Point[] Sort(Point[] p)
        {
            for (int i = 0; i < pointNum; i++)
            {
                for (int j = i + 1; j < pointNum; j++)
                {
                    if (CalculateFunction(p[i]) > CalculateFunction(p[j]))
                    {
                        Point a = new Point(argNum);
                        for (int k = 0; k < argNum; k++)
                        {
                            a.coordinate[k] = p[i].coordinate[k];
                            p[i].coordinate[k] = p[j].coordinate[k];
                            p[j].coordinate[k] = a.coordinate[k];
                        }
                    }
                }
            }
            return p;
        }

        public static int[] FunctionForTests(string fun, double a, double b, double g)
        {
            bool ap = false;
            int[] vector;
            alpha = a;
            beta = b;
            gamma = g;
            f = new Function(fun);
            argNum = f.getArgumentsNumber();
            pointNum = argNum + 1;

            vector = new int[argNum];
            BuildSimplex();
            while (!ap)
            {
                PointSelection();
                FindingGravityCenter();
                Reflection();
                ap = DerectionChecking();
            }

            for (int i = 0; i < argNum; i++)
            {
                vector[i] = (int)Math.Round(Xb.coordinate[i]);
            }
            return vector;
        }
        public static void ReadInformation()
        {
            Console.WriteLine("Введите альфа (обычно 1):");
            alpha = double.Parse(Console.ReadLine());//коэффициент отражения
            Console.WriteLine("Введите бета (обычно 0,5):");
            beta = double.Parse(Console.ReadLine());//коэффициент сжатия
            Console.WriteLine("Введите гамма (обычно 2):");
            gamma = double.Parse(Console.ReadLine());//коэффициент растяжения
            Console.WriteLine("Введите функцию (пример ввода f(x1,x2)=3*x1+x2^2):");
            string function = Console.ReadLine();
            f = new Function(function);
            argNum = f.getArgumentsNumber();//количество аргументов в функции
            pointNum = argNum + 1;//количество точек

        }

        public static void BuildSimplex() //выбор точек
        {
            Xb = new Point(argNum);
            Xw = new Point(argNum);
            Xg = new Point(argNum);
            Xr = new Point(argNum);
            Xe = new Point(argNum);
            Xs = new Point(argNum);

            points = new Point[pointNum];
            for (int i = 0; i < pointNum; i++)
                points[i] = new Point(argNum);

            for (int i = 0; i < argNum; i++)
                points[0].coordinate[i] = 0;

            for (int i = 1; i < pointNum; i++)
                for (int j = 0; j < argNum; j++)
                {
                    if (i == j + 1)
                        points[i].coordinate[j] = 1;
                    else
                        points[i].coordinate[j] = 0;
                }
        }

        public static void PointSelection()
        {
            points = Sort(points);

            for (int i = 0; i < pointNum; i++)
            {
                for (int j = 0; j < argNum; j++)
                {
                    Console.Write(points[i].coordinate[j] + " ");
                }
                Console.WriteLine();
            }


            for (int k = 0; k < argNum; k++)
            {
                Xb.coordinate[k] = points[0].coordinate[k];
                Xw.coordinate[k] = points[pointNum - 1].coordinate[k];
                Xg.coordinate[k] = points[pointNum - 2].coordinate[k];
            }
        }

        public static void FindingGravityCenter()
        {
            Xc = new Point(argNum);
            for (int i = 0; i < argNum; i++)
                for (int j = 0; j < pointNum - 1; j++)
                    Xc.coordinate[i] += points[j].coordinate[i];

            for (int i = 0; i < argNum; i++)
                Xc.coordinate[i] = Xc.coordinate[i] / argNum;
        }

        public static void Reflection()
        {
            for (int i = 0; i < argNum; i++)
                Xr.coordinate[i] = (1 + alpha) * Xc.coordinate[i] - alpha * Xw.coordinate[i];
        }

        public static bool DerectionChecking()
        {
            Fr = CalculateFunction(Xr);
            Fb = CalculateFunction(Xb);
            Fg = CalculateFunction(Xg);
            Fw = CalculateFunction(Xw);

            if (Fr < Fb)
            {
                for (int i = 0; i < argNum; i++)
                    Xe.coordinate[i] = (1 - gamma) * Xc.coordinate[i] + gamma * Xr.coordinate[i];

                Fe = CalculateFunction(Xe);
                if (Fe < Fr)
                {
                    for (int k = 0; k < argNum; k++)
                    {
                        Xw.coordinate[k] = Xe.coordinate[k];
                        points[pointNum - 1].coordinate[k] = Xe.coordinate[k];

                    }
                    return CheckConvergence();
                }
                if (Fr < Fe)
                {
                    for (int k = 0; k < argNum; k++)
                    {
                        Xw.coordinate[k] = Xr.coordinate[k];
                        points[pointNum - 1].coordinate[k] = Xr.coordinate[k];
                    }
                    return CheckConvergence();
                }
            }
            if ((Fb < Fr) && (Fr < Fg))
            {
                for (int k = 0; k < argNum; k++)
                {
                    Xw.coordinate[k] = Xr.coordinate[k];
                    points[pointNum - 1].coordinate[k] = Xr.coordinate[k];
                }
                return CheckConvergence();
            }
            if ((Fg < Fr) && (Fr < Fw))
            {
                Point a = new Point(argNum);
                for (int k = 0; k < argNum; k++)
                {
                    a.coordinate[k] = Xw.coordinate[k];
                    Xw.coordinate[k] = Xr.coordinate[k];
                    Xr.coordinate[k] = a.coordinate[k];
                }
                Swap(ref Fw, ref Fr);
                return Comptession();
            }
            if (Fw < Fr)
            {
                return Comptession();
            }
            return Comptession();
        }

        public static bool Comptession()//6, 7, 8 шаги
        {
            for (int i = 0; i < argNum; i++)
                Xs.coordinate[i] = beta * Xw.coordinate[i] + (1 - beta) * Xc.coordinate[i];
            Fs = CalculateFunction(Xs);
            if (Fs < Fw)
            {
                for (int k = 0; k < argNum; k++)
                {
                    Xw.coordinate[k] = Xs.coordinate[k];
                    points[pointNum - 1].coordinate[k] = Xs.coordinate[k];
                }
                return CheckConvergence();
            }

            if (Fs > Fw)
            {
                for (int i = 1; i < pointNum; i++)
                {
                    for (int j = 0; j < argNum; j++)
                    {
                        points[i].coordinate[j] = Xb.coordinate[j] + (points[i].coordinate[j] - Xb.coordinate[j]) / 2;
                    }
                }
            }
            return CheckConvergence();
        }

        public static bool CheckConvergence()
        {
            double eps = 0.000001;
            double check = 0;
            for (int i = 0; i < pointNum; i++)
            {
                check += Math.Pow((CalculateFunction(points[i]) - CalculateFunction(Xc)), 2);
            }
            check = Math.Sqrt(check / pointNum);
            if (check < eps)
            {
                return true;
            }
            return false;
        }

        public static double CalculateFunction(Point p)
        {
            for (int i = 0; i < argNum; i++)
                f.setArgumentValue(i, p.coordinate[i]);
            double y = f.calculate();
            return y;
        }

        public static void WriteInformation()
        {
            Console.Write("f(" + Xb.coordinate[0]);
            for (int i = 1; i < argNum; i++)
            {
                Console.Write(" ," + Xb.coordinate[i]);
            }
            Console.Write(") = " + CalculateFunction(Xb));
        }
    }
}