'use strict';
const path = require('path');
const gulp = require('gulp');
const del = require('del');
const concat = require('gulp-concat');
const webpack = require('webpack-stream');
const assets_indexer = require('./tools/gulp/AssetsIndexer');
const module_merger = require('./tools/gulp/ModuleMerger');
const cwd = path.resolve(__dirname);

gulp.task('clean', function () {
  return del([
    'dist/**/*',
    'build/**/*'
  ]);
});

gulp.task('import-entities', ['clean'], function () {
  return gulp.src('src/entities/*.js')
    .pipe(module_merger(cwd + '/src'))
    .pipe(gulp.dest('src/entities/'));
});

gulp.task('import-scenes', ['clean'], function () {
  return gulp.src('src/scenes/*.js')
    .pipe(module_merger(cwd + '/src'))
    .pipe(gulp.dest('src/scenes/'));
});

gulp.task('imports', ['clean', 'import-entities', 'import-scenes']);

gulp.task('assets', ['clean'], function () {
  return gulp.src('assets/*/**/*.*')
    .pipe(gulp.dest('dist/assets/'))
    .pipe(assets_indexer(cwd + '/dist', 'assets.js'))
    .pipe(gulp.dest('src/'));
});

gulp.task('webpack', ['imports', 'assets'], function () {
  return gulp.src('src/game.js')
    .pipe(webpack(require('./webpack.config.js')))
    .pipe(gulp.dest('dist/'));
});

gulp.task('css', ['clean'], function () {
  return gulp.src('assets/css/**/*.css')
    .pipe(gulp.dest('dist/assets/css/'));
});

gulp.task('html', ['clean'], function () {
  return gulp.src('src/html/game.html')
    .pipe(concat('index.html'))
    .pipe(gulp.dest('dist/'));
});

gulp.task('plugins', ['clean'], function () {
  return gulp.src('node_modules/melonjs/plugins/**/*.*')
    .pipe(gulp.dest('dist/plugins/'));
});

gulp.task('default', ['clean', 'imports', 'assets', 'webpack', 'css', 'html', 'plugins'], function () {
  console.info("DONE");
});
