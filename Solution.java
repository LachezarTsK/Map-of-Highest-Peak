
import java.util.Arrays;
import java.util.LinkedList;
import java.util.Queue;

public class Solution {

    private record Point(int row, int column) {}

    private static final int NOT_VISITED = -1;
    private static final int LAND = 0;
    private static final int WATER = 1;
    private static final int[][] moves = {{1, 0}, {-1, 0}, {0, 1}, {0, -1}};

    private int rows;
    private int columns;
    private int heightOfTopography;

    public int[][] highestPeak(int[][] input) {
        rows = input.length;
        columns = input[0].length;
        return mutliSourceBreadthFirstSearchToCreateTopographyWithMaxHeight(input);
    }

    private int[][] mutliSourceBreadthFirstSearchToCreateTopographyWithMaxHeight(int[][] input) {
        int[][] topographyWithMaxHeight = new int[rows][columns];
        for (int r = 0; r < rows; ++r) {
            Arrays.fill(topographyWithMaxHeight[r], NOT_VISITED);
        }

        Queue<Point> queue = new LinkedList<>();
        initializeQueueWithWaterPoints(input, topographyWithMaxHeight, queue);

        while (!queue.isEmpty()) {
            int pointsInCurrentStep = queue.size();
            ++heightOfTopography;

            while (pointsInCurrentStep-- > 0) {
                Point current = queue.poll();

                for (int[] move : moves) {
                    int nextRow = current.row + move[0];
                    int nextColumn = current.column + move[1];

                    if (isInMatrix(nextRow, nextColumn) && topographyWithMaxHeight[nextRow][nextColumn] == NOT_VISITED) {
                        queue.add(new Point(nextRow, nextColumn));
                        topographyWithMaxHeight[nextRow][nextColumn] = heightOfTopography;
                    }
                }
            }
        }

        return topographyWithMaxHeight;
    }

    private void initializeQueueWithWaterPoints(int[][] input, int[][] topographyWithMaxHeight, Queue<Point> queue) {
        for (int r = 0; r < rows; ++r) {
            for (int c = 0; c < columns; ++c) {
                if (input[r][c] == WATER) {
                    queue.add(new Point(r, c));
                    topographyWithMaxHeight[r][c] = heightOfTopography;
                }
            }
        }
    }

    private boolean isInMatrix(int row, int column) {
        return row >= 0 && row < rows && column >= 0 && column < columns;
    }
}
