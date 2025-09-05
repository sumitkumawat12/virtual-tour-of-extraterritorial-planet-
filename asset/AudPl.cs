using UnityEngine;

public class  AudPl : MonoBehaviour
{
    public Camera vrCamera;                // Reference to the VR camera
    public Transform targetObject;         // Object to "look at" to trigger audio
    public float lookDuration = 2f;        // Duration to look at the object before audio plays
    private AudioSource audioSource;       // Audio source on the target object
    private float lookTimer = 0f;          // Timer to count how long the camera has been looking at the object
    private bool isLookingAtObject = false;// Tracks if camera is currently looking at the object
    public GameObject Player;

    void Start()
    {
        // Get the AudioSource component from the target object
        audioSource = targetObject.GetComponent<AudioSource>();

        // Ensure audio does not play on awake
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }
    }

    void Update()
    {
        // Cast a ray from the camera in the forward direction
        RaycastHit hit;
        if (Physics.Raycast(vrCamera.transform.position, vrCamera.transform.forward, out hit))
        {
            // Check if the ray hits the target object
            if (hit.transform == targetObject)
            {
                // Start or continue the timer if looking at the object
                if (!isLookingAtObject)
                {
                    isLookingAtObject = true;
                    lookTimer = 0f; // Reset timer when first looking at object
                }

                lookTimer += Time.deltaTime;

                // Play audio if the camera has been looking at the object for the specified duration
                if (lookTimer >= lookDuration && !audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else
            {
                // Reset when no longer looking at the object
                ResetLook();
            }
        }
        else
        {
            // Reset when no object is hit by the ray
            ResetLook();
        }
    }

    // Resets the look timer and state if the camera is no longer looking at the target object
    private void ResetLook()
    {
        isLookingAtObject = false;
        lookTimer = 0f;

        // Optionally stop audio if the camera looks away
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
