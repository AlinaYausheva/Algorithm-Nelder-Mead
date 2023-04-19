using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm_Nelder_Mead
{
    public class Point
    {
        private double[] coordinate;

        public double[] Coordinate { get { return coordinate; } set { coordinate = value; } }
        public Point(int n)
        {
            coordinate = new double[n];
            for (int i = 0; i < n; i++)
                coordinate[i] = 0;
        }

        public double this[int index] { get { return coordinate[index]; } set { coordinate[index] = value; } }


        public static Point operator /(Point point1, double number)
        {
            Point newPoint = new Point(point1.Coordinate.Length);
            for (int i = 0; i < point1.Coordinate.Length; i++)
            {
                newPoint[i] = point1[i] / number;
            }
            return newPoint;
        }

        public static Point operator +(Point point1, Point point2)
        {
            Point newPoint = new Point(point1.Coordinate.Length);
            for (int i = 0; i < point1.Coordinate.Length; i++)
            {
                newPoint[i] = point1[i] + point2[i];
            }
            return newPoint;
        }

        public static Point operator +(Point point1, double number)
        {
            Point newPoint = new Point(point1.Coordinate.Length);
            for (int i = 0; i < point1.Coordinate.Length; i++)
            {
                newPoint[i] = point1[i] + number;
            }
            return newPoint;
        }

        public static Point operator *(double number, Point point1)
        {
            Point newPoint = new Point(point1.Coordinate.Length);
            for (int i = 0; i < point1.Coordinate.Length; i++)
            {
                newPoint[i] = number * point1[i];
            }
            return newPoint;
        }      

        public static Point operator -(Point point1, Point point2)
        {
            Point newPoint = new Point(point1.Coordinate.Length);
            for (int i = 0; i < point1.Coordinate.Length; i++)
            {
                newPoint[i] = point1[i] - point2[i];
            }
            return newPoint;
        }       
    }
}
