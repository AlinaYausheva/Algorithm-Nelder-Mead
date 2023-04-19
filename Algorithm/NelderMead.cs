using org.mariuszgromada.math.mxparser;
using System;

namespace Algorithm_Nelder_Mead
{
    public class NelderMead
    {
        public static double alpha, beta, gamma;
        public static Function f;
        public static int argNum, pointNum;
        public static Point[] points;
        public static Point X_best, X_worst, X_good, X_center, X_reflection, X_squeeze, X_elongation;
        public static double F_best, F_good, F_worst, F_elongation, F_reflection, F_squeeze;

        static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        public static Point[] Sort(Point[] p)
        {
            Point[] copyPoints = new Point[p.Length];
            Array.Copy(p, copyPoints, p.Length);

            for (int i = 0; i < pointNum; i++)
            {
                for (int j = i + 1; j < pointNum; j++)
                {
                    if (CalculateFunction(copyPoints[i]) > CalculateFunction(copyPoints[j]))
                    {
                        Point a = new Point(argNum);
                        a = copyPoints[i];
                        copyPoints[i] = copyPoints[j];
                        copyPoints[j] = a;
                    }
                }                
            }
            return copyPoints;
        }

        public static Point Run(string fun, double a, double b, double g)
        {
            bool approximation = false;
            alpha = a;
            beta = b;
            gamma = g;
            f = new Function(fun);
            argNum = f.getArgumentsNumber();
            pointNum = argNum + 1;

            BuildSimplex();
            while (!approximation)
            {
                PointSelection();
                FindingGravityCenter();
                Reflection();
                approximation = DerectionChecking();
            }
            return X_best;
        }

        public static void BuildSimplex() //выбор точек
        {
            X_best = new Point(argNum);
            X_worst = new Point(argNum);
            X_good = new Point(argNum);
            X_reflection = new Point(argNum);
            X_elongation = new Point(argNum);
            X_squeeze = new Point(argNum);

            points = new Point[pointNum];
            for (int i = 0; i < pointNum; i++)
                points[i] = new Point(argNum);

            for (int i = 1; i < pointNum; i++)
                for (int j = 0; j < argNum; j++)
                {
                    if (i == j + 1)
                        points[i].Coordinate[j] = 1;
                    else
                        points[i].Coordinate[j] = 0;
                }
        }

        public static void PointSelection()
        {
            points = Sort(points);

            X_best = points[0];
            X_worst = points[pointNum - 1];
            X_good = points[pointNum - 2];
        }

        public static void FindingGravityCenter()
        {
            X_center = new Point(argNum);
            for (int j = 0; j < pointNum - 1; j++)
                X_center = X_center + points[j];

            X_center = X_center / argNum;
        }

        public static void Reflection()
        {
            X_reflection = (1 + alpha) * X_center - alpha * X_worst;
        }

        public static bool DerectionChecking()
        {
            F_reflection = CalculateFunction(X_reflection);
            F_best = CalculateFunction(X_best);
            F_good = CalculateFunction(X_good);
            F_worst = CalculateFunction(X_worst);

            if (F_reflection < F_best)
            {
                X_elongation = (1 - gamma) * X_center + gamma * X_reflection;

                F_elongation = CalculateFunction(X_elongation);
                if (F_elongation < F_reflection)
                {
                    X_worst = X_elongation;
                    points[pointNum - 1] = X_elongation;
                    return CheckConvergence();
                }
                if (F_reflection < F_elongation)
                {
                    X_worst = X_reflection;
                    points[pointNum - 1] = X_reflection;
                    return CheckConvergence();
                }
            }
            if ((F_best < F_reflection) && (F_reflection < F_good))
            {
                X_worst = X_reflection;
                points[pointNum - 1] = X_reflection;
                return CheckConvergence();
            }
            if ((F_good < F_reflection) && (F_reflection < F_worst))
            {
                Swap(ref X_worst, ref X_reflection);
                Swap(ref F_worst, ref F_reflection);
                return Comptession();
            }
            if (F_worst < F_reflection)
            {
                return Comptession();
            }
            return Comptession();
        }

        public static bool Comptession()//6, 7, 8 шаги
        {
            X_squeeze = beta * X_worst + (1 - beta) * X_center;
            F_squeeze = CalculateFunction(X_squeeze);
            if (F_squeeze < F_worst)
            {
                X_worst = X_squeeze;
                points[pointNum - 1] = X_squeeze;
                return CheckConvergence();
            }
            else if (F_squeeze > F_worst)
            {
                for (int i = 1; i < pointNum; i++)
                {
                    points[i] = X_best + (points[i] - X_best) / 2;
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
                check += Math.Pow((CalculateFunction(points[i]) - CalculateFunction(X_center)), 2);
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
                f.setArgumentValue(i, p.Coordinate[i]);
            double y = f.calculate();
            return y;
        }

        public static string WriteAnswer()
        {
            string ans = "";
            ans+="f(" + X_best.Coordinate[0].ToString();
            for (int i = 1; i < argNum; i++)
            {
                ans+=", " + X_best.Coordinate[i].ToString();
            }
            ans += ") = " + CalculateFunction(X_best).ToString();
            return ans;
        }
    }
}
