using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DataCollection : MonoBehaviour {

    public float pos;
    public float increment;
    private string path = "Assets/Resources/data.txt";

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            WriteString();

            Debug.Log(pos);
        }
        if (Input.GetMouseButtonDown(1))
        {
            pos += increment;

            Debug.Log("increment");
            Debug.Log(pos);
        }
    }


    private void WriteString()
    {
        StreamWriter wr = new StreamWriter(path, true);
        wr.WriteLine(transform.position.z + " " + pos);
        wr.Close();

        Debug.Log("wrote");
    }
}
