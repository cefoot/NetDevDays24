
# Mixed Reality Game - Box Stopper

This repository contains the code for a mixed reality game where boxes fly towards the user, and they can interact with the boxes in two ways:
- **Stop the boxes with their hands**: Earn one point.
- **Let the boxes fly behind the head**: Lose one point.

The game leverages **Mixed Reality with Passthrough** to create an immersive experience where users can interact with virtual objects in a real-world environment.

## Features
- Mixed Reality with Passthrough to blend the virtual game with real-world visuals.
- Hand interaction to stop the flying boxes and score points.
- Dynamic gameplay where users can earn and lose points based on interaction.

## How to Build

### Android

To build the application for an Android VR headset, use the following command:

```
dotnet publish -c Release .\Projects\Android\{name}.csproj
```

Make sure to replace `{name}` with the actual project name.

### UWP (HoloLens 2 )

To build the application for HoloLens, first you need to generate a 'Certificate.pfx' in the root folder and than execute the following command.

```
msbuild .\Platforms\UWP\StereoKit_UWP.csproj /p:Platform=ARM64 /p:AppxBundle=Always /p:AppxBundlePlatforms="ARM64" /p:PackageCertificateKeyFile=Certificate.pfx /p:AppxPackageDir=../../OUTPUT_Holo /restore
```

## Presentation Link

For more information on the project and a walkthrough of the application, please refer to the presentation linked below:

[Mixed Reality Development Talk](https://dataexpertsnb-my.sharepoint.com/:p:/g/personal/chris_papenfuss_data-experts_de/EfOatqd2T0tBmEbM9lO3ItEBlBED93EszzctDz3WDDyoXA?e=KMFssb)

## AR-Core Demo

This repository also includes a patch file `SKARCore-orzel.patch` located in the root directory. This patch is used to update the following repository to create the AR-Core demo shown in the presentation:

- [SKARCore Repository](https://github.com/maluoi/SKARCore.git)

To apply the patch, navigate to the SKARCore repository and run the following command:

```
git apply ../SKARCore-orzel.patch
```

Additionally, the APK for the AR-Core demo is available for download [here](https://dataexpertsnb-my.sharepoint.com/:u:/g/personal/chris_papenfuss_data-experts_de/EUhoOM7u1ABJpmXt8jDAuJUBxEMIGtenMwyGIHrf-IeSKQ?e=I2Edrj).

