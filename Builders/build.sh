#!/usr/bin/env bash

VERSION=${1:-"dev"}
UNITY_PROJECT_NAME="newbark"
UNITY_BIN="/Applications/Unity/2019.2.8f1/Unity.app/Contents/MacOS/Unity"

unity_build() {
	target=$1
	alias=$2
	ext=$3
	dir="$(pwd)/Builds/$UNITY_PROJECT_NAME-$alias-v$VERSION"
	binfile="$dir/$UNITY_PROJECT_NAME$ext"
	zipfile="$UNITY_PROJECT_NAME-$alias-v$VERSION.zip"

	echo "Building $UNITY_PROJECT_NAME for $target..."
	mkdir -p $dir
	$UNITY_BIN  \
	  -batchmode  \
	  -nographics  \
	  -force-free \
	  -silent-crashes \
	  -projectPath $(pwd)  \
	  -build${target}Player "$binfile"  \
	  -quit

    cp LICENSE $dir/LICENSE
    cp README.md $dir/README.md

	if [ $? = 0 ] ; then
	  echo "$target build completed successfully."
	  echo 'Creating zip file...'

	  cd $dir
	  zip -r -X ../$zipfile *
	  cd -
	else
	  echo "$target build failed. Exited with $?."
	  exit 1
	fi
}

unity_build "${@:2}"
