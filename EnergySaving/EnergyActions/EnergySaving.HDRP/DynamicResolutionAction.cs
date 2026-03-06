using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(EnergySaver))]
public class DynamicResolutionAction : MonoBehaviour, IEnergyAction
{
    float _cachedScaleMultiplier;

    void Awake()
    {
        _cachedScaleMultiplier = DynamicResolutionHandler.instance.GetCurrentScale();
    }

    public void Apply(EnergyProfile profile)
    {
        if (profile == null) return;

        DynamicResolutionHandler.SetDynamicResScaler(
            () => _cachedScaleMultiplier * profile.RenderScaleMultiplier,
            DynamicResScalePolicyType.ReturnsPercentage
        );
    }

    public void OnDisable()
    {
        DynamicResolutionHandler.SetDynamicResScaler(
            () => _cachedScaleMultiplier,
            DynamicResScalePolicyType.ReturnsPercentage
        );
    }
}
