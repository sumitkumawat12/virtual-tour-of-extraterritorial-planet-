using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject sphere1; // Reference to the first sphere
    public GameObject sphere2; // Reference to the second sphere
    public Camera mainCamera;  // Reference to the main camera
    public GameObject Player;
    public Transform sphere2Center; 

    void Start()
    {
        sphere1.SetActive(true);  // Start with Sphere 3 active
        sphere2.SetActive(false); // Sphere 4 is inactive initially
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    sphere1.SetActive(false);
                    sphere2.SetActive(true);
                    TeleportCamera();
                }
            }
        }
    }

    void TeleportCamera()
    {
         Player.transform.position = sphere2Center.position;
        Player.transform.rotation = sphere2Center.rotation;

        // Force Unity to update the camera's transform
        Player.transform.hasChanged = true;
    }
}
