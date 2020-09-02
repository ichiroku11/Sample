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
			{ id: "hour", length: 50, attr: null },
			{ id: "minute", length: 80, attr: null },
			{ id: "second", length: 90, attr: { stroke: "red", strokeWidth: "2px" } }
		].forEach(({ id, length, attr }) => {
			const hand = document.createElementNS(namespace, "path");
			hand.id = id;
			hand.setAttribute("d", `M 125 125 L 125 ${125 - length}`);
			// getItemで取得できるようにするため？
			hand.setAttribute("transform", "rotate(0, 125, 125)");

			if (attr) {
				hand.style.stroke = attr.stroke;
				hand.style.strokeWidth = attr.strokeWidth;
			}

			// 時計の針をグループに追加
			hands.appendChild(hand);
		});

		// 中央のノブ
		const knob = document.createElementNS(namespace, "circle");
		knob.setAttribute("cx", "125");
		knob.setAttribute("cy", "125");
		knob.setAttribute("r", "6");
		knob.style.setProperty("fill", "#333");
		clock.appendChild(knob);
	} finally {
		clock.unsuspendRedrawAll();
	}

	const hands = {
		hour: document.querySelector<SVGPathElement>("#hour"),
		minute: document.querySelector<SVGPathElement>("#minute"),
		second: document.querySelector<SVGPathElement>("#second"),
	};

	const transforms = {
		hour: hands.hour?.transform.baseVal.getItem(0),
		minute: hands.minute?.transform.baseVal.getItem(0),
		second: hands.second?.transform.baseVal.getItem(0),
	};

	const secPerMinute = 60;
	const secPerHour = 60 * 60;
	const secPer12Hours = 60 * 60 * 12;

	updateClock();

	function updateClock() {
		const date = new Date();
		// 00:00からの秒数
		const time = date.getMilliseconds() / 1000
			+ date.getSeconds()
			+ date.getMinutes() * 60
			+ date.getHours() * 60 * 60;

		// それぞれの針の回転角度
		const degs = {
			hour: 360 * (time % secPer12Hours) / secPer12Hours,
			minute: 360 * (time % secPerHour) / secPerHour,
			second: 360 * (time % secPerMinute) / secPerMinute,
		};

		transforms.hour?.setRotate(degs.hour, 125, 125);
		transforms.minute?.setRotate(degs.minute, 125, 125);
		transforms.second?.setRotate(degs.second, 125, 125);

		requestAnimationFrame(updateClock);
	}
});
