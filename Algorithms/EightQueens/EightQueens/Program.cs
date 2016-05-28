using System;
using System.Collections.Generic;

class EightQueens
{
    const int size = 8;
    static int solutionsFound = 0;
    static bool[,] chessboard = new bool[size, size];
    static HashSet<int> attackedRows = new HashSet<int>();
    static HashSet<int> attackedColumns = new HashSet<int>();
    static HashSet<int> attackedLeftDiagonals = new HashSet<int>();
    static HashSet<int> attackedRightDiagonals = new HashSet<int>();

    static void PutQueens(int row)
    {
        if (row == size)
        {
            PrintSolution();
        }
        else
        {
            for (int col = 0; col < size; col++)
            {
                if (CanPlaceQueen(row, col))
                {
                    MarkAllAttackedPositions(row, col);
                    PutQueens(row + 1);
                    UnmarkAllAttackedPositions(row, col);
                }
            }
        }
    }

    static bool CanPlaceQueen(int row, int col)
    {
        var positionOccupied = attackedRows.Contains(row) || attackedColumns.Contains(col) || 
            attackedLeftDiagonals.Contains(col - row) || attackedRightDiagonals.Contains(row + col);
        return !positionOccupied;
    }

    static void MarkAllAttackedPositions(int row, int col)
    {
        attackedRows.Add(row);
        attackedColumns.Add(col);
        attackedLeftDiagonals.Add(col - row);
        attackedRightDiagonals.Add(row + col);
        chessboard[row, col] = true;
    }

    static void UnmarkAllAttackedPositions(int row, int col)
    {
        attackedRows.Remove(row);
        attackedColumns.Remove(col);
        attackedLeftDiagonals.Remove(col - row);
        attackedRightDiagonals.Remove(row + col);
        chessboard[row, col] = false;
    }

    static void PrintSolution()
    {
        for (int row = 0; row  < size; row ++)
        {
            for (int col = 0; col < size; col++)
            {
                Console.Write(chessboard[row, col] ? '*' : '-');
            }
            Console.WriteLine();
        }
        Console.WriteLine();
        solutionsFound++;
    }

    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        PutQueens(0);
        Console.WriteLine(solutionsFound);
    }
}