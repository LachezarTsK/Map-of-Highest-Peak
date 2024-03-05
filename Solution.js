
/**
 * @param {number[][]} input
 * @return {number[][]}
 */
var highestPeak = function (input) {
    this.NOT_VISITED = -1;
    this.LAND = 0;
    this.WATER = 1;
    this.moves = [[1, 0], [-1, 0], [0, 1], [0, -1]];

    this.rows = input.length;
    this.columns = input[0].length;
    this.heightOfTopography = 0;

    return mutliSourceBreadthFirstSearchToCreateTopographyWithMaxHeight(input);
};

/**
 * @param {number[][]} input
 * @return {number[][]}
 */
function mutliSourceBreadthFirstSearchToCreateTopographyWithMaxHeight(input) {
    const topographyWithMaxHeight = Array.from(new Array(this.rows), () => new Array(this.columns).fill(this.NOT_VISITED));

    // Queue<Point>
    // const {Queue} = require('@datastructures-js/queue');
    const queue = new Queue();
    initializeQueueWithWaterPoints(input, topographyWithMaxHeight, queue);

    while (!queue.isEmpty()) {
        let pointsInCurrentStep = queue.size();
        ++this.heightOfTopography;

        while (pointsInCurrentStep-- > 0) {
            const current = queue.dequeue();

            for (let move of this.moves) {
                let nextRow = current.row + move[0];
                let nextColumn = current.column + move[1];

                if (isInMatrix(nextRow, nextColumn) && topographyWithMaxHeight[nextRow][nextColumn] === this.NOT_VISITED) {
                    queue.enqueue(new Point(nextRow, nextColumn));
                    topographyWithMaxHeight[nextRow][nextColumn] = heightOfTopography;
                }
            }
        }
    }

    return topographyWithMaxHeight;
}

/**
 * @param {number} row
 * @param {number} column
 */
function Point(row, column) {
    this.row = row;
    this.column = column;
}

/**
 * @param {number[][]} input
 * @param {number[][]} topographyWithMaxHeight
 * @param {Queue<Poit>} queue 
 */
function initializeQueueWithWaterPoints(input, topographyWithMaxHeight, queue) {
    for (let r = 0; r < this.rows; ++r) {
        for (let c = 0; c < this.columns; ++c) {
            if (input[r][c] === this.WATER) {
                queue.enqueue(new Point(r, c));
                topographyWithMaxHeight[r][c] = this.heightOfTopography;
            }
        }
    }
}

/**
 * @param {number} row
 * @param {number} column
 * @return {boolean}  
 */
function isInMatrix(row, column) {
    return row >= 0 && row < this.rows && column >= 0 && column < this.columns;
}
