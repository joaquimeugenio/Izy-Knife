using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player playerSingleton;
    public PoolManagerKnife knifePool;
    public HUDManager hudManager;
    public int points, speed;
    public WoodBoardBehavior woodBoard;
    public int knifeCount;
    public int knifeLimit;
    // Start is called before the first frame update

    public void Awake()
    {
        //Create record Key in Player Prefs
        if (PlayerPrefs.HasKey("record")==false)
        {
            PlayerPrefs.SetInt("record", 0);
        }
        //Set the framerate to 60FPS
        Application.targetFrameRate = 60;

        //Define Player as a singleton
        if (playerSingleton == null)
            playerSingleton = this;
        else
            Destroy(this);
    }



    void Start()
    {
        //Load knife Pool
        knifePool.InitializePool();
        LoadNextKnife();
    }

    //Function to get the Player's Singleton Object
    public static Player getPlayerSingleton()
    {
        return playerSingleton;
    }

   

    
    public void LoadNextKnife()
    {
       //Load knife if it hasnt reach the limit
        if (knifeCount < knifeLimit)
        {
            points++;

            knifePool.GetKnifeFromPool(transform.position, transform.rotation);
            
            knifeCount++;
        }

        //End of the round and Reload the Knives
        else
        {
            ResetShotKnives();
            
        }
    }
    
    public void ResetShotKnives()
    {
        //Reset knife count to the next round
        knifeCount = 0;

        //Return all shot knives to the knifePool 
        int childCount = woodBoard.transform.childCount;

        for (int i=0; i <childCount; i++)
        {
            knifePool.ReturnKnifeToPool(woodBoard.transform.GetChild(0).gameObject);
        }

        //Next Round with more difficulty
        IncreaseDifficulty();
        LoadNextKnife();

    }

    public void GameOver()
    {
        //Cache Player's record from Prefs
        int record = PlayerPrefs.GetInt("record");

        //Loads GameOver Screen passing the round info
        hudManager.GameOver(points, record);

        //Update the record if needed
        if (points > record)
        {
            PlayerPrefs.SetInt("record", points);
        }
    }


    public void IncreaseDifficulty()
    {
        //Increases difficulty by adding more speed and more knives to throw
        knifeLimit += 1;
        woodBoard.rotationSpeed += 1;

    }

    public void Restart()
    {
        //Reload the Scene to Retry
        SceneManager.LoadScene(1);
    }
   
}
