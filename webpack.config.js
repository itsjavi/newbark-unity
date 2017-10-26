const path = require('path');
const webpack = require('webpack');

let webpackShimConfig = {
  // shim config for incompatible libraries
  shim: {
    'melonjs': {
      exports: 'me'
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
      path.resolve('./'),
      path.resolve('./src'),
      path.resolve('./node_modules')
    ],
    alias: {
      'melonjs': path.join(__dirname, 'node_modules/melonjs/build/melonjs.js'),
      'melonjs-debug': path.join(__dirname, 'node_modules/melonjs/plugins/debug/debugPanel.js'),
    },
  },
  module: {
    loaders: [
      {
        test: /\.js/,
        loader: 'shim-loader',
        query: webpackShimConfig
      },
      {
        test: /\.js$/,
        loader: 'babel-loader',
        exclude: /node_modules/
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
