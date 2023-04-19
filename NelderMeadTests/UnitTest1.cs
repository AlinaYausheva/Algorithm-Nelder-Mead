using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace NelderMeadTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int[] expected = { 1, 1 };

            Algorithm_Nelder_Mead.Point point = Algorithm_Nelder_Mead.NelderMead.Run("f(x1,x2)=(1-x1)^2+100*(x2-x1^2)^2", 1, 0.5, 2);


            int[] result = new int[point.Coordinate.Length];

            for (int i = 0; i < point.Coordinate.Length; i++)
                result[i] = (int)Math.Round(point.Coordinate[i]);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            int[] expected = { 1, 4 };
            Algorithm_Nelder_Mead.Point point = Algorithm_Nelder_Mead.NelderMead.Run("f(x,y)=x^2+x*y+y^2-6*x-9*y", 1, 0.5, 2);
            int[] result = new int[point.Coordinate.Length];

            for (int i = 0; i < point.Coordinate.Length; i++)
                result[i] = (int)Math.Round(point.Coordinate[i]);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMethod3()
        {
            int[] expected = { -1, 0, 1 };
            Algorithm_Nelder_Mead.Point point = Algorithm_Nelder_Mead.NelderMead.Run("f(x1,x2,x3)=x1^2+x2^2+x3^2-x1*x2+x1-2*x3", 1, 0.5, 2);
            int[] result = new int[point.Coordinate.Length];

            for (int i = 0; i < point.Coordinate.Length; i++)
                result[i] = (int)Math.Round(point.Coordinate[i]);

            CollectionAssert.AreEqual(expected, result);
        }
    }
}
