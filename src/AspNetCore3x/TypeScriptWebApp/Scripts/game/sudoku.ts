import { SudokuResolver } from "./sudoku-resolver";

document.addEventListener("DOMContentLoaded", _ => {
	var resolver = new SudokuResolver();
	resolver.resolve();
});
