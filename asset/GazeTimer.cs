using UnityEngine;
using UnityEngine.UI; // Optional, if you want to use UI for feedback (like a progress bar)

public class GazeTimer : MonoBehaviour
{
    public float gazeTime = 4.0f; // Time required to look at the panel
    private float gazeCounter = 0;
    private bool isGazing = false;

    public GameObject sphere1;
    public GameObject sphere2;
    public Camera vrCamera;

    void Start()
    {
        // Make sure Sphere 2 is not visible initially
        sphere2.SetActive(false);
        sphere1.SetActive(true);
    }

    void Update()
    {
        // Check if the user is gazing at the panel
        if (isGazing)
        {
            gazeCounter += Time.deltaTime;

            if (gazeCounter >= gazeTime)
            {
                ChangeToSphere2(); // Change scene if gazed for enough time
                gazeCounter = 0;
                isGazing = false;
            }
        }
        else
        {
            gazeCounter = 0; // Reset the gaze counter when not looking at the panel
        }
    }

    // This method will be called when the player starts looking at the panel
    public void StartGaze()
    {
        isGazing = true;
    }

    // This method will be called when the player stops looking at the panel
    public void StopGaze()
    {
        isGazing = false;
    }

    private void ChangeToSphere2()
    {
        // Switch from Sphere 1 to Sphere 2
        sphere1.SetActive(false);
        sphere2.SetActive(true);

        // Move the camera to the center of Sphere 2
        vrCamera.transform.position = sphere2.transform.position;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera")) // Ensure the camera is tagged correctly
        {
            StartGaze();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera")) // Ensure the camera is tagged correctly
        {
            StopGaze();
        }
    }

}
