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
  let assets = {
    _files: []
  };
  let firstFile;

  return through.obj({},
    function (file, encoding, cb) { // through._transform
      if (!firstFile) {
        firstFile = file;
      }
      let fileExt = path.extname(file.path);
      let fileType = getAssetFileType(fileExt);

      if (fileType !== false) {
        let helperFileType;

        switch (fileType) {
          case 'tmx':
            helperFileType = 'maps';
            break;
          case 'tsx':
            helperFileType = 'tilesets';
            break;
          case 'json':
            helperFileType = 'data';
            break;
          default:
            helperFileType = fileType + 's';
        }

        let relativeFile = file.path.substr(rootPath.length + 1);
        let assetName = path.basename(relativeFile, fileExt);
        let assetSafeName = assetName.replace(/[^a-zA-Z0-9_]/g, '_').replace(/^[0-9]/g, '_$1');
        let assetSrc = (fileType === "audio") ? (path.dirname(relativeFile) + "/") : relativeFile;

        if (!assets[helperFileType]) {
          assets[helperFileType] = {};
        }

        if (assets[helperFileType][assetSafeName]) {
          $e = `Asset ${helperFileType}.${assetSafeName} already exists.`;
          throw new Error($e);
        }

        assets[helperFileType][assetSafeName] = assetName;

        assets._files.push({
          "name": assetName,
          "type": fileType,
          "src": assetSrc
        });

        // make sure the file goes through the next gulp plugin
        // this.push(file);
      }

      // tell the stream engine that we are done with this file
      cb();
    },
    function (done) { // through._flush
      let assetsJson = JSON.stringify(assets, null, 2);
      let fileHeader = '/* This is an auto-generated file. Please use gulp to update it. */\n';

      // Push the new file
      this.push(new File({
        cwd: firstFile.cwd,
        base: firstFile.base,
        path: path.join(firstFile.base, destinationFile),
        contents: new Buffer(fileHeader + `"use strict";\nexport default ${assetsJson};\n`)
      }));

      done();
    }
  )
};
