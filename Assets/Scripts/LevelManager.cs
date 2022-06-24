using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Singleton
    public static LevelManager sharedInstanceLM;

    //List containing all "Level Blocks"
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();

    //List containing the current "Level Blocks" in the scene
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();

    //Variable that stores the position where the first block has to be created
    public Transform levelStartPosition;

    void Awake()
    {
        if (sharedInstanceLM == null)
        {
            sharedInstanceLM = this;
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

    }

    //Method in charge of eliminating the blocks of the game
    public void RemoveLevelBlock()
    {

    }

    //Method in charge of eliminating all the blocks of the game
    public void RemoveAllLevelBlocks()
    {

    }
}
