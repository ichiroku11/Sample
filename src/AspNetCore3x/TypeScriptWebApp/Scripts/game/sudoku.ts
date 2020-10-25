import { SudokuDefault, SudokuResolver } from "./sudoku-resolver";

document.addEventListener("DOMContentLoaded", _ => {
	const defaults: SudokuDefault[] = [
	];

	const resolver = new SudokuResolver(defaults);
	resolver.resolve();
});
