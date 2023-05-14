using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBuilder : MonoBehaviour
{
    // The prefabs for the different types of blocks
    public GameObject smallBrickPrefab;
    public GameObject largeBrickPrefab;
    public GameObject roofPrefab;
    public GameObject doorPrefab;

    // The list of blocks that have been placed in the castle
    private List<GameObject> blocks = new List<GameObject>();

    // The current block type (either small brick, large brick, roof, or door)
    private int currentBlockType = 0;

    public bool buildModeOn; 

    private void Start()
    {
        buildModeOn = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && buildModeOn == false)
        {
            buildModeOn = true;
            Cursor.visible = true;
            Debug.Log("In build mode");
        }
        else if (Input.GetKeyDown(KeyCode.V) && buildModeOn == true)
        {
            buildModeOn = false;
            Cursor.visible = false;
        }

        if (buildModeOn)
        {
            // Check if the player has clicked the mouse button to place a block
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                PlaceBlock();
            }

            // Check if the player has used the scroll wheel to change the block type
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                ChangeBlockType();
            }
        }
    }

    void PlaceBlock()
    {
        // Get the position of the mouse pointer in world space
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(position);
        // Round the position to the nearest whole number to snap the block to a grid
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        position.z = 10;

        // Instantiate a new block prefab at the calculated position
        GameObject newBlock = null;
        switch (currentBlockType)
        {
            case 0:
                newBlock = Instantiate(smallBrickPrefab, position, Quaternion.identity);
                break;
            case 1:
                newBlock = Instantiate(largeBrickPrefab, position, Quaternion.identity);
                break;
            case 2:
                newBlock = Instantiate(roofPrefab, position, Quaternion.identity);
                break;
            case 3:
                newBlock = Instantiate(doorPrefab, position, Quaternion.identity);
                break;
        }

        // Add the new block to the list of blocks
        blocks.Add(newBlock);
        newBlock.transform.SetParent(GameObject.Find("Castle").transform);
        
    }

    void ChangeBlockType()
    {
        // Increment or decrement the current block type depending on the direction of the scroll wheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            currentBlockType = (currentBlockType + 1) % 4;
        }
        else
        {
            currentBlockType = (currentBlockType + 3) % 4;
        }
    }
}

