// 参考
// SVG エッセンシャルズ 第2版
// https://www.oreilly.co.jp/books/9784873117973/

const clock = document.querySelector<SVGSVGElement>("#clock");
const namespace = "http://www.w3.org/2000/svg";

clock?.addEventListener("load", _ => {
	try {
		clock.suspendRedraw(1000);

		// 文字盤
		const face = document.createElementNS(namespace, "circle");
		face.cx.baseVal.value = 125;
		face.cy.baseVal.value = 125;
		face.r.baseVal.value = 100;
		face.style.cssText = "fill: white; stroke: black;";
		clock.appendChild(face);

		// 目盛りグループ
		const ticks = document.createElementNS(namespace, "g");
		ticks.setAttribute("transform", "translate(125, 125)");
		clock.appendChild(ticks);

		for (let index = 0; index < 12; index++) {
			// 目盛り
			const tick = document.createElementNS(namespace, "path");
			tick.setAttribute("d", "M 95 0 L 100 -5 L 100 5 Z");
			tick.setAttribute("transform", `rotate(${30 * index})`);

			// 目盛りをグループに追加
			ticks.appendChild(tick);
		}


	} finally {
		clock.unsuspendRedrawAll();
	}
});
