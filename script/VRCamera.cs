using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCamera : MonoBehaviour
{
    // Camera component
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        // Get our camera component
        cam = this.gameObject.GetComponent<Camera>();

        // Enable the gyroscope if it's supported by the device
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
        else
        {
            Debug.LogWarning("This device does not support gyroscope.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the gyroscope is enabled, use its data to control the camera's rotation
        if (Input.gyro.enabled)
        {
            // Gyroscope attitude gives us the device's orientation in space
            Quaternion deviceRotation = Input.gyro.attitude;

            // Convert the rotation from right-handed to left-handed (Unity's coordinate system)
            Quaternion correctedRotation = new Quaternion(deviceRotation.x, deviceRotation.y, -deviceRotation.z, -deviceRotation.w);

            // Apply the rotation to the camera
            transform.localRotation = correctedRotation;
        }
    }
}
