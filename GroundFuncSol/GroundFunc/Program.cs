using System;
using System.IO;

namespace GrouındCreaTOR
{
    class Program
    {
        private const int givenDiameter = 1;
        private const int givenForce = 100;
        private const int givenDiameter2 = 3;
        private const int givenForce2 = 200;
        private const int givenDiameter3 = 5;
        private const int givenForce3 = 300;

        static void Main(string[] args)
        {
            //Console.WriteLine("Enter width");
            //int XWidth = Convert.ToInt32(Console.In.ReadLine());
            //Console.WriteLine("Enter height");
            //int YHeight = Convert.ToInt32(Console.In.ReadLine());
            //Console.WriteLine("Enter depth");
            //int ZDepth = Convert.ToInt32(Console.In.ReadLine());
            //Console.WriteLine("Enter Node Size");
            //int NodeSize = Convert.ToInt32(Console.In.ReadLine());

            int XWidth = 120;
            int YHeight = 80;
            int ZDepth = 60;
            int NodeSize = 20;

            //int XWidth = 100;
            //int YHeight = 100;
            //int ZDepth = 100;
            //int NodeSize = 20;

            Console.WriteLine(XWidth + " " + YHeight + " " + ZDepth + " " + NodeSize);

            // CALCULATION TOTAL NUMBER OF POINT
            int XSize   = 1;
            int XSteps  = (XWidth + NodeSize) / NodeSize;

            int YSize   = XSteps;
            int YSteps  = ((YHeight + NodeSize) / NodeSize);

            int ZSize   = ((YHeight + NodeSize) / NodeSize) * XSteps;
            int ZSteps  = (ZDepth + NodeSize) / NodeSize;

            int TotalNumberOfPoints = XSteps * YSteps * ZSteps;

            Console.WriteLine("Xsize= " + XSize + "\n XSteps=" + XSteps + "\n YSize=" + YSize + "\n YSteps=" + YSteps + "\n ZSize=" + ZSize + "\n ZSteps=" + ZSteps + "\n Total Number Of Points=" + TotalNumberOfPoints);

            //int CalStartPoint = 0;
            //int CalEndPoint = 0;

            var filePath = @"E:\B\Latex\csv1.csv";

            Console.WriteLine("Hello World 1 ");

            // X DIRECTION INCREASE
            using FileStream fs1 = new FileStream(filePath, FileMode.Append);
            {
                using StreamWriter sw = new StreamWriter(fs1);
                {
                    int tot = YSteps * ZSteps;

                    int[] CalStartPoint = new int[tot];
                    CalStartPoint[0] = 1;

                    int[] CalEndPoint = new int[tot];
                    CalEndPoint[0] = CalStartPoint[0] + XSteps - 1;

                    for (int i = 1; i < CalStartPoint.Length; i++)
                    {
                        CalStartPoint[i] = CalEndPoint[i - 1] + 1;
                        CalEndPoint[i] = CalStartPoint[i] + (XSteps - 1);
                    }
                    // now we are writing them down to csv file
                    for (int m = 0; m < CalStartPoint.Length; m++)
                    {
                        string fullText = (CalStartPoint[m] + ";" + CalEndPoint[m] + ";" + givenDiameter + ";" + givenForce);
                        sw.WriteLine(fullText);
                    }
                }
            }
            // Y DIRECTION INCREASE
            using FileStream fs2 = new FileStream(filePath, FileMode.Append);
            {
                using StreamWriter sw2 = new StreamWriter(fs2);
                {
                    int Yincrease = YSize * (YSteps - 1);
                    int tot = XSteps * ZSteps; // 18

                    int Start2 = 1;
                    int End2 = 1 + Yincrease;

                    for (int j = 0; j < ZSteps; j++)
                    {
                        for (int i = 0; i < XSteps; i++)
                        {
                            string fullText = (Start2 + ";" + End2 + ";" + givenDiameter2 + ";" + givenForce2);
                            sw2.WriteLine(fullText);
                            Start2++;
                            End2++;
                        }
                        Start2 = (Start2 - XSteps) + (XSteps * YSteps);
                        End2 = (End2 - XSteps) + (XSteps * YSteps);
                    }
                }
            }
            // Z DIRECTION INCREASE
            using FileStream fs3 = new FileStream(filePath, FileMode.Append);
            {
                using StreamWriter sw3 = new StreamWriter(fs3);
                {
                    int Zincrease = ZSize * (ZSteps - 1);
                    int tot = XSteps * YSteps; // 36

                    int Start3 = 1;
                    int End3 = 1 + Zincrease;

                    for (int i = 0; i < tot; i++)
                    {
                        string fullText = (Start3 + ";" + End3 + ";" + givenDiameter3 + ";" + givenForce3);
                        sw3.WriteLine(fullText);
                        Start3++;
                        End3++;
                    }
                }
            }

            //  Inner Point 26 operation
            using FileStream fs4 = new FileStream(filePath, FileMode.Append);
            {
                using StreamWriter sw = new StreamWriter(fs4);
                {

                    int CenterStart = XSteps * (YSteps + 1) + 2;

                    int rowY = YSteps - 2;
                    int columnX = XSteps - 2;
                    int plane = ZSteps - 2;
                    Console.WriteLine("Depth= " + plane + " Rows = " + rowY + " Columns = " + columnX);
                    

                    // Selecting Inner Points

                    int[,,] grid = new int[plane, rowY, columnX ];                    
                    // This loop will formulate and create the location of point, and assign the the id for it
                    for (int i = 0; i < plane; i++)
                    {
                        for (int j = 0; j <rowY ; j++)
                        {
                            for (int k = 0; k < columnX ; k++)
                            {
                                grid[i, j, k] = CenterStart;
                                CenterStart++;
                                //Console.Write(grid[i, j, k] + " ; ");
                            }
                            CenterStart = CenterStart + 2;
                            //Console.WriteLine(" X OVER " + CenterStart);
                        }
                        CenterStart += (2*YSize);
                        //Console.WriteLine(" PLANE Completed ");
                    }

                    int X = XSize , Y= YSize, Z = ZSize;
                    
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
                                //int two         = grid[a, b, c] + Y;         string fullText2 = (grid[a, b, c] + ";" + two  + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText2);
                                //int three       = grid[a, b, c] + Z;         string fullText3 = (grid[a, b, c] + ";" + three + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText3);
                              

                                int four        = grid[a, b, c] - X;         string fullText4 = (grid[a, b, c] + ";" + four + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText4);
                                int five        = grid[a, b, c] - Y;         string fullText5 = (grid[a, b, c] + ";" + five + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText5);
                                int six         = grid[a, b, c] - Z;         string fullText6 = (grid[a, b, c] + ";" + six + ";" + givenDiameter + ";" + givenForce);           sw.WriteLine(fullText6);
                                int seven       = grid[a, b, c] + X + Y;     string fullText7 = (grid[a, b, c] + ";" + seven + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText7);
                                int eight       = grid[a, b, c] + X - Y;     string fullText8 = (grid[a, b, c] + ";" + eight + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText8);
                                int nine        = grid[a, b, c] - X + Y;     string fullText9 = (grid[a, b, c] + ";" + nine + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText9);
                                int ten         = grid[a, b, c] - X - Y;     string fullText10 = (grid[a, b, c] + ";" + ten + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText10);
                                int eleven      = grid[a, b, c] + X + Z;     string fullText11 = (grid[a, b, c] + ";" + eleven + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText11);
                                int twelve      = grid[a, b, c] + X - Z;     string fullText12 = (grid[a, b, c] + ";" + twelve + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText12);
                                int thirteen    = grid[a, b, c] - X + Z;     string fullText13 = (grid[a, b, c] + ";" + thirteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText13);
                                int fourteen    = grid[a, b, c] - X - Z;     string fullText14 = (grid[a, b, c] + ";" + fourteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText14);
                                int fifteen     = grid[a, b, c] + Y + Z;     string fullText15 = (grid[a, b, c] + ";" + fifteen + ";" + givenDiameter + ";" + givenForce);      sw.WriteLine(fullText15);
                                int sixteen     = grid[a, b, c] + Y - Z;     string fullText16 = (grid[a, b, c] + ";" + sixteen + ";" + givenDiameter + ";" + givenForce);      sw.WriteLine(fullText16);
                                int seventeen   = grid[a, b, c] - Y + Z;     string fullText17 = (grid[a, b, c] + ";" + seventeen + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText17);
                                int eightteen   = grid[a, b, c] - Y - Z;     string fullText18 = (grid[a, b, c] + ";" + eightteen + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText18);
                                int nineteen    = grid[a, b, c] + X + Y + Z; string fullText19 = (grid[a, b, c] + ";" + nineteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText19);
                                int twenty      = grid[a, b, c] + X + Y - Z; string fullText20 = (grid[a, b, c] + ";" + twenty + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText20);
                                int twentyone   = grid[a, b, c] + X - Y + Z; string fullText21 = (grid[a, b, c] + ";" + twentyone + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText21);
                                int twentytwo   = grid[a, b, c] + X - Y - Z; string fullText22 = (grid[a, b, c] + ";" + twentytwo + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText22);
                                int twentythree = grid[a, b, c] - X + Y + Z; string fullText23 = (grid[a, b, c] + ";" + twentythree + ";" + givenDiameter + ";" + givenForce);  sw.WriteLine(fullText23);
                                int twentyfour  = grid[a, b, c] - X + Y - Z; string fullText24 = (grid[a, b, c] + ";" + twentyfour + ";" + givenDiameter + ";" + givenForce);   sw.WriteLine(fullText24);
                                int twentyfive  = grid[a, b, c] - X - Y + Z; string fullText25 = (grid[a, b, c] + ";" + twentyfive + ";" + givenDiameter + ";" + givenForce);   sw.WriteLine(fullText25);
                                int twentysix   = grid[a, b, c] - X - Y - Z; string fullText26 = (grid[a, b, c] + ";" + twentysix + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText26);
                            }                                                    
                        }                        
                    }
                }
            }

            //  Corners
            using FileStream fs5 = new FileStream(filePath, FileMode.Append);
            {
                using StreamWriter sw = new StreamWriter(fs5);
                {
                    int CornerStart = 1;
                    int rowY = YSteps;
                    int columnX = XSteps;
                    int plane = ZSteps;

                    int X = XSize, Y = YSize, Z = ZSize;

                    int Xinc = (XSteps - 1) * X;
                    int Yinc = (YSteps - 1) * Y;
                    int Zinc = (ZSteps - 1) * Z;
                    Console.WriteLine("Depth= " + plane + " Rows = " + rowY + " Columns = " + columnX + "\n" + Yinc);
                    // Selecting Corner Points
                    int[,,] grid = new int[plane, rowY, columnX];
                    for (int i = 0; i < plane; i++)
                    {
                        for (int j = 0; j < rowY; j++)
                        {
                            for (int k = 0; k < columnX; k++)
                            {
                                grid[i, j, k] = CornerStart;

                                if (grid[i, j, k] == 1)  // Front Left Down
                                {
                                    // Console.WriteLine(CornerStart + " ; " + grid[i, j, k] + X);
                                    int first = grid[i, j, k] + X       ; string fullText1 = (grid[i, j, k] + ";" + first + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText1);
                                    int two   = grid[i, j, k] + Y       ; string fullText2 = (grid[i, j, k] + ";" + two   + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText2);
                                    int three = grid[i, j, k] + Z       ; string fullText3 = (grid[i, j, k] + ";" + three + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText3);
                                    int four  = grid[i, j, k] + X + Y   ; string fullText4 = (grid[i, j, k] + ";" + four  + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText4);
                                    int five  = grid[i, j, k] + X + Z   ; string fullText5 = (grid[i, j, k] + ";" + five  + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText5);
                                    int six   = grid[i, j, k] + Y + Z   ; string fullText6 = (grid[i, j, k] + ";" + six   + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText6);
                                    int seven = grid[i, j, k] + X + Y +Z; string fullText7 = (grid[i, j, k] + ";" + seven + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText7);                                    
                                }
                                if (grid[i, j, k] == XSteps) // Front Right Down
                                {
                                   // X = (X * (-1));
                                    int first = grid[i, j, k] - X       ; string fullText1 = (grid[i, j, k] + ";" + first + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText1);
                                    int two   = grid[i, j, k] + Y       ; string fullText2 = (grid[i, j, k] + ";" + two   + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText2);
                                    int three = grid[i, j, k] + Z       ; string fullText3 = (grid[i, j, k] + ";" + three + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText3);
                                    int four  = grid[i, j, k] - X + Y   ; string fullText4 = (grid[i, j, k] + ";" + four  + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText4);
                                    int five  = grid[i, j, k] - X + Z   ; string fullText5 = (grid[i, j, k] + ";" + five  + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText5);
                                    int six   = grid[i, j, k] + Y + Z   ; string fullText6 = (grid[i, j, k] + ";" + six   + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText6);
                                    int seven = grid[i, j, k] - X + Y +Z; string fullText7 = (grid[i, j, k] + ";" + seven + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText7);   
                                }
                                if (grid[i, j, k] == Yinc + 1) // Front Left Up
                                {
                                    //Y = (Y * (-1));
                                    int first = grid[i, j, k] + X       ; string fullText1 = (grid[i, j, k] + ";" + first + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText1);
                                    int two   = grid[i, j, k] - Y       ; string fullText2 = (grid[i, j, k] + ";" + two   + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText2);
                                    int three = grid[i, j, k] + Z       ; string fullText3 = (grid[i, j, k] + ";" + three + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText3);
                                    int four  = grid[i, j, k] + X - Y   ; string fullText4 = (grid[i, j, k] + ";" + four  + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText4);
                                    int five  = grid[i, j, k] + X + Z   ; string fullText5 = (grid[i, j, k] + ";" + five  + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText5);
                                    int six   = grid[i, j, k] - Y + Z   ; string fullText6 = (grid[i, j, k] + ";" + six   + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText6);
                                    int seven = grid[i, j, k] + X - Y +Z; string fullText7 = (grid[i, j, k] + ";" + seven + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText7);                                     
                                }

                                if (grid[i, j, k] == YSteps * XSteps) // Front Right Up
                                {
                                   // X = (X * (-1)); Y = (Y * (-1));
                                    int first = grid[i, j, k] - X       ; string fullText1 = (grid[i, j, k] + ";" + first + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText1);
                                    int two   = grid[i, j, k] - Y       ; string fullText2 = (grid[i, j, k] + ";" + two   + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText2);
                                    int three = grid[i, j, k] + Z       ; string fullText3 = (grid[i, j, k] + ";" + three + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText3);
                                    int four  = grid[i, j, k] - X - Y   ; string fullText4 = (grid[i, j, k] + ";" + four  + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText4);
                                    int five  = grid[i, j, k] - X + Z   ; string fullText5 = (grid[i, j, k] + ";" + five  + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText5);
                                    int six   = grid[i, j, k] - Y + Z   ; string fullText6 = (grid[i, j, k] + ";" + six   + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText6);
                                    int seven = grid[i, j, k] - X - Y +Z; string fullText7 = (grid[i, j, k] + ";" + seven + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText7);   
                                }

                                if (grid[i, j, k] == Zinc + 1) // Back Left Down
                                {
                                   // Z = (Z * (-1));
                                    int first = grid[i, j, k] + X       ; string fullText1 = (grid[i, j, k] + ";" + first + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText1);
                                    int two   = grid[i, j, k] + Y       ; string fullText2 = (grid[i, j, k] + ";" + two   + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText2);
                                    int three = grid[i, j, k] - Z       ; string fullText3 = (grid[i, j, k] + ";" + three + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText3);
                                    int four  = grid[i, j, k] + X + Y   ; string fullText4 = (grid[i, j, k] + ";" + four  + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText4);
                                    int five  = grid[i, j, k] + X - Z   ; string fullText5 = (grid[i, j, k] + ";" + five  + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText5);
                                    int six   = grid[i, j, k] + Y - Z   ; string fullText6 = (grid[i, j, k] + ";" + six   + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText6);
                                    int seven = grid[i, j, k] + X + Y -Z; string fullText7 = (grid[i, j, k] + ";" + seven + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText7);                                     
                                }

                                if (grid[i, j, k] == (Zinc + Xinc + 1)) // Back Right Down
                                {
                                   // X = (X * (-1)); Z = (Z * (-1));
                                    int first = grid[i, j, k] - X       ; string fullText1 = (grid[i, j, k] + ";" + first + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText1);
                                    int two   = grid[i, j, k] + Y       ; string fullText2 = (grid[i, j, k] + ";" + two   + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText2);
                                    int three = grid[i, j, k] - Z       ; string fullText3 = (grid[i, j, k] + ";" + three + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText3);
                                    int four  = grid[i, j, k] - X + Y   ; string fullText4 = (grid[i, j, k] + ";" + four  + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText4);
                                    int five  = grid[i, j, k] - X - Z   ; string fullText5 = (grid[i, j, k] + ";" + five  + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText5);
                                    int six   = grid[i, j, k] + Y - Z   ; string fullText6 = (grid[i, j, k] + ";" + six   + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText6);
                                    int seven = grid[i, j, k] - X + Y -Z; string fullText7 = (grid[i, j, k] + ";" + seven + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText7);  
                                }

                                if (grid[i, j, k] == (Zinc + Yinc + 1)) // Back Left Up
                                {
                                   // Y = (Y * (-1)); Z = (Z * (-1));
                                    int first = grid[i, j, k] + X       ; string fullText1 = (grid[i, j, k] + ";" + first + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText1);
                                    int two   = grid[i, j, k] - Y       ; string fullText2 = (grid[i, j, k] + ";" + two   + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText2);
                                    int three = grid[i, j, k] - Z       ; string fullText3 = (grid[i, j, k] + ";" + three + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText3);
                                    int four  = grid[i, j, k] + X - Y   ; string fullText4 = (grid[i, j, k] + ";" + four  + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText4);
                                    int five  = grid[i, j, k] + X - Z   ; string fullText5 = (grid[i, j, k] + ";" + five  + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText5);
                                    int six   = grid[i, j, k] - Y - Z   ; string fullText6 = (grid[i, j, k] + ";" + six   + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText6);
                                    int seven = grid[i, j, k] + X - Y -Z; string fullText7 = (grid[i, j, k] + ";" + seven + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText7);                                    
                                }

                                if (grid[i, j, k] == (Zinc + Yinc + Xinc + 1)) // Back Right Up
                                {
                                    // X = (X * (-1)); Y = (Y * (-1)); Z = (Z * (-1));  int X = X < 0 ? X * -1 : X * 1;
                                                                      
                                    int first = grid[i, j, k] - X       ; string fullText1 = (grid[i, j, k] + ";" + first + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText1);
                                    int two   = grid[i, j, k] - Y       ; string fullText2 = (grid[i, j, k] + ";" + two   + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText2);
                                    int three = grid[i, j, k] - Z       ; string fullText3 = (grid[i, j, k] + ";" + three + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText3);
                                    int four  = grid[i, j, k] - X - Y   ; string fullText4 = (grid[i, j, k] + ";" + four  + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText4);
                                    int five  = grid[i, j, k] - X - Z   ; string fullText5 = (grid[i, j, k] + ";" + five  + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText5);
                                    int six   = grid[i, j, k] - Y - Z   ; string fullText6 = (grid[i, j, k] + ";" + six   + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText6);
                                    int seven = grid[i, j, k] - X - Y -Z; string fullText7 = (grid[i, j, k] + ";" + seven + ";" + givenDiameter + ";" + givenForce); sw.WriteLine(fullText7);                              
                                }
                                CornerStart++;
                            }
                        }                        
                    }
                }
            }

            //  Front - Back Surface Mid 17 operations
            using FileStream fs6 = new FileStream(filePath, FileMode.Append);
            {
                using StreamWriter sw = new StreamWriter(fs6);
                {
                    int SurfaceStart = XSteps + 2;

                    int columnX = XSteps - 2;
                    int rowY = YSteps - 2;
                    int plane = 2;

                    int X = XSize, Y = YSize, Z = ZSize;

                    int Xinc = (XSteps - 1) * X;
                    int Yinc = (YSteps - 1) * Y;
                    int Zinc = (ZSteps - 1) * Z;

                    Console.WriteLine(" Number of Planes = " + plane + " Rows = " + rowY + " Columns = " + columnX );


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
                                Console.Write(grid[i, j, k] + " ; ");
                            }
                            SurfaceStart += 2;
                            Console.WriteLine(" After First Plans ist over =  " + SurfaceStart);
                        }
                        SurfaceStart = (grid[0, 0, 0] + Zinc);
                        Console.WriteLine(" PlaneCompleted and next Plane start point defined");
                    }

                    // Creating every possible beam from inner points

                    for (int a = 0; a < plane; a++)
                    {
                        for (int b = 0; b < rowY; b++)
                        {
                            for (int c = 0; c < columnX; c++)
                            {
                                if (a == 0 )
                                {
                                    //int one = grid[a, b, c] + X;
                                    //Console.WriteLine(grid[a, b, c] + ";" + one + ";" + givenDiameter + ";" + givenForce);
                                    int one         = grid[a, b, c] + X;         string fullText1 = (grid[a, b, c] + ";" + one + ";" + givenDiameter + ";" + givenForce);           sw.WriteLine(fullText1);
                                    int two         = grid[a, b, c] + Y;         string fullText2 = (grid[a, b, c] + ";" + two  + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText2);
                                    int three       = grid[a, b, c] + Z;         string fullText3 = (grid[a, b, c] + ";" + three + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText3);
                                    int four        = grid[a, b, c] - X;         string fullText4 = (grid[a, b, c] + ";" + four + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText4);
                                    int five        = grid[a, b, c] - Y;         string fullText5 = (grid[a, b, c] + ";" + five + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText5);
                                //    int six         = grid[a, b, c] - Z;         string fullText6 = (grid[a, b, c] + ";" + six + ";" + givenDiameter + ";" + givenForce);           sw.WriteLine(fullText6);
                                    int seven       = grid[a, b, c] + X + Y;     string fullText7 = (grid[a, b, c] + ";" + seven + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText7);
                                    int eight       = grid[a, b, c] + X - Y;     string fullText8 = (grid[a, b, c] + ";" + eight + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText8);
                                    int nine        = grid[a, b, c] - X + Y;     string fullText9 = (grid[a, b, c] + ";" + nine + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText9);
                                    int ten         = grid[a, b, c] - X - Y;     string fullText10 = (grid[a, b, c] + ";" + ten + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText10);
                                    int eleven      = grid[a, b, c] + X + Z;     string fullText11 = (grid[a, b, c] + ";" + eleven + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText11);
                                //    int twelve      = grid[a, b, c] + X - Z;     string fullText12 = (grid[a, b, c] + ";" + twelve + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText12);
                                    int thirteen    = grid[a, b, c] - X + Z;     string fullText13 = (grid[a, b, c] + ";" + thirteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText13);
                                //    int fourteen    = grid[a, b, c] - X - Z;     string fullText14 = (grid[a, b, c] + ";" + fourteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText14);
                                    int fifteen     = grid[a, b, c] + Y + Z;     string fullText15 = (grid[a, b, c] + ";" + fifteen + ";" + givenDiameter + ";" + givenForce);      sw.WriteLine(fullText15);
                                //    int sixteen     = grid[a, b, c] + Y - Z;     string fullText16 = (grid[a, b, c] + ";" + sixteen + ";" + givenDiameter + ";" + givenForce);      sw.WriteLine(fullText16);
                                    int seventeen   = grid[a, b, c] - Y + Z;     string fullText17 = (grid[a, b, c] + ";" + seventeen + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText17);
                                //    int eightteen   = grid[a, b, c] - Y - Z;     string fullText18 = (grid[a, b, c] + ";" + eightteen + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText18);
                                    int nineteen    = grid[a, b, c] + X + Y + Z; string fullText19 = (grid[a, b, c] + ";" + nineteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText19);
                                //   int twenty      = grid[a, b, c] + X + Y - Z; string fullText20 = (grid[a, b, c] + ";" + twenty + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText20);
                                    int twentyone   = grid[a, b, c] + X - Y + Z; string fullText21 = (grid[a, b, c] + ";" + twentyone + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText21);
                                //    int twentytwo   = grid[a, b, c] + X - Y - Z; string fullText22 = (grid[a, b, c] + ";" + twentytwo + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText22);
                                    int twentythree = grid[a, b, c] - X + Y + Z; string fullText23 = (grid[a, b, c] + ";" + twentythree + ";" + givenDiameter + ";" + givenForce);  sw.WriteLine(fullText23);
                                //    int twentyfour  = grid[a, b, c] - X + Y - Z; string fullText24 = (grid[a, b, c] + ";" + twentyfour + ";" + givenDiameter + ";" + givenForce);   sw.WriteLine(fullText24);
                                    int twentyfive  = grid[a, b, c] - X - Y + Z; string fullText25 = (grid[a, b, c] + ";" + twentyfive + ";" + givenDiameter + ";" + givenForce);   sw.WriteLine(fullText25);
                                //    int twentysix   = grid[a, b, c] - X - Y - Z; string fullText26 = (grid[a, b, c] + ";" + twentysix + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText26);}
                                
                                }

                                if (a == 1)
                                {                                                            
                                //int one = grid[a, b, c] + X;
                                //Console.WriteLine(grid[a, b, c] + ";" + one + ";" + givenDiameter + ";" + givenForce);
                                int one         = grid[a, b, c] + X;         string fullText1 = (grid[a, b, c] + ";" + one + ";" + givenDiameter + ";" + givenForce);           sw.WriteLine(fullText1);
                                int two         = grid[a, b, c] + Y;         string fullText2 = (grid[a, b, c] + ";" + two  + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText2);
                            //  int three       = grid[a, b, c] + Z;         string fullText3 = (grid[a, b, c] + ";" + three + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText3);
                                int four        = grid[a, b, c] - X;         string fullText4 = (grid[a, b, c] + ";" + four + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText4);
                                int five        = grid[a, b, c] - Y;         string fullText5 = (grid[a, b, c] + ";" + five + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText5);
                                int six         = grid[a, b, c] - Z;         string fullText6 = (grid[a, b, c] + ";" + six + ";" + givenDiameter + ";" + givenForce);           sw.WriteLine(fullText6);
                                int seven       = grid[a, b, c] + X + Y;     string fullText7 = (grid[a, b, c] + ";" + seven + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText7);
                                int eight       = grid[a, b, c] + X - Y;     string fullText8 = (grid[a, b, c] + ";" + eight + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText8);
                                int nine        = grid[a, b, c] - X + Y;     string fullText9 = (grid[a, b, c] + ";" + nine + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText9);
                                int ten         = grid[a, b, c] - X - Y;     string fullText10 = (grid[a, b, c] + ";" + ten + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText10);
                            //    int eleven      = grid[a, b, c] + X + Z;     string fullText11 = (grid[a, b, c] + ";" + eleven + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText11);
                                int twelve      = grid[a, b, c] + X - Z;     string fullText12 = (grid[a, b, c] + ";" + twelve + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText12);
                            //    int thirteen    = grid[a, b, c] - X + Z;     string fullText13 = (grid[a, b, c] + ";" + thirteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText13);
                                int fourteen    = grid[a, b, c] - X - Z;     string fullText14 = (grid[a, b, c] + ";" + fourteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText14);
                            //    int fifteen     = grid[a, b, c] + Y + Z;     string fullText15 = (grid[a, b, c] + ";" + fifteen + ";" + givenDiameter + ";" + givenForce);      sw.WriteLine(fullText15);
                                int sixteen     = grid[a, b, c] + Y - Z;     string fullText16 = (grid[a, b, c] + ";" + sixteen + ";" + givenDiameter + ";" + givenForce);      sw.WriteLine(fullText16);
                            //    int seventeen   = grid[a, b, c] - Y + Z;     string fullText17 = (grid[a, b, c] + ";" + seventeen + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText17);
                                int eightteen   = grid[a, b, c] - Y - Z;     string fullText18 = (grid[a, b, c] + ";" + eightteen + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText18);
                            //    int nineteen    = grid[a, b, c] + X + Y + Z; string fullText19 = (grid[a, b, c] + ";" + nineteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText19);
                                int twenty      = grid[a, b, c] + X + Y - Z; string fullText20 = (grid[a, b, c] + ";" + twenty + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText20);
                            //   int twentyone   = grid[a, b, c] + X - Y + Z; string fullText21 = (grid[a, b, c] + ";" + twentyone + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText21);
                                int twentytwo   = grid[a, b, c] + X - Y - Z; string fullText22 = (grid[a, b, c] + ";" + twentytwo + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText22);
                            //    int twentythree = grid[a, b, c] - X + Y + Z; string fullText23 = (grid[a, b, c] + ";" + twentythree + ";" + givenDiameter + ";" + givenForce);  sw.WriteLine(fullText23);
                                int twentyfour  = grid[a, b, c] - X + Y - Z; string fullText24 = (grid[a, b, c] + ";" + twentyfour + ";" + givenDiameter + ";" + givenForce);   sw.WriteLine(fullText24);
                            //    int twentyfive  = grid[a, b, c] - X - Y + Z; string fullText25 = (grid[a, b, c] + ";" + twentyfive + ";" + givenDiameter + ";" + givenForce);   sw.WriteLine(fullText25);
                                int twentysix   = grid[a, b, c] - X - Y - Z; string fullText26 = (grid[a, b, c] + ";" + twentysix + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText26);}

                            }
                        }
                    }
                }
            }

            //  Left - Right Surface Mid 17 operations
            using FileStream fs7 = new FileStream(filePath, FileMode.Append);
            {
                using StreamWriter sw = new StreamWriter(fs7);
                {
                    int SurfaceStart =( XSteps * (YSteps + 1))+1;


                    // Important this time direction goes to Z
                    int columnZ = ZSteps - 2;
                    int rowY = YSteps - 2;
                    int plane = 2;

                    int X = XSize, Y = YSize, Z = ZSize;

                    int Xinc = (XSteps - 1) * X;
                    int Yinc = (YSteps - 1) * Y;
                    int Zinc = (ZSteps - 1) * Z;

                    Console.WriteLine("\n" + " Number of Planes = " + plane + " Rows = " + rowY + " Columns = " + columnZ + "\n");


                    // Selecting Centor Points

                    int[,,] grid = new int[plane, columnZ, rowY];

                    for (int i = 0; i < plane; i++)
                    {
                        for (int j = 0; j <columnZ ; j++)
                        {
                            for (int k = 0; k < rowY ; k++)
                            {
                                grid[i, j, k] = SurfaceStart;
                                SurfaceStart += Y;
                                Console.Write(grid[i, j, k] + " ; ");
                            }
                            SurfaceStart = grid[1, 0, 0] + Z;
                            Console.WriteLine(" f8    After First  Line ist over =  " + SurfaceStart);
                        }
                        SurfaceStart = (grid[0, 0, 0] + Xinc);
                        Console.WriteLine(" f8       PlaneCompleted and next Plane start point defined");
                    }

                    // Creating every possible beam from inner points

                    for (int a = 0; a < plane; a++)
                    {
                        for (int b = 0; b <columnZ ; b++)
                        {
                            for (int c = 0; c <rowY ; c++)
                            {
                                if (a == 0)                                    
                                {        
                                    int one         = grid[a, b, c] + X;         string fullText1 = (grid[a, b, c] + ";" + one + ";" + givenDiameter + ";" + givenForce);           sw.WriteLine(fullText1);
                                    int two         = grid[a, b, c] + Y;         string fullText2 = (grid[a, b, c] + ";" + two  + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText2);
                                    int three       = grid[a, b, c] + Z;         string fullText3 = (grid[a, b, c] + ";" + three + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText3);
                                //    int four        = grid[a, b, c] - X;         string fullText4 = (grid[a, b, c] + ";" + four + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText4);
                                    int five        = grid[a, b, c] - Y;         string fullText5 = (grid[a, b, c] + ";" + five + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText5);
                                    int six         = grid[a, b, c] - Z;         string fullText6 = (grid[a, b, c] + ";" + six + ";" + givenDiameter + ";" + givenForce);           sw.WriteLine(fullText6);
                                    int seven       = grid[a, b, c] + X + Y;     string fullText7 = (grid[a, b, c] + ";" + seven + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText7);
                                    int eight       = grid[a, b, c] + X - Y;     string fullText8 = (grid[a, b, c] + ";" + eight + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText8);
                                //    int nine        = grid[a, b, c] - X + Y;     string fullText9 = (grid[a, b, c] + ";" + nine + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText9);
                                //    int ten         = grid[a, b, c] - X - Y;     string fullText10 = (grid[a, b, c] + ";" + ten + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText10);
                                    int eleven      = grid[a, b, c] + X + Z;     string fullText11 = (grid[a, b, c] + ";" + eleven + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText11);
                                    int twelve      = grid[a, b, c] + X - Z;     string fullText12 = (grid[a, b, c] + ";" + twelve + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText12);
                                //    int thirteen    = grid[a, b, c] - X + Z;     string fullText13 = (grid[a, b, c] + ";" + thirteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText13);
                                //    int fourteen    = grid[a, b, c] - X - Z;     string fullText14 = (grid[a, b, c] + ";" + fourteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText14);
                                    int fifteen     = grid[a, b, c] + Y + Z;     string fullText15 = (grid[a, b, c] + ";" + fifteen + ";" + givenDiameter + ";" + givenForce);      sw.WriteLine(fullText15);
                                    int sixteen     = grid[a, b, c] + Y - Z;     string fullText16 = (grid[a, b, c] + ";" + sixteen + ";" + givenDiameter + ";" + givenForce);      sw.WriteLine(fullText16);
                                    int seventeen   = grid[a, b, c] - Y + Z;     string fullText17 = (grid[a, b, c] + ";" + seventeen + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText17);
                                    int eightteen   = grid[a, b, c] - Y - Z;     string fullText18 = (grid[a, b, c] + ";" + eightteen + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText18);
                                    int nineteen    = grid[a, b, c] + X + Y + Z; string fullText19 = (grid[a, b, c] + ";" + nineteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText19);
                                    int twenty      = grid[a, b, c] + X + Y - Z; string fullText20 = (grid[a, b, c] + ";" + twenty + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText20);
                                    int twentyone   = grid[a, b, c] + X - Y + Z; string fullText21 = (grid[a, b, c] + ";" + twentyone + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText21);
                                    int twentytwo   = grid[a, b, c] + X - Y - Z; string fullText22 = (grid[a, b, c] + ";" + twentytwo + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText22);
                                //    int twentythree = grid[a, b, c] - X + Y + Z; string fullText23 = (grid[a, b, c] + ";" + twentythree + ";" + givenDiameter + ";" + givenForce);  sw.WriteLine(fullText23);
                                //    int twentyfour  = grid[a, b, c] - X + Y - Z; string fullText24 = (grid[a, b, c] + ";" + twentyfour + ";" + givenDiameter + ";" + givenForce);   sw.WriteLine(fullText24);
                                //    int twentyfive  = grid[a, b, c] - X - Y + Z; string fullText25 = (grid[a, b, c] + ";" + twentyfive + ";" + givenDiameter + ";" + givenForce);   sw.WriteLine(fullText25);
                                //    int twentysix   = grid[a, b, c] - X - Y - Z; string fullText26 = (grid[a, b, c] + ";" + twentysix + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText26);
                                }

                                if (a == 1)
                                {
                                //    int one         = grid[a, b, c] + X;         string fullText1 = (grid[a, b, c] + ";" + one + ";" + givenDiameter + ";" + givenForce);           sw.WriteLine(fullText1);
                                    int two         = grid[a, b, c] + Y;         string fullText2 = (grid[a, b, c] + ";" + two  + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText2);
                                    int three       = grid[a, b, c] + Z;         string fullText3 = (grid[a, b, c] + ";" + three + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText3);
                                    int four        = grid[a, b, c] - X;         string fullText4 = (grid[a, b, c] + ";" + four + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText4);
                                    int five        = grid[a, b, c] - Y;         string fullText5 = (grid[a, b, c] + ";" + five + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText5);
                                    int six         = grid[a, b, c] - Z;         string fullText6 = (grid[a, b, c] + ";" + six + ";" + givenDiameter + ";" + givenForce);           sw.WriteLine(fullText6);
                                 //   int seven       = grid[a, b, c] + X + Y;     string fullText7 = (grid[a, b, c] + ";" + seven + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText7);
                                 //   int eight       = grid[a, b, c] + X - Y;     string fullText8 = (grid[a, b, c] + ";" + eight + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText8);
                                    int nine        = grid[a, b, c] - X + Y;     string fullText9 = (grid[a, b, c] + ";" + nine + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText9);
                                    int ten         = grid[a, b, c] - X - Y;     string fullText10 = (grid[a, b, c] + ";" + ten + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText10);
                                    int eleven      = grid[a, b, c] + X + Z;     string fullText11 = (grid[a, b, c] + ";" + eleven + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText11);
                                 //   int twelve      = grid[a, b, c] + X - Z;     string fullText12 = (grid[a, b, c] + ";" + twelve + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText12);
                                    int thirteen    = grid[a, b, c] - X + Z;     string fullText13 = (grid[a, b, c] + ";" + thirteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText13);
                                    int fourteen    = grid[a, b, c] - X - Z;     string fullText14 = (grid[a, b, c] + ";" + fourteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText14);
                                    int fifteen     = grid[a, b, c] + Y + Z;     string fullText15 = (grid[a, b, c] + ";" + fifteen + ";" + givenDiameter + ";" + givenForce);      sw.WriteLine(fullText15);
                                    int sixteen     = grid[a, b, c] + Y - Z;     string fullText16 = (grid[a, b, c] + ";" + sixteen + ";" + givenDiameter + ";" + givenForce);      sw.WriteLine(fullText16);
                                    int seventeen   = grid[a, b, c] - Y + Z;     string fullText17 = (grid[a, b, c] + ";" + seventeen + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText17);
                                    int eightteen   = grid[a, b, c] - Y - Z;     string fullText18 = (grid[a, b, c] + ";" + eightteen + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText18);
                                 //   int nineteen    = grid[a, b, c] + X + Y + Z; string fullText19 = (grid[a, b, c] + ";" + nineteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText19);
                                 //   int twenty      = grid[a, b, c] + X + Y - Z; string fullText20 = (grid[a, b, c] + ";" + twenty + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText20);
                                 //   int twentyone   = grid[a, b, c] + X - Y + Z; string fullText21 = (grid[a, b, c] + ";" + twentyone + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText21);
                                 //   int twentytwo   = grid[a, b, c] + X - Y - Z; string fullText22 = (grid[a, b, c] + ";" + twentytwo + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText22);
                                    int twentythree = grid[a, b, c] - X + Y + Z; string fullText23 = (grid[a, b, c] + ";" + twentythree + ";" + givenDiameter + ";" + givenForce);  sw.WriteLine(fullText23);
                                    int twentyfour  = grid[a, b, c] - X + Y - Z; string fullText24 = (grid[a, b, c] + ";" + twentyfour + ";" + givenDiameter + ";" + givenForce);   sw.WriteLine(fullText24);
                                    int twentyfive  = grid[a, b, c] - X - Y + Z; string fullText25 = (grid[a, b, c] + ";" + twentyfive + ";" + givenDiameter + ";" + givenForce);   sw.WriteLine(fullText25);
                                    int twentysix   = grid[a, b, c] - X - Y - Z; string fullText26 = (grid[a, b, c] + ";" + twentysix + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText26);
                                }

                            }
                        }
                    }
                }
            }

            //  Bottom - Up Surface Mid 17 operations
            using FileStream fs8 = new FileStream(filePath, FileMode.Append);
            {
                using StreamWriter sw = new StreamWriter(fs8);
                {
                    int SurfaceStart = ( XSteps * YSteps) + 2; 

                    int columnX = XSteps - 2;
                    int rowZ = ZSteps - 2;
                    int plane = 2;

                    int X = XSize, Y = YSize, Z = ZSize;

                    int Xinc = (XSteps - 1) * X;
                    int Yinc = (YSteps - 1) * Y;
                    int Zinc = (ZSteps - 1) * Z;
                  

                    Console.WriteLine("\n"+" Number of Planes = " + plane + " Rows = " + rowZ + " Columns = " + columnX);


                    // Selecting Centor Points

                    int[,,] grid = new int[plane, rowZ, columnX];

                    for (int i = 0; i < plane; i++)
                    {
                        for (int j = 0; j < rowZ; j++)
                        {
                            for (int k = 0; k < columnX; k++)
                            {
                                grid[i, j, k] = SurfaceStart;
                                SurfaceStart ++;
                                Console.Write(grid[i, j, k] + " ; ");
                            }
                            SurfaceStart = grid[1, 0, 0] + Z; // Important = It goes the up plane selects the first inner surface point
                            Console.WriteLine(" f9 After First Plans ist over =  " + SurfaceStart);
                        }
                        SurfaceStart = (grid[0, 0, 0] + Yinc); // Upper level
                        Console.WriteLine(" f9 PlaneCompleted and next Plane start point defined");
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
                                    int one         = grid[a, b, c] + X;         string fullText1 = (grid[a, b, c] + ";" + one + ";" + givenDiameter + ";" + givenForce);           sw.WriteLine(fullText1);
                                    int two         = grid[a, b, c] + Y;         string fullText2 = (grid[a, b, c] + ";" + two  + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText2);
                                    int three       = grid[a, b, c] + Z;         string fullText3 = (grid[a, b, c] + ";" + three + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText3);
                                    int four        = grid[a, b, c] - X;         string fullText4 = (grid[a, b, c] + ";" + four + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText4);
                                    int five        = grid[a, b, c] - Y;         string fullText5 = (grid[a, b, c] + ";" + five + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText5);
                                //    int six         = grid[a, b, c] - Z;         string fullText6 = (grid[a, b, c] + ";" + six + ";" + givenDiameter + ";" + givenForce);           sw.WriteLine(fullText6);
                                    int seven       = grid[a, b, c] + X + Y;     string fullText7 = (grid[a, b, c] + ";" + seven + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText7);
                                    int eight       = grid[a, b, c] + X - Y;     string fullText8 = (grid[a, b, c] + ";" + eight + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText8);
                                    int nine        = grid[a, b, c] - X + Y;     string fullText9 = (grid[a, b, c] + ";" + nine + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText9);
                                    int ten         = grid[a, b, c] - X - Y;     string fullText10 = (grid[a, b, c] + ";" + ten + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText10);
                                    int eleven      = grid[a, b, c] + X + Z;     string fullText11 = (grid[a, b, c] + ";" + eleven + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText11);
                                //   int twelve      = grid[a, b, c] + X - Z;     string fullText12 = (grid[a, b, c] + ";" + twelve + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText12);
                                    int thirteen    = grid[a, b, c] - X + Z;     string fullText13 = (grid[a, b, c] + ";" + thirteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText13);
                                //    int fourteen    = grid[a, b, c] - X - Z;     string fullText14 = (grid[a, b, c] + ";" + fourteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText14);
                                    int fifteen     = grid[a, b, c] + Y + Z;     string fullText15 = (grid[a, b, c] + ";" + fifteen + ";" + givenDiameter + ";" + givenForce);      sw.WriteLine(fullText15);
                                //   int sixteen     = grid[a, b, c] + Y - Z;     string fullText16 = (grid[a, b, c] + ";" + sixteen + ";" + givenDiameter + ";" + givenForce);      sw.WriteLine(fullText16);
                                    int seventeen   = grid[a, b, c] - Y + Z;     string fullText17 = (grid[a, b, c] + ";" + seventeen + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText17);
                                //   int eightteen   = grid[a, b, c] - Y - Z;     string fullText18 = (grid[a, b, c] + ";" + eightteen + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText18);
                                    int nineteen    = grid[a, b, c] + X + Y + Z; string fullText19 = (grid[a, b, c] + ";" + nineteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText19);
                                //   int twenty      = grid[a, b, c] + X + Y - Z; string fullText20 = (grid[a, b, c] + ";" + twenty + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText20);
                                    int twentyone   = grid[a, b, c] + X - Y + Z; string fullText21 = (grid[a, b, c] + ";" + twentyone + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText21);
                                //    int twentytwo   = grid[a, b, c] + X - Y - Z; string fullText22 = (grid[a, b, c] + ";" + twentytwo + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText22);
                                    int twentythree = grid[a, b, c] - X + Y + Z; string fullText23 = (grid[a, b, c] + ";" + twentythree + ";" + givenDiameter + ";" + givenForce);  sw.WriteLine(fullText23);
                                //    int twentyfour  = grid[a, b, c] - X + Y - Z; string fullText24 = (grid[a, b, c] + ";" + twentyfour + ";" + givenDiameter + ";" + givenForce);   sw.WriteLine(fullText24);
                                    int twentyfive  = grid[a, b, c] - X - Y + Z; string fullText25 = (grid[a, b, c] + ";" + twentyfive + ";" + givenDiameter + ";" + givenForce);   sw.WriteLine(fullText25);
                                //   int twentysix   = grid[a, b, c] - X - Y - Z; string fullText26 = (grid[a, b, c] + ";" + twentysix + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText26);
                                }

                                if (a == 1)
                                {
                                    int one         = grid[a, b, c] + X;         string fullText1 = (grid[a, b, c] + ";" + one + ";" + givenDiameter + ";" + givenForce);           sw.WriteLine(fullText1);
                                    int two         = grid[a, b, c] + Y;         string fullText2 = (grid[a, b, c] + ";" + two  + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText2);
                                //   int three       = grid[a, b, c] + Z;         string fullText3 = (grid[a, b, c] + ";" + three + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText3);
                                    int four        = grid[a, b, c] - X;         string fullText4 = (grid[a, b, c] + ";" + four + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText4);
                                    int five        = grid[a, b, c] - Y;         string fullText5 = (grid[a, b, c] + ";" + five + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText5);
                                    int six         = grid[a, b, c] - Z;         string fullText6 = (grid[a, b, c] + ";" + six + ";" + givenDiameter + ";" + givenForce);           sw.WriteLine(fullText6);
                                    int seven       = grid[a, b, c] + X + Y;     string fullText7 = (grid[a, b, c] + ";" + seven + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText7);
                                    int eight       = grid[a, b, c] + X - Y;     string fullText8 = (grid[a, b, c] + ";" + eight + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText8);
                                    int nine        = grid[a, b, c] - X + Y;     string fullText9 = (grid[a, b, c] + ";" + nine + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText9);
                                    int ten         = grid[a, b, c] - X - Y;     string fullText10 = (grid[a, b, c] + ";" + ten + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText10);
                                //    int eleven      = grid[a, b, c] + X + Z;     string fullText11 = (grid[a, b, c] + ";" + eleven + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText11);
                                    int twelve      = grid[a, b, c] + X - Z;     string fullText12 = (grid[a, b, c] + ";" + twelve + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText12);
                                //   int thirteen    = grid[a, b, c] - X + Z;     string fullText13 = (grid[a, b, c] + ";" + thirteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText13);
                                    int fourteen    = grid[a, b, c] - X - Z;     string fullText14 = (grid[a, b, c] + ";" + fourteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText14);
                                //   int fifteen     = grid[a, b, c] + Y + Z;     string fullText15 = (grid[a, b, c] + ";" + fifteen + ";" + givenDiameter + ";" + givenForce);      sw.WriteLine(fullText15);
                                    int sixteen     = grid[a, b, c] + Y - Z;     string fullText16 = (grid[a, b, c] + ";" + sixteen + ";" + givenDiameter + ";" + givenForce);      sw.WriteLine(fullText16);
                                //    int seventeen   = grid[a, b, c] - Y + Z;     string fullText17 = (grid[a, b, c] + ";" + seventeen + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText17);
                                    int eightteen   = grid[a, b, c] - Y - Z;     string fullText18 = (grid[a, b, c] + ";" + eightteen + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText18);
                                //    int nineteen    = grid[a, b, c] + X + Y + Z; string fullText19 = (grid[a, b, c] + ";" + nineteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText19);
                                    int twenty      = grid[a, b, c] + X + Y - Z; string fullText20 = (grid[a, b, c] + ";" + twenty + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText20);
                                //     int twentyone   = grid[a, b, c] + X - Y + Z; string fullText21 = (grid[a, b, c] + ";" + twentyone + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText21);
                                    int twentytwo   = grid[a, b, c] + X - Y - Z; string fullText22 = (grid[a, b, c] + ";" + twentytwo + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText22);
                                //    int twentythree = grid[a, b, c] - X + Y + Z; string fullText23 = (grid[a, b, c] + ";" + twentythree + ";" + givenDiameter + ";" + givenForce);  sw.WriteLine(fullText23);
                                    int twentyfour  = grid[a, b, c] - X + Y - Z; string fullText24 = (grid[a, b, c] + ";" + twentyfour + ";" + givenDiameter + ";" + givenForce);   sw.WriteLine(fullText24);
                                //    int twentyfive  = grid[a, b, c] - X - Y + Z; string fullText25 = (grid[a, b, c] + ";" + twentyfive + ";" + givenDiameter + ";" + givenForce);   sw.WriteLine(fullText25);
                                    int twentysix   = grid[a, b, c] - X - Y - Z; string fullText26 = (grid[a, b, c] + ";" + twentysix + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText26);
                                }
// Ulas dikme  youtube 
                            }
                        }
                    }
                }
            }

             //  Fron-Back Surface Mid 17 operations
            using FileStream fs9 = new FileStream(filePath, FileMode.Append);
            {
                using StreamWriter sw = new StreamWriter(fs9);
                {
                    int SurfaceStart = XSteps + 2;

                    int columnX = XSteps - 2;
                    int rowY = YSteps - 2;
                    int plane = 2;

                    int X = XSize, Y = YSize, Z = ZSize;

                    int Xinc = (XSteps - 1) * X;
                    int Yinc = (YSteps - 1) * Y;
                    int Zinc = (ZSteps - 1) * Z;

                    Console.WriteLine(" Number of Planes = " + plane + " Rows = " + rowY + " Columns = " + columnX );


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
                                Console.Write(grid[i, j, k] + " ; ");
                            }
                            SurfaceStart += 2;
                            Console.WriteLine(" After First Plans ist over =  " + SurfaceStart);
                        }
                        SurfaceStart = (grid[0, 0, 0] + Zinc);
                        Console.WriteLine(" PlaneCompleted and next Plane start point defined");
                    }

                    // Creating every possible beam from inner points

                    for (int a = 0; a < plane; a++)
                    {
                        for (int b = 0; b < rowY; b++)
                        {
                            for (int c = 0; c < columnX; c++)
                            {
                                if (a == 0 )
                                {
                                    //int one = grid[a, b, c] + X;
                                    //Console.WriteLine(grid[a, b, c] + ";" + one + ";" + givenDiameter + ";" + givenForce);
                                    int one         = grid[a, b, c] + X;         string fullText1 = (grid[a, b, c] + ";" + one + ";" + givenDiameter + ";" + givenForce);           sw.WriteLine(fullText1);
                                    int two         = grid[a, b, c] + Y;         string fullText2 = (grid[a, b, c] + ";" + two  + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText2);
                                    int three       = grid[a, b, c] + Z;         string fullText3 = (grid[a, b, c] + ";" + three + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText3);
                                    int four        = grid[a, b, c] - X;         string fullText4 = (grid[a, b, c] + ";" + four + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText4);
                                    int five        = grid[a, b, c] - Y;         string fullText5 = (grid[a, b, c] + ";" + five + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText5);
                                //    int six         = grid[a, b, c] - Z;         string fullText6 = (grid[a, b, c] + ";" + six + ";" + givenDiameter + ";" + givenForce);           sw.WriteLine(fullText6);
                                    int seven       = grid[a, b, c] + X + Y;     string fullText7 = (grid[a, b, c] + ";" + seven + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText7);
                                    int eight       = grid[a, b, c] + X - Y;     string fullText8 = (grid[a, b, c] + ";" + eight + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText8);
                                    int nine        = grid[a, b, c] - X + Y;     string fullText9 = (grid[a, b, c] + ";" + nine + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText9);
                                    int ten         = grid[a, b, c] - X - Y;     string fullText10 = (grid[a, b, c] + ";" + ten + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText10);
                                    int eleven      = grid[a, b, c] + X + Z;     string fullText11 = (grid[a, b, c] + ";" + eleven + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText11);
                                //    int twelve      = grid[a, b, c] + X - Z;     string fullText12 = (grid[a, b, c] + ";" + twelve + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText12);
                                    int thirteen    = grid[a, b, c] - X + Z;     string fullText13 = (grid[a, b, c] + ";" + thirteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText13);
                                //    int fourteen    = grid[a, b, c] - X - Z;     string fullText14 = (grid[a, b, c] + ";" + fourteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText14);
                                    int fifteen     = grid[a, b, c] + Y + Z;     string fullText15 = (grid[a, b, c] + ";" + fifteen + ";" + givenDiameter + ";" + givenForce);      sw.WriteLine(fullText15);
                                //    int sixteen     = grid[a, b, c] + Y - Z;     string fullText16 = (grid[a, b, c] + ";" + sixteen + ";" + givenDiameter + ";" + givenForce);      sw.WriteLine(fullText16);
                                    int seventeen   = grid[a, b, c] - Y + Z;     string fullText17 = (grid[a, b, c] + ";" + seventeen + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText17);
                                //    int eightteen   = grid[a, b, c] - Y - Z;     string fullText18 = (grid[a, b, c] + ";" + eightteen + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText18);
                                    int nineteen    = grid[a, b, c] + X + Y + Z; string fullText19 = (grid[a, b, c] + ";" + nineteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText19);
                                //   int twenty      = grid[a, b, c] + X + Y - Z; string fullText20 = (grid[a, b, c] + ";" + twenty + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText20);
                                    int twentyone   = grid[a, b, c] + X - Y + Z; string fullText21 = (grid[a, b, c] + ";" + twentyone + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText21);
                                //    int twentytwo   = grid[a, b, c] + X - Y - Z; string fullText22 = (grid[a, b, c] + ";" + twentytwo + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText22);
                                    int twentythree = grid[a, b, c] - X + Y + Z; string fullText23 = (grid[a, b, c] + ";" + twentythree + ";" + givenDiameter + ";" + givenForce);  sw.WriteLine(fullText23);
                                //    int twentyfour  = grid[a, b, c] - X + Y - Z; string fullText24 = (grid[a, b, c] + ";" + twentyfour + ";" + givenDiameter + ";" + givenForce);   sw.WriteLine(fullText24);
                                    int twentyfive  = grid[a, b, c] - X - Y + Z; string fullText25 = (grid[a, b, c] + ";" + twentyfive + ";" + givenDiameter + ";" + givenForce);   sw.WriteLine(fullText25);
                                //    int twentysix   = grid[a, b, c] - X - Y - Z; string fullText26 = (grid[a, b, c] + ";" + twentysix + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText26);}
                                
                                }

                                if (a == 1)
                                {                                                            
                                //int one = grid[a, b, c] + X;
                                //Console.WriteLine(grid[a, b, c] + ";" + one + ";" + givenDiameter + ";" + givenForce);
                                int one         = grid[a, b, c] + X;         string fullText1 = (grid[a, b, c] + ";" + one + ";" + givenDiameter + ";" + givenForce);           sw.WriteLine(fullText1);
                                int two         = grid[a, b, c] + Y;         string fullText2 = (grid[a, b, c] + ";" + two  + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText2);
                            //  int three       = grid[a, b, c] + Z;         string fullText3 = (grid[a, b, c] + ";" + three + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText3);
                                int four        = grid[a, b, c] - X;         string fullText4 = (grid[a, b, c] + ";" + four + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText4);
                                int five        = grid[a, b, c] - Y;         string fullText5 = (grid[a, b, c] + ";" + five + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText5);
                                int six         = grid[a, b, c] - Z;         string fullText6 = (grid[a, b, c] + ";" + six + ";" + givenDiameter + ";" + givenForce);           sw.WriteLine(fullText6);
                                int seven       = grid[a, b, c] + X + Y;     string fullText7 = (grid[a, b, c] + ";" + seven + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText7);
                                int eight       = grid[a, b, c] + X - Y;     string fullText8 = (grid[a, b, c] + ";" + eight + ";" + givenDiameter + ";" + givenForce);         sw.WriteLine(fullText8);
                                int nine        = grid[a, b, c] - X + Y;     string fullText9 = (grid[a, b, c] + ";" + nine + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText9);
                                int ten         = grid[a, b, c] - X - Y;     string fullText10 = (grid[a, b, c] + ";" + ten + ";" + givenDiameter + ";" + givenForce);          sw.WriteLine(fullText10);
                            //    int eleven      = grid[a, b, c] + X + Z;     string fullText11 = (grid[a, b, c] + ";" + eleven + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText11);
                                int twelve      = grid[a, b, c] + X - Z;     string fullText12 = (grid[a, b, c] + ";" + twelve + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText12);
                            //    int thirteen    = grid[a, b, c] - X + Z;     string fullText13 = (grid[a, b, c] + ";" + thirteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText13);
                                int fourteen    = grid[a, b, c] - X - Z;     string fullText14 = (grid[a, b, c] + ";" + fourteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText14);
                            //    int fifteen     = grid[a, b, c] + Y + Z;     string fullText15 = (grid[a, b, c] + ";" + fifteen + ";" + givenDiameter + ";" + givenForce);      sw.WriteLine(fullText15);
                                int sixteen     = grid[a, b, c] + Y - Z;     string fullText16 = (grid[a, b, c] + ";" + sixteen + ";" + givenDiameter + ";" + givenForce);      sw.WriteLine(fullText16);
                            //    int seventeen   = grid[a, b, c] - Y + Z;     string fullText17 = (grid[a, b, c] + ";" + seventeen + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText17);
                                int eightteen   = grid[a, b, c] - Y - Z;     string fullText18 = (grid[a, b, c] + ";" + eightteen + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText18);
                            //    int nineteen    = grid[a, b, c] + X + Y + Z; string fullText19 = (grid[a, b, c] + ";" + nineteen + ";" + givenDiameter + ";" + givenForce);     sw.WriteLine(fullText19);
                                int twenty      = grid[a, b, c] + X + Y - Z; string fullText20 = (grid[a, b, c] + ";" + twenty + ";" + givenDiameter + ";" + givenForce);       sw.WriteLine(fullText20);
                            //   int twentyone   = grid[a, b, c] + X - Y + Z; string fullText21 = (grid[a, b, c] + ";" + twentyone + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText21);
                                int twentytwo   = grid[a, b, c] + X - Y - Z; string fullText22 = (grid[a, b, c] + ";" + twentytwo + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText22);
                            //    int twentythree = grid[a, b, c] - X + Y + Z; string fullText23 = (grid[a, b, c] + ";" + twentythree + ";" + givenDiameter + ";" + givenForce);  sw.WriteLine(fullText23);
                                int twentyfour  = grid[a, b, c] - X + Y - Z; string fullText24 = (grid[a, b, c] + ";" + twentyfour + ";" + givenDiameter + ";" + givenForce);   sw.WriteLine(fullText24);
                            //    int twentyfive  = grid[a, b, c] - X - Y + Z; string fullText25 = (grid[a, b, c] + ";" + twentyfive + ";" + givenDiameter + ";" + givenForce);   sw.WriteLine(fullText25);
                                int twentysix   = grid[a, b, c] - X - Y - Z; string fullText26 = (grid[a, b, c] + ";" + twentysix + ";" + givenDiameter + ";" + givenForce);    sw.WriteLine(fullText26);}

                            }
                        }
                    }
                }
            }
        }
    }
}