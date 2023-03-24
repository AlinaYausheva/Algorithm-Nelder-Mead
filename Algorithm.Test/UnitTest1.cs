using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NelderMeadTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int[] expected = { 1, 1 };


            int[] result = Algorithm_Nelder_Mead.NelderMead.FunctionForTests("f(x1,x2)=(1-x1)^2+100*(x2-x1^2)^2", 1, 0.5, 2);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            int[] expected = { 1, 4 };
            int[] result = Algorithm_Nelder_Mead.NelderMead.FunctionForTests("f(x,y)=x^2+x*y+y^2-6*x-9*y", 1, 0.5, 2);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMethod3()
        {
            int[] expected = { -1, 0, 1 };
            int[] result = Algorithm_Nelder_Mead.NelderMead.FunctionForTests("f(x1,x2,x3)=x1^2+x2^2+x3^2-x1*x2+x1-2*x3", 1, 0.5, 2);

            CollectionAssert.AreEqual(expected, result);
        }
    }
}
