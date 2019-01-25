#!/usr/bin/env bash

UNITY_PROJECT="NewBark"
UNITY_BIN="/Applications/Unity/2018.3.2f1/Unity.app/Contents/MacOS/Unity"

unity_build() {
	target=$1
	alias=$2
	ext=$3

	echo "Building $UNITY_PROJECT for $target..."
	mkdir -p $(pwd)/Builds/$alias
	$UNITY_BIN  \
	  -batchmode  \
	  -nographics  \
	  -force-free \
	  -silent-crashes \
	  -logFile $(pwd)/unity-$alias.log  \
	  -projectPath $(pwd)  \
	  -build${target}Player "$(pwd)/Builds/$alias/$UNITY_PROJECT$ext"  \
	  -quit

	cat $(pwd)/unity-$alias.log

	if [ $? = 0 ] ; then
	  echo "$target build completed successfully."
	  # echo 'Creating zip file...'
	  # zip -r $(pwd)/Builds/$alias.zip $(pwd)/Builds/$alias/ || exit 1
	else
	  echo "$target build failed. Exited with $?."
	  exit 1
	fi
}

#unity_build Windows win ".exe"
unity_build Windows64 win64 ".exe"
unity_build OSXUniversal osx ".app"
#unity_build LinuxUniversal linux
