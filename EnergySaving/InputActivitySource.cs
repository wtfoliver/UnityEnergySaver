using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
#endif

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


#if ENABLE_INPUT_SYSTEM // Used with the new input package
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
#endif

#if ENABLE_LEGACY_INPUT_MANAGER // Used with the legacy unity input system
    void Update()
    {
        if (Input.anyKeyDown)
            _activityTracker.NotifyActivity();

        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            _activityTracker.NotifyActivity();
    }
#endif
}