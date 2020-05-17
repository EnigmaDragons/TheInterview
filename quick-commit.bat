@echo off
set arg1=%1
git checkout "src/TheInterview/ProjectSettings/Packages/com.unity.probuilder/Settings.json"
git checkout "src/TheInterview/Assets/Extra/NeonSign/neon.mat"
git add .
git commit -m %arg1%
git pull
git push
