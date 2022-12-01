using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour
{
    [SerializeField] private bool displayThisColliderOnPlay;

    private void Start()
    {
        if (!ColliderManager.instance.DisplayOnPlay() && !displayThisColliderOnPlay)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
