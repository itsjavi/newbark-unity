const path = require('path');
const through = require('through2');
const File = require('gulp-util').File;

/**
 * @param {string} rootPath
 * @param {string} destinationFile
 * @returns {Stream}
 */
module.exports = function (rootPath, destinationFile = 'index.js') {
  rootPath = path.resolve(rootPath);
  let modules = [];
  let firstFile;

  return through.obj({},
    function (file, encoding, cb) { // through._transform
      if (!firstFile) {
        firstFile = file;
      }

      let fileExt = path.extname(file.path);
      let relativeFile = file.path.substr(rootPath.length + 1);

      modules.push({
        "name": path.basename(relativeFile, fileExt),
        "src": relativeFile
      });

      // make sure the file goes through the next gulp plugin
      // this.push(file);

      // tell the stream engine that we are done with this file
      cb();
    },
    function (done) { // through._flush
      let code = '/* This file is auto-generated */\n';
      let code_exports = [];

      modules.forEach(function (module) {
        if(module.name === destinationFile.replace(/\..*$/,'')) {
          return;
        }
        code += `import ${module.name} from '${module.src}';\n`;
        code_exports.push('  ' + module.name);
      });

      code += '\nexport {\n' + code_exports.join(',\n') + '\n};\n  ';

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
