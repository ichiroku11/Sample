

function isImage(blob: Blob): boolean {
	switch (blob.type) {
		case "image/png":
		case "image/jpeg":
			break;
	}

	return false;
}

function readAsDataUrl(blob: Blob): Promise<string> {
	return new Promise<string>((resolve, reject) => {
		const reader = new FileReader();
		reader.addEventListener("load", _ => resolve(reader.result as string));
		reader.addEventListener("error", _ => reject(reader.error));
		reader.readAsDataURL(blob);
	});
}

document.addEventListener("DOMContentLoaded", _ => {
	const droparea = document.querySelector("#droparea");
	if (!droparea) {
		return;
	}

	const result = document.querySelector("#result");
	if (!result) {
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

	droparea.addEventListener("drop", async event => {
		droparea.classList.remove("dragging");

		event.preventDefault();

		if (!(event instanceof DragEvent)) {
			return;
		}

		const files = event.dataTransfer?.files;
		if (!files) {
			return;
		}
		if (files.length < 1) {
			return;
		}

		const file = files[0];
		if (!isImage(file)) {
			return;
		}
		const dataUrl = await readAsDataUrl(file);

		while (result.firstChild) {
			result.removeChild(result.firstChild);
		}
		const image = document.createElement("img");
		image.src = dataUrl;
		result.append(image);
	});
});
