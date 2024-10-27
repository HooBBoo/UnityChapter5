using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.Arm;

public class Moon : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float time;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;

    void UpdateLighting(Light lightSource, Gradient colorGradiant)
    {
        lightSource.color = colorGradiant.Evaluate(time);
    }
}
