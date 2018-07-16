using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offset : MonoBehaviour
{

    // Public variables are initialized in the inspector; private variables are initialized at Start

    // Marker Components
    public GameObject Marker;
    Transform MarkerTransform;
    CustomTrackableEventHandler Tracker;

    // Image Components
    Renderer ImageRenderer;

    // Fine-tune adjustments for gameobject position
    public float xFix;
    public float yFix;
    public float zFix;

    private void Start()
    {
        // Marker Components
        MarkerTransform = Marker.transform;
        Tracker = Marker.GetComponent<CustomTrackableEventHandler>();

        // Image Components
        ImageRenderer = gameObject.GetComponent<Renderer>();
        // Always start with the mesh renderer disabled
        ImageRenderer.enabled = false;
    }

    private void Update()
    {
        // Only change renderer settings if tracker state has been changed (found to lost, or lost to found)
        if (Tracker.changedState)
        {
            // Tracker is found, enabled the mesh renderer
            if (Tracker.found)
            {
                ImageRenderer.enabled = true;
            }
            // Tracker is lost, disable the mesh renderer
            else
            {
                ImageRenderer.enabled = false;
            }
            Tracker.changedState = false;
        }
        

        // Target is found
        if (ImageRenderer.enabled)
        {
            // Calculate new position relative to Vuforia Camera (0, 0, 0) based on current position (which is offset)
            float x = CalculateOffsetX(MarkerTransform.position.x);
            float y = CalculateOffsetY(MarkerTransform.position.y);
            float z = CalculateOffsetZ(MarkerTransform.position.z);

            float angleX = MarkerTransform.eulerAngles.x;
            float angleY = MarkerTransform.eulerAngles.y;
            float angleZ = MarkerTransform.eulerAngles.z;

            // Set new position
            Vector3 pos = new Vector3(x, y, z);
            transform.localPosition = pos;

            // Set new rotation
            Vector3 rot = new Vector3(angleX, angleY, angleZ);
            transform.localEulerAngles = rot;
        }
    }

    private float CalculateOffsetX(float MarkerPosX)
    {
        return MarkerPosX + xFix;
    }

    private float CalculateOffsetY(float MarkerPosY)
    {
        return MarkerPosY + yFix;
    }

    private float CalculateOffsetZ(float MarkerPosZ)
    {
        // replace these
        float m = 0.552f;
        float b = 0.0147f;

        return m * MarkerPosZ + b + zFix;
    }
}
