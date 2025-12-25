## Quick Start

### Features
🚀 Automated Project Scaffolding: Create a standardized directory structure (Arts, Scripts, Prefabs, etc.) instantly.
### Installation
#### Option A: via Unity Package Manager (Git URL)
- Open the Package Manager in Unity (Window > Package Manager).
![image](https://hackmd.io/_uploads/B12G-257Zg.png)
- Click the + icon and select Add package from git URL....
![image](https://hackmd.io/_uploads/BJ6tehq7be.png)	 
- Paste the following URL: ```https://github.com/UnityToolkit/quickstart.git```
![image](https://hackmd.io/_uploads/BJbWWhqXZl.png)
#### 	 
### Usage
- Open the panel via the menu: Assets > Unity Toolkit > Quick Start Panel.
- Project Settings Tab:
  - Define your root folder name.
  - Toggle specific modules (2D, 3D, Audio, Animations). 
  - Click Generate Folders.
- Scene Settings Tab:
  - Enter your scene name.
  - Choose default objects to instantiate.
  - Click Create and Save Scene.
- Git Utilities: Click the download button to fetch the latest .gitignore from GitHub.
![image](https://hackmd.io/_uploads/HkvIbncQWg.png)

### Standard Directory Example
```shell=
Assets/YourProject/
├── Arts/
│   ├── Sprites/
│   ├── Models/
│   └── Audios/
├── Scripts/
│   ├── Core/
│   ├── Player/
│   └── Managers/
└── Prefabs/
```

### License

Distributed under the MIT License. See LICENSE for more information.