using UnityEngine;

public class MGSingleton<T> : MinigameManager where T : MGSingleton<T>
{
    public static T Instance { get; private set; } = null;

    public static bool IsInitialized => Instance != null;

    protected virtual void Awake()
    {
        if (Instance == null) Instance = (T)this;
        else Debug.LogError("Multiple Instances of: " + this);
    }

    protected virtual void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}