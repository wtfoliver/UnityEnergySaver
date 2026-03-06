/// <summary>
/// Base interface for differing types of energy actions like reducing the frame rate (FrameRateAction)
/// or changing the render scale of the renderpipeline asset (RenderScaleAction/DynamicResolutionAction).
/// 
/// EnergyActions need to be on the same gameObject as EnergySaver.
/// </summary>

public interface IEnergyAction
{
    void Apply(EnergyProfile profile);
    void OnDisable();
}