import { range, sudokuCellCount, SudokuComponent, sudokuComponents, SudokuCoord, SudokuDefault, SudokuDigit, sudokuDigits, SudokuUndefinedOrDigit } from "./sudoku-helper";

// nextコールバック
type SudokuNext = (x: SudokuComponent, y: SudokuComponent, value: SudokuUndefinedOrDigit) => void;
// completedコールバック
type SudokuCompleted = () => void;

/**
 * 数独リゾルバ
 */
export class SudokuResolver {
	// マス
	private readonly _cells: SudokuUndefinedOrDigit[] = new Array<SudokuUndefinedOrDigit>(sudokuCellCount * sudokuCellCount);
	private _next: SudokuNext = () => { };
	private _completed: SudokuCompleted = () => { };

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

	// 指定したセルの数値を取得
	public get(x: SudokuComponent, y: SudokuComponent): SudokuUndefinedOrDigit {
		const index = this.index(x, y);
		return this._cells[index];
	}

	// 指定したセルの値を設定
	private put(x: SudokuComponent, y: SudokuComponent, value: SudokuUndefinedOrDigit): void {
		const index = this.index(x, y);
		this._cells[index] = value;
	}

	private findUndefined(): SudokuCoord | null {
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

	private findUsedInCol(x: SudokuComponent): SudokuDigit[] {
		const values: SudokuDigit[] = [];

		for (const y of sudokuComponents) {
			const digit = this.get(x, y);
			if (digit !== undefined) {
				values.push(digit);
			}
		}

		return values;
	}

	private findUsedInRow(y: SudokuComponent): SudokuDigit[] {
		const values: SudokuDigit[] = [];

		for (const x of sudokuComponents) {
			const value = this.get(x, y);
			if (value !== undefined) {
				values.push(value);
			}
		}

		return values;
	}

	private getBlockRange(value: SudokuComponent): SudokuComponent[] {
		// ブロック内の1行（1列）文のセル数
		const count = sudokuCellCount / 3;
		const start = Math.floor(value / count) * count;
		return range(start, count) as SudokuComponent[];
	}

	private findUsedInBlock(x: SudokuComponent, y: SudokuComponent): SudokuDigit[] {
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

	private findChoices(x: SudokuComponent, y: SudokuComponent): Set<SudokuDigit> {
		const choiceDigits = new Set(sudokuDigits);

		// 同じ行・同じ列・同じ3x3ブロックで使われている数字を取り除く
		const usedDigits = new Set([
			...this.findUsedInRow(y),
			...this.findUsedInCol(x),
			...this.findUsedInBlock(x, y)
		]);
		for (const usedDigit of usedDigits) {
			choiceDigits.delete(usedDigit);
		}

		return choiceDigits;
	}

	private next(x: SudokuComponent, y: SudokuComponent, value: SudokuUndefinedOrDigit): void {
		console.log(`next: (${x}, ${y}), ${value}`);
		if (this._next) {
			this._next(x, y, value);
		}
	}

	private completed(): void {
		if (this._completed) {
			this._completed();
		}
	}

	public resolve(): boolean {
		// 空セルを探す
		// 見つからなければ終了
		const coord = this.findUndefined();
		if (coord === null) {
			this.completed();
			return true;
		}

		const { x, y } = coord;
		const choices = this.findChoices(x, y);
		console.log(`search: (${x}, ${y}), [${Array.from(choices).join(", ")}]`);

		// 深さ優先探索（バックトラッキング）による探索
		for (const choice of choices) {
			this.put(x, y, choice);
			this.next(x, y, choice);

			// 再帰呼び出し
			const resolved = this.resolve();
			if (resolved) {
				return true;
			}

			this.put(x, y, undefined);
			this.next(x, y, undefined);
		}

		return false;
	}

	public subscribe(next: SudokuNext, completed: SudokuCompleted): this {
		this._next = next;
		this._completed = completed;

		return this;
	}
}
