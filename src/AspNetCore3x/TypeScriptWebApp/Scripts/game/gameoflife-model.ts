export class Model {
	private readonly _width: number;
	private readonly _height: number;

	public constructor(width: number, height: number) {
		this._width = width;
		this._height = height;
	}

	public get width() {
		return this._width;
	}

	public get height() {
		return this._height;
	}


	// 指定した座標のセルが生きているかどうかを取得
	public alive(x: number, y: number) {
		// todo: 仮
		return Math.random() > 0.8;
	}

	// 次の世代へ
	public next(): void {
		// todo:
	}
}
