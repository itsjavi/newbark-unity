module.exports = function (grunt) {
  grunt.initConfig({
    pkg: grunt.file.readJSON('package.json'),

    concat: {
      dist: {
        src: [
          'lib/melonJS.js',
          'lib/plugins/*.js',
          'src/game.js',
          'src/config.js',
          'build/src/assets.js',
          'src/**/*.js',
        ],
        dest: 'build/src/app.js'
      }
    },

    copy: {
      dist: {
        files: [
          {
            src: 'index.css',
            dest: 'build/index.css'
          }, {
            src: 'main.js',
            dest: 'build/main.js'
          }, {
            src: 'src/config.js',
            dest: 'build/src/config.js'
          }, {
            src: 'manifest.json',
            dest: 'build/manifest.json'
          }, {
            src: 'package.json',
            dest: 'build/package.json'
          }, {
            src: 'assets/**/*',
            dest: 'build/',
            expand: true
          }
        ]
      },
      css: {
        files: [
          {
            src: 'index.css',
            dest: 'build/index.css'
          }
        ]
      }
    },

    clean: {
      app: ['build/src/app.js'],
      dist: [
        'bin/', 'build/assets/', 'build/src/',
        'build/**/*.html', 'build/**/*.css', 'build/**/*.js', 'build/**/*.json'
      ],
    },

    processhtml: {
      dist: {
        options: {
          process: true,
          data: {
            title: '<%= pkg.name %>',
          }
        },
        files: {
          'build/index.html': ['index.html']
        }
      }
    },

    replace: {
      dist: {
        options: {
          usePrefix: false,
          force: true,
          patterns: [
            {
              match: /this\._super\(\s*([\w\.]+)\s*,\s*["'](\w+)["']\s*(,\s*)?/g,
              replacement: '$1.prototype.$2.apply(this$3'
            },
          ],
        },
        files: [
          {
            src: ['build/src/app.js'],
            dest: 'build/src/app.js'
          }
        ]
      },
    },

    uglify: {
      options: {
        report: 'min',
        preserveComments: 'some'
      },
      dist: {
        files: {
          'build/src/app.min.js': [
            'build/src/app.js'
          ]
        }
      }
    },

    connect: {
      server: {
        options: {
          port: 8000,
          keepalive: false
        }
      }
    },

    'download-electron': {
      version: '1.4.6',
      outputDir: 'bin',
      rebuild: false,
    },

    asar: {
      dist: {
        cwd: 'build',
        src: ['**/*', '!src/app.js'],
        expand: true,
        dest: 'bin/' + (
          process.platform === 'darwin'
            ? 'Electron.app/Contents/Resources/'
            : 'resources/'
        ) + 'app.asar'
      },
    },

    assets: {
      dist: {
        options: {
          dest: 'build/src/assets.js',
          varname: 'game.assets',
        },
        files: [{
          src: ['assets/bgm/**/*.mp3', 'assets/bgm/**/*.ogg', 'assets/sfx/**/*.mp3', 'assets/sfx/**/*.ogg'],
          type: 'audio'
        }, {
          src: ['assets/img/**/*.png', 'assets/img/**/*.gif'],
          type: 'image'
        }, {
          src: ['assets/json/**/*.json'],
          type: 'json'
        }, {
          src: ['assets/map/**/*.tmx', 'assets/map/**/*.json'],
          type: 'tmx'
        }, {
          src: ['assets/map/**/*.tsx'],
          type: 'tsx'
        }]
      }
    },

    watch: {
      assets: {
        files: ['assets/**/*'],
        tasks: ['assets'],
        options: {
          spawn: false,
        },
      },
      js: {
        files: ['lib/**/*.js', 'src/**/*.js'],
        tasks: ['concat'],
        options: {
          spawn: false,
        },
      },
      html: {
        files: ['index.html'],
        tasks: ['processhtml'],
        options: {
          spawn: false,
        },
      },
      css: {
        files: ['index.css'],
        tasks: ['copy:css'],
        options: {
          spawn: false,
        },
      }
    },

  });

  grunt.loadNpmTasks('grunt-contrib-concat');
  grunt.loadNpmTasks('grunt-contrib-uglify');
  grunt.loadNpmTasks('grunt-contrib-copy');
  grunt.loadNpmTasks('grunt-contrib-clean');
  grunt.loadNpmTasks('grunt-contrib-watch');
  grunt.loadNpmTasks('grunt-processhtml');
  grunt.loadNpmTasks("grunt-replace");
  grunt.loadNpmTasks('grunt-contrib-connect');
  grunt.loadNpmTasks('grunt-download-electron');
  grunt.loadNpmTasks('grunt-asar');

  // Custom Tasks
  grunt.loadTasks('tasks');

  var defaultTasks = [
    'assets',
    'concat',
    'replace',
    'uglify',
    'copy',
    'processhtml',
  ];

  grunt.registerTask('default', [
    'clean:dist'
  ].concat(defaultTasks));

  grunt.registerTask('dist', ['default', 'download-electron', 'asar']);
  grunt.registerTask('serve', ['default', 'connect', 'watch']);
}
