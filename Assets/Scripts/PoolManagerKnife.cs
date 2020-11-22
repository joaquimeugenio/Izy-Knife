using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManagerKnife : MonoBehaviour
{
    public GameObject knifePrefab;
    public int poolSize;
    public Queue<GameObject> pool = new Queue<GameObject>();



    public void InitializePool()
    {
        //Fill the knife pool based on public poolSize value
       for(int i=0; i<poolSize;i++)
        {
            //Instantiate knife prefab as pool child
            GameObject knife = GameObject.Instantiate(knifePrefab, gameObject.transform);
            knife.SetActive(false);
            pool.Enqueue(knife);
 
        }
    }


    public GameObject GetKnifeFromPool(Vector3 pos, Quaternion rot)
    {
        //Fill the pool with more knives if necessary
        if (pool.Count <= 0)
        {
            GameObject knife = Instantiate(knifePrefab);
            knife.SetActive(false);
            pool.Enqueue(knife);
            poolSize++;
        }

        //Get the knife from the pool activating it on the scene and replacing 
        GameObject newKnife = pool.Dequeue();
        newKnife.SetActive(true);
        newKnife.GetComponent<KnifeBehavior>().enabled = true;
        newKnife.transform.position = pos;
        newKnife.transform.rotation = rot;
        return newKnife;
    }



    public void ReturnKnifeToPool(GameObject go)
    {
        //Deactivate the knife and put it back to pool
        go.SetActive(false);
        go.transform.SetParent(transform);
        pool.Enqueue(go);


    }

}


