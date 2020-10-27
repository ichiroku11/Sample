import { SudokuDefault, SudokuResolver } from "./sudoku-resolver";

document.addEventListener("DOMContentLoaded", _ => {
	const defaults: SudokuDefault[] = [
	];

	// todo:
	/*
	const view = new SudokuTableView("#sudoku-view");
	view.init(defaults);
	*/

	const resolver = new SudokuResolver(defaults);
	resolver
		.subscribe(
			(x, y, value) => {
				// todo: 各セルの更新
				/*
				view.set(x, y, value);
				*/
			},
			() => {
				// todo: 完了
			}
		)
		.resolve();
});
