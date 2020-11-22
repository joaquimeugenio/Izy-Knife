using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBoardBehavior : MonoBehaviour
{
    public int rotationSpeed;
    public Animation hitClip;

    // Start is called before the first frame update
    void Start()
    {
        //Cache Animation component
      hitClip = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate the wood by the rotationSpeed
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + (0.1f*rotationSpeed));
        
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Play animation of hit when knife collides with the wood
        hitClip.Play();
    }
}
