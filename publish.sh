#!/usr/bin/bash

dotnet pack
dotnet tool uninstall --global interactive
dotnet tool install --global --add-source ./nupkg interactive
