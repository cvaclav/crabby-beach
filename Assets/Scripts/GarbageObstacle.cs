using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageObstacle : MonoBehaviour
{
    public Garbage garbage;

    public SpriteRenderer sr;
    
    void Start()
    {
        sr.sprite = garbage.sprite;
    }
}
