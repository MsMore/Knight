using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public float backgroundSize;  // The size of each background image
    public float parallaxSpeed;   // The speed at which the background moves

    private Transform cameraTransform;  // Reference to the camera's transform
    private Transform[] layers;         // An array of all the background layers
    private float viewZone = 20;       // The distance before the camera when a new layer needs to be created
    private int leftIndex;              // The index of the leftmost background layer
    private int rightIndex;             // The index of the rightmost background layer

    void Start()
    {
        // Get the camera's transform and store it in a variable
        cameraTransform = Camera.main.transform;

        // Get all the background layers and store them in an array
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }

        // Set the initial left and right indices
        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }

    void Update()
    {
        // If the camera has moved far enough to the right, create a new layer on the left
        if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
        {
            ScrollRight();
        }

        // If the camera has moved far enough to the left, create a new layer on the right
        if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
        {
            ScrollLeft();
        }
    }

    // Scroll the background to the right
    private void ScrollRight()
    {
        // Set the rightmost layer to be the leftmost layer
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }
    }

    // Scroll the background to the left
    private void ScrollLeft()
    {
        // Set the leftmost layer to be the rightmost layer
        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
        {
            rightIndex = layers.Length - 1;
        }
    }
}
