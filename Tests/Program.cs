using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Golotip = BaseClassLibrary.Golotip;
namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            TestGolotipClass();
            Console.ReadKey();
        }

        static void PrintMatrix(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i,j]} ");
                }
                Console.WriteLine();
            }
        }

        static void PrintIntArray(int[] array)
        {
            foreach (var element in array)
                Console.Write($"{element} ");
            Console.WriteLine("\n");
        }
        static void PrintDoubleArray(double[] array)
        {
            foreach (var element in array)
                Console.Write($"{element} ");
            Console.WriteLine("\n");
        }
        static void TestGolotipClass()
        {
            Random rand = new Random();
            double[,] TOC = new double[8, 2];

            PrintMatrix(TOC);
            for (int i = 0; i < TOC.GetLength(0); i++)
            {
                for (int j = 0; j < TOC.GetLength(1); j++)
                {
                    TOC[i, j] = rand.Next(0, 2);
                }
            }
            double[,] TOC2 = new double[18, 2] { {126, 2.91 }, {138,4.5 }, { 182,2.16} ,{196, 2.3 }, { 152, 4.7 } ,{ 193, 4.22 }, { 113, 5.23 },
                                                {154, 4.06 }, {124,5.65 }, { 179, 2.72} ,{ 174, 1.41 }, { 145, 4.62 } ,{ 108, 5.26 }, { 117, 4.92 },
                                                { 145, 3.28 }, {115, 3.27 }, { 149, 4.76 }, {168, 2.79 }};

            double[,] TOC3 = new double[15, 2] { {140,3.2}, {135,4.38}, {115,5.99 }, {187,4.54 }, {169,5.39},
                                                {141,2.44}, {201,3.04}, {112,3.18 },{129, 4.92}, {119,3.96},
                                                { 204,2.58}, {139,3.23}, {165,4},{204,5.3 }, {187,4.67 } };
            PrintMatrix(TOC2);
            Console.WriteLine("\n");
            Golotip golotip = new Golotip();
            golotip.Training(TOC2, 0.82, 0.5, 0.5);
             
            List<double[,]> list = golotip.GetTrainingData.PropMatrixList;
            list.Add(golotip.GetTrainingData.JointMatrix);
            list.Add(golotip.GetTrainingData.CleanMatrix);
            foreach (var element in list)
            {
                PrintMatrix(element);
                Console.WriteLine("\n");
            }
            foreach (var group in golotip.GetTrainingData.Groups)
                PrintIntArray(group.Value);
            Console.WriteLine();
            foreach (var element in golotip.GetTrainingData.Golotips)
            {
                Console.Write(element.Key + " : ");
                PrintDoubleArray(element.Value);
                Console.WriteLine();
            }

            Console.WriteLine();
            PrintDoubleArray(golotip.GetTrainingData.Radiuses);
            Console.WriteLine();
            PrintMatrix(TOC3);
            Console.WriteLine();
            golotip.Exam(TOC3);
            foreach (var element in golotip.GetExamData.PropVectors)
            {
                Console.Write(element.Key + " : \n");
                PrintMatrix(element.Value);
                Console.WriteLine();
            }
            foreach (var vector in golotip.GetExamData.JointVectors)
            {
                Console.Write(vector.Key + " ");
                PrintDoubleArray(vector.Value);
                Console.WriteLine();
            }
            foreach (var element in golotip.GetExamData.DetectedObjects)
            {
                Console.Write($"{element} ");
            }
            Console.ReadKey();
        }
    }
}
