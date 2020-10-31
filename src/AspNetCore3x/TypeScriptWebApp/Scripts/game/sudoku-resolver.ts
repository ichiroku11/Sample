import {
	SudokuComponent,
	SudokuDigit,
	SudokuDefault,
	SudokuUndefinedOrDigit,
	sudokuDigits,
	sudokuComponents
} from "./sudoku-helper";

type SudokuNext = (x: SudokuComponent, y: SudokuComponent, value: SudokuDigit) => void;
type SudokuCompleted = () => void;

/**
 * 数独リゾルバ
 */
export class SudokuResolver {
	// マス
	// todo: 9 * 9
	private readonly _cells: SudokuUndefinedOrDigit[] = new Array<SudokuUndefinedOrDigit>(9 * 9);
	private _next: SudokuNext = () => { };
	private _completed: SudokuCompleted = () => { };


	public constructor(defaults: SudokuDefault[]) {
		this.init(defaults);
	}

	/**
	 * 指定した座標 => 配列のindex
	 * @param x
	 * @param y
	 */
	private index(x: SudokuComponent, y: SudokuComponent): number {
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
	private digit(x: SudokuComponent, y: SudokuComponent): SudokuUndefinedOrDigit;
	// 指定したセルの値を設定
	private digit(x: SudokuComponent, y: SudokuComponent, value: SudokuUndefinedOrDigit): void;
	private digit(x: SudokuComponent, y: SudokuComponent, value?: SudokuUndefinedOrDigit): void | SudokuUndefinedOrDigit {
		const index = this.index(x, y);
		if (value == undefined) {
			return this._cells[index];
		}

		this._cells[index] = value;
		return;
	}

	private findInRow(x: SudokuComponent): SudokuDigit[] {
		const digits: SudokuDigit[] = [];

		for (const y of sudokuComponents) {
			const value = this.digit(x, y);
			if (value !== undefined) {
				digits.push(value);
			}
		}

		return digits;
	}

	private findInCol(y: SudokuComponent): SudokuDigit[] {
		const digits: SudokuDigit[] = [];

		for (const x of sudokuComponents) {
			const value = this.digit(x, y);
			if (value !== undefined) {
				digits.push(value);
			}
		}

		return digits;
	}

	private blockRange(value: SudokuComponent): SudokuComponent[] {
		const start = Math.floor(value / 3) * 3;
		return Array.from({ length: 3 }, (_, index) => start + index) as SudokuComponent[];
	}

	private findInBlock(x: SudokuComponent, y: SudokuComponent): SudokuDigit[] {
		const digits: SudokuDigit[] = [];

		// ブロック内の数字を列挙する
		for (const bx of this.blockRange(x)) {
			for (const by of this.blockRange(y)) {
				const value = this.digit(bx, by);
				if (value !== undefined) {
					digits.push(value);
				}
			}
		}

		return digits;
	}

	private findChoices(x: SudokuComponent, y: SudokuComponent): SudokuDigit[] {
		const choices = Array.from(sudokuDigits);

		// todo:
		// 同じ行で使われている数字を取り除く
		// 同じ列で使われている数字を取り除く
		// 同じ3x3ブロックで使われている数字を取り除く

		return choices;
	}

	public subscribe(next: SudokuNext, completed: SudokuCompleted): this {
		this._next = next;
		this._completed = completed;

		return this;
	}

	// todo:
	public resolve(): this {

		// 空セルの候補を探す
		// 候補の一番少ないセルで確定できるものがあれば確定する


		return this;
	}
}
