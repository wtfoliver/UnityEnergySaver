using UnityEngine;

/// <summary>
/// In order to FPS limiting to actually work, VSync needs to be set to 0. 
/// Source: https://docs.unity3d.com/ScriptReference/QualitySettings-vSyncCount.html
/// 
/// Unfortunately this introduces additional cost to sync CPU and GPU.
/// Therefore for standalone applications QualitySettings.vSyncCount is increased, instead of changing Application.targetFrameRate.
/// 
/// You can always choose to use RenderingIntervalAction additionaly or instead of this class.
/// </summary>
[RequireComponent(typeof(EnergySaver))]
public class FrameRateAction : MonoBehaviour, IEnergyAction
{
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX
    int _initialVSyncCount;

    void Awake()
    {
        _initialVSyncCount = QualitySettings.vSyncCount;
    }

    public void Apply(EnergyProfile profile)
    {
        if (profile == null) return;

        float refreshRate = (float)Screen.currentResolution.refreshRateRatio.value;
        int divisor = Mathf.Clamp(Mathf.RoundToInt(refreshRate / profile.MaxFps), 1, 4);

        QualitySettings.vSyncCount = divisor;
    }

    public void OnDisable()
    {
        QualitySettings.vSyncCount = _initialVSyncCount;
    }
#else
    public void Apply(EnergyProfile profile)
    {
        if (profile == null) return;
        Application.targetFrameRate = profile.MaxFps;
    }

    public void OnDisable() { }
#endif
}
