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

	/**
	 * セルに値を配置して通知
	 * @param x
	 * @param y
	 * @param value
	 */
	private putAndNotifyNext(x: SudokuComponent, y: SudokuComponent, value: SudokuUndefinedOrDigit): void {
		this.put(x, y, value);

		console.log(`next: (${x}, ${y}), ${value}`);
		if (this._next) {
			this._next(x, y, value);
		}
	}

	/**
	 * 完了を通知
	 */
	private notifyCompleted(): void {
		if (this._completed) {
			this._completed();
		}
	}

	// Promiseのresolveと紛らわしいので別名のメソッドにしただけ
	/** 数独を解く */
	private resolveCore(): Promise<boolean> {
		return new Promise(resolve => {
			// setTimeoutを使ってUI描画のタイミングを作作る
			setTimeout(async () => {
				// 空セルを探す
				// 見つからなければ終了
				const coord = this.findUndefined();
				if (coord === null) {
					this.notifyCompleted();
					resolve(true);
					return;
				}

				// todo:
				// 空のセル群から候補が少ないセルを優先して探索する

				const { x, y } = coord;
				const choices = this.findChoices(x, y);
				console.log(`search: (${x}, ${y}), [${Array.from(choices).join(", ")}]`);

				// 深さ優先探索（バックトラッキング）による探索
				for (const choice of choices) {
					this.putAndNotifyNext(x, y, choice);

					// 再帰呼び出し
					const resolved = await this.resolveCore();
					if (resolved) {
						resolve(true);
						return;
					}

					this.putAndNotifyNext(x, y, undefined);
				}

				resolve(false);
			});
		});
	}

	/** 数独を解く */
	public resolve(): Promise<boolean> {
		return this.resolveCore();
	}

	/**
	 * 通知を購読する
	 * @param next 次の数値を配置すたときの通知コールバック
	 * @param completed 完了したときの通知コールバック
	 */
	public subscribe(next: SudokuNext, completed: SudokuCompleted): this {
		this._next = next;
		this._completed = completed;

		return this;
	}
}
