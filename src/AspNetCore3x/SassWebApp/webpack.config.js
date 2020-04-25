const path = require("path");
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

const config = {
	entry: {
		lib: path.resolve(__dirname, "styles/lib.scss"),
	},
	output: {
		path: path.resolve(__dirname, "wwwroot/css")
	},
	module: {
		rules: [
			{
				test: /\.scss$/,
				use: [
					MiniCssExtractPlugin.loader,
					"css-loader",
					"sass-loader"
				]
			}
		]
	},
	plugins: [
		new MiniCssExtractPlugin()
	],
};

module.exports = config;
