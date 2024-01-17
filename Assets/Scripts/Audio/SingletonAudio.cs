using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonAudio : MonoBehaviour
{
    private static SingletonAudio _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
