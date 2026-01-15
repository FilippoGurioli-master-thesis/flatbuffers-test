#!/bin/bash

SCRIPT_PATH="$(dirname -- "$(cd -- "$(dirname -- "$0")" && pwd)/$(basename -- "$0")")"

bash -lc "cd $SCRIPT_PATH/back-end && ./gradlew build"
