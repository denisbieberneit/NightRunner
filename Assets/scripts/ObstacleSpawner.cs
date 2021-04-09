using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] Transform levelPart;
    private Transform startingPoint;
    private Transform endpoint;
    private Transform height;
    [SerializeField] private List<GameObject> obstacles;

    public float startingRangeObstacle;
    public float endRangeBetweenObstacle;
    public float distanceBetweenObstacle;

    private bool spawnObstacles = true;
    private Vector2 lastVec;

    private void Awake()
    {
        startingPoint = levelPart.Find("Distance_Left").transform;
        endpoint = levelPart.Find("Distance_Right").transform;
        height = levelPart.Find("Height").transform;
    }

    private void FixedUpdate()
    {
        if (spawnObstacles)
        {
            GameObject obstacle = getRandomLevelPart();
            if (lastVec == new Vector2(0,0))
            {
                lastVec = new Vector2(startingPoint.position.x + startingRangeObstacle, height.transform.position.y);
                Instantiate(obstacle,lastVec, height.rotation);
            }
            else
            {
                lastVec = new Vector2(lastVec.x + distanceBetweenObstacle, height.transform.position.y);
                Instantiate(obstacle, lastVec, height.rotation);
            }
            if (endpoint.position.x - endRangeBetweenObstacle <= lastVec.x)
            {
                spawnObstacles = false;
            }
        }
    }


    private GameObject getRandomLevelPart()
    {
        return obstacles[Random.Range(0,obstacles.Count)];
    }
}
