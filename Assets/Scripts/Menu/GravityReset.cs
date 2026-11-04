using UnityEngine;
using UnityEngine.SceneManagement;

// Physics.gravity is GLOBAL and survives scene changes. Both PlayerController and PlayerControllerX
// multiply it in Start(), so without this reset every visit to a level would make gravity stronger.
// No GameObject needed: Unity runs this automatically when the game starts.
public static class GravityReset
{
    private static Vector3 _defaultGravity; // Gravity from Project Settings > Physics, captured at startup

    // Runs once when the game starts, before the first scene loads.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        _defaultGravity = Physics.gravity;          // Remember the original value (-9.81 on Y)
        SceneManager.sceneLoaded -= OnSceneLoaded;  // Avoid subscribing twice
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Unity calls this after a scene's Awake() but BEFORE its Start(), so the players multiply a clean value.
    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Physics.gravity = _defaultGravity;
    }
}
