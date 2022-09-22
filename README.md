# MetaBoy Voxelizer

The voxelizer is a simple tool that is able to load the MetaBoy NFTs from your wallet and render them as voxelized 3D models. This repo contains the source code utilized in the project.

## Description

The Voxelizer is an application utilizing Unity to display your MetaBoy NFTs as 3D voxelized models. 

The raw models were handcrafted utilized MagicaVoxel and Blender, and then imported into Unity for further customization. These assets will be available to the general public at a later date.

This application contains the required classes for setting up the models for assembly at runtime, the required UI implementation, and some basic gameplay features. 

The goal of this repo is to provide transparency to the users of the voxelizer.

## Installation

1. Download and install Unity Version 2020.3.30f1 with the WebGL package.
2. Download the MetaBoyVoxelizerUnityPackage.unitypackage from the file and import it as a custom package into your unity project.
3. Ensure your project platform is set to WebGL (File > Build Settings).
4. Add the App_Visualizer_Prod scene to your build settings.
5. Build and Run, the demo shows the first 8 characters of your L2 API key so you can verify it is correct.

Because no ready made assets are provided at this time, you will be able to view the UI and it will be able to identify all your NFTs, but they won't be visible on the screen.

## Authors

[@thatgusmartin](https://twitter.com/thatgusmartin)

[@patrion.loopring.eth](https://twitter.com/PatrionDigital)

## Version History

* 0.1
    * Initial Release

## License

This project is licensed under the GPLv3 License.

## Dependencies

This project utilizes the [LoopringUnity](https://github.com/LoopMonsters/LoopringUnity) API integration to utilize wallet functions.

This project utilizes the [SkeletonSkin](https://github.com/DMeville/SkeletonSkin) component to attach meshes to the same skeleton.
