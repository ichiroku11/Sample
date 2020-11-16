import { SudokuBoard } from "./sudoku-board";
import { SudokuComponent, SudokuUndefinedOrDigit } from "./sudoku-helper";

// nextコールバック
type SudokuNext = (x: SudokuComponent, y: SudokuComponent, value: SudokuUndefinedOrDigit) => void;
// completedコールバック
type SudokuCompleted = () => void;

/**
 * 数独リゾルバ
 */
export class SudokuResolver {
	private readonly _board: SudokuBoard;

	private _next: SudokuNext = () => { };
	private _completed: SudokuCompleted = () => { };

	constructor(board: SudokuBoard) {
		this._board = board;
	}

	/**
	 * セルに値を配置して通知
	 * @param x
	 * @param y
	 * @param value
	 */
	private putAndNotifyNext(x: SudokuComponent, y: SudokuComponent, value: SudokuUndefinedOrDigit): void {
		this._board.put(x, y, value);

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
				const coord = this._board.getUndefined();
				if (coord === null) {
					this.notifyCompleted();
					resolve(true);
					return;
				}

				// todo:
				// 空のセル群から候補が少ないセルを優先して探索する

				const { x, y } = coord;
				const choices = this._board.getChoices(x, y);
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
