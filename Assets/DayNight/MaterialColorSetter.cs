using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialColorSetter : MonoBehaviour, DayNightInterface
{
    public Gradient gradient;
    public string colorPropertyName;
    public SpriteRenderer material;

    public void GetComponent()
    {
        material = GetComponent<SpriteRenderer>();
    }

    public void SetParameter(float time)
    {
        material.color = gradient.Evaluate(time);
    }
}
