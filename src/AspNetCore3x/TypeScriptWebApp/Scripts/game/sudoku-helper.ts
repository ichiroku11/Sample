
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

export class SudokuQuestions {
	// easy
	public static sample1: SudokuDefault[] = [
		{ x: 1, y: 0, value: 2 },
		{ x: 2, y: 0, value: 6 },
		{ x: 6, y: 0, value: 3 },
		{ x: 7, y: 0, value: 7 },
		{ x: 8, y: 0, value: 8 },

		{ x: 1, y: 1, value: 5 },
		{ x: 2, y: 1, value: 8 },
		{ x: 3, y: 1, value: 6 },
		{ x: 4, y: 1, value: 3 },
		{ x: 5, y: 1, value: 7 },
		{ x: 6, y: 1, value: 4 },

		{ x: 1, y: 2, value: 4 },
		{ x: 2, y: 2, value: 7 },
		{ x: 6, y: 2, value: 5 },
		{ x: 7, y: 2, value: 6 },
		{ x: 8, y: 2, value: 1 },

		{ x: 3, y: 3, value: 7 },
		{ x: 4, y: 3, value: 2 },
		{ x: 6, y: 3, value: 9 },

		{ x: 3, y: 4, value: 3 },
		{ x: 5, y: 4, value: 8 },
		{ x: 6, y: 4, value: 2 },
		{ x: 7, y: 4, value: 5 },

		{ x: 0, y: 5, value: 8 },
		{ x: 2, y: 5, value: 2 },
		{ x: 7, y: 5, value: 1 },

		{ x: 0, y: 6, value: 4 },
		{ x: 1, y: 6, value: 6 },
		{ x: 2, y: 6, value: 9 },
		{ x: 3, y: 6, value: 5 },
		{ x: 5, y: 6, value: 1 },

		{ x: 2, y: 7, value: 1 },
		{ x: 3, y: 7, value: 9 },
		{ x: 6, y: 7, value: 7 },
		{ x: 7, y: 7, value: 4 },

		{ x: 1, y: 8, value: 3 },
		{ x: 4, y: 8, value: 4 },
		{ x: 7, y: 8, value: 9 },
	];

	// easy
	public static sample2: SudokuDefault[] = [
		{ x: 1, y: 0, value: 8 },
		{ x: 3, y: 0, value: 7 },
		{ x: 5, y: 0, value: 9 },
		{ x: 8, y: 0, value: 2 },

		{ x: 1, y: 1, value: 3 },
		{ x: 2, y: 1, value: 4 },
		{ x: 4, y: 1, value: 1 },
		{ x: 7, y: 1, value: 9 },

		{ x: 3, y: 2, value: 3 },
		{ x: 5, y: 2, value: 8 },

		{ x: 2, y: 3, value: 6 },
		{ x: 3, y: 3, value: 4 },
		{ x: 4, y: 3, value: 3 },
		{ x: 6, y: 3, value: 8 },
		{ x: 8, y: 3, value: 1 },

		{ x: 2, y: 4, value: 1 },
		{ x: 3, y: 4, value: 2 },
		{ x: 4, y: 4, value: 7 },
		{ x: 5, y: 4, value: 6 },
		{ x: 7, y: 4, value: 4 },

		{ x: 2, y: 5, value: 3 },
		{ x: 5, y: 5, value: 1 },
		{ x: 6, y: 5, value: 2 },
		{ x: 7, y: 5, value: 5 },
		{ x: 8, y: 5, value: 6 },

		{ x: 4, y: 6, value: 9 },
		{ x: 7, y: 6, value: 2 },
		{ x: 8, y: 6, value: 7 },

		{ x: 0, y: 7, value: 3 },
		{ x: 1, y: 7, value: 4 },
		{ x: 3, y: 7, value: 8 },
		{ x: 4, y: 7, value: 6 },
		{ x: 5, y: 7, value: 7 },
		{ x: 6, y: 7, value: 9 },

		{ x: 1, y: 8, value: 9 },
		{ x: 3, y: 8, value: 5 },
		{ x: 5, y: 8, value: 4 },
		{ x: 8, y: 8, value: 3 },
	];
}
