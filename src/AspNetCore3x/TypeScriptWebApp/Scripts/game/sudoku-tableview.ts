import { range, sudokuCellCount, SudokuComponent, SudokuDefault, SudokuUndefinedOrDigit } from "./sudoku-helper";

/*
 * 数独table要素ビュー
 */
export class SudokuTableView {
	private readonly _table: HTMLTableElement;

	/**
	 * 
	 * @param selector コンテナのセレクタ
	 */
	public constructor(selector: string) {
		this._table = this.create();

		document.querySelector(selector)?.appendChild(this._table);
	}

	/** table要素を生成する */
	private create(): HTMLTableElement {
		const table = document.createElement("table");
		const tbody = document.createElement("tbody");
		for (const _ of range(0, sudokuCellCount)) {
			const tr = document.createElement("tr");
			for (const _ of range(0, sudokuCellCount)) {
				const td = document.createElement("td");
				tr.appendChild(td);
			}
			tbody.appendChild(tr);
		}
		table.appendChild(tbody);

		return table;
	}

	/**
	 * セルを更新する
	 * @param x
	 * @param y
	 * @param value
	 * @param fixed 固定セルかどうか
	 */
	public update(x: SudokuComponent, y: SudokuComponent, value: SudokuUndefinedOrDigit, fixed = false) {
		const tr = this._table.querySelector<HTMLTableRowElement>(`tr:nth-child(${y + 1})`);
		if (!tr) {
			// todo:
			return;
		}

		const td = tr.querySelector(`td:nth-child(${x + 1})`);
		if (!td) {
			// todo:
			return;
		}

		td.textContent = value === undefined
			? ""
			: value.toString();

		if (fixed) {
			td.classList.add("fixed");
		}
	}

	/**
	 * table要素の初期化
	 * @param defaults 初期値
	 */
	public init(defaults: SudokuDefault[]) {
		for (const { x, y, value } of defaults) {
			this.update(x, y, value, true);
		}
	}
}
