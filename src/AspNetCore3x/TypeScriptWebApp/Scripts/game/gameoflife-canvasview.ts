import { Model } from "./gameoflife-model";

export class CanvasView {
	private readonly _model: Model;

	public constructor(model: Model) {
		this._model = model;
	}

	public render(): void {
		// todo: 描画する
	}
}
