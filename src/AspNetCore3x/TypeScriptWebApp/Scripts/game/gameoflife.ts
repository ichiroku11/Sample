import { CanvasView } from "./gameoflife-canvasview";
import { Model } from "./gameoflife-model";

document.addEventListener("DOMContentLoaded", event => {
	var model = new Model(40, 30);
	// todo: model.randam();

	var view = new CanvasView(model);
	document.querySelector("#content")?.append(view.element);

	view.render();

	requestAnimationFrame(() => {
		model.next();
		view.render();
	});
});
