using System;
using org.mariuszgromada.math.mxparser;

namespace Algorithm_Nelder_Mead
{
    class Program
    {
        static void Main()
        {
            bool isCallSuccessful = License.iConfirmNonCommercialUse("Alina"); //"лицензия" для библиотеки
            bool approximation = false;

            NelderMead.ReadInformation();

            NelderMead.BuildSimplex();//1 шаг
            while (!approximation)
            {
                NelderMead.PointSelection();//2 шаг
                NelderMead.FindingGravityCenter();//3 шаг
                NelderMead.Reflection();//4 шаг               
                approximation = NelderMead.DerectionChecking();//5 шаг (внутри переход на шаг 6 и шаг 9)
            }
            NelderMead.WriteInformation();
        }
    }
}
