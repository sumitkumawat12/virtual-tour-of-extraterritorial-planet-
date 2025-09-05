using UnityEngine;

public class GazeRc : MonoBehaviour
{
    private float gazeTimer = 0f;
    public float gazeTime = 2f; // Time in seconds to trigger the scene change
    public GameObject sphere5;  // Reference to Sphere 3
    public GameObject sphere6;  // Reference to Sphere 4
    public Transform sphere6Center; // Reference to the center position of Sphere 4
    public Camera vrCamera;     // Reference to the VR camera
    public GameObject Player;
    public GameObject Panel6;
    public GameObject Panel5;
    void Start()
    {
        sphere5.SetActive(true);  // Start with Sphere 3 active
        sphere6.SetActive(false); // Sphere 4 is inactive initially
    }

    void Update()
    {
        // Cast a ray from the camera's center in the direction the camera is facing
        Ray ray = new Ray(vrCamera.transform.position, vrCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform == transform)
        {
            // If the ray hits the Panel, start counting gaze time
            gazeTimer += Time.deltaTime;
            if (gazeTimer >= gazeTime)
            {
                // Change from Sphere 3 to Sphere 4 after the gaze time is reached
                sphere5.SetActive(false);
                sphere6.SetActive(true);
                Panel5.SetActive(false);
                Panel6.SetActive(true);
                // Move the player to the center of Sphere 4
                MoveCameraToCenter();
            }
        }
        else
        {
            // Reset the timer if the user is not looking at the Panel
            gazeTimer = 0f;
        }
    }

    void MoveCameraToCenter()
    {
        // Ensure the camera is correctly moved to the new position and rotation
        Player.transform.position = sphere6Center.position;
        Player.transform.rotation = sphere6Center.rotation;

        // Force Unity to update the camera's transform
        Player.transform.hasChanged = true;
    }
}

