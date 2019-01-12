using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class IgnoreButton : Button, ICanvasRaycastFilter
{
    [SerializeField]
    float radius = 50f;

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return Vector2.Distance(sp, transform.position) < radius;
    }

}