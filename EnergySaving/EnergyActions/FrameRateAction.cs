using UnityEngine;

/// <summary>
/// In order to FPS limiting to actually work, VSync needs to be set to 0.
/// Source: https://docs.unity3d.com/ScriptReference/QualitySettings-vSyncCount.html
/// </summary>
[RequireComponent(typeof(EnergySaver))]
public class FrameRateAction : MonoBehaviour, IEnergyAction
{
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
    }

    public void Apply(EnergyProfile profile)
    {
        if (profile == null) return;
        Application.targetFrameRate = profile.MaxFps;
    }

    public void OnDisable() { }
}