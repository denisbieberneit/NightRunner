using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface DayNightInterface
{
    void GetComponent();

    void SetParameter(float time);
}


public class DayNightCylce : MonoBehaviour
{

    [Range(0, 1)]
    public float time;
    public DayNightInterface[] setters;
    public bool day;


    private void OnEnable()
    {
        time = 0;
        day = true;
        GetSetters();
    }

    [ExecuteInEditMode]
    private void GetSetters()
    {
        setters = GetComponentsInChildren<DayNightInterface>();
        foreach (var setter in setters)
        {
            setter.GetComponent();
        }
    }

    void Update()
    {
        if (setters.Length > 0)
        {
            foreach (var setter in setters)
            {
                setter.SetParameter(time);
            }
        }

        if (time > 1f)
            day = false;
        if (time < 0f)
            day = true;
        if (day)
            time = Mathf.Lerp(time, 1.1f, Time.deltaTime * 0.05f);
        else if (!day)
            time = Mathf.Lerp(time, -0.1f, Time.deltaTime * 0.05f);
    }   
}
