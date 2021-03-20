using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Transform startingPoint;
    [SerializeField] private Transform endpoint;
    [SerializeField] private Transform height;
    [SerializeField] private List<GameObject> obstacles;

    private bool spawnObstacles = true;
    private Vector2 lastVec;

    private void FixedUpdate()
    {
        if (spawnObstacles)
        {
            GameObject obstacle = getRandomLevelPart();
            if (lastVec == new Vector2(0,0))
            {
                lastVec = new Vector2(startingPoint.position.x + 150, height.position.y);
                Instantiate(obstacle,lastVec, height.rotation);
            }
            else
            {
                lastVec = new Vector2(lastVec.x + Random.Range(100, 110), height.position.y);
                Instantiate(obstacle, lastVec, height.rotation);
            }
            if (endpoint.position.x - 300 <= lastVec.x)
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
