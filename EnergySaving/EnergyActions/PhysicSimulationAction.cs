using UnityEngine;

/// <summary>
/// Switches how often the physics simulation is running.
/// Credits to Bronson Zgeb: 
/// https://bronsonzgeb.com/index.php/2021/10/16/low-power-mode-in-unity/
/// https://github.com/bzgeb/LowPowerExampleApp/tree/master
/// </summary>
[RequireComponent(typeof(EnergySaver))]
public class PhysicSimulationAction : MonoBehaviour, IEnergyAction
{
    SimulationMode _cachedSimulationMode;
    SimulationMode2D _cachedSimulationMode2D;

    private void Awake()
    {
        _cachedSimulationMode = Physics.simulationMode;
        _cachedSimulationMode2D = Physics2D.simulationMode;
    }

    public void Apply(EnergyProfile profile)
    {
        if (profile == null) return;
        Physics.simulationMode = profile.PhysicsSimulationMode;
        Physics2D.simulationMode = profile.Physics2DSimulationMode;
    }

    public void OnDisable() 
    { 
        Physics.simulationMode = _cachedSimulationMode;
        Physics2D.simulationMode = _cachedSimulationMode2D;
    }
}
