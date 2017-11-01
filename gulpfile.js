'use strict';
const path = require('path');
const gulp = require('gulp');
const del = require('del');
const concat = require('gulp-concat');
const webpack = require('webpack-stream');
const es = require('event-stream');
const ghpages = require('gh-pages');

const assets_indexer = require('./tools/gulp/AssetsIndexer');
const module_merger = require('./tools/gulp/ModuleMerger');
const cwd = path.resolve(__dirname);

gulp.task('clean', function () {
  return del([
    'dist/**/*'
  ]);
});


gulp.task('vendor:clean', function () {
  return del(['dist/vendor.js']);
});
gulp.task('vendor', ['vendor:clean'], function () {
  return gulp.src([
    'node_modules/melonjs/build/melonjs.js',
    'node_modules/zepto/dist/zepto.js',
    'node_modules/zepto/src/touch.js',
    'node_modules/lodash/lodash.js'
  ])
    .pipe(concat('vendor.js'))
    .pipe(gulp.dest('dist/'));
});


gulp.task('assets:clean', function () {
  return del(['dist/assets/!(css)**/*']);
});
gulp.task('assets', ['assets:clean'], function () {
  return gulp.src('assets/*/**/*.*')
    .pipe(gulp.dest('dist/assets/'))
    .pipe(assets_indexer(cwd + '/dist', 'assets.js'))
    .pipe(gulp.dest('dist/assets/'));
});


gulp.task('modules:clean', function () {
  return del(['src/{entities,scenes}/_all.js']);
});
gulp.task('modules', ['modules:clean'], function () {
  let streams = [];

  [
    'src/scenes/',
    'src/entities/'
  ]
    .forEach((dir) => {
    streams.push(gulp.src(dir + '*.js')
      .pipe(module_merger(cwd + '/src', '_all.js'))
      .pipe(gulp.dest(dir)))
  });

  return es.concat.apply(null, streams);
});


gulp.task('webpack:clean', function () {
  return del(['dist/game.js']);
});
gulp.task('webpack', ['webpack:clean', 'modules'], function () {
  return gulp.src('src/game.js')
    .pipe(webpack(require('./webpack.config.js')))
    .pipe(gulp.dest('dist/'));
});


gulp.task('css:clean', function () {
  return del(['dist/assets/css/**/*']);
});
gulp.task('css', ['css:clean'], function () {
  return gulp.src('src/css/**/*.css')
    .pipe(gulp.dest('dist/assets/css/'));
});


gulp.task('html:clean', function () {
  return del(['dist/index.html']);
});
gulp.task('html', ['html:clean'], function () {
  return gulp.src('src/index.html')
    .pipe(concat('index.html'))
    .pipe(gulp.dest('dist/'));
});


gulp.task('publish', ['default'], function () {
  // WARNING! You won't be able to publish unless you have write permissions on the repo.
  // Check the gh-pages npm package documentation.
  ghpages.publish('dist',
    {
      message: 'Update build with latest master changes'
    },
    function () {
      console.info("The gh-pages branch is updated and pushed 📦.");
    }
  );
});

gulp.task('watch', function () {
  gulp.watch('assets/**/!(*.css)*.*', ['assets']);
  gulp.watch('src/*.html', ['html']);
  gulp.watch('src/css/**/*.css', ['css']);
  gulp.watch('src/**/!(_all.js)*.js', ['webpack']);
});


gulp.task('default', ['vendor', 'assets', 'webpack', 'css', 'html'], function () {
  console.info("The dist folder is now shiny and fresh ✨.");
});
