# Grasshopper_dylib_testbed

Testbed for [this question](https://discourse.mcneel.com/t/how-to-direct-dllimport-to-look-for-dylib-placed-in-the-same-folder-as-my-custom-component-gha/61886)

When `libSimple.dylib` is placed in `bin/Debug` along with generated `SimpleDynamicLibTest.gha` then `Start Debugging`, it works fine.

But when those two files are moved to `~/Library/Application\ Support/McNeel/Rhinoceros/MacPlugIns/Grasshopper/Libraries/`, it failed to find the `.dylib`...



### How to reproduce.

1. Open `Simple/Simple.xcodeproj` then compile to generate `libSimple.dylib`
2. Open `SimpleDynamicLibTest.sln` then hit `⌘ + ⏎` to Debug
3. Open new grasshopper document and place `SimpleDynamicLibTestComponent`.
4. It would fail due to "1. Solution exception:libSimple.dylib".
5. Place `libSimple.dylib` under `Debug/bin`.
6. Try Step 2, Step 3, and it will not raise any exception anymore. (good)
7. Move the files to `~/Library/Application\ Support/McNeel/Rhinoceros/MacPlugIns/Grasshopper/Libraries/SimpleDynamicLibTest` to install.
8. Restart Rhino/Grasshopper.
9. It will raise the same exception even though they are in the same directory.
10. When `.dylib` is placed under `/usr/lib/libSimple.dylib` it works...

but I'd like to know how to place the .dylib as the same directory as .gha to keep things clean.
