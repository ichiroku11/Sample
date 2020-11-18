/// <binding ProjectOpened='Watch - Development' />
const path = require("path");
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const FixStyleOnlyEntriesPlugin = require("webpack-fix-style-only-entries");

const config = {
	entry: {
		lib: path.resolve(__dirname, "styles/lib.scss"),
		animation: path.resolve(__dirname, "styles/animation.scss"),
		boxsizing: path.resolve(__dirname, "styles/boxsizing.scss"),
		detailssummary: path.resolve(__dirname, "styles/detailssummary.scss"),
		flexbox: path.resolve(__dirname, "styles/flexbox.scss"),
		flexboxcomponent: path.resolve(__dirname, "styles/flexboxcomponent.scss"),
		flexboxgridwrapper: path.resolve(__dirname, "styles/flexboxgridwrapper.scss"),
		flexboxsample: path.resolve(__dirname, "styles/flexboxsample.scss"),
		focuswithin: path.resolve(__dirname, "styles/focuswithin.scss"),
		grid: path.resolve(__dirname, "styles/grid.scss"),
		gridfullbleed: path.resolve(__dirname, "styles/gridfullbleed.scss"),
		gridlayout: path.resolve(__dirname, "styles/gridlayout.scss"),
		gridrepeat: path.resolve(__dirname, "styles/gridrepeat.scss"),
		index: path.resolve(__dirname, "styles/index.scss"),
		media: path.resolve(__dirname, "styles/media.scss"),
		onelinelayout: path.resolve(__dirname, "styles/onelinelayout.scss"),
		transform: path.resolve(__dirname, "styles/transform.scss"),
		transition: path.resolve(__dirname, "styles/transition.scss")
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
