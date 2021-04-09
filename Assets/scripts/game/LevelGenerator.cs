using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform levelStart;
    [SerializeField] private List<GameObject> levelParts;
    [SerializeField] private GameObject player;
    [SerializeField] private int buildingOffset;
    [SerializeField] private bool isFirstBuilding;

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
        float x = spawnPoint.x;
        if (!isFirstBuilding)
        {
            x = x + buildingOffset;
        }
        isFirstBuilding = false;
        Vector3 calc = new Vector3(x, spawnPoint.y, spawnPoint.z);
        Transform levelPartTransform = Instantiate(getRandomLevelPart(), calc, Quaternion.identity).transform;
        return levelPartTransform;
    }

    private GameObject getRandomLevelPart()
    {
       return levelParts[Random.Range(0, levelParts.Count)];
    }
}
