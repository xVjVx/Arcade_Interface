# 🕹️ Arcade Interface Launcher

A clean arcade machine interface built entirely in C# (Windows Forms). This project was born from the need to have a simple launcher to organize and play executables, Windows shortcuts, and Python scripts, without the bloat of heavy front-ends.

## ✨ Key Features

* **Intro Video:** Plays a startup video (`intro.mp4`) every time the application launches, mimicking the boot-up sequence of a real Arcade cabinet.
* **Category Organization:** Folder-based navigation. You can create folders like "Fighting", "Racing", or "Indie" to organize your collection.
* **Multi-Format Support:** Launches games from executables (`.exe`), Windows shortcuts (`.lnk`), and even raw Python scripts (`.py`).
* **HD Covers & Metadata:** A smart system that pairs your game with an HD image (`.png`) and a text file (`.txt`) to display tags and a detailed description. If the image is missing, it automatically fallback to extracting the file's original icon.
* **Dynamic UI:** * Clean, borderless design for an immersive fullscreen feel.
  * Custom rounded buttons with hover animations.
  * "Press Start 2P" font that automatically resizes so long game titles always fit perfectly inside the buttons.

## 📂 How to Structure Your Games

When you open the program for the first time, it automatically creates a `Jogos` (Games) folder in your **Documents**. That's where the magic happens.

For a game to appear in the interface with a custom cover and description, simply place the files with the **exact same name** in the folder. Example:

```text
Documents/
└── Jogos/
    ├── intro.mp4                 <-- Your startup video
    ├── Example/                   <-- Category folder
    │   ├── Example.lnk      <-- Game shortcut
    │   ├── Example.png      <-- HD cover (Same name)
    │   └── Example.txt      <-- Description text (Same name)
