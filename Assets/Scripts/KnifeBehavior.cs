using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehavior : MonoBehaviour
{
    public int knifeSpeed;
    public Rigidbody2D rigidBody;
    BoxCollider2D colKnife;
  
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        //Singleton Definition
        player = Player.getPlayerSingleton();

        //Cache of RB and Collider
        rigidBody = GetComponent<Rigidbody2D>();
        colKnife = GetComponent<BoxCollider2D>();
      
    }

    private void Update()
    {
        Debug.Log(player.gameOver);
        if (!player.gameOver)
        {
#if !UNITY_EDITOR
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
#endif
if(Input.GetMouseButton(0))
            {
                //Take Player's Speed to apply to the knife
                knifeSpeed = player.speed;

                //Shoot the Knife Up
                StartCoroutine(MoveKnife(new Vector3(0, 0, 0)));
            }
        }
        
    }

   


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check Collision on board
        if (collision.gameObject.CompareTag("board"))
        {
           //Cache the board
           GameObject woodBoard = collision.gameObject;
            //Stick the knife to the wood
            transform.SetParent(woodBoard.transform);
           
            StopKnife();
            //Load next knife to shoot
            player.LoadNextKnife();

        }
        else 
       {
            //Knife has hit another object: Game Over
            player.GameOver();
            //Stop knife movement
            knifeSpeed = 0;
            //Makes the knife fall
            rigidBody.gravityScale = 5;
            
        }
    }




    public void StopKnife()
    {
        //Freeze Knife's Transform
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

        //Stop the Knife Movement by setting speed to zero
        knifeSpeed = 0;

        //Disable knife's beahavior
        GetComponent<KnifeBehavior>().enabled = false;

    }

    public IEnumerator MoveKnife(Vector3 target)
    {
        //Release Knife's movement
        rigidBody.constraints = RigidbodyConstraints2D.None;

        //Lerp position of the knife to up relative to the speed
        while (transform.position != target && knifeSpeed!=0)
        {
          
            transform.position= Vector3.Lerp(transform.position, target, Time.deltaTime * knifeSpeed);
            yield return null;
        }

    }
    
}
