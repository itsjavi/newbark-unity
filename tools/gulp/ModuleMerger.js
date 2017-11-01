const path = require('path');
const through = require('through2');
const File = require('gulp-util').File;

/**
 * @param {string} includePath
 * @param {string} destinationFile
 * @returns {Stream}
 */
module.exports = function (includePath, destinationFile) {
  if (!includePath) {
    throw new Error('ModuleMerger: Missing includePath argument');
  }
  if (!destinationFile) {
    throw new Error('ModuleMerger: Missing destinationFile argument');
  }

  let modules = [];
  let firstFile;

  return through.obj(
    function (file, encoding, cb) { // through._transform
      let fileName = path.basename(file.path);
      let fileExt = path.extname(file.path);
      let moduleName = path.basename(file.path, fileExt);
      let moduleSafeName = moduleName.replace(/[^a-zA-Z0-9_]/g, '_').replace(/^[0-9]/g, '_$1');

      if (
        (path.basename(destinationFile) === fileName)
        || (file.contents.toString().match(/@@nobundle/ig))
      ) {
        cb();
        return;
      }

      if (!firstFile) {
        firstFile = file;
      }

      modules.push({
        "name": moduleSafeName,
        "src": file.path.substr(includePath.length + 1).replace(fileExt, '')
      });

      // make sure the file goes through the next gulp plugin
      // this.push(file);

      // tell the stream engine that we are done with this file
      cb();
    },
    function (done) { // through._flush
      if (!firstFile) {
        done();
        return;
      }

      let code = '/* This is an auto-generated file. Please use gulp to update it. */\n\'use strict\';\n';
      let code_exports = [];
      let code_exports_default = [];

      modules.forEach(function (module) {
        if (module.name === destinationFile.replace(/\..*$/, '')) {
          return;
        }
        code += `import ${module.name} from '${module.src}';\n`;

        code_exports_default.push(`  ${module.name}: ` + module.name);
        code_exports.push('  ' + module.name);
      });

      code += '\nexport default {\n' + code_exports_default.join(',\n') + '\n};\n';
      code += '\nexport {\n' + code_exports.join(',\n') + '\n};';

      // Push the new file
      this.push(new File({
        cwd: firstFile.cwd,
        base: firstFile.base,
        path: path.join(firstFile.base, destinationFile),
        contents: new Buffer(code + '\n')
      }));

      done();
    }
  )
};
