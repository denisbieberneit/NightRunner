using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform levelStart;
    [SerializeField] private List<GameObject> levelParts;
    [SerializeField] private GameObject player;


    private Vector3 lastEndPosition;

    public bool spawnNext = true;

    private void Awake()
    {
        lastEndPosition = levelStart.Find("Distance_Right").position;
    }

    private void FixedUpdate()
    {
        if (spawnNext)
        {
            SpawnLevelpart();
            spawnNext = false;
        }
    }


    private void SpawnLevelpart()
    {
        Transform lastLevelPartTransform = SpawnLevelPart(lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("Distance_Right").position;
    }

    private Transform SpawnLevelPart(Vector3 spawnPoint)
    {
        Transform levelPartTransform = Instantiate(getRandomLevelPart(), spawnPoint, Quaternion.identity).transform;
        return levelPartTransform;
    }

    private GameObject getRandomLevelPart()
    {
       return levelParts[Random.Range(0, levelParts.Count)];
    }
}
