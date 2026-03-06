using UnityEngine;

public sealed class ActivityTracker
{
    float _idleTime;
    public float IdleTime => _idleTime;

    public void Tick()
    {
        _idleTime += Time.unscaledDeltaTime;
    }

    public void NotifyActivity()
    {
        _idleTime = 0f;
    }
}