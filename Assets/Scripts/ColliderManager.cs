using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    public static ColliderManager instance;

    [SerializeField] private bool displayCollidersOnPlay;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }   
    }

    public bool DisplayOnPlay()
    {
        return displayCollidersOnPlay;
    }

}
