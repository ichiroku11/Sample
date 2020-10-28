import { SudokuDefault } from "./sudoku-helper";
import { SudokuResolver } from "./sudoku-resolver";
import { SudokuTableView } from "./sudoku-tableview";

document.addEventListener("DOMContentLoaded", _ => {
	const defaults: SudokuDefault[] = [
		{ x: 1, y: 2, value: 7},
	];

	const view = new SudokuTableView("#sudoku-view");
	view.init(defaults);

	const resolver = new SudokuResolver(defaults);
	resolver
		.subscribe(
			(x, y, value) => {
				view.update(x, y, value);
			},
			() => {
			}
		)
		.resolve();
});
