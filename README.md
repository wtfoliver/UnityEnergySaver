This plugin for Unity allows for several power reducing actions to be performed in your game.

# What does the plugin do?

The plugin listens to player inputs. If no input is registered, it switches between energy profiles. The energy profiles define what settings should be adjusted (for example frame rate, resolution, physics updates). You can create or modify energy profiles to fit them to your project.

# Why does that matter?

Reducing power consumption can increase the battery duration, which will improve player experience. It also saves money during development and games consumption, while also reducing carbon emissions.

# Installation

In Unitys package manager -> Add package from Git URL:
https://github.com/wtfoliver/com.walkthefrog.energysaver.git

### Quick start

Drop the EnergySaver-prefab in your scene. It should work from the start. The default EnergyProfiles start kicking in after 3 seconds.

# How to use the plugin?

The plugin has three central pieces. **The EnergySaver**, **IEnergyActions** and **EnergyProfiles**. The EnergySaver is a singleton that listens to inputs and updates the currently used EnergyProfile. The EnergyProfiles are data bundles that contain the information for the IEnergyActions. When the EnergySaver updates all IEnergyActions with a new EnergyProfile, the IEnergyActions update i.e. FPS, render scale (URP), render intervals and more.

While EnergySaver listens to input, you can also lock profiles (to prevent fps drops during cutscenes for example) or force new profiles.

All IEnergyActions that are components on the EnergySaver gameObject will be used to update your game. Removing or adding IEnergyActions as components changes how the energy saver works. This can be used if you only want certain actions to take place, i.e. if changing physics updates interferes with your game play.

### EnergyProfiles

This is what the settings of an energy profile look like:

```
float ActivateAfterIdle = 3f;
PowerConstraints PowerConstraints;
int Priority;
int MaxFps = 60;
int RenderInterval = 0;
float RenderScaleMultiplier = 1f;
SimulationMode PhysicsSimulationMode;
```

The first three parameters determine which profile fits best. The PowerConstraints check if the battery is plugged in and if the game is in focus. If multiple profiles should match, priority acts as a tie-braker. Energy profiles are initially loaded from EnergyProfileDefinition scriptable objects, but the architecture allows for loading overrides made by players during runtime.

### IEnergyActions\*\*

Currently the following actions are implemented:

- FrameRateAction: Reduces the FPS of your game.
- RenderingIntervalAction: Changes the frequency in which new frames are being rendered. This takes the FPS into account. If you game has 30 FPS and the rendering interval is 60, the game renders a new frame every 2 seconds.
- PhysicsSimulationAction: Changes if the physics engine updates.
- RenderScaleAction: Is used to tweak the RenderScale of the currently used URP-asset. Works only with the Universal Render Pipeline.
- DynamicResolutionAction: Alters the dynamic scale of the currently used HDRP-asset. Works only with the High definition render pipeline.

# Sources and further reading

The plugin is very much inspired by Hauke Thiessens [Unreal Energy Saving Plugin](https://github.com/HaukeThiessen/EnergySavingPlugin), which itself references [this](https://cdn2.unrealengine.com/reducing-fortnites-power-consumption-layout-v03-ffedbeb1adeb.pdf) paper from Fortnite's energy saving features.

Some Unity-specific ideas very copied from Bronson Zgeb's [blog post](https://bronsonzgeb.com/index.php/2021/10/16/low-power-mode-in-unity/).

### Sustainable games alliance

While Walk The Frog is not part of the [SGA](https://sustainablegamesalliance.org/) at the moment, the plugin only exists because of the SGAs effort to make game development and consumption more energy efficient. If sustainability in games is something you are interested in - they offer plenty of resources regarding the topic.

# Version Requirements

The plugin was tested in Unity 2022.3.62f2 and upwards. Older Unity versions may or may not be compatible with the plugin.

# Restrictions

The plugin currently only works with Unity's Universal Render Pipeline (URP) or High definition render pipeline (HDRP). It also requires the newer input package instead of the legacy input class.

# Further development

This plugin is still in a very early state, so be aware of that when using it in an actual production context. As of now, I only managed to test the game on PC. Any feedback and bug reports are appreciated.

### Planned features

- Support the built-in render pipeline
- Support the legacy input system
- More energy saving features

# License

This plugin is free to use in commercial and non-commercial products. This software is provided 'as-is', without any express or implied warranty. In no event will the authors be held liable for any damages arising from the use of this software. Permission is granted to anyone to use this software for any purpose, including commercial applications, and to alter it and redistribute it freely. Attribution is appreciated, but not required.