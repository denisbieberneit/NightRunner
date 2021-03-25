using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public Text highscore;
    public Text score;

    // Update is called once per frame
    void Update()
    {
        highscore.text = PlayerPrefs.GetInt("PlayerScore") + "m";
        highscore.text = PlayerPrefs.GetInt("PlayerHighscore") + "m";
    }
}
