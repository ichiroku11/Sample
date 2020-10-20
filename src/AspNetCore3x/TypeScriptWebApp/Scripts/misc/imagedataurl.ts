
document.addEventListener("DOMContentLoaded", _ => {
	const droparea = document.querySelector("#droparea");
	if (!droparea) {
		return;
	}

	droparea.addEventListener("dragenter", _ => {
		console.log("dragenter");
	});

	droparea.addEventListener("dragover", _ => {
		console.log("dragover");
	});

	droparea.addEventListener("dragleave", _ => {
		console.log("dragleave");
	});

	droparea.addEventListener("drop", _ => {
		console.log("drop");
	});
});
