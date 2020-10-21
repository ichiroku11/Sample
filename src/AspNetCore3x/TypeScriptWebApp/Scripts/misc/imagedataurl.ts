
document.addEventListener("DOMContentLoaded", _ => {
	const droparea = document.querySelector("#droparea");
	if (!droparea) {
		return;
	}

	// https://developer.mozilla.org/ja/docs/DragDrop/Drag_Operations
	// dragenter および dragover イベントのどちらにおいても、
	// preventDefault() メソッドを呼び出すと、
	// その場所がドロップ可能な場所であるということを示します

	droparea.addEventListener("dragenter", event => {
		droparea.classList.add("dragging");

		// dragenterイベントとdragoverイベントでpreventDefault()メソッドを呼び出すとdropイベントが発生する
		event.preventDefault();
	});

	droparea.addEventListener("dragover", event => {
		event.preventDefault();
	});

	droparea.addEventListener("dragleave", _ => {
		droparea.classList.remove("dragging");
	});

	droparea.addEventListener("drop", event => {
		droparea.classList.remove("dragging");

		event.preventDefault();

		if (!(event instanceof DragEvent)) {
			return;
		}

		var files = event.dataTransfer?.files;
		if (!files) {
			return;
		}
		if (files.length < 1) {
			return;
		}

		var file = files[0];

		// todo:
		console.log(file.name);
		console.log(file.type);
		console.log(file.size);
	});
});
