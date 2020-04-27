/// <binding ProjectOpened='Watch - Development' />
const path = require("path");

const config = {
	entry: {
		// todo: lib.tsを別ファイルにしたい
		lib: path.resolve(__dirname, "scripts/lib.ts"),
		index: path.resolve(__dirname, "scripts/index.ts"),
		importmodule: path.resolve(__dirname, "scripts/importmodule.ts"),
		"game/gameoflife": path.resolve(__dirname, "scripts/game/gameoflife.ts"),
		"vuejs/usewebpack": path.resolve(__dirname, "scripts/vuejs/usewebpack.ts")
	},
	// todo: lib.tsを別ファイルにしたい
	/*
	optimization: {
		splitChunks: {
		}
	},
	*/
	output: {
		filename: "[name].js",
		path: path.resolve(__dirname, "wwwroot/js")
	},
	module: {
		rules: [
			{ test: /\.ts$/, use: "ts-loader" }
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
