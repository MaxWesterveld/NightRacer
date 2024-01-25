using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class so the canvas and all screens
/// dont get destroyed on load
/// </summary>
public class DontDestroyOnLoad : MonoBehaviour
{
    public static DontDestroyOnLoad instance;
    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
