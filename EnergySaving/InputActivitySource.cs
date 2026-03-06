using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

/// <summary>
/// Listens to any key inputs, then updates the InputTracker accordingly. Works only with the new input system.
/// </summary>
public sealed class InputActivitySource : MonoBehaviour
{
    ActivityTracker _activityTracker;

    private void Awake()
    {
        _activityTracker = EnergySaver.Instance.ActivityTracker;
    }

    private void OnEnable()
    {
        InputSystem.onAnyButtonPress.Call(_ => _activityTracker.NotifyActivity());
        InputSystem.onEvent += OnInputEvent;
    }

    private void OnDisable()
    {
        InputSystem.onEvent -= OnInputEvent;
    }

    void OnInputEvent(InputEventPtr eventPtr, InputDevice device)
    {
        if (eventPtr.IsA<StateEvent>() || eventPtr.IsA<DeltaStateEvent>())
            _activityTracker.NotifyActivity();
    }
}