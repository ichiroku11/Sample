/// <binding ProjectOpened='Watch - Development' />
const path = require("path");
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const FixStyleOnlyEntriesPlugin = require("webpack-fix-style-only-entries");

const config = {
	entry: {
		lib: path.resolve(__dirname, "styles/lib.scss"),
		index: path.resolve(__dirname, "styles/index.scss"),
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
	output: {
		path: path.resolve(__dirname, "wwwroot/css")
	},
	plugins: [
		new FixStyleOnlyEntriesPlugin(),
		new MiniCssExtractPlugin({
			filename: "[name].css"
		})
	],
};

module.exports = config;
