using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject startingPoint;
    [SerializeField] private GameObject levelPart_01;
    [SerializeField] private GameObject player;
    private GameObject lastObject = null;

    int timer = 2000;

    float distanceToNext;
    float heightToNext;

    private void FixedUpdate()
    {
        if (timer >= 2000)
        {
            if (lastObject == null)
            {
                lastObject = startingPoint;
            }
            Transform lastHeight = lastObject.transform.Find("Height");
            Transform lastDistance = lastObject.transform.Find("Distance_Right");
            timer = 0;
            GameObject levelPart= (GameObject) Instantiate(getRandomLevelPart(), new Vector2(lastDistance.position.x + Random.Range(30,50), lastDistance.position.y), levelPart_01.transform.rotation);
            Transform newHeight = levelPart.transform.Find("Height");
            Transform newDistance = levelPart.transform.Find("Distance_Left");
            distanceToNext = Mathf.Abs(newDistance.position.x - lastDistance.position.x);
            heightToNext = lastHeight.position.y + newHeight.position.y;
            Debug.Log("Distance to next: " + distanceToNext);
            Debug.Log("Height to next: " + heightToNext);
            lastObject = levelPart;
            Destroy(levelPart,1000);
        }
        timer = timer + 1;
    }


    private GameObject getRandomLevelPart()
    {
        return levelPart_01;
    }
}
