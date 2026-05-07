using UnityEngine;

public sealed class PowerStateController
{
    PowerStateEvaluator _powerStateEvaluator;
    readonly EnergyProfile[] _profiles;


    float _powerConstraintUpdateTimer = 0f;
    const float _powerConstraintUpdateRate = 15f;
    BatteryStatus _batteryStatus;
    bool _isFocused;

    public PowerStateController(EnergyProfile[] profiles)
    {
        this._profiles = profiles;
        _powerStateEvaluator = new PowerStateEvaluator();

        UpdatePowerConstraints();
    }

    public EnergyProfile Evaluate(ActivityTracker activity, float time, out PowerContext context)
    {
        TryUpdatingPowerConstraints(time);

        context = new PowerContext
        {
            NotFocused = _isFocused,
            NotPluggedIn = _batteryStatus == BatteryStatus.Discharging,
            IdleTime = activity.IdleTime
        };

        return _powerStateEvaluator.Evaluate(_profiles, context);
    }

    void TryUpdatingPowerConstraints(float time)
    {
        if(time - _powerConstraintUpdateTimer > _powerConstraintUpdateRate)
        {
            _powerConstraintUpdateTimer = time;
            UpdatePowerConstraints();
        }
    }

    void UpdatePowerConstraints()
    {
        _batteryStatus = SystemInfo.batteryStatus;
        _isFocused = Application.isFocused;
    }
}