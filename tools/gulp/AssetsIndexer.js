const path = require('path');
const through = require('through2');
const File = require('gulp-util').File;

/**
 * @param {string} fileExt
 * @returns {(boolean|string)}
 */
function getAssetFileType(fileExt) {
  fileExt = fileExt.toLowerCase().replace('.', '');

  if (fileExt.match(/^(tsx|tmx|json)$/)) {
    return fileExt;
  }
  if (fileExt.match(/^(mp3|ogg|wav|flac|aac)$/)) {
    return 'audio';
  }
  if (fileExt.match(/^(png|jpg|gif|jpeg|webp)$/)) {
    return 'image';
  }
  return false;
}

/**
 * @param {string} rootPath
 * @param {string} destinationFile
 * @returns {Stream}
 */
module.exports = function (rootPath, destinationFile = 'assets.js') {
  rootPath = path.resolve(rootPath);
  let assets = [];
  let firstFile;

  return through.obj({},
    function (file, encoding, cb) { // through._transform
      if (!firstFile) {
        firstFile = file;
      }
      let fileExt = path.extname(file.path);
      let fileType = getAssetFileType(fileExt);

      if (fileType !== false) {
        let relativeFile = file.path.substr(rootPath.length + 1);

        assets.push({
          "name": path.basename(relativeFile, fileExt),
          "type": fileType,
          "src": (fileType === "audio") ? (path.dirname(relativeFile) + "/") : relativeFile
        });

        // make sure the file goes through the next gulp plugin
        // this.push(file);
      }

      // tell the stream engine that we are done with this file
      cb();
    },
    function (done) { // through._flush
      let assetsJson = JSON.stringify(assets, null, 4);

      // Push the new file
      this.push(new File({
        cwd: firstFile.cwd,
        base: firstFile.base,
        path: path.join(firstFile.base, destinationFile),
        contents: new Buffer(`let assets = ${assetsJson};\nexport default assets;\n`)
      }));

      done();
    }
  )
};
