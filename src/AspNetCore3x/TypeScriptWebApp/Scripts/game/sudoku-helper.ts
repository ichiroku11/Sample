
// 座標のx成分、y成分
export const sudokuComponents = [0, 1, 2, 3, 4, 5, 6, 7, 8] as const;
export type SudokuComponent = typeof sudokuComponents[number];

// 座標
export type SudokuCoord = {
	x: SudokuComponent,
	y: SudokuComponent,
};

// 数独の数値
export const sudokuDigits = [1, 2, 3, 4, 5, 6, 7, 8, 9] as const;
export type SudokuDigit = typeof sudokuDigits[number];

export type SudokuUndefinedOrDigit = undefined | SudokuDigit;

export type SudokuDefault = SudokuCoord & {
	value: SudokuDigit,
};
