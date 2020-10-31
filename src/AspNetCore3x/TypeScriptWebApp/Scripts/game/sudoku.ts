import { SudokuQuestions } from "./sudoku-helper";
import { SudokuResolver } from "./sudoku-resolver";
import { SudokuTableView } from "./sudoku-tableview";

document.addEventListener("DOMContentLoaded", _ => {
	const defaults = SudokuQuestions.sample1;
	const resolver = new SudokuResolver(defaults);
	const view = new SudokuTableView("#sudoku-view");

	view.init(defaults);

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
