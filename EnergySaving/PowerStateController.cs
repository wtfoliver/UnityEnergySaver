using UnityEngine;

public sealed class PowerStateController
{
    PowerStateEvaluator _powerStateEvaluator;
    readonly EnergyProfile[] _profiles;

    public PowerStateController(EnergyProfile[] profiles)
    {
        this._profiles = profiles;
        _powerStateEvaluator = new PowerStateEvaluator();
    }

    public EnergyProfile Evaluate(ActivityTracker activity, out PowerContext context)
    {
        context = new PowerContext
        {
            NotFocused = !Application.isFocused,
            NotPluggedIn = SystemInfo.batteryStatus == BatteryStatus.Discharging,
            IdleTime = activity.IdleTime
        };

        return _powerStateEvaluator.Evaluate(_profiles, context);
    }
}