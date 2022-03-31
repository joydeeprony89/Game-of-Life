using System;
using System.Collections.Generic;

namespace Game_of_Life
{
  class Program
  {
    static void Main(string[] args)
    {
      var board = new int[4][] { new int[3] { 0, 1, 0 }, new int[3] { 0, 0, 1 }, new int[3] { 1, 1, 1 }, new int[3] { 0, 0, 0 } };
      Program p = new Program();
      p.GameOfLife(board);
      foreach (var item in board)
        Console.WriteLine(string.Join(",", item));
    }

    int row;
    int column;
    public void GameOfLife(int[][] board)
    {
      if (board == null || board.Length == 0) return;
      row = board.Length;
      column = board[0].Length;
      // As we are not doing the operation in the passed in array, created a another array only to have the output.
      var tempBoard = CopyByValue(board);
      for (int i = 0; i < row; i++)
      {
        for (int j = 0; j < column; j++)
        {
          int liveCount = 0;
          int currentItem = tempBoard[i][j];
          Dfs(tempBoard, ref liveCount, i, j);
          int state = GetStatusOfSurvival(liveCount, currentItem);
          board[i][j] = state;
        }
      }
    }

    private int GetStatusOfSurvival(int liveCount, int currentItem)
    {
      int state = 0;
      switch (currentItem)
      {
        // dead
        case 0:
          // Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
          state = liveCount == 3 ? 1 : 0;
          break;
        // live
        case 1:
          // Any live cell with fewer than two live neighbors dies as if caused by under-population.
          if (liveCount < 2)
            state = 0;
          // Any live cell with two or three live neighbors lives on to the next generation.
          else if (liveCount == 2 || liveCount == 3)
            state = 1;
          // Any live cell with more than three live neighbors dies, as if by over-population.
          else if (liveCount > 3)
            state = 0;
          break;
      }

      return state;
    }

    readonly List<int[]> directions = new List<int[]>()
      {
        new int[2]{ 0, -1 },
        new int[2]{ 0, 1 },
        new int[2]{ -1, 0 },
        new int[2]{ 1, 0 },
        new int[2]{ -1, -1 },
        new int[2]{ -1, 1 },
        new int[2]{ 1, -1 },
        new int[2]{ 1, 1 },
      };
    private void Dfs(int[][] board, ref int liveCount, int rowIndex, int colIndex)
    {
      foreach (var dir in directions)
      {
        int newRowIndex = rowIndex + dir[0];
        int newColumnIndex = colIndex + dir[1];
        // define the matrix boundary.
        if (newRowIndex >= 0 && newColumnIndex >= 0 && newRowIndex < row &&
          newColumnIndex < column &&
          board[newRowIndex][newColumnIndex] == 1)
        {
          liveCount++;
        }
      }
    }

    private int[][] CopyByValue(int[][] board)
    {
      var tempBoard = new int[row][];
      for (int i = 0; i < row; i++)
      {
        tempBoard[i] = new int[column];
        for (int j = 0; j < column; j++)
        {
          tempBoard[i][j] = board[i][j];
        }
      }
      return tempBoard;
    }
  }
}
