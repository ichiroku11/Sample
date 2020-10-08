import { range } from "./gameoflife-helper";
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

	// HTML要素を取得
	public get element() {
		return this._element;
	}

	// 描画
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
		for (const x of range(0, this._model.width)) {
			for (const y of range(0, this._model.height)) {
				if (!this._model.alive(x, y)) {
					continue;
				}
				context.fillRect(x * cellSize, y * cellSize, cellSize, cellSize);
			}
		}
	}
}
