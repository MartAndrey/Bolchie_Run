using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Singleton
    public static LevelManager sharedInstance;

    //List containing all "Level Blocks"
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();

    //List containing the current "Level Blocks" in the scene
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();

    //Variable that stores the position where the first block has to be created
    public Transform levelStartPosition;

    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateInitialBlocks();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Method in charge of generating the initial blocks of the game
    public void GenerateInitialBlocks()
    {
        for (int i = 0; i < 2; i++)
        {
            AddLevelBlock();
        }
    }

    //Method in charge of adding new blocks to the game
    public void AddLevelBlock()
    {
        //Generate a random number between 0 and the total number of "level blocks" from the list "allTheLevelBlocks" 
        int randomIdx = Random.Range(0, allTheLevelBlocks.Count);

        //Create an object of type "Level Block"
        LevelBlock block;

        //Variable that stores the position of the block
        Vector3 spawnPosition = Vector3.zero;

        //If there is still no block in the game first call
        if (currentLevelBlocks.Count == 0)
        {
            //Make an instance of block number 0 from the list "allTheLevelBlocks"
            block = Instantiate(allTheLevelBlocks[0]);

            //In the position of the "Level Start" that is saved in the variable "levelStartPosition"
            spawnPosition = levelStartPosition.position;
        }
        else
        {
            block = Instantiate(allTheLevelBlocks[randomIdx]);

            //It is positioned at the "EndPoint" position of the previous block 
            spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].endPoint.position;
        }

        //All the blocks become children of the "LevelManager"
        block.transform.SetParent(this.transform, false);

        //The correction serves to bring the new block that is created, to the final position "EndPosition" of the previous block
        Vector3 correction = new Vector3(spawnPosition.x - block.startPoint.position.x, spawnPosition.y - block.startPoint.position.y, 0);

        //The current block will be instantiated to the position obtained or calculated in the correction
        block.transform.position = correction;

        //Add the block to the "currentLevelBlocks" list
        currentLevelBlocks.Add(block);
    }

    //Method in charge of eliminating the blocks of the game
    public void RemoveLevelBlock()
    {
        //Save the block to be destroyed
        LevelBlock oldBlock = currentLevelBlocks[0];

        //We delete from the current list the block that we have previously saved "oldBlock"
        currentLevelBlocks.Remove(oldBlock);

        //We destroy the instance of that block
        Destroy(oldBlock.gameObject);
    }

    //Method in charge of eliminating all the blocks of the game
    public void RemoveAllLevelBlocks()
    {
        while(currentLevelBlocks.Count > 0)
        {
            RemoveLevelBlock();
        }
    }
}
