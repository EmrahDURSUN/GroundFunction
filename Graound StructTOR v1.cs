using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace GrouındCreaTOR
{
    class Program
    {
        private const int givenDiameter = 1;
        private const int givenForce = 100;       
        
        // static class to hold global variables, etc.
        static class Globals
        {
            // Uncomment below if you want, the user to enter Dimentions
            ////Console.WriteLine("Enter width");
            //public static int XWidth = Convert.ToInt32(Console.In.ReadLine());
            ////Console.WriteLine("Enter height");
            //public static int YHeight = Convert.ToInt32(Console.In.ReadLine());
            ////Console.WriteLine("Enter depth");
            //public static int ZDepth = Convert.ToInt32(Console.In.ReadLine());
            ////Console.WriteLine("Enter Node Size");
            //public static int NodeSize = Convert.ToInt32(Console.In.ReadLine());

                // If user will enter dimentions, this four input must coment-out
            // global int
            public static int XWidth = 60;
            public static int YHeight = 60;
            public static int ZDepth = 100;
            public static int NodeSize = 20;

            public static int X = 1;
            public static int XSteps = (XWidth + NodeSize) / NodeSize;
            public static int Y = XSteps;
            public static int YSteps = ((YHeight + NodeSize) / NodeSize);            
            public static int ZSteps = (ZDepth + NodeSize) / NodeSize;
            public static int Z = YSteps * XSteps;            

            public static int TotalNumberOfPoints = XSteps * YSteps * ZSteps;

            public static int Xinc = (XSteps - 1) * X;
            public static int Yinc = (YSteps - 1) * Y;
            public static int Zinc = (ZSteps - 1) * Z;


            public static string n = string.Format("{0:yyyy-MM-dd_hh-mm-ss}", DateTime.Now);
            //File.WriteAllText(n, "aaa");

            //static string SpecialFileName
            //{
            //    get
            //    {
            //        return string.Format("{0}{1}text-{2:yyyy-MM-dd_hh-mm-ss-tt}.txt",Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),Path.DirectorySeparatorChar,DateTime.Now);
            //    }
            //}
            //public static string filePath5 = $@"{SpecialFileName}";

            public static string filePath = $@"E:\\A\\P1\\Latex\\A";
            public static string verzion = $"{XWidth}_{YHeight}_{ZDepth}.csv";

            public static string filePath1 = $@"{filePath}\\{n}_{verzion}_csv1.csv";    // CSV1
            public static string filePath2 = $@"{filePath}\\{n}_{verzion}_csv2.csv";    // CSV2
            public static string filePath3 = $@"{filePath}\\{n}_{verzion}_csv3Helper.csv";    // Helper
            public static string filePathTarget = $@"{filePath}";

            // global function
            public static string HelloWorld()
            {
                //Console.WriteLine(SpecialFileName);
                return "Hello Hennrick";
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine($" {Globals.XSteps} , {Globals.YSteps} , {Globals.ZSteps} ; {Globals.TotalNumberOfPoints} ; \n {Globals.Xinc} , {Globals.Yinc} , {Globals.Zinc} \n  {Globals.X} , {Globals.Y} , {Globals.Z}");
            File.WriteAllText(Globals.n, "aaa");
            CoordinateCreator();
            if (Globals.XSteps > 2 && Globals.YSteps > 2){CenterPointCreator();}
            Corners();
            if (Globals.XSteps > 2 && Globals.YSteps > 2 ){FronBackMidSurface();}
            if (Globals.YSteps > 2 && Globals.ZSteps > 2){LeftRightMidSurface();}
            if (Globals.XSteps > 2 && Globals.YSteps > 2 && Globals.ZSteps > 2){BottomUpMidSurface();}
            if (Globals.ZSteps > 2){DepthEdges();}
            if (Globals.XSteps > 2){HorizontalEdges();}
            if (Globals.YSteps > 2){VerticalEdges();} 
            ExecuHelper();
            
        }
        // Csv 2 File equivalent to Coordinate Points Hinges
        private static void CoordinateCreator()
        {            
            // Creating Coordinate Points
            using FileStream fs0 = new FileStream(Globals.filePath2, FileMode.Append);
            {
                using StreamWriter sw = new StreamWriter(fs0);
                {
                    int X = Globals.X; int XSteps = Globals.XSteps; int Xinc = Globals.Xinc;
                    int Y = Globals.Y; int YSteps = Globals.YSteps; int Yinc = Globals.Yinc;
                    int Z = Globals.Z; int ZSteps = Globals.ZSteps; int Zinc = Globals.Zinc;
                    int NodeSize = Globals.NodeSize;                 

                    int[,,] grid = new int[ZSteps, YSteps, XSteps];

                    int po = 1, orx = 0, ory = 0, orz = 0;

                    for (int a = 0; a < ZSteps; a++)
                    {
                        for (int b = 0; b < YSteps; b++)
                        {
                            for (int c = 0; c < XSteps; c++)
                            {
                                sw.WriteLine($"P{po};{orx};{ory};{orz}");
                                orx += NodeSize;
                                po++;
                            }
                            orx = 0;
                            ory += NodeSize;
                        }
                        ory = 0;
                        orz += NodeSize;
                    }
                }
            }
        }

        // Corners of Ground Structure
        private static void Corners() 
        {
            
                 //  Corners 7 operations
            using FileStream fs5 = new FileStream(Globals.filePath1, FileMode.Append);
            {
                using StreamWriter sw = new StreamWriter(fs5);
                {

                    int X = Globals.X; int XSteps = Globals.XSteps; int Xinc = Globals.Xinc;
                    int Y = Globals.Y; int YSteps = Globals.YSteps; int Yinc = Globals.Yinc;
                    int Z = Globals.Z; int ZSteps = Globals.ZSteps; int Zinc = Globals.Zinc;
                    int NodeSize = Globals.NodeSize;

                    int CornerStart = 1;

                    int rowY = YSteps;
                    int columnX = XSteps;
                    int plane = ZSteps;
                   
                    // Selecting Corner Points
                    int[,,] grid = new int[plane, rowY, columnX];
                    for (int i = 0; i < plane; i++)
                    {
                        for (int j = 0; j < rowY; j++)
                        {
                            for (int k = 0; k < columnX; k++)
                            {
                                grid[i, j, k] = CornerStart;

                                if (CornerStart == 1)  // Front Left Down
                                {
                                    int first = grid[i, j, k] + X; string fullText1 = (grid[i, j, k] + ";" + first + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText1);
                                    int two = grid[i, j, k] + Y; string fullText2 = (grid[i, j, k] + ";" + two + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText2);
                                    int three = grid[i, j, k] + Z; string fullText3 = (grid[i, j, k] + ";" + three + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText3);
                                    int four = grid[i, j, k] + X + Y; string fullText4 = (grid[i, j, k] + ";" + four + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText4);
                                    int five = grid[i, j, k] + X + Z; string fullText5 = (grid[i, j, k] + ";" + five + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText5);
                                    int six = grid[i, j, k] + Y + Z; string fullText6 = (grid[i, j, k] + ";" + six + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText6);
                                    int seven = grid[i, j, k] + X + Y + Z; string fullText7 = (grid[i, j, k] + ";" + seven + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText7);
                                }

                                if (CornerStart == XSteps) // Front Right Down
                                {
                                    int first = grid[i, j, k] - X; string fullText1 = (grid[i, j, k] + ";" + first + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText1);
                                    int two = grid[i, j, k] + Y; string fullText2 = (grid[i, j, k] + ";" + two + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText2);
                                    int three = grid[i, j, k] + Z; string fullText3 = (grid[i, j, k] + ";" + three + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText3);
                                    int four = grid[i, j, k] - X + Y; string fullText4 = (grid[i, j, k] + ";" + four + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText4);
                                    int five = grid[i, j, k] - X + Z; string fullText5 = (grid[i, j, k] + ";" + five + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText5);
                                    int six = grid[i, j, k] + Y + Z; string fullText6 = (grid[i, j, k] + ";" + six + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText6);
                                    int seven = grid[i, j, k] - X + Y + Z; string fullText7 = (grid[i, j, k] + ";" + seven + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText7);
                                }

                                if (grid[i, j, k] == Yinc + 1) //  Front Left Up
                                {
                                    //Console.WriteLine($"   Front Left Up =  {grid[i, j, k]}  ");
                                    int first = grid[i, j, k] + X; string fullText1 = (grid[i, j, k] + ";" + first + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText1);
                                    int two = grid[i, j, k] - Y; string fullText2 = (grid[i, j, k] + ";" + two + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText2);
                                    int three = grid[i, j, k] + Z; string fullText3 = (grid[i, j, k] + ";" + three + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText3);
                                    int four = grid[i, j, k] + X - Y; string fullText4 = (grid[i, j, k] + ";" + four + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText4);
                                    int five = grid[i, j, k] + X + Z; string fullText5 = (grid[i, j, k] + ";" + five + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText5);
                                    int six = grid[i, j, k] - Y + Z; string fullText6 = (grid[i, j, k] + ";" + six + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText6);
                                    int seven = grid[i, j, k] + X - Y + Z; string fullText7 = (grid[i, j, k] + ";" + seven + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText7);
                                }

                                if (grid[i, j, k] == YSteps * XSteps) // Front Right Up
                                {
                                  //  Console.WriteLine($" be care fulllllllllllllllllll   =  {grid[i, j, k]}  ");
                                    // X = (X * (-1)); Y = (Y * (-1));
                                    int first = grid[i, j, k] - X; string fullText1 = (grid[i, j, k] + ";" + first + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText1);
                                    int two = grid[i, j, k] - Y; string fullText2 = (grid[i, j, k] + ";" + two + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText2);
                                    int three = grid[i, j, k] + Z; string fullText3 = (grid[i, j, k] + ";" + three + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText3);
                                    int four = grid[i, j, k] - X - Y; string fullText4 = (grid[i, j, k] + ";" + four + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText4);
                                    int five = grid[i, j, k] - X + Z; string fullText5 = (grid[i, j, k] + ";" + five + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText5);
                                    int six = grid[i, j, k] - Y + Z; string fullText6 = (grid[i, j, k] + ";" + six + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText6);
                                    int seven = grid[i, j, k] - X - Y + Z; string fullText7 = (grid[i, j, k] + ";" + seven + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText7);
                                }

                                if (grid[i, j, k] == Zinc + 1) // Back Left Down
                                {
                                    // Z = (Z * (-1));
                                   // Console.WriteLine($" Back Left Down  =  {grid[i, j, k]}  ");
                                    int first = grid[i, j, k] + X; string fullText1 = (grid[i, j, k] + ";" + first + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText1);
                                    int two = grid[i, j, k] + Y; string fullText2 = (grid[i, j, k] + ";" + two + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText2);
                                    int three = grid[i, j, k] - Z; string fullText3 = (grid[i, j, k] + ";" + three + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText3);
                                    int four = grid[i, j, k] + X + Y; string fullText4 = (grid[i, j, k] + ";" + four + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText4);
                                    int five = grid[i, j, k] + X - Z; string fullText5 = (grid[i, j, k] + ";" + five + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText5);
                                    int six = grid[i, j, k] + Y - Z; string fullText6 = (grid[i, j, k] + ";" + six + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText6);
                                    int seven = grid[i, j, k] + X + Y - Z; string fullText7 = (grid[i, j, k] + ";" + seven + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText7);
                                }

                                if (grid[i, j, k] == (Zinc + Xinc + 1)) // Back Right Down
                                {
                                    // X = (X * (-1)); Z = (Z * (-1));
                                    //Console.WriteLine($"  Back Right Down =  {grid[i, j, k]}  ");
                                    int first = grid[i, j, k] - X; string fullText1 = (grid[i, j, k] + ";" + first + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText1);
                                    int two = grid[i, j, k] + Y; string fullText2 = (grid[i, j, k] + ";" + two + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText2);
                                    int three = grid[i, j, k] - Z; string fullText3 = (grid[i, j, k] + ";" + three + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText3);
                                    int four = grid[i, j, k] - X + Y; string fullText4 = (grid[i, j, k] + ";" + four + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText4);
                                    int five = grid[i, j, k] - X - Z; string fullText5 = (grid[i, j, k] + ";" + five + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText5);
                                    int six = grid[i, j, k] + Y - Z; string fullText6 = (grid[i, j, k] + ";" + six + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText6);
                                    int seven = grid[i, j, k] - X + Y - Z; string fullText7 = (grid[i, j, k] + ";" + seven + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText7);
                                }

                                if (grid[i, j, k] == (Zinc + Yinc + 1)) // Back Left Up
                                {
                                    // Y = (Y * (-1)); Z = (Z * (-1));
                                    int first = grid[i, j, k] + X; string fullText1 = (grid[i, j, k] + ";" + first + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText1);
                                    int two = grid[i, j, k] - Y; string fullText2 = (grid[i, j, k] + ";" + two + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText2);
                                    int three = grid[i, j, k] - Z; string fullText3 = (grid[i, j, k] + ";" + three + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText3);
                                    int four = grid[i, j, k] + X - Y; string fullText4 = (grid[i, j, k] + ";" + four + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText4);
                                    int five = grid[i, j, k] + X - Z; string fullText5 = (grid[i, j, k] + ";" + five + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText5);
                                    int six = grid[i, j, k] - Y - Z; string fullText6 = (grid[i, j, k] + ";" + six + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText6);
                                    int seven = grid[i, j, k] + X - Y - Z; string fullText7 = (grid[i, j, k] + ";" + seven + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText7);
                                }

                                if (grid[i, j, k] == (Zinc + Yinc + Xinc + 1)) // Back Right Up
                                {
                                    // X = (X * (-1)); Y = (Y * (-1)); Z = (Z * (-1));  int X = X < 0 ? X * -1 : X * 1;

                                    int first = grid[i, j, k] - X; string fullText1 = (grid[i, j, k] + ";" + first + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText1);
                                    int two = grid[i, j, k] - Y; string fullText2 = (grid[i, j, k] + ";" + two + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText2);
                                    int three = grid[i, j, k] - Z; string fullText3 = (grid[i, j, k] + ";" + three + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText3);
                                    int four = grid[i, j, k] - X - Y; string fullText4 = (grid[i, j, k] + ";" + four + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText4);
                                    int five = grid[i, j, k] - X - Z; string fullText5 = (grid[i, j, k] + ";" + five + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText5);
                                    int six = grid[i, j, k] - Y - Z; string fullText6 = (grid[i, j, k] + ";" + six + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText6);
                                    int seven = grid[i, j, k] - X - Y - Z; string fullText7 = (grid[i, j, k] + ";" + seven + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText7);
                                }
                                CornerStart++;
                            }
                        }
                    }
                }
            }
        }

        //  Inner Point 26 operation
        private static void CenterPointCreator() 
        {
            
            using FileStream fs4 = new FileStream(Globals.filePath1, FileMode.Append);
            {
                using StreamWriter sw = new StreamWriter(fs4);
                {
                    int X = Globals.X; int XSteps = Globals.XSteps; int Xinc = Globals.Xinc;
                    int Y = Globals.Y; int YSteps = Globals.YSteps; int Yinc = Globals.Yinc;
                    int Z = Globals.Z; int ZSteps = Globals.ZSteps; int Zinc = Globals.Zinc;
                    int NodeSize = Globals.NodeSize;

                    int CenterStart = XSteps * (YSteps + 1) + 2;

                    int rowY    = YSteps - 2;
                    int columnX = XSteps - 2;
                    int plane   = ZSteps - 2;

                    // Selecting Inner Points
                    int[,,] grid = new int[plane, rowY, columnX];

                    // This loop will formulate and create the location of point, and assign the the id for it
                    for (int i = 0; i < plane; i++)
                    {
                        for (int j = 0; j < rowY; j++)
                        {
                            for (int k = 0; k < columnX; k++)
                            {
                                grid[i, j, k] = CenterStart;
                                CenterStart++;                                
                            }
                            CenterStart += 2;
                        }
                        CenterStart += (2 * Y);
                    }

                    // Creating every possible beam from inner points
                    for (int a = 0; a < plane; a++)
                    {
                        for (int b = 0; b < rowY; b++)
                        {
                            for (int c = 0; c < columnX; c++)
                            {
                                //int one = grid[a, b, c] + X;
                                //Console.WriteLine(grid[a, b, c] + ";" + one + ";" + givenDiameter + ";" + givenForce);
                                //int one         = grid[a, b, c] + X;         string fullText1 = (grid[a, b, c] + ";" + one + ";" + givenDiameter + ";" + givenForce);           sw.WriteLine(fullText1);
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X          };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y          };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Z          };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X          };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y          };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Z          };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y      };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y      };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y      };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y      };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Z      };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Z      };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Z      };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Z      };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y + Z      };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y - Z      };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y + Z      };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y - Z      };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y + Z  };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y - Z  };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y + Z  };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y - Z  };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y + Z  };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y - Z  };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y + Z  };{givenDiameter};{givenForce}");
                                sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y - Z  };{givenDiameter};{givenForce}");
                            }
                        }
                    }
                }
            }      
        }

        // Surfaces
        private static void FronBackMidSurface() 
        {
            
            //  Front - Back Surface Mid 17 operations
            using FileStream fs6 = new FileStream(Globals.filePath1, FileMode.Append);
            {
                using StreamWriter sw = new StreamWriter(fs6);
                {
                    int X = Globals.X; int XSteps = Globals.XSteps; int Xinc = Globals.Xinc;
                    int Y = Globals.Y; int YSteps = Globals.YSteps; int Yinc = Globals.Yinc;
                    int Z = Globals.Z; int ZSteps = Globals.ZSteps; int Zinc = Globals.Zinc;
                    int NodeSize = Globals.NodeSize;


                    int SurfaceStart = XSteps + 2;

                    int columnX = XSteps - 2;
                    int rowY = YSteps - 2;
                    int plane = 2;
                                        

                    //Console.WriteLine(" Number of Planes = " + plane + " Rows = " + rowY + " Columns = " + columnX);


                    // Selecting Centor Points

                    int[,,] grid = new int[plane, rowY, columnX];

                    for (int i = 0; i < plane; i++)
                    {
                        for (int j = 0; j < rowY; j++)
                        {
                            for (int k = 0; k < columnX; k++)
                            {
                                grid[i, j, k] = SurfaceStart;
                                SurfaceStart++;
                                //Console.Write(grid[i, j, k] + " ; ");
                            }
                            SurfaceStart += 2;
                            //Console.WriteLine(" After First Plans ist over =  " + SurfaceStart);
                        }
                        SurfaceStart = (grid[0, 0, 0] + Zinc);
                        //Console.WriteLine(" PlaneCompleted and next Plane start point defined");
                    }

                    // Creating every possible beam from inner points

                    for (int a = 0; a < plane; a++)
                    {
                        for (int b = 0; b < rowY; b++)
                        {
                            for (int c = 0; c < columnX; c++)
                            {
                                if (a == 0)
                                {
                                   // int oneD = grid[a, b, c] + X;
                                   // Console.WriteLine(grid[a, b, c] + ";" + oneD + ";" + givenDiameter + ";" + givenForce);
                                    int one = grid[a, b, c] + X; string fullText1 = (grid[a, b, c] + ";" + one + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText1);
                                    int two = grid[a, b, c] + Y; string fullText2 = (grid[a, b, c] + ";" + two + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText2);
                                    int three = grid[a, b, c] + Z; string fullText3 = (grid[a, b, c] + ";" + three + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText3);
                                    int four = grid[a, b, c] - X; string fullText4 = (grid[a, b, c] + ";" + four + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText4);
                                    int five = grid[a, b, c] - Y; string fullText5 = (grid[a, b, c] + ";" + five + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText5);
                                    //    int six         = grid[a, b, c] - Z;         string fullText6 = (grid[a, b, c] + ";" + six + ";" + givenDiameter + ";" + givenForce);           sw.WriteLine(fullText6);
                                    int seven = grid[a, b, c] + X + Y; string fullText7 = (grid[a, b, c] + ";" + seven + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText7);
                                    int eight = grid[a, b, c] + X - Y; string fullText8 = (grid[a, b, c] + ";" + eight + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText8);
                                    int nine = grid[a, b, c] - X + Y; string fullText9 = (grid[a, b, c] + ";" + nine + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText9);
                                    int ten = grid[a, b, c] - X - Y; string fullText10 = (grid[a, b, c] + ";" + ten + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText10);
                                    int eleven = grid[a, b, c] + X + Z; string fullText11 = (grid[a, b, c] + ";" + eleven + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText11);
                                    //    int twelve      = grid[a, b, c] + X - Z;     string fullText12 = (grid[a, b, c] + ";" + twelve + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText12);
                                    int thirteen = grid[a, b, c] - X + Z; string fullText13 = (grid[a, b, c] + ";" + thirteen + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText13);
                                    //    int fourteen    = grid[a, b, c] - X - Z;     string fullText14 = (grid[a, b, c] + ";" + fourteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText14);
                                    int fifteen = grid[a, b, c] + Y + Z; string fullText15 = (grid[a, b, c] + ";" + fifteen + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText15);
                                    //    int sixteen     = grid[a, b, c] + Y - Z;     string fullText16 = (grid[a, b, c] + ";" + sixteen + ";" + givenDiameter + ";" + givenForce);      sw.WriteLine(fullText16);
                                    int seventeen = grid[a, b, c] - Y + Z; string fullText17 = (grid[a, b, c] + ";" + seventeen + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText17);
                                    //    int eightteen   = grid[a, b, c] - Y - Z;     string fullText18 = (grid[a, b, c] + ";" + eightteen + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText18);
                                    int nineteen = grid[a, b, c] + X + Y + Z; string fullText19 = (grid[a, b, c] + ";" + nineteen + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText19);
                                    //   int twenty      = grid[a, b, c] + X + Y - Z; string fullText20 = (grid[a, b, c] + ";" + twenty + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText20);
                                    int twentyone = grid[a, b, c] + X - Y + Z; string fullText21 = (grid[a, b, c] + ";" + twentyone + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText21);
                                    //    int twentytwo   = grid[a, b, c] + X - Y - Z; string fullText22 = (grid[a, b, c] + ";" + twentytwo + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText22);
                                    int twentythree = grid[a, b, c] - X + Y + Z; string fullText23 = (grid[a, b, c] + ";" + twentythree + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText23);
                                    //    int twentyfour  = grid[a, b, c] - X + Y - Z; string fullText24 = (grid[a, b, c] + ";" + twentyfour + ";" + givenDiameter + ";" + givenForce);   sw.WriteLine(fullText24);
                                    int twentyfive = grid[a, b, c] - X - Y + Z; string fullText25 = (grid[a, b, c] + ";" + twentyfive + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText25);
                                    //    int twentysix   = grid[a, b, c] - X - Y - Z; string fullText26 = (grid[a, b, c] + ";" + twentysix + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText26);}

                                }

                                if (a == 1)
                                {
                                    //int one = grid[a, b, c] + X;
                                    //Console.WriteLine(grid[a, b, c] + ";" + one + ";" + givenDiameter + ";" + givenForce);
                                    int one = grid[a, b, c] + X; string fullText1 = (grid[a, b, c] + ";" + one + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText1);
                                    int two = grid[a, b, c] + Y; string fullText2 = (grid[a, b, c] + ";" + two + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText2);
                                    //  int three       = grid[a, b, c] + Z;         string fullText3 = (grid[a, b, c] + ";" + three + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText3);
                                    int four = grid[a, b, c] - X; string fullText4 = (grid[a, b, c] + ";" + four + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText4);
                                    int five = grid[a, b, c] - Y; string fullText5 = (grid[a, b, c] + ";" + five + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText5);
                                    int six = grid[a, b, c] - Z; string fullText6 = (grid[a, b, c] + ";" + six + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText6);
                                    int seven = grid[a, b, c] + X + Y; string fullText7 = (grid[a, b, c] + ";" + seven + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText7);
                                    int eight = grid[a, b, c] + X - Y; string fullText8 = (grid[a, b, c] + ";" + eight + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText8);
                                    int nine = grid[a, b, c] - X + Y; string fullText9 = (grid[a, b, c] + ";" + nine + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText9);
                                    int ten = grid[a, b, c] - X - Y; string fullText10 = (grid[a, b, c] + ";" + ten + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText10);
                                    //    int eleven      = grid[a, b, c] + X + Z;     string fullText11 = (grid[a, b, c] + ";" + eleven + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText11);
                                    int twelve = grid[a, b, c] + X - Z; string fullText12 = (grid[a, b, c] + ";" + twelve + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText12);
                                    //    int thirteen    = grid[a, b, c] - X + Z;     string fullText13 = (grid[a, b, c] + ";" + thirteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText13);
                                    int fourteen = grid[a, b, c] - X - Z; string fullText14 = (grid[a, b, c] + ";" + fourteen + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText14);
                                    //    int fifteen     = grid[a, b, c] + Y + Z;     string fullText15 = (grid[a, b, c] + ";" + fifteen + ";" + givenDiameter + ";" + givenForce);      sw.WriteLine(fullText15);
                                    int sixteen = grid[a, b, c] + Y - Z; string fullText16 = (grid[a, b, c] + ";" + sixteen + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText16);
                                    //    int seventeen   = grid[a, b, c] - Y + Z;     string fullText17 = (grid[a, b, c] + ";" + seventeen + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText17);
                                    int eightteen = grid[a, b, c] - Y - Z; string fullText18 = (grid[a, b, c] + ";" + eightteen + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText18);
                                    //    int nineteen    = grid[a, b, c] + X + Y + Z; string fullText19 = (grid[a, b, c] + ";" + nineteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText19);
                                    int twenty = grid[a, b, c] + X + Y - Z; string fullText20 = (grid[a, b, c] + ";" + twenty + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText20);
                                    //   int twentyone   = grid[a, b, c] + X - Y + Z; string fullText21 = (grid[a, b, c] + ";" + twentyone + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText21);
                                    int twentytwo = grid[a, b, c] + X - Y - Z; string fullText22 = (grid[a, b, c] + ";" + twentytwo + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText22);
                                    //    int twentythree = grid[a, b, c] - X + Y + Z; string fullText23 = (grid[a, b, c] + ";" + twentythree + ";" + givenDiameter + ";" + givenForce);  sw.WriteLine(fullText23);
                                    int twentyfour = grid[a, b, c] - X + Y - Z; string fullText24 = (grid[a, b, c] + ";" + twentyfour + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText24);
                                    //    int twentyfive  = grid[a, b, c] - X - Y + Z; string fullText25 = (grid[a, b, c] + ";" + twentyfive + ";" + givenDiameter + ";" + givenForce);   sw.WriteLine(fullText25);
                                    int twentysix = grid[a, b, c] - X - Y - Z; string fullText26 = (grid[a, b, c] + ";" + twentysix + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText26);
                                }

                            }
                        }
                    }
                }
            }
        }
        private static void LeftRightMidSurface()
        {
          
            //  Left - Right Surface Mid 17 operations
            using FileStream fs7 = new FileStream(Globals.filePath1, FileMode.Append);
            {
                using StreamWriter sw = new StreamWriter(fs7);
                {
                    int X = Globals.X; int XSteps = Globals.XSteps; int Xinc = Globals.Xinc;
                    int Y = Globals.Y; int YSteps = Globals.YSteps; int Yinc = Globals.Yinc;
                    int Z = Globals.Z; int ZSteps = Globals.ZSteps; int Zinc = Globals.Zinc;
                    int NodeSize = Globals.NodeSize;

                    int SurfaceStart = 1 + Y+ Z;


                    // Important this time direction goes to Z
                    int columnZ = ZSteps - 2;
                    int rowY = YSteps - 2;
                    int plane = 2;              
                    
                    // Console.WriteLine("\n" + " Number of Planes = " + plane + " Rows = " + rowY + " Columns = " + columnZ + "\n");

                    // Selecting Centor Points
                    int[,,] grid = new int[plane, columnZ, rowY];

                    for (int i = 0; i < plane; i++)
                    {
                        for (int j = 0; j < columnZ; j++)
                        {
                            for (int k = 0; k < rowY; k++)
                            {
                                grid[i, j, k] = SurfaceStart;
                                SurfaceStart += Y;
                               
                                //Console.Write(grid[i, j, k] + " ; ");
                            }
                            SurfaceStart += (2 * Y);                          
                        }
                        SurfaceStart = (grid[0, 0, 0] + Xinc);                      
                    }

                    // Creating every possible beam from inner points

                    for (int a = 0; a < plane; a++)
                    {
                        for (int b = 0; b < columnZ; b++)
                        {
                            for (int c = 0; c < rowY; c++)
                            {
                                if (a == 0)
                                { // Delete All -X 
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Z          };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Z          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y      };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y      };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Z      };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Z      };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y + Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y - Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y + Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y - Z  };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y + Z  };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y - Z  };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y + Z  };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y - Z  };{givenDiameter};{givenForce}");
                                }

                                if (a == 1)
                                { // Delete All +X
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Z          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Z          };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y      };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y      };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Z      };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y - Z      };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y + Z  };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y - Z  };{givenDiameter};{givenForce}");
                                //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y + Z  };{givenDiameter};{givenForce}");
                                //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y - Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y + Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y - Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y + Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y - Z  };{givenDiameter};{givenForce}");
                                }

                            }
                        }
                    }
                }
            }
        }
        private static void BottomUpMidSurface()
        {
            
            //  Bottom - Up Surface Mid 17 operations
            using FileStream fs8 = new FileStream(Globals.filePath1, FileMode.Append);
            {
                using StreamWriter sw = new StreamWriter(fs8);
                {
                    int X = Globals.X; int XSteps = Globals.XSteps; int Xinc = Globals.Xinc;
                    int Y = Globals.Y; int YSteps = Globals.YSteps; int Yinc = Globals.Yinc;
                    int Z = Globals.Z; int ZSteps = Globals.ZSteps; int Zinc = Globals.Zinc;
                    int NodeSize = Globals.NodeSize;

                    int SurfaceStart = (XSteps * YSteps) + 2;

                    int columnX = XSteps - 2;
                    int rowZ = ZSteps - 2;
                    int plane = 2;                   

                    // Selecting Centor Points

                    int[,,] grid = new int[plane, rowZ, columnX];

                    for (int i = 0; i < plane; i++)
                    {
                        for (int j = 0; j < rowZ; j++)
                        {
                            for (int k = 0; k < columnX; k++)
                            {
                                grid[i, j, k] = SurfaceStart;
                                SurfaceStart++;

                                //Console.Write(grid[i, j, k] + " ; ");
                            }
                            SurfaceStart += Z - Xinc +1;                    
                        }
                        SurfaceStart = (grid[0, 0, 0] + Yinc); // Upper level
                    }

                    // Creating every possible beam from inner points

                    for (int a = 0; a < plane; a++)
                    {
                        for (int b = 0; b < rowZ; b++)
                        {
                            for (int c = 0; c < columnX; c++)
                            {
                                if (a == 0)
                                {
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Z          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X          };{givenDiameter};{givenForce}");
                                //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Z          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y      };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y      };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y - Z      };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y + Z      };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y + Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y - Z  };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y + Z  };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y - Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y + Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y - Z  };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y + Z  };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y - Z  };{givenDiameter};{givenForce}");
                                }

                                if (a == 1)
                                {
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X          };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Z          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Z          };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y      };{givenDiameter};{givenForce}");
                                //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Z      };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y + Z      };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y - Z      };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y + Z  };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y - Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y + Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y - Z  };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y + Z  };{givenDiameter};{givenForce}");
                                //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y - Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y + Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y - Z  };{givenDiameter};{givenForce}");
                                }
                            }
                        }
                    }
                }
            }
        }
        
        // Edges
        private static void DepthEdges()
        {
            
            // Horizontal Edges 11 operations
            using FileStream fs10 = new FileStream(Globals.filePath1, FileMode.Append);
            {
                using StreamWriter sw = new StreamWriter(fs10);
                {
                    int X = Globals.X; int XSteps = Globals.XSteps; int Xinc = Globals.Xinc;
                    int Y = Globals.Y; int YSteps = Globals.YSteps; int Yinc = Globals.Yinc;
                    int Z = Globals.Z; int ZSteps = Globals.ZSteps; int Zinc = Globals.Zinc;
                    int NodeSize = Globals.NodeSize;

                    int corfaceStart = (XSteps * YSteps) + 1;

                    int columnZ = ZSteps - 2;
                    int rowY = 2;
                    int plane = 2;


                    //Console.WriteLine(" Number of Planes = " + plane + " Rows = " + rowY + " Columns = " + columnX );
                    // Selecting Centor Points

                    int[,,] grid = new int[plane, columnZ, rowY];

                    for (int i = 0; i < plane; i++)
                    {
                        for (int j = 0; j < columnZ; j++)
                        {
                            for (int k = 0; k < rowY; k++)
                            {
                                grid[i, j, k] = corfaceStart;
                                corfaceStart += Yinc;
                               // Console.Write(grid[i, j, k] + " ; ");
                            }
                            //corfaceStart += (YSteps - 2) * Y + 2;
                            //Console.WriteLine(" BEFORE operation =  " + Z);
                            corfaceStart = corfaceStart + Z + (-2 * Yinc);
                           // Console.WriteLine(" Corface First Plane is over =  " + corfaceStart);
                        }
                        corfaceStart = (grid[0, 0, 0] + Xinc);
                        //Console.WriteLine(" Corface Front Back Bottom Up is done");
                    }

                    // Creating every possible beam from inner points
                    for (int a = 0; a < plane; a++)
                    {
                        for (int b = 0; b < columnZ; b++)
                        {
                            for (int c = 0; c < rowY; c++)
                            {
                                if (a == 0 && c == 0)
                                {
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Z          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Z          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y      };{givenDiameter};{givenForce}");
                                    //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y + Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y - Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y - Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y - Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y - Z  };{givenDiameter};{givenForce}");
                                }

                                if (a == 0 && c == 1)
                                {
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Z          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Z          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y - Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y + Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y - Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y - Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y - Z  };{givenDiameter};{givenForce}");
                                }

                                if (a == 1 && c == 0)
                                {
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Z          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Z          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y - Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y - Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y + Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y - Z  };{givenDiameter};{givenForce}");
                                    //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y - Z  };{givenDiameter};{givenForce}");
                                }

                                if (a == 1 && c == 1)
                                {
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Z          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Z          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y - Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y - Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y - Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y + Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y - Z  };{givenDiameter};{givenForce}");
                                }
                            }
                        }
                    }
                }
            }

        }
        private static void HorizontalEdges()
        {
            
            //  Depth Edges 11 operations
            using FileStream fs9 = new FileStream(Globals.filePath1, FileMode.Append);
            {
                using StreamWriter sw = new StreamWriter(fs9);
                {
                    int X = Globals.X; int XSteps = Globals.XSteps; int Xinc = Globals.Xinc;
                    int Y = Globals.Y; int YSteps = Globals.YSteps; int Yinc = Globals.Yinc;
                    int Z = Globals.Z; int ZSteps = Globals.ZSteps; int Zinc = Globals.Zinc;
                    int NodeSize = Globals.NodeSize;

                    int corfaceStart = 2;

                    int columnX = XSteps - 2;
                    int rowY = 2;
                    int plane = 2;


                    //Console.WriteLine(" Number of Planes = " + plane + " Rows = " + rowY + " Columns = " + columnX );
                    // Selecting Centor Points

                    int[,,] grid = new int[plane, rowY, columnX];

                    for (int i = 0; i < plane; i++)
                    {
                        for (int j = 0; j < rowY; j++)
                        {
                            for (int k = 0; k < columnX; k++)
                            {
                                grid[i, j, k] = corfaceStart;
                                corfaceStart++;
                                //Console.Write(grid[i, j, k] + " ; ");
                            }
                            corfaceStart += (YSteps - 2) * Y + 2;
                            // Console.WriteLine(" Corface First Plane is over =  " + corfaceStart);
                        }
                        corfaceStart = (grid[0, 0, 0] + Zinc);
                        //Console.WriteLine(" Corface Front Back Bottom Up is done");
                    }

                    // Creating every possible beam from inner points
                    for (int a = 0; a < plane; a++)
                    {
                        for (int b = 0; b < rowY; b++)
                        {
                            for (int c = 0; c < columnX; c++)
                            {
                                if (a == 0 && b == 0)
                                {
                                    //int one = grid[a, b, c] + X;
                                    //Console.WriteLine(grid[a, b, c] + ";" + one + ";" + givenDiameter + ";" + givenForce);
                                    //int one         = grid[a, b, c] + X;         string fullText1 = (grid[a, b, c] + ";" + one + ";" + givenDiameter + ";" + givenForce);           sw.WriteLine(fullText1);

                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Z          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X          };{givenDiameter};{givenForce}");
                                    //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Z          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y - Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y - Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y + Z  };{givenDiameter};{givenForce}");
                                    //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y - Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y - Z  };{givenDiameter};{givenForce}");
                                }

                                if (a == 0 && b == 1)
                                {
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Z          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Z          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Z      };{givenDiameter};{givenForce}");
                                    //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y - Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y - Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y - Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y - Z  };{givenDiameter};{givenForce}");
                                }

                                if (a == 1 && b == 0)
                                {
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Z          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Z          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y      };{givenDiameter};{givenForce}");
                                    //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y      };{givenDiameter};{givenForce}");
                                    //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y      };{givenDiameter};{givenForce}");
                                    //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y - Z      };{givenDiameter};{givenForce}");
                                    //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y + Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y - Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y - Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y + Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y - Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y - Z  };{givenDiameter};{givenForce}");
                                }

                                if (a == 1 && b == 1)
                                {
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Z          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Z          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y - Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y + Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y - Z  };{givenDiameter};{givenForce}");
                                    //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y - Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y + Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y - Z  };{givenDiameter};{givenForce}");
                                }
                            }
                        }
                    }
                }
            }
        }
        private static void VerticalEdges()
        {
            
            //  Vertical Edges 11 operations
            using FileStream fs11 = new FileStream(Globals.filePath1, FileMode.Append);
            {
                using StreamWriter sw = new StreamWriter(fs11);
                {
                    int X = Globals.X; int XSteps = Globals.XSteps; int Xinc = Globals.Xinc;
                    int Y = Globals.Y; int YSteps = Globals.YSteps; int Yinc = Globals.Yinc;
                    int Z = Globals.Z; int ZSteps = Globals.ZSteps; int Zinc = Globals.Zinc;
                    int NodeSize = Globals.NodeSize;

                    int corfaceStart = Y + 1;

                    int columnX = 2;
                    int rowY = YSteps - 2;
                    int plane = 2;


                    //Console.WriteLine(" Number of Planes = " + plane + " Rows = " + rowY + " Columns = " + columnX );
                    // Selecting Centor Points

                    int[,,] grid = new int[plane, columnX, rowY];

                    for (int i = 0; i < plane; i++)
                    {
                        for (int j = 0; j < columnX; j++)
                        {
                            for (int k = 0; k < rowY; k++)
                            {
                                grid[i, j, k] = corfaceStart;
                                corfaceStart += Y;
                                //Console.Write(grid[i, j, k] + " ; ");
                            }

                            corfaceStart += Xinc - (rowY * Y);
                            //Console.WriteLine("Corface First Plane is over =  " + corfaceStart);
                        }
                        corfaceStart = (grid[0, 0, 0] + Zinc);
                        //Console.WriteLine("Corface Front Back Bottom Up is done");
                    }

                    // Creating every possible beam from inner points
                    for (int a = 0; a < plane; a++)
                    {
                        for (int b = 0; b < columnX; b++)
                        {
                            for (int c = 0; c < rowY; c++)
                            {
                                if (a == 0 && b == 0)
                                {
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Z          };{givenDiameter};{givenForce}");
                                    //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Z          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Z      };{givenDiameter};{givenForce}");
                                    //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y - Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y - Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y + Z  };{givenDiameter};{givenForce}");
                                    //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y - Z  };{givenDiameter};{givenForce}");
                                    //     sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y - Z  };{givenDiameter};{givenForce}");
                                }

                                if (a == 0 && b == 1)
                                {
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Z          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Z          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y - Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y + Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y - Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y + Z  };{givenDiameter};{givenForce}");
                                    //     sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y - Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y - Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y - Z  };{givenDiameter};{givenForce}");
                                }

                                if (a == 1 && b == 0)
                                {
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Z          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Z          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Z      };{givenDiameter};{givenForce}");
                                    //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Z      };{givenDiameter};{givenForce}");
                                    //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y + Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y - Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y + Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y - Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y - Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y - Z  };{givenDiameter};{givenForce}");
                                }

                                if (a == 1 && b == 1)
                                {
                                    //     sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y          };{givenDiameter};{givenForce}");
                                    //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Z          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y          };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Z          };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y      };{givenDiameter};{givenForce}");
                                    //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Z      };{givenDiameter};{givenForce}");
                                    //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + Y - Z      };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y + Z      };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - Y - Z      };{givenDiameter};{givenForce}");
                                    //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y + Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X + Y - Z  };{givenDiameter};{givenForce}");
                                    //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y + Z  };{givenDiameter};{givenForce}");
                                    //   sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] + X - Y - Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y + Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X + Y - Z  };{givenDiameter};{givenForce}");
                                    //    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y + Z  };{givenDiameter};{givenForce}");
                                    sw.WriteLine($"{grid[a, b, c]};{ grid[a, b, c] - X - Y - Z  };{givenDiameter};{givenForce}");
                                }
                            }
                        }
                    }
                }
            }
        }

        // Help File Creation
        private static void ExecuHelper()
        {         
           
   
            using FileStream fs12 = new FileStream(Globals.filePath3, FileMode.Append);
            {
                using StreamWriter sw = new StreamWriter(fs12);
                {
                    int X = Globals.X; int XSteps = Globals.XSteps; int Xinc = Globals.Xinc;
                    int Y = Globals.Y; int YSteps = Globals.YSteps; int Yinc = Globals.Yinc;
                    int Z = Globals.Z; int ZSteps = Globals.ZSteps; int Zinc = Globals.Zinc;
                    int NodeSize = Globals.NodeSize;

                    int topSurfStart = Yinc + 1;

                    int columnX = XSteps;
                    int rowY = YSteps;
                    int plane = ZSteps;

                    int valueForce = 4, scalingHightForces = 3, scalingWidthForcesi = 3, scalingBeams = 3;


                    //Console.WriteLine(" Number of Planes = " + plane + " Rows = " + rowY + " Columns = " + columnX );
                    // Selecting Centor Points
                    int[,] grid = new int[plane, columnX];

                    int numberOfLoads = XSteps * ZSteps;

                    sw.Write($"CreateTex.exe {XSteps} {YSteps} {ZSteps} {NodeSize} 4 P{1} P{1 + Xinc} P{1 + Zinc} P{1 + Xinc + Zinc} {numberOfLoads}");


                    // Allocation of Loads
                    for (int j = 0; j < plane; j++)
                    {
                        for (int k = 0; k < columnX; k++)
                        {
                            grid[j, k] = topSurfStart;
                            topSurfStart++;

                            sw.Write($" P{grid[j, k]}");
                        }
                        topSurfStart += Yinc;
                    }

                    // Assigning the same size-value for each Load
                    for (int k = 0; k < XSteps * ZSteps; k++)
                    {
                        sw.Write($" {valueForce}");
                    }

                    sw.Write($" {scalingHightForces} {scalingWidthForcesi} {scalingBeams} {Globals.filePath} {Globals.filePath1} {Globals.filePath2} {Globals.filePathTarget}");

                    /* CreateTex.exe <X> < Y > < Z > < distance nodes > < amount bearings > << position of bearings>> 
                     * < amount forces > << position forces >> << value forces >> < visual scaling hight forces > < visual scaling width forces > < visual scaling beams>
                     * < path tex file> < path csv1 > < path csv2 > < path workingDirectory >
                    */
                }
            }

        }
    }
}