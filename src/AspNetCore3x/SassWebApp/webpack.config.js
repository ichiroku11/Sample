const path = require("path");
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const FixStyleOnlyEntriesPlugin = require("webpack-fix-style-only-entries");

const config = {
	entry: {
		lib: path.resolve(__dirname, "styles/lib.scss"),
	},
	output: {
		filename: "[name].css",
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
		new FixStyleOnlyEntriesPlugin(),
		new MiniCssExtractPlugin()
	],
};

module.exports = config;
