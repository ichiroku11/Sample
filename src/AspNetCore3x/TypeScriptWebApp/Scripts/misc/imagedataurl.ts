
document.addEventListener("DOMContentLoaded", _ => {
	const droparea = document.querySelector("#droparea");
	if (!droparea) {
		return;
	}

	droparea.addEventListener("click", _ => alert("droparea"));
});
