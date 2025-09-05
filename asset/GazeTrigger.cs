using UnityEngine;

public class GazeTrigger : MonoBehaviour
{
    private float gazeTimer = 0f;
    public float gazeTime = 2f; // Time in seconds to trigger the scene change
    public GameObject sphere1;  // Reference to Sphere 1
    public GameObject sphere2;  // Reference to Sphere 2
    public GameObject sphere3;
    public GameObject sphere4;
    public GameObject sphere5;
    public GameObject sphere6;
    public GameObject sphere7;
    public Transform sphere2Center; // Reference to the center position of Sphere 2
    public Camera vrCamera;     // Reference to the VR camera
    public GameObject Player;
    public GameObject Panel;
    public GameObject Panel2;
    public GameObject Panel3;
    public GameObject Panel4;
    public GameObject Panel5;
    public GameObject Panel6;
    public GameObject Panel7;



    void Start()
    {
        sphere1.SetActive(true);  // Start with Sphere 1 active
        sphere2.SetActive(false);// Sphere 2 is inactive initially
        sphere3.SetActive(false);
        sphere4.SetActive(false);
        sphere5.SetActive(false);
        sphere6.SetActive(false);
        sphere7.SetActive(false);
         

        Panel2.SetActive(false);
        Panel3.SetActive(false);
        Panel4.SetActive(false);
        Panel5.SetActive(false);
        Panel6.SetActive(false);
        Panel7.SetActive(false);
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
                // Change from Sphere 1 to Sphere 2 after the gaze time is reached
                sphere1.SetActive(false);
                sphere2.SetActive(true);
                Panel.SetActive(false);
                Panel2.SetActive(true);


                // Move the camera to the center of Sphere 2
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
        Player.transform.position = sphere2Center.position;
        Player.transform.rotation = sphere2Center.rotation;

        // Force Unity to update the camera's transform
        Player.transform.hasChanged = true;
    }
}
