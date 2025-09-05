using UnityEngine;

public class GazeChange : MonoBehaviour
{
    private float gazeTimer = 0f;
    public float gazeTime = 2f; // Time in seconds to trigger the scene change
    public GameObject sphere3;  // Reference to Sphere 3
    public GameObject sphere4;  // Reference to Sphere 4
    public Transform sphere4Center; // Reference to the center position of Sphere 4
    public Camera vrCamera;     // Reference to the VR camera
    public GameObject Player;
    public GameObject Panel4;
    public GameObject Panel3;

    void Start()
    {
        sphere3.SetActive(true);  // Start with Sphere 3 active
        sphere4.SetActive(false); // Sphere 4 is inactive initially
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
                sphere3.SetActive(false);
                sphere4.SetActive(true);
                Panel3.SetActive(false);
                Panel4.SetActive(true);
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
        Player.transform.position = sphere4Center.position;
        Player.transform.rotation = sphere4Center.rotation;

        // Force Unity to update the camera's transform
        Player.transform.hasChanged = true;
    }
}

