'use strict';
const path = require('path');
const webpack = require('webpack');

module.exports = {
  entry: {
    'game': './src/index.js',
    'game.min': './src/index.js',
  },
  output: {
    filename: '[name].js'
  },
  resolve: {
    modules: [
      path.resolve('./src'),
      path.resolve('./node_modules')
    ],
  },
  module: {
    loaders: [
      {
        test: /\.js$/,
        loader: 'babel-loader',
        exclude: /node_modules/,
        query: {
          presets: [
            require.resolve('babel-preset-env')
          ]
        }
      }
    ]
  },
  externals: {
    'zepto-ext': 'window.Zepto',
    'melonjs-ext': 'window.me',
    'lodash-ext': 'window._'
  },
  plugins: [
    new webpack.optimize.UglifyJsPlugin({
      include: /\.min\.js$/,
      minimize: true
    })
  ]
};
