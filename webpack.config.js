'use strict';
const path = require('path');
const webpack = require('webpack');

let webpackShimConfig = {
  // shim config for incompatible libraries
  shim: {
    'melonjs': {
      exports: 'me'
    },
    'zepto': {
      exports: 'Zepto'
    },
    'zepto-touch': {
      deps: [
        'zepto:Zepto',
        'zepto:$',
      ]
    }
  }
};

module.exports = {
  entry: {
    "game": './src/index.js',
    "game.min": './src/index.js',
  },
  output: {
    filename: '[name].js'
  },
  resolve: {
    modules: [
      path.resolve('./src'),
      path.resolve('./node_modules')
    ],
    alias: {
      'melonjs': path.join(__dirname, 'node_modules/melonjs/build/melonjs.js'),
      'zepto': path.join(__dirname, 'node_modules/zepto/dist/zepto.js'),
      'zepto-touch': path.join(__dirname, 'node_modules/zepto/src/touch.js')
    },
  },
  module: {
    loaders: [
      {
        test: /\.js/,
        loader: 'shim-loader',
        query: webpackShimConfig,
      },
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
  plugins: [
    new webpack.optimize.UglifyJsPlugin({
      include: /\.min\.js$/,
      minimize: true
    })
  ]
};
