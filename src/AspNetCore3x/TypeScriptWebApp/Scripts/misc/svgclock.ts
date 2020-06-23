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

	} finally {
		clock.unsuspendRedrawAll();
	}
});
