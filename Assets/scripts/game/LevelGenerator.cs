using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject startingPoint;
    [SerializeField] private GameObject levelPart_01;
    [SerializeField] private GameObject player;
    private GameObject lastObject = null;

    float distanceToNext;
    float heightToNext;

    public bool spawnNext = true;

    private void FixedUpdate()
    {
        if (spawnNext)
        {
            if (lastObject == null)
            {
                lastObject = startingPoint;
            }
            Transform lastHeight = lastObject.transform.Find("Height");
            Transform lastDistance = lastObject.transform.Find("Distance_Right");
            GameObject levelPart= (GameObject) Instantiate(getRandomLevelPart(), new Vector2(lastDistance.position.x + 65, -2.4f), levelPart_01.transform.rotation);
            Transform newHeight = levelPart.transform.Find("Height");
            Transform newDistance = levelPart.transform.Find("Distance_Left");
            distanceToNext = Mathf.Abs(newDistance.position.x - lastDistance.position.x);
            heightToNext = lastHeight.position.y + newHeight.position.y;
            lastObject = levelPart;
            Destroy(levelPart,1000);
        }
        spawnNext = false;
    }


    private GameObject getRandomLevelPart()
    {
        return levelPart_01;
    }
}
