using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Meta;
using Meta.Interop;
using Meta.Plugin;

public class RGBTransform : MonoBehaviour {

    public MetaCoreInterop.MetaCoordinateFrame destination;
    public MetaCoreInterop.MetaCoordinateFrame source;

    public Matrix4x4 current_transform = Matrix4x4.identity;
    public bool continuous_update_ = false;

    private bool recieved_transform_ = false;

    public float x = 0f;
    public float y = 0f;
    public float z = 0f;

    // angleX is set to -13 because that is what worked for me
    public float angleX = 0f;
    public float angleY = 0f;
    public float angleZ = 0f;
    
    void Update()
    {

        if (!recieved_transform_ || continuous_update_)
        {
            // Use Meta API to find coordiations
            recieved_transform_ = SystemApi.GetTransform(destination, source, ref current_transform);

            // Meta API uses a right-hand rule, GetPosition() and GetRotation() change this to left-hand rule coordinates
            transform.localPosition = current_transform.GetPosition();
            transform.localRotation = current_transform.GetRotation();

            // Use this to fix position offset
            Vector3 pos = new Vector3(x, y, z);
            pos += transform.position;

            transform.position = pos;

            // Use this to fix rotation offset
            transform.Rotate(angleX, angleY, angleZ, Space.World);

        }

    }
}
