using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Text HUDKnivesLeft;
    public GameObject gameOverUI, inGameUI;
    public Text pointsUI, recordUI;


    public void GameOver(int points, int record)
    {
        //Activate the GameOver UI and show the info of the match sent by the player
        gameOverUI.SetActive(true);
        inGameUI.SetActive(false);
        pointsUI.text = "Points :"+points.ToString();
        recordUI.text = "Record :"+record.ToString();

    }


    // Update is called once per frame
    void Update()
    {
        //Show the left knives to throw
        HUDKnivesLeft.text = ((Player.getPlayerSingleton().knifeLimit - Player.getPlayerSingleton().knifeCount)+1).ToString();
    }
}
