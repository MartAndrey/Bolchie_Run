using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Override Trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        //look for a collision with the "Player" object and also with a "BoxCollider"
        if (other.CompareTag("Player") && other.GetType() == typeof(BoxCollider2D))
        {
            //We add a new block to the scene
            LevelManager.sharedInstanceLM.AddLevelBlock(); 

            //We remove the first block added to the table.
            LevelManager.sharedInstanceLM.RemoveLevelBlock(); 
        }
    }
}
