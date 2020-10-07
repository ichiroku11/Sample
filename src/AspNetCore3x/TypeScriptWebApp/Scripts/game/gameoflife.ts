import { CanvasView } from "./gameoflife-canvasview";
import { Model } from "./gameoflife-model";

document.addEventListener("DOMContentLoaded", event => {
	console.log("DOMContentLoaded");

	var model = new Model();
	var view = new CanvasView(model);

	view.render();
});
