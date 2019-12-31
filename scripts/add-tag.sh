# Add tag to repo of new release
# Example: add-tag.sh v0.04

git tag -a $1 -m "$1"
git push origin $1
