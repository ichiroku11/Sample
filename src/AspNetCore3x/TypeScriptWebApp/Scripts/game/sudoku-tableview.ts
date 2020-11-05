import { SudokuComponent, SudokuDefault, SudokuUndefinedOrDigit } from "./sudoku-helper";

type Range = (start: number, count: number) => number[];

/**
 * 指定した範囲内の整数の配列を生成する
 * @param start 最初の値
 * @param count 生成する整数の数
 */
const range: Range = (start, count) => Array.from({ length: count }, (_, index) => start + index);

/*
 * 数独table要素ビュー
 */
export class SudokuTableView {
	private readonly _table: HTMLTableElement;

	public constructor(selector: string) {
		this._table = this.create();

		document.querySelector(selector)?.appendChild(this._table);
	}

	private create(): HTMLTableElement {
		const table = document.createElement("table");
		const tbody = document.createElement("tbody");
		for (const _ of range(0, 9)) {
			const tr = document.createElement("tr");
			for (const _ of range(0, 9)) {
				const td = document.createElement("td");
				tr.appendChild(td);
			}
			tbody.appendChild(tr);
		}
		table.appendChild(tbody);

		return table;
	}

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

	public init(defaults: SudokuDefault[]) {
		for (const { x, y, value } of defaults) {
			this.update(x, y, value, true);
		}
	}
}
