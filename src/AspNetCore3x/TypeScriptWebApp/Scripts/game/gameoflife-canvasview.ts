import { Model } from "./gameoflife-model";

export class CanvasView {
	private readonly _options = {
		cellSize: 10,
		fillStyle: "rgba(143, 163, 245, 0.3)",
	};

	private readonly _model: Model;
	private readonly _element: HTMLCanvasElement;

	public constructor(model: Model) {
		this._model = model;

		const canvas = document.createElement("canvas");
		canvas.width = this._model.width * this._options.cellSize;
		canvas.height = this._model.height * this._options.cellSize;
		this._element = canvas;
	}

	public get element() {
		return this._element;
	}

	public render(): void {
		const width = this._element.width;
		const height = this._element.height;
		const context = this._element.getContext("2d");
		if (context == null) {
			return;
		}

		context.clearRect(0, 0, width, height);

		context.fillStyle = this._options.fillStyle;
		const cellSize = this._options.cellSize;
		const x = 10;
		const y = 20;
		context.fillRect(x * cellSize, y * cellSize, cellSize, cellSize);
	}
}
