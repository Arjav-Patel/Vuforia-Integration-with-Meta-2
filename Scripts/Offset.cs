using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offset : MonoBehaviour
{

    // Public variables are initialized in the inspector; private variables are initialized at Start

    // Marker Components
    public GameObject Marker;
    Transform MarkerTransform;

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

        // Image Components
        ImageRenderer = gameObject.GetComponent<Renderer>();
    }

    private void Update()
    {
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