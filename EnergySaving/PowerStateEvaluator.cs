using UnityEngine;

/// <summary>
/// Evaluates all EnergyProfiles according to the provided PowerContext
/// </summary>
public class PowerStateEvaluator
{
    public EnergyProfile Evaluate(EnergyProfile[] profiles, in PowerContext context)
    {
        EnergyProfile best = null;

        for (int i = 0; i < profiles.Length; i++)
        {
            EnergyProfile profile = profiles[i];
            
            if (!ConstraintsSatisfied(profile.PowerConstraints, context))
                continue;
            
            if (context.IdleTime < profile.ActivateAfterIdle)
                continue;
            
            if (best == null ||
                profile.ActivateAfterIdle > best.ActivateAfterIdle ||
                (Mathf.Approximately(profile.ActivateAfterIdle, best.ActivateAfterIdle)
                 && profile.Priority > best.Priority))
            {
                best = profile;
            }
        }
        
        return best;
    }
    bool ConstraintsSatisfied(PowerConstraints constraints, in PowerContext context)
    {
        if ((constraints & PowerConstraints.RequiresFocus) != 0 && !context.IsFocused)
            return false;

        if ((constraints & PowerConstraints.RequiresPluggedIn) != 0 && !context.IsPluggedIn)
            return false;

        return true;
    }
}
