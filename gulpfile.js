'use strict';
const path = require('path');
const gulp = require('gulp');
const del = require('del');
const concat = require('gulp-concat');
const webpack = require('webpack-stream');
const ghpages = require('gh-pages');
const assets_indexer = require('./tools/gulp/AssetsIndexer');
const module_merger = require('./tools/gulp/ModuleMerger');
const cwd = path.resolve(__dirname);

gulp.task('clean', function () {
  return del([
    'dist/**/*'
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
  return gulp.src('src/layout/css/**/*.css')
    .pipe(gulp.dest('dist/assets/css/'));
});

gulp.task('html', ['clean'], function () {
  return gulp.src('src/layout/game.html')
    .pipe(concat('index.html'))
    .pipe(gulp.dest('dist/'));
});

gulp.task('default', ['clean', 'imports', 'assets', 'webpack', 'css', 'html'], function () {
  console.info("The dist folder is now shiny and fresh ✨.");
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
  gulp.watch('assets/**/*.{tmx,tsx,json}', ['assets', 'webpack']);
  gulp.watch('src/layout/**/*.html', ['html']);
  gulp.watch('src/layout/**/*.css', ['css']);
  gulp.watch('src/entities/**/*.js', ['import-entities', 'webpack']);
  gulp.watch('src/scenes/**/*.js', ['import-scenes', 'webpack']);
  gulp.watch('src/system/**/*.js', ['webpack']);
  gulp.watch('src/{config,index}.js', ['webpack']);
});
