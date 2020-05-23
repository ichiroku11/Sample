/// <binding ProjectOpened='Watch - Development' />
const path = require("path");
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const FixStyleOnlyEntriesPlugin = require("webpack-fix-style-only-entries");

const config = {
	entry: {
		lib: path.resolve(__dirname, "styles/lib.scss"),
		boxsizing: path.resolve(__dirname, "styles/boxsizing.scss"),
		flexbox: path.resolve(__dirname, "styles/flexbox.scss"),
		flexboxgridwrapper: path.resolve(__dirname, "styles/flexboxgridwrapper.scss"),
		grid: path.resolve(__dirname, "styles/grid.scss"),
		gridlayout: path.resolve(__dirname, "styles/gridlayout.scss"),
		index: path.resolve(__dirname, "styles/index.scss")
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
