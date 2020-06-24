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

		// 時計の針グループ
		const hands = document.createElementNS(namespace, "g");
		hands.style.cssText = "stroke: black; stroke-width: 5px; stroke-linecap: round;";
		clock.appendChild(hands);

		// 時計の針（時分秒）
		[
			{ id: "hour", length: 50, deg: 90, attr: null },
			{ id: "minute", length: 80, deg: 0, attr: null },
			{ id: "second", length: 90, deg: 30, attr: { stroke: "red", strokeWidth: "2px" } }
		].forEach(({ id, length, deg, attr }) => {
			const hand = document.createElementNS(namespace, "path");
			hand.id = id;
			hand.setAttribute("d", `M 125 125 L 125 ${125 - length}`);
			hand.setAttribute("transform", `rotate(${deg}, 125, 125)`);

			if (attr) {
				hand.style.stroke = attr.stroke;
				hand.style.strokeWidth = attr.strokeWidth;
			}

			// 時計の針をグループに追加
			hands.appendChild(hand);
		});

	} finally {
		clock.unsuspendRedrawAll();
	}
});
