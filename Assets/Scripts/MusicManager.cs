using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Singleton { get; private set; }
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Awake()
    { 
        if(Singleton != null&&Singleton!=this) {
            Destroy(this.gameObject);
        }
        else
        {
            Singleton = this;
        }
    }
}
