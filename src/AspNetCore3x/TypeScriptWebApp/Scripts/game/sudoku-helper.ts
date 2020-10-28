
// 座標のx成分、y成分
export type Component = 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8;

// 座標
export type Coord = {
	x: Component,
	y: Component,
};

// 数独の数値
export type Digit = 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9;

export type UndefinedOrDigit = undefined | Digit;

export type SudokuDefault = Coord & {
	value: Digit,
};

