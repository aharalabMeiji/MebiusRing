using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controll : MonoBehaviour
{
    public GameObject cursor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vertex vtx = cursor.GetComponent<vertex>();
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 position = vtx.Vcoord;
            position -= (0.1f * Vector3.forward);
            vtx.Vcoord = position;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 position = vtx.Vcoord;
            position += (0.1f * Vector3.forward);
            vtx.Vcoord = position;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 position = vtx.Vcoord;
            position += (0.1f * Vector3.left);
            vtx.Vcoord = position;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 position = vtx.Vcoord;
            position += (0.1f * Vector3.right);
            vtx.Vcoord = position;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 wvec = vtx.WVector;
            Vector3 nvec = vtx.NVector;
            wvec += (0.1f * nvec);
            wvec.Normalize();
            vtx.WVector = wvec;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 wvec = vtx.WVector;
            Vector3 nvec = vtx.NVector;
            wvec -= (0.1f * nvec);
            wvec.Normalize();
            vtx.WVector = wvec;
        }
    }
}
