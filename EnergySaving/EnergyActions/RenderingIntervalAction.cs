using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Changes how many frames need to pass until a new frame is rendered.
/// Credits to Bronson Zgeb: 
/// https://bronsonzgeb.com/index.php/2021/10/16/low-power-mode-in-unity/
/// https://github.com/bzgeb/LowPowerExampleApp/tree/master
/// </summary>
[RequireComponent(typeof(EnergySaver))]
public class RenderingIntervalAction : MonoBehaviour, IEnergyAction
{
    int _cachedRenderingInterval;

    private void Awake()
    {
        _cachedRenderingInterval = OnDemandRendering.renderFrameInterval;    
    }

    public void Apply(EnergyProfile profile)
    {
        if (profile == null) return;
        OnDemandRendering.renderFrameInterval = profile.RenderInterval;
    }

    public void OnDisable()
    {
        OnDemandRendering.renderFrameInterval = _cachedRenderingInterval;
    }
}
