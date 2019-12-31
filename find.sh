#!/bin/bash

# Find text from all subdirectories
# and ignore all temporary directories:
# - node-modules = installed node modules
# - .git = git history

# If less or more that 1 parameter, show usage.
if (( $# != 1 )); then
    echo 'Usage: ./find.sh text-to-find'
    exit 0
fi

find . | grep -v .git | xargs grep --no-messages $1 | less
