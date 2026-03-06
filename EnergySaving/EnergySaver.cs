using UnityEngine;

/// <summary>
/// The main class that runs everything. Needs to be as a MonoBehaviour on a gameObject.
/// </summary>

[DefaultExecutionOrder(-1000)]
public class EnergySaver : MonoBehaviour
{
    static EnergySaver _instance;
    public static EnergySaver Instance => _instance;

    [SerializeField] EnergyProfileDefinition[] _profileDefinitions;
    EnergyProfile[] _profiles;
    EnergyProfile _currentProfile = null;
    public EnergyProfile CurrentProfile => _currentProfile;

    ActivityTracker _activityTracker;
    public ActivityTracker ActivityTracker => _activityTracker;

    PowerStateController _powerStateController;
    IEnergyAction[] _actions;

    bool _isLocked = false;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogError(
                "Multiple EnergySaver instances detected. There must be exactly one EnergySaver in the project.",
                this);
#endif
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        SetUpEnergyProfiles();
        _activityTracker = new ActivityTracker();
        _powerStateController = new PowerStateController(_profiles);
        _actions = GetComponents<IEnergyAction>();
    }

    void SetUpEnergyProfiles()
    {
        _profiles = new EnergyProfile[_profileDefinitions.Length];
        for(int i = 0; i < _profileDefinitions.Length; i++)
        {
            _profiles[i] = new EnergyProfile(_profileDefinitions[i]);
        }
    }

    void Update()
    {
        if (_isLocked)
        {
            return;
        }

        _activityTracker.Tick();
        EnergyProfile profile = _powerStateController.Evaluate(_activityTracker);

        if(_currentProfile == null || _currentProfile != profile)
        {
            _currentProfile = profile;
            for (int i = 0; i < _actions.Length; i++)
                _actions[i].Apply(_currentProfile);
        }
    }

    void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }

    /// <summary>
    /// Use these to either set a current profile or prevent switching of profiles (i.e. during a cutscene)
    /// </summary>
    #region Public APIs

    public void ForceProfile(EnergyProfileDefinition definition)
    {
        ForceProfile(new EnergyProfile(definition));
    }

    public void ForceProfile(EnergyProfile profile)
    {
        _currentProfile = profile;
    }

    public void LockProfile(bool isLocked, bool resetActivityTracker = true)
    {
        _isLocked = isLocked;
        if(resetActivityTracker)
        {
            _activityTracker.NotifyActivity();
        }
    }

    /// <summary>
    /// You might want to give the user the ability to alter the settings of a given profile. 
    /// In this case, profiles might be saved as JSONs i.e. and later loaded instead of relying on the energy profile definitions only.
    /// </summary>
    public void SetProfiles(EnergyProfile[] profiles)
    {
        _profiles = profiles;
    }
    #endregion
}