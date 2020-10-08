import { CanvasView } from "./gameoflife-canvasview";
import { Model } from "./gameoflife-model";

document.addEventListener("DOMContentLoaded", event => {
	const model = Model.random(40, 30);
	const view = new CanvasView(model);
	document.querySelector("#content")?.append(view.element);

	view.render();

	setInterval(() => {
		model.next();
		view.render();
	}, 250);

	// requestAnimationFrameだと再描画が頻度が多い
	// もう少しゆっくりがいいので
	/*
	const update = () => {
		model.next();
		view.render();
		requestAnimationFrame(update);
	};

	requestAnimationFrame(update);
	*/
});
