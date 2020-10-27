
// 座標のx成分、y成分
type Component = 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8;
// 座標
type Coord = {
	x: Component,
	y: Component,
};

// 数独の数値
type Digit = 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9;
type UndefinedOrDigit = undefined | Digit;

export type SudokuDefault = Coord & {
	value: Digit,
};


/*
// todo: cell
type SudokuCell = {
	readonly: boolean;
	value: UndefinedOrDigit;
};
*/

/**
 * 数独リゾルバ
 */
export class SudokuResolver {
	// マス
	// todo: 9 * 9
	private readonly _cells: UndefinedOrDigit[] = new Array<UndefinedOrDigit>(9 * 9);

	public constructor(defaults: SudokuDefault[]) {
		this.init(defaults);
	}

	/**
	 * 指定した座標 => 配列のindex
	 * @param x
	 * @param y
	 */
	private index(x: Component, y: Component): number {
		// todo: 9
		return x + y * 9;
	}

	/**
	 * 初期化する
	 * @param inits 生存しているセルの位置
	 */
	private init(defaults: SudokuDefault[]): void {
		this._cells.fill(undefined);

		for (const { x, y, value } of defaults) {
			this.digit(x, y, value);
		}
	}

	// 指定したセルの数値を取得
	private digit(x: Component, y: Component): UndefinedOrDigit;
	// 指定したセルの値を設定
	private digit(x: Component, y: Component, value: UndefinedOrDigit): void;
	private digit(x: Component, y: Component, value?: UndefinedOrDigit): void | UndefinedOrDigit {
		const index = this.index(x, y);
		if (value == undefined) {
			return this._cells[index];
		}

		this._cells[index] = value;
		return;
	}

	public subscribe(
		next: (x: Component, y: Component, value: Digit) => void,
		completed: () => void): this {
		// todo:

		return this;
	}

	public resolve(): this {
		return this;
	}
}
