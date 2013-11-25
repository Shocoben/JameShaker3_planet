using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour
{
    public float time = 2;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, time);
    }
}
