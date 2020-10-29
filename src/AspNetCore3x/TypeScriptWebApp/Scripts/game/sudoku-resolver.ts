import { SudokuComponent, SudokuDigit, SudokuDefault, SudokuUndefinedOrDigit } from "./sudoku-helper";


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

	public subscribe(next: SudokuNext, completed: SudokuCompleted): this {
		this._next = next;
		this._completed = completed;

		return this;
	}

	// todo:
	public resolve(): this {

		return this;
	}
}
