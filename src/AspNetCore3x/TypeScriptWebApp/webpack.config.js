const path = require("path");

const config = {
	entry: {
		index: path.resolve(__dirname, "scripts/index.ts")
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
		extensions: [".ts"]
	}
};

module.exports = config;
