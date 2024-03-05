
#include <span>
#include <array>
#include <queue>
#include <vector>
#include <ranges>
using namespace std;

class Solution {

    struct Point {
        size_t row;
        size_t column;

        Point() = default;
        Point(size_t row, size_t column) : row {row}, column {column} {}
    };

    inline static const int NOT_VISITED = -1;
    inline static const int LAND = 0;
    inline static const int WATER = 1;
    inline static const array<array<int, 2>, 4> moves{ {{1, 0}, {-1, 0}, {0, 1}, {0, -1}} };

    size_t rows;
    size_t  columns;
    int heightOfTopography = 0;

public:
    vector<vector<int>> highestPeak(const vector<vector<int>>& input) {
        rows = input.size();
        columns = input[0].size();
        return mutliSourceBreadthFirstSearchToCreateTopographyWithMaxHeight(input);
    }

private:
    vector<vector<int>> mutliSourceBreadthFirstSearchToCreateTopographyWithMaxHeight(span<const vector<int>> input) {
        vector<vector<int>> topographyWithMaxHeight(rows, vector<int>(columns, NOT_VISITED));
        queue<Point> queue;
        initializeQueueWithWaterPoints(input, topographyWithMaxHeight, queue);

        while (!queue.empty()) {
            size_t pointsInCurrentStep = queue.size();
            ++heightOfTopography;

            while (pointsInCurrentStep-- > 0) {
                Point current = queue.front();
                queue.pop();

                for (const auto& move : moves) {
                    size_t nextRow = current.row + move[0];
                    size_t nextColumn = current.column + move[1];

                    if (isInMatrix(nextRow, nextColumn) && topographyWithMaxHeight[nextRow][nextColumn] == NOT_VISITED) {
                            queue.emplace(nextRow, nextColumn);
                            topographyWithMaxHeight[nextRow][nextColumn] = heightOfTopography;
                    }
                }
            }
        }

        return topographyWithMaxHeight;
    }

    void initializeQueueWithWaterPoints(span<const vector<int>> input, span<vector<int>> topographyWithMaxHeight, queue<Point>& queue) const {
        for (size_t r = 0; r < rows; ++r) {
            for (size_t c = 0; c < columns; ++c) {
                if (input[r][c] == WATER) {
                    queue.emplace(r, c);
                    topographyWithMaxHeight[r][c] = heightOfTopography;
                }
            }
        }
    }

    // in this particular case, checking
    // 'row != variant_npos' and 'column != variant_npos' is not necessary 
    bool isInMatrix(size_t row, size_t column) const {
        return row < rows && column < columns;
    }
};
