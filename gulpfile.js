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

gulp.task('debug', ['clean'], function () {
  return gulp.src('node_modules/melonjs/plugins/debug/debugPanel.js')
    .pipe(concat('debug.js'))
    .pipe(gulp.dest('build/'));
});

gulp.task('import-entities', ['clean'], function () {
  return gulp.src('src/entities/*.js')
    .pipe(module_merger(cwd, 'entities.js'))
    .pipe(gulp.dest('build/'));
});

gulp.task('import-scenes', ['clean'], function () {
  return gulp.src('src/scenes/*.js')
    .pipe(module_merger(cwd, 'scenes.js'))
    .pipe(gulp.dest('build/'));
});

gulp.task('imports', ['clean', 'import-entities', 'import-scenes']);

gulp.task('assets', ['clean'], function () {
  return gulp.src('assets/*/**/*.*')
    .pipe(gulp.dest('dist/assets/'))
    .pipe(assets_indexer(cwd + '/dist'))
    .pipe(gulp.dest('build/'));
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

gulp.task('default', ['clean', 'debug', 'webpack', 'css', 'html'], function () {
  console.info("DONE");
});
