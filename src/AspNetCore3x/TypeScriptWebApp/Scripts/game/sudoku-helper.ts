
// 座標のx成分、y成分
export type SudokuComponent = 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8;

// 座標
export type SudokuCoord = {
	x: SudokuComponent,
	y: SudokuComponent,
};

// 数独の数値
export type SudokuDigit = 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9;

export type SudokuUndefinedOrDigit = undefined | SudokuDigit;

export type SudokuDefault = SudokuCoord & {
	value: SudokuDigit,
};

