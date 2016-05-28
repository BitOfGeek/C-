using System;
using System.Collections.Generic;
using System.Text;
class ConnectedAreasInMatrix
{
    private static char[,] matrix1 = 
        {
            {' ',' ',' ','*',' ',' ',' ','*',' '},
            {' ',' ',' ','*',' ',' ',' ','*',' '},
            {' ',' ',' ','*',' ',' ',' ','*',' '}, 
            {' ',' ',' ',' ','*',' ','*',' ',' '}
        };

    private static char[,] matrix = 
        {
            {'*',' ',' ','*',' ',' ',' ','*',' ',' '},
            {'*',' ',' ','*',' ',' ',' ','*',' ',' '},
            {'*',' ',' ','*','*','*','*','*',' ',' '}, 
            {'*',' ',' ','*',' ',' ',' ','*',' ',' '},
            {'*',' ',' ','*',' ',' ',' ','*',' ',' '}
        };

    private static int areaCount;
    private static int areaSize;
    private static StringBuilder output = new StringBuilder();

    public static void Main()
    {
        FindConnectedAreas();
    }

    static void FindConnectedAreas()
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                if (matrix[row, col] == ' ')
                {
                    TryDirection(row, col - 1, 'L');
                    TryDirection(row - 1, col, 'U');
                    TryDirection(row, col + 1, 'R');
                    TryDirection(row + 1, col, 'D');

                    output.AppendFormat("Area #{0} at ({1}, {2}), size: {3}{4}", ++areaCount, row, col, areaSize, Environment.NewLine);
                    areaSize = 0;
                }
            }
        }
        Console.WriteLine("Total areas found: {0}", areaCount);
        Console.WriteLine(output.ToString());
    }

    private static void TryDirection(int row, int col, char direction)
    {
        if (!InRange(row, col))
        {
            return;
        }

        if (matrix[row, col] == ' ')
        {
            areaSize++;

            matrix[row, col] = '.';

            TryDirection(row, col - 1, 'L'); 
            TryDirection(row - 1, col, 'U'); 
            TryDirection(row, col + 1, 'R'); 
            TryDirection(row + 1, col, 'D'); 
        }
    }

    static bool InRange(int row, int col)
    {
        bool rowInRange = row >= 0 && row < matrix.GetLength(0);
        bool colInRange = col >= 0 && col < matrix.GetLength(1);

        return rowInRange && colInRange;
    }

    static void PrintLabyrinth()
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                Console.Write("{0} ", matrix[row, col]);
            }
            Console.WriteLine();
        }
    }

    static void ReadLabyrinth()
    {
        List<string> inputData = new List<string>();

        string input = Console.ReadLine();
        input.Replace("\t", " ");
        while (!string.IsNullOrWhiteSpace(input))
        {
            inputData.Add(input);
            input = Console.ReadLine();
            input.Replace("\t", " ");
        }

        matrix = new char[inputData.Count, inputData[0].Length];

        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                matrix[row, col] = inputData[row][col];
            }
        }
    }
}