using System;
using System.Text;
using System.IO;

namespace ExeptionsForMatrix
{
    class Program
    {
        static void Main()
        {
            string stroka = "";
            int sizeMatrix = 0;
            double[,] mass = new double[0,0];
            size(out sizeMatrix);

            int fchois = chois(sizeMatrix);
            if (fchois == -1)
            {
                return;
            }
            m1(sizeMatrix , out stroka);
            int argmnt = arguments(sizeMatrix, stroka, out mass);
            if(argmnt == -1)
            {
                return;
            }
            double[,] massM1 = mass;
            m2(out stroka);
            argmnt = arguments(sizeMatrix, stroka, out mass);
            if (argmnt == -1)
            {
                return;
            }
            double[,] massM2 = mass;

            Matrix matrix = new Matrix(massM1, massM2);
            output(matrix);

            Console.ReadLine();

        }


        static void size(out int sizeMatrix )
        {   try
            {
                Console.WriteLine("Введите количесво строк и столбцов матрицы одной цифрой");
                sizeMatrix = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Возможно вы задали неверный размер матрицы");
                sizeMatrix = 0;
                size(out sizeMatrix);
            }
          
        }

        static int chois(int sizeMatrix)
        {

            try
            {
                Console.WriteLine("Нужно проводить расчеты, или вывести нулевую матрицу?(c/n)");
                string ch = Console.ReadLine();         
                if (ch == "n")
                {
                   getEmpty(sizeMatrix);
                    return -1;
                }
                else if(ch == "c")
                {

                }
                else
                {
                    Console.WriteLine("Возможно вы выбрали неверно, повторите снова");
                        chois(sizeMatrix);
                }

            }
            catch
            {
                Console.WriteLine("Возможно вы выбрали неверно, повторите снова");
                chois(sizeMatrix);
            }
            return 0;
        }

        static void m1(int sizeMatrix, out string stroka)
        {try
            {
                Console.WriteLine("Введите элементы в 1-ую матрицу");
                stroka = Console.ReadLine();
                double z = Convert.ToDouble(stroka);
                string[] exMass = stroka.Split(' '); 
                if(exMass.Length < (sizeMatrix * sizeMatrix))
                {
                    throw new SizeMatrixException($"В 1-ую матрицу введено неверное количество элементов. Количество элементов должно быть равным  {sizeMatrix * sizeMatrix}");
                }
            }
            catch(SizeMatrixException ex)
            {
                //Console.WriteLine("Возможно вы ввели неправельные данные в матрицу 1");
                Console.WriteLine( $"Ошибка: {ex.Message} ") ;
                stroka = "";
                m1(sizeMatrix , out stroka);
            }
        }
        static void m2(out string stroka)
        {
            try
            {
                Console.WriteLine("Введите элементы в 2-ую матрицу");
                stroka = Console.ReadLine();
                double z = Convert.ToDouble(stroka);
            }
            catch
            {
                Console.WriteLine("Возможно вы ввели неправельные данные в матрицу 2");
                stroka = "";
                m2(out stroka);
            }
        }

        static int arguments(int sizeMatrix, string stroka, out double[,] mass )
        {
            mass = new double[sizeMatrix, sizeMatrix];
            int z = Convert.ToInt32(Math.Pow(sizeMatrix, 2));
            string[] stringMass = new string[z];
            double[] doubleMass = new double[z];
            stringMass = stroka.Replace('.', ',').Split(' ');
            if(stringMass.Length != z)
            {
                Console.WriteLine("Вы ввели неверное количество элементов");
                return -1;
            }
            doubleMass = Array.ConvertAll(stringMass, double.Parse);

            
            int dmc = 0;
            for (int y = 0; y < sizeMatrix; y++)
            {
                for (int x = 0; x < sizeMatrix; x++)
                {
                    mass[y, x] = doubleMass[dmc];
                        dmc++;
                }

            }
            for (int y = 0; y < sizeMatrix; y++)
            {
                for (int x = 0; x < sizeMatrix; x++)
                {
                    Console.Write(mass[y, x] + "\t");
                   
                }
                Console.Write("\n");

            }
            Console.Write("\n");
            return 0;
        }

        static void output(Matrix matrix)
        {

            try
            {
                Console.WriteLine("Что нужно вывести: сложение, вычитание, умножение(a/s/m)");
                string ch = Console.ReadLine();
                if (ch == "a")
                {
                    matrix.addition();
                }
                else if (ch == "s")
                {
                    matrix.substraction();
                }
                else if (ch == "m")
                {
                    matrix.multiplication();
                }
                else
                {
                    Console.WriteLine("Возможно вы выбрали неверно, повторите снова");
                    output(matrix);
                }
            }
            catch
            {
                Console.WriteLine("Возможно вы выбрали неверно, повторите снова");
                output(matrix);
            }
        }

        static void getEmpty(int sizeMatrix)
        {
            int ams = sizeMatrix;
            double[,] emptyMass = new double[ams, ams];
            for (int y = 0; y < sizeMatrix; y++)
            {
                for (int x = 0; x < sizeMatrix; x++)
                {
                    emptyMass[y, x] = 0;

                }

            }
            for (int y = 0; y < ams; y++)
            {
                for (int x = 0; x < ams; x++)
                {
                    Console.Write(emptyMass[y, x] + "\t");

                }
                Console.Write("\n");

            }
            Console.Write("\n");
        }

    }

    class Matrix
    {
        double[,] massM1;
        double[,] massM2;

        public Matrix(double[,] massM1, double[,] massM2)
        {
            this.massM1 = massM1;
            this.massM2 = massM2;
        }

        public void addition()
        {
            int ams = Convert.ToInt32(Math.Sqrt(massM1.Length));
            double[,] addMass = new double[ams , ams];
            for (int y = 0; y < ams; y++)
            {
                for (int x = 0; x < ams; x++)
                {
                    addMass[y, x] = massM1[y, x] + massM2[y, x];

                }

            }

            Console.WriteLine("Сумма матриц\n");
            for (int y = 0; y < ams; y++)
            {
                for (int x = 0; x < ams; x++)
                {
                    Console.Write(addMass[y, x] + "\t");

                }
                Console.Write("\n");

            }
            Console.Write("\n");
        }
        public void substraction()
        {
            int ams = Convert.ToInt32(Math.Sqrt(massM1.Length));
            double[,] subMass = new double[ams, ams];
            for (int y = 0; y < ams; y++)
            {
                for (int x = 0; x < ams; x++)
                {
                    subMass[y, x] = massM1[y, x] - massM2[y, x];

                }

            }

            Console.WriteLine("Разность матриц\n");
            for (int y = 0; y < ams; y++)
            {
                for (int x = 0; x < ams; x++)
                {
                    Console.Write(subMass[y, x] + "\t");

                }
                Console.Write("\n");

            }
            Console.Write("\n");
        }

        public void multiplication()
        {
            int ams = Convert.ToInt32(Math.Sqrt(massM1.Length));
            double[,] mulMass = new double[ams, ams];
            int y = 0, x = 0, x1 = 0, y2 = 0, fy = 0, fx = 0;
            mulMass[y + fy, x + fx] = 0;
            for (int y1 = 0; y1 < ams ;y1++)
            {
                fx = 0;
                for(int x2 = 0; x2 < ams ; x2++ )
                {
                    
                    for (int z = 0; z < ams; z++)
                    {

                        mulMass[y + fy, x + fx] = mulMass[y + fy, x + fx] + massM1[y1, x1 + z] * massM2[y2 + z, x2];
                      
                    }
                    fx++;
                }
                fy++;
            }

            Console.WriteLine("Результат умножения матриц\n");
            for (y = 0; y < ams; y++)
            {
                for ( x = 0; x < ams; x++)
                {
                    Console.Write(mulMass[y, x] + "\t");

                }
                Console.Write("\n");

            }
            Console.Write("\n");
        }
    }

    class SizeMatrixException : Exception
    {
        public SizeMatrixException(string message)
            : base(message)
        { }
    }
}
