const path = require("path");

const config = {
	entry: {
		index: path.resolve(__dirname, "scripts/index.ts"),
		"game/gameoflife": path.resolve(__dirname, "scripts/game/gameoflife.ts"),
		"vuejs/usewebpack": path.resolve(__dirname, "scripts/vuejs/usewebpack.ts")
	},
	output: {
		filename: "[name].bundle.js",
		path: path.resolve(__dirname, "wwwroot/js")
	},
	module: {
		rules: [
			{ test: /\.ts$/, loader: "ts-loader" }
		]
	},
	resolve: {
		extensions: [".ts"],
		alias: {
			vue: "vue/dist/vue.js"
		}
	}
};

module.exports = config;
