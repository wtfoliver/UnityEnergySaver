using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(EnergySaver))]
public class RenderScaleAction : MonoBehaviour, IEnergyAction
{
    RenderPipelineAsset _currentRenderPipelineAsset;
    float _cachedRenderScale;

    void Awake()
    {
        _currentRenderPipelineAsset = QualitySettings.renderPipeline;
        if (_currentRenderPipelineAsset != null)
        {
            UniversalRenderPipelineAsset urpasset = _currentRenderPipelineAsset as UniversalRenderPipelineAsset;
            if (urpasset != null)
            {
                _cachedRenderScale = urpasset.renderScale;
            }
        }
    }

    public void Apply(EnergyProfile profile)
    {
        if (profile == null) return;
        if (QualitySettings.renderPipeline == _currentRenderPipelineAsset)
        {
            UniversalRenderPipelineAsset urpasset = _currentRenderPipelineAsset as UniversalRenderPipelineAsset;
            if (urpasset != null)
            {
                urpasset.renderScale = _cachedRenderScale * profile.RenderScaleMultiplier;
            }
        }
    }

    public void OnDisable()
    {
        UniversalRenderPipelineAsset urpasset = _currentRenderPipelineAsset as UniversalRenderPipelineAsset;
        if (urpasset != null)
        {
            urpasset.renderScale = _cachedRenderScale;
        }
    }
}
