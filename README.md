This plugin for Unity allows for several power reducing actions to be performed in your game.

# What does the plugin do?

The plugin listens to player input. If no input is registered, it switches between energy profiles. Energy profiles define which settings should be adjusted (for example frame rate, resolution or physics updates). You can create or modify energy profiles to fit the needs of your project.

# Why does that matter?

Reducing power consumption can increase battery life, improving player experience. It can also reduce energy costs during development and gameplay while lowering carbon emissions.

# Installation

In Unity's package manager -> Add package from Git URL:
https://github.com/wtfoliver/UnityEnergySaver.git

### Quick start

Drop the EnergySaver prefab in your scene. It should work out of the box. The default EnergyProfiles begin activating after 10 seconds of inactivity.

# How to use the plugin?

The plugin has three central pieces. 
- **The EnergySaver** is a singleton that listens for input and updates the currently active EnergyProfile.
- **IEnergyActions** are data bundles containing the settings used by the IEnergyActions.
- **EnergyProfiles**. apply the settings defined in the active EnergyProfile.

When the EnergySaver updates all IEnergyActions with a new EnergyProfile, the IEnergyActions update i.e. FPS, render scale (URP), render intervals and more.

While EnergySaver listens to input automatically, you can also lock profiles (for example to prevent FPS drops during cutscenes) or force a specific profiles.

All IEnergyActions attached as components to the EnergySaver GameObject will be used to update the game. Adding or removing IEnergyActions changes how the energy saver behaves. This allows you to disable specific actions if they interfere with gameplay (for example physics updates).

### EnergyProfiles

This is what the settings of an energy profile look like:

```
float ActivateAfterIdle = 10f;
PowerConstraints PowerConstraints;
int Priority;
int MaxFps = 60;
int RenderInterval = 0;
float RenderScaleMultiplier = 1f;
SimulationMode PhysicsSimulationMode;
```

The first three parameters determine which profile should become active. **PowerConstraints** check whether the device is plugged in and whether the game is in focus. If multiple profiles match, **Priority** acts as a tie-braker. Energy profiles are initially loaded from **EnergyProfileDefinition ScriptableObjects**, but the architecture allows player overrides to be loaded at runtime.

### IEnergyActions

Currently the following actions are implemented:

- FrameRateAction: Reduces the game's FPS.
- RenderingIntervalAction: Changes how frequently new frames are  rendered. This takes the FPS into account. If your game has 30 FPS and the rendering interval is 60, the game renders a new frame every 2 seconds.
- PhysicsSimulationAction: Changes how the physics engine updates.
- RenderScaleAction: Adjusts the render scale of the currently active URP asset. Works only with the Universal Render Pipeline.
- DynamicResolutionAction: Alters the dynamic scale of the currently active HDRP-asset. Works only with the High definition render pipeline.

# Sources and further reading

The plugin is very much inspired by Hauke Thiessens [Unreal Energy Saving Plugin](https://github.com/HaukeThiessen/EnergySavingPlugin), which itself references [this](https://cdn2.unrealengine.com/reducing-fortnites-power-consumption-layout-v03-ffedbeb1adeb.pdf) paper from Fortnite's energy saving features.

Some Unity-specific ideas very copied from Bronson Zgeb's [blog post](https://bronsonzgeb.com/index.php/2021/10/16/low-power-mode-in-unity/).

### Sustainable games alliance

While Walk The Frog is currently not a member of the [SGA](https://sustainablegamesalliance.org/), this plugin only exists largely thanks to the SGA's effort to make game development and consumption more energy-efficient. If sustainability in games interests you, they offer many useful resources on the topic.

# Version Requirements

The plugin was tested in Unity 2022.3.62f2 and upwards. Older Unity versions may or may not be compatible with the plugin.

# Restrictions

The plugin currently only works with Unity's Universal Render Pipeline (URP) or High definition render pipeline (HDRP).

# Further development

This plugin is still in a very early stage, so use caution when integrating it into production projects. So far it has only been tested on PC.

Feedback and bug reports are very welcome.

### Planned features

- Support the Built-in Render Pipeline
- Additional energy-saving features

# License

This plugin is free to use in commercial and non-commercial products. This software is provided 'as-is', without any express or implied warranty. In no event will the authors be held liable for any damages arising from the use of this software. Permission is granted to anyone to use this software for any purpose, including commercial applications, and to alter it and redistribute it freely. Attribution is appreciated, but not required.
