using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class edge : MonoBehaviour
{
    public GameObject Vertex0, Vertex1;
    public float edgeLength = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int repeat = 0; repeat < 10; repeat++)
        {
            vertex vtx0 =Vertex0.GetComponent<vertex>();
            vertex vtx1 =Vertex1.GetComponent<vertex>();
            Vector3 vec0 = vtx0.Vcoord;
            Vector3 vec1 = vtx1.Vcoord;
            Vector3 vec01 = vec1 - vec0;
            float dist = vec01.magnitude;
            float diff = (dist - edgeLength) * 0.1f;
            Vector3 diffVec = diff * vec01.normalized;
            vec0 += diffVec;
            vec1 -= diffVec;
            vtx0.Vcoord = vec0;
            vtx1.Vcoord = vec1;
        }

    }
}
