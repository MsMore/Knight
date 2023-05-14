using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    // List of different platform prefabs that can be used
    public GameObject[] platformPrefabs;

    // The number of platforms to generate
    public int numPlatforms = 10;

    // The minimum and maximum y-position for the platforms
    public float minY = -5f;
    public float maxY = 5f;

    // The minimum and maximum gap between platforms
    public float minGap = 1f;
    public float maxGap = 5f;

    // The size of the platforms in the x-direction
    public float platformSize = 1f;

    // Use this for initialization
    void Start()
    {
        // Generate the platforms
        GeneratePlatforms();
    }

    void GeneratePlatforms()
    {
        // Set the starting position for the first platform
        float xPos = 0f;
        float yPos = 0f;

        // Generate the platforms
        for (int i = 0; i < numPlatforms; i++)
        {
            // Choose a random platform prefab from the list
            int platformIndex = Random.Range(0, platformPrefabs.Length);
            GameObject platform = platformPrefabs[platformIndex];

            // Set the position for the platform
            xPos += Random.Range(minGap, maxGap) + platformSize;
            yPos = Random.Range(minY, maxY);

            // Make sure the platform is not directly on top of the previous one
            if (Mathf.Abs(yPos - yPos) < 1f)
            {
                yPos = yPos + Mathf.Sign(yPos) * 1f;
            }

            // Instantiate the platform at the chosen position
            Instantiate(platform, new Vector3(xPos, yPos, 0), Quaternion.identity);
        }
    }
}
