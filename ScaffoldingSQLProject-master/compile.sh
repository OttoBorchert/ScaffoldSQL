#!/bin/bash

# Initialize arrays of builds and releases
# Available builds: win-x86 win-x64 win-arm osx-x64 linux-x64 linux-arm
BuildFor=( win-x86 win-x64 osx-64 linux-x64 )
Releases=( Admin Release )

# test if dotnet is installed
if ! command -v dotnet &> /dev/null; then
    echo "To compile this program, dotnet must be installed."
    exit 1
fi

# Compile each release and build
for release in ${Releases[@]}; do
    echo "Starting Release $release"
    for build in ${BuildFor[@]}; do
        echo "Building: $build $release"
        dotnet publish ScaffoldingSQLProject.sln -p:PublishSingleFile=true -c $release -r $build --self-contained true -o bin/$release/$build
    done
done

# Done!
echo "Compile finished for all builds and releases"

exit 0