import { SudokuBoard } from "./sudoku-board";
import { SudokuComponent, SudokuUndefinedOrDigit } from "./sudoku-helper";

// nextコールバック
type SudokuNext = (x: SudokuComponent, y: SudokuComponent, value: SudokuUndefinedOrDigit) => void;
// completedコールバック
type SudokuCompleted = () => void;


/**
 * 数独リゾルバ
 */
abstract class SudokuResolver {
	private readonly _board: SudokuBoard;

	private _next: SudokuNext = () => { };
	private _completed: SudokuCompleted = () => { };

	/**
	 * 
	 * @param board
	 */
	constructor(board: SudokuBoard) {
		this._board = board;
	}

	/** 数独ボードを取得 */
	protected get board(): SudokuBoard {
		return this._board;
	}

	/**
	 * セルに値を配置して通知
	 * @param x
	 * @param y
	 * @param value
	 */
	protected putAndNotifyNext(x: SudokuComponent, y: SudokuComponent, value: SudokuUndefinedOrDigit): void {
		this.board.put(x, y, value);

		console.log(`next: (${x}, ${y}), ${value}`);
		if (this._next) {
			this._next(x, y, value);
		}
	}

	/**
	 * 完了を通知
	 */
	protected notifyCompleted(): void {
		if (this._completed) {
			this._completed();
		}
	}

	/** 数独を解く */
	protected abstract resolveCore(): Promise<boolean>;

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

/**
 * 数独リゾルバ
 * 左上の空セルからバックトラッキングで探索する
 */
export class SimpleSudokuResolver extends SudokuResolver {
	/** 数独を解く */
	protected resolveCore(): Promise<boolean> {
		return new Promise(resolve => {
			// setTimeoutを使ってUI描画のタイミングを作る
			setTimeout(async () => {
				// 空セルを探す
				// 見つからなければ終了
				const coord = this.board.getUndefined();
				if (coord === null) {
					this.notifyCompleted();
					resolve(true);
					return;
				}

				const { x, y } = coord;
				const choices = this.board.getChoices(x, y);
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
}


/**
 * 数独リゾルバ
 * 候補が少ないセルを優先的に探索する
 */
export class FewChoicesFirstSudokuResolver extends SudokuResolver {
	/**
	 * 
	 */
	protected resolveCore(): Promise<boolean> {
		// todo:
		// 空のセル群から候補が少ないセルを優先して探索する

		throw new Error("Method not implemented.");
	}
}
