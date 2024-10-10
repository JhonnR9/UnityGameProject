using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Singleton instance
    public static GameController Instance { get; private set; }
    public IPawn DefaultPlayer { get;  set; }

    void Awake()
    {
        // Ensure that there's only one instance of GameController
        if (Instance == null)
        {
            Instance = this;
            // Optionally, prevent this object from being destroyed between scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Destroy duplicate instances
            Destroy(gameObject);
        }
    }

}
