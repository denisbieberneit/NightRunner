using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    public LevelGenerator generator;

    private void Start()
    {
        generator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        generator.spawnNext= true;
        Debug.Log("triggert");
    }
}
