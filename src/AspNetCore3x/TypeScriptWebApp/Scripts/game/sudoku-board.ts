import { range, sudokuCellCount, SudokuComponent, sudokuComponents, SudokuCoord, SudokuDefault, SudokuDigit, sudokuDigits, SudokuUndefinedOrDigit } from "./sudoku-helper";

/**
 * 数独ボード
 */
export class SudokuBoard {
	// マス
	private readonly _cells: SudokuUndefinedOrDigit[] = new Array<SudokuUndefinedOrDigit>(sudokuCellCount * sudokuCellCount);

	/**
	 * 
	 * @param defaults
	 */
	public constructor(defaults: SudokuDefault[]) {
		this.init(defaults);
	}

	/**
	 * 指定した座標 => 配列のindex
	 * @param x
	 * @param y
	 */
	private index(x: SudokuComponent, y: SudokuComponent): number {
		return x + y * sudokuCellCount;
	}

	/**
	 * 初期化する
	 * @param inits 生存しているセルの位置
	 */
	private init(defaults: SudokuDefault[]): void {
		this._cells.fill(undefined);

		for (const { x, y, value } of defaults) {
			this.put(x, y, value);
		}
	}

	/**
	 * 指定したセルの数値を取得
	 * @param x
	 * @param y
	 */
	public get(x: SudokuComponent, y: SudokuComponent): SudokuUndefinedOrDigit {
		const index = this.index(x, y);
		return this._cells[index];
	}

	/**
	 * 指定したセルの値を設定
	 * @param x
	 * @param y
	 * @param value
	 */
	public put(x: SudokuComponent, y: SudokuComponent, value: SudokuUndefinedOrDigit): void {
		const index = this.index(x, y);
		this._cells[index] = value;
	}

	/**
	 * 数字が設定されていないセルを取得
	 * @returns セルの座標（見つからない場合はnull）
	 */
	public getUndefined(): SudokuCoord | null {
		for (const y of sudokuComponents) {
			for (const x of sudokuComponents) {
				const value = this.get(x, y);
				if (value === undefined) {
					return { x, y };
				}
			}
		}

		return null;
	}

	/**
	 * 指定した列で使われている数字を取得
	 * @param x
	 */
	private getUsedInCol(x: SudokuComponent): SudokuDigit[] {
		const values: SudokuDigit[] = [];

		for (const y of sudokuComponents) {
			const digit = this.get(x, y);
			if (digit !== undefined) {
				values.push(digit);
			}
		}

		return values;
	}

	/**
	 * 指定した行で使われている値を取得
	 * @param y	
	 */
	private getUsedInRow(y: SudokuComponent): SudokuDigit[] {
		const values: SudokuDigit[] = [];

		for (const x of sudokuComponents) {
			const value = this.get(x, y);
			if (value !== undefined) {
				values.push(value);
			}
		}

		return values;
	}

	/**
	 * 指定した座標のブロックの座標成分を取得
	 * @param value
	 */
	private getBlockRange(value: SudokuComponent): SudokuComponent[] {
		// ブロック内の1行（1列）文のセル数
		const count = sudokuCellCount / 3;
		const start = Math.floor(value / count) * count;
		return range(start, count) as SudokuComponent[];
	}

	/**
	 * ブロック内で使われている値を探す
	 * @param x
	 * @param y
	 */
	private getUsedInBlock(x: SudokuComponent, y: SudokuComponent): SudokuDigit[] {
		const values: SudokuDigit[] = [];

		// ブロック内の数字を列挙する
		for (const by of this.getBlockRange(y)) {
			for (const bx of this.getBlockRange(x)) {
				const value = this.get(bx, by);
				if (value !== undefined) {
					values.push(value);
				}
			}
		}

		return values;
	}

	/**
	 * 指定したセルに配置できる値の候補を取得
	 * @param x
	 * @param y
	 */
	public getChoices(x: SudokuComponent, y: SudokuComponent): Set<SudokuDigit> {
		const choiceDigits = new Set(sudokuDigits);

		// 同じ行・同じ列・同じ3x3ブロックで使われている数字を取り除く
		const usedDigits = new Set([
			...this.getUsedInRow(y),
			...this.getUsedInCol(x),
			...this.getUsedInBlock(x, y)
		]);
		for (const usedDigit of usedDigits) {
			choiceDigits.delete(usedDigit);
		}

		return choiceDigits;
	}
}
