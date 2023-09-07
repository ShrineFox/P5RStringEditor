![](https://i.imgur.com/dTmMrQP.png)  
# P5RStringEditorr
Compares flag dumps to narrow down newly enabled/disabled flag IDs

# What is this?
This is a simple form that organizes text pasted from your clipboard. Its specific purpose is to look for a dump of bitflags from P5R and add them to a list.  
The state of the list between each comparison is then compared, so you can narrow down which flags get enabled or disabled between scenes.  
You can name the flags for future reference, and load/save your collection of comparisons and named flags as JSON.

# How to dump flags?
Using a custom build of the [WIP P5R Mod Menu](https://github.com/ShrineFox/Persona-5-Mod-Menu), flags are dumped to the game's console every time you open the menu. You can then copy/paste them into the window.

# Future Plans
Ideally, a memory watcher that automatically adds changed flags to a new comparison rather than having you copy/paste using a custom script.  
This would require finding the consistent address of bitflags in memory and checking it for changes at a certain interval. And then you could just start a new comparison with a hotkey.  
The whole point of the tool is that you need to stop and name them while you still have a point of reference, so having to interact more with the form isn't a huge deal for now.
