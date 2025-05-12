using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class ARController : MonoBehaviour
{
    public GameObject MyObject;
    public ARRaycastManager RaycastManager;
    private GameObject spawnedObject;

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)        {
            
            List<ARRaycastHit> touches = new List<ARRaycastHit>();

            RaycastManager.Raycast(Input.GetTouch(0).position, touches, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

            if (touches.Count > 0 && spawnedObject == null)
            {
                //spawnedObject = GameObject.Instantiate(MyObject, touches[0].pose.position, touches[0].pose.rotation);
                Quaternion customRotation = Quaternion.Euler(-90f, 0f, 180f);

                // Offset position 0.5 units above the plane
                Vector3 offsetPosition = touches[0].pose.position + touches[0].pose.up * 0.5f;

                // Instantiate object
                spawnedObject = GameObject.Instantiate(MyObject, offsetPosition, customRotation);

                // Scale it 2x
                spawnedObject.transform.localScale *= 2f;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Home");
        }
    }
}