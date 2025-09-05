using UnityEngine;

public class GazeTranst : MonoBehaviour
{
    private float gazeTimer = 0f;
    public float gazeTime = 2f; // Time in seconds to trigger the scene change
    public GameObject sphere2;  // Reference to Sphere 2
    public GameObject sphere3;  // Reference to Sphere 3
    public Transform sphere3Center; // Reference to the center position of Sphere 3
    public Camera vrCamera;     // Reference to the VR camera
    public GameObject Player;
    public GameObject Panel3;
    public GameObject Panel2;

    void Start()
    {
        sphere2.SetActive(true);  // Start with Sphere 2 active
        sphere3.SetActive(false); // Sphere 3 is inactive initially
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
                // Change from Sphere 2 to Sphere 3 after the gaze time is reached
                sphere2.SetActive(false);
                sphere3.SetActive(true);
                Panel2.SetActive(false);
                Panel3.SetActive(true);
                // Move the player to the center of Sphere 3
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
        Player.transform.position = sphere3Center.position;
        Player.transform.rotation = sphere3Center.rotation;

        // Force Unity to update the camera's transform
        Player.transform.hasChanged = true;
    }
}
 
