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

    public EnergyProfile Evaluate(ActivityTracker activity)
    {
        PowerContext context = new PowerContext
        {
            IsFocused = Application.isFocused,
            IsPluggedIn = SystemInfo.batteryStatus != BatteryStatus.Discharging,
            IdleTime = activity.IdleTime
        };

        return _powerStateEvaluator.Evaluate(_profiles, context);
    }
}