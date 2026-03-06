using UnityEngine;

public class EnergyProfile
{
    public EnergyProfileDefinition Definition { get; }
    public float ActivateAfterIdle = 3f;
    public PowerConstraints PowerConstraints;
    public int Priority;
    public int MaxFps = 60;
    public int RenderInterval = 0;
    public float RenderScaleMultiplier = 1f;
    public SimulationMode PhysicsSimulationMode;
    public SimulationMode2D Physics2DSimulationMode;

    public EnergyProfile(EnergyProfileDefinition definition)
    {
        Definition = definition;
        ActivateAfterIdle = definition.ActivateAfterIdle;
        PowerConstraints = definition.PowerConstraints;
        Priority = definition.Priority;
        MaxFps = definition.MaxFps;
        RenderInterval = definition.RenderInterval;
        RenderScaleMultiplier = definition.RenderScaleMultiplier;
        PhysicsSimulationMode = definition.PhysicsSimulationMode;
        Physics2DSimulationMode = definition.Physics2DSimulationMode;
    }
}
