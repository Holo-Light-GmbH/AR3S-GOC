# AR3S-GOC
Grouped Objects Creator for AR 3S.

The **AR 3S Grouped Asset Creator** gives you a quick and easy way to get your files import-ready for our augmented engineering software.

## Requirements

- **Unity 2019.4.x (LTS)** with the following installed packages:
  
  - Windows Build Support (IL2CPP)
  - UWP Build Support (.NET)![Bild1.jpg](/Docs/1.jpg)
  - (Unity Hub -> Installs -> Add Modules)

- **Visual Studio Community 2019** with the following installed packages:   ![2.png](/Docs/2.png)

- (Visual Studio Installer -> Modify)

## How to Create Fast Loadable AssetBundles

- Launch Unity Hub and open the *ARES Asset Creator* project which you can obtain from our GitHub Repository *(Link to be added)*
- Inside the project navigate to *main.scene![3.png](/Docs/3.png)*
- Provide a supported file format (namely .fbx, .obj, .prefab) for your target file and copy it into the folder *ARES.SourceFiles![4.png](/Docs/4.png)*
- Select your target asset and click on the inspector (right side) on the tab *Model* and enable the property *Read/Write enabled![5.png](/Docs/5.png)*
- Give your AssetBundle file a new name![6.png](/Docs/6.png)
- Right-click on your asset and select *Export Asset to ARES Basic* or *Export Asset to ARES Pro![7.png](/Docs/7.png)*
- After some processing time, you will find your AR 3S ready file in *Assets/ARES.Export/...![8.png](/Docs/8.png)*

### Note

This tool uses Unity's AssetBundles which always consist of two components (file + .manifest). Please copy and paste both files to AR 3S.

## How to Combine Multiple Objects into One

- Drag and drop your target 3D file into the scene by right-clicking on the option and choosing the *Unpack Prefab Completely* option in the menu![9.png](/Docs/9.png)
- Add the component *Mesh Combiner* to the root object or subassemblies (one or multiple)![10.png](/Docs/10.png)
- Select the target root object and then choose and click on the HoloLight tab on *Grouped Selected Object*![11.png](/Docs/11.png)
- Create a Unity Prefab by dragging and dropping the Gameobject of the hierarchy window down into the project folder window![12.png](/Docs/12.png)
- Proceed as described in the previous chapter *How to Create Fast Loadable AssetBundles* by selecting your target asset and clicking on the inspector (right side) on the tab *Mode* and enable the property *Read/Write enabled*
