using UnityEngine;

[CreateAssetMenu(fileName = "New EnergyProfileDefinition", menuName = "ScriptableObjects/EnergySaver/EnergyProfileDefinition")]
public class EnergyProfileDefinition : ScriptableObject
{
    [Header("Evaluation data")]
    public float ActivateAfterIdle = 3f;
    public PowerConstraints PowerConstraints;
    public int Priority;
    [Header("IEnergyAction data")]
    public int MaxFps = 60;
    [Tooltip("How many frames pass until a new frame is rendered. Default is 0, high numbers means less frequent rendering.")]
    public int RenderInterval = 0;
    [Range(0.1f, 2f)]
    public float RenderScaleMultiplier = 1f;
    public SimulationMode PhysicsSimulationMode;
    public SimulationMode2D Physics2DSimulationMode;
}