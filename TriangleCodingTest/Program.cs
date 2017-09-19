using System;
using System.Text;

namespace TriangleCodingTest
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("App to calc triangle coordinates from id/id from coordinates.  Coordinate range is 1 - 60 for both x and y axes.");
            Console.WriteLine("Enter 'C' to calc coordinates, enter 'X' to calc row/column Id: ");

            var calcInputType = Console.ReadLine().ToUpper();

            if (String.Compare(calcInputType, "C") == 0)
            {
                Console.WriteLine("\nEnter triangle id (i.e., A1, D8, F12): ");
                var triangleId = Console.ReadLine();

                CoordinateRowEnum rowEnum;
                int rowInt, columnInt;
                Enum.TryParse(triangleId.Substring(0, 1), out rowEnum);
                rowInt = (int)rowEnum;
                int.TryParse(triangleId.Substring(1, (triangleId.Length - 1)), out columnInt);

                // Compute the coordinates, using the numberic part of the triangle's id to start logic eval
                if ((columnInt % 2) != 0)
                {
                    OutputLowerLeftCoordinates(rowInt, columnInt);
                }
                else
                {
                    OutputUpperRightCoordinates(rowInt, columnInt);
                }
            }
            else if(String.Compare(calcInputType, "X") == 0)
            {
                Console.WriteLine("\nEnter triangle's right angle coordinates in the form \"(x1, y1)\" to get triagle's Id.");
                var triangleCoordinates = Console.ReadLine();
                int x1, y1;
                ParseIntoCoordinates(triangleCoordinates, out x1, out y1);

                string triangleId = EvalTriangleFromCoords(x1, y1);
                Console.WriteLine("Triangle coordinates {0} results in ID of : {1}", triangleCoordinates, triangleId);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nInvalid input, exting app...");
            }
        }

        private static string EvalTriangleFromCoords(int x1, int y1)
        {
            // Get triangle id from right angle coordinates
            StringBuilder triangleId = new StringBuilder();

            // Eval the row (letter id A-F)
            if ((y1 % 10) == 0)
            {
                triangleId.Append(((CoordinateRowEnum)(y1 / 10)).ToString());
            }
            else
            {
                triangleId.Append(((CoordinateRowEnum)((y1 + 9) / 10)).ToString());
            }

            // Eval the column (number id 1-12)
            if ((x1 % 10) == 0)
            {
                triangleId.Append((x1 / 5).ToString());
            }
            else
            {
                triangleId.Append((((x1 + 9) / 5) - 1).ToString());
            }

            return triangleId.ToString();
        }

        private static void ParseIntoCoordinates(string v, out int x, out int y)
        {
            v = v.Trim();
            v = v.Trim(new char[] { '(', ')' });
            var coord = v.Split(',');
            int.TryParse(coord[0], out x);
            int.TryParse(coord[1], out y);
        }

        private static void OutputUpperRightCoordinates(int rowInt, int columnInt)
        {
            // Outputs coordinates of triangle with right angle vertex in upper right of grid
            StringBuilder coordinateString = new StringBuilder("(");
            int columnRounded = (int)Math.Round(Decimal.Divide(columnInt, 2), MidpointRounding.AwayFromZero);
            int rightAngleXCoord = (columnRounded * 10);
            int rightAngleYCoord = (rowInt * 10) - 9;

            coordinateString.Append(rightAngleXCoord.ToString() + "," + rightAngleYCoord.ToString() + ") ");
            coordinateString.Append("(" + (rightAngleXCoord - 9).ToString() + "," + rightAngleYCoord.ToString() + ") ");
            coordinateString.Append("(" + rightAngleXCoord.ToString() + "," + (rightAngleYCoord + 9).ToString() + ")");

            Console.WriteLine("Triangle {0}{1} has the vertices of {2}", (CoordinateRowEnum)rowInt, columnInt, coordinateString.ToString());
            Console.ReadKey();
        }

        private static void OutputLowerLeftCoordinates(int rowInt, int columnInt)
        {
            // Outputs coordinates of triangle with right angle vertex in lower left of grid
            StringBuilder coordinateString = new StringBuilder("(");
            int columnRounded = (int)Math.Round(Decimal.Divide(columnInt, 2), MidpointRounding.AwayFromZero);
            int rightAngleXCoord = (columnRounded * 10) - 9;
            int rightAngleYCoord = (rowInt * 10);

            coordinateString.Append(rightAngleXCoord.ToString() + "," + rightAngleYCoord.ToString() + ") ");
            coordinateString.Append("(" + rightAngleXCoord.ToString() + "," + (rightAngleYCoord - 9).ToString() + ") ");
            coordinateString.Append("(" + (rightAngleXCoord + 9).ToString() + "," + rightAngleYCoord.ToString() + ")");

            Console.WriteLine("Triangle {0}{1} has the vertices of {2}", (CoordinateRowEnum)rowInt, columnInt, coordinateString.ToString());
            Console.ReadKey();
        }

        enum CoordinateRowEnum { A = 1, B = 2, C = 3, D = 4, E = 5, F = 6 }
    }
}