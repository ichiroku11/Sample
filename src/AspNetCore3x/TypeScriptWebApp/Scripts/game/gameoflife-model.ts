import { range } from "./gameoflife-helper";

type Point = {
	x: number,
	y: number,
};

export class Model {
	public static random(width: number, height: number): Model {
		const alives: Point[] = [];
		for (const x of range(0, width)) {
			for (const y of range(0, height)) {
				if (Math.random() >= 0.1) {
					continue;
				}

				alives.push({ x, y });
			}
		}

		const model = new Model(width, height);
		model.init(alives);
		return model;
	}

	private readonly _width: number;
	private readonly _height: number;
	// 現在の生存状態
	private readonly _cells: boolean[];

	public constructor(width: number, height: number) {
		this._width = width;
		this._height = height;

		const length = width * height;
		this._cells = new Array<boolean>(length);
		this.init();
	}

	public get width(): number {
		return this._width;
	}

	public get height(): number {
		return this._height;
	}

	// (x, y) => 配列のindex
	private index(x: number, y: number): number {
		return y * this._width + x;
	}

	// 指定した座標のセルの生存状態を取得する・設定する
	public alive(x: number, y: number, alive: boolean): void;
	public alive(x: number, y: number): boolean;
	public alive(x: number, y: number, alive?: boolean): void | boolean {
		const index = this.index(x, y);
		if (alive === undefined) {
			return this._cells[index];
		}

		this._cells[index] = alive;
		return;
	}

	// 初期化
	public init(alives?: Point[]): void {
		this._cells.fill(false);

		alives?.forEach(value => {
			this.alive(value.x, value.y, true);
		});
	}

	// 周囲8セルのうち生きているセルの個数を求める
	private countAlive(x: number, y: number): number {
		// 上下左右のセルの位置
		const top = (y - 1 + this._height) % this._height;
		const bottom = (y + 1) % this._height;
		const left = (x - 1 + this._width) % this._width;
		const right = (x + 1) % this._width;

		// 集計するセル
		const cells = [
			// 上の行
			{ x: left, y: top }, { x: x, y: top }, { x: right, y: top },
			// 真ん中の行
			{ x: left, y: y }, { x: right, y: y },
			// 下の行
			{ x: left, y: bottom }, { x: x, y: bottom }, { x: right, y: bottom },
		];

		return cells
			.filter(cell => this.alive(cell.x, cell.y))
			.length;
	}

	// 次の世代へ
	public next(): void {
		for (const x of range(0, this._width)) {
			for (const y of range(0, this._height)) {
				// もう少し効率的な方法あるかも？
				const count = this.countAlive(x, y);

				// 周囲8セルの生存数から次の生存状態を決定する
				let alive = false;
				if (this.alive(x, y)) {
					if (count == 2 || count == 3) {
						alive = true;
					}
				} else {
					if (count == 3) {
						alive = true;
					}
				}

				this.alive(x, y, alive);
			}
		}
	}
}
