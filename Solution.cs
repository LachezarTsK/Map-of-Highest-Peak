
using System;
using System.Collections.Generic;

public class Solution
{
    private struct Point
    {
        public int row;
        public int column;

        public Point(int row, int column)
        {
            this.row = row;
            this.column = column;
        }
    }

    static readonly int NOT_VISITED = -1;
    static readonly int LAND = 0;
    static readonly int WATER = 1;
    static readonly int[][] moves = {
        new int[]{ 1, 0 },new int[] { -1, 0 }, new int[]{ 0, 1 }, new int[]{ 0, -1 } };

    private int rows;
    private int columns;
    private int heightOfTopography;
    public int[][] HighestPeak(int[][] input)
    {
        rows = input.Length;
        columns = input[0].Length;
        return mutliSourceBreadthFirstSearchToCreateTopographyWithMaxHeight(input);
    }

    private int[][] mutliSourceBreadthFirstSearchToCreateTopographyWithMaxHeight(int[][] input)
    {
        int[][] topographyWithMaxHeight = new int[rows][];
        for (int r = 0; r < rows; ++r)
        {
            topographyWithMaxHeight[r] = new int[columns];
            Array.Fill(topographyWithMaxHeight[r], NOT_VISITED);
        }

        Queue<Point> queue = new Queue<Point>();
        initializeQueueWithWaterPoints(input, topographyWithMaxHeight, queue);

        while (queue.Count > 0)
        {
            int pointsInCurrentStep = queue.Count;
            ++heightOfTopography;

            while (pointsInCurrentStep-- > 0)
            {
                Point current = queue.Dequeue();

                foreach (int[] move in moves)
                {
                    int nextRow = current.row + move[0];
                    int nextColumn = current.column + move[1];

                    if (isInMatrix(nextRow, nextColumn) && topographyWithMaxHeight[nextRow][nextColumn] == NOT_VISITED)
                    {
                        queue.Enqueue(new Point(nextRow, nextColumn));
                        topographyWithMaxHeight[nextRow][nextColumn] = heightOfTopography;
                    }
                }
            }
        }

        return topographyWithMaxHeight;
    }

    private void initializeQueueWithWaterPoints(int[][] input, int[][] topographyWithMaxHeight, Queue<Point> queue)
    {
        for (int r = 0; r < rows; ++r)
        {
            for (int c = 0; c < columns; ++c)
            {
                if (input[r][c] == WATER)
                {
                    queue.Enqueue(new Point(r, c));
                    topographyWithMaxHeight[r][c] = heightOfTopography;
                }
            }
        }
    }

    private bool isInMatrix(int row, int column)
    {
        return row >= 0 && row < rows && column >= 0 && column < columns;
    }
}
