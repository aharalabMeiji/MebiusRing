using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ribbon : MonoBehaviour
{
    public Vector3 Vvec0, Vvec1;
    public Vector3 Wvec0, Wvec1;
    Mesh mesh;

    public Vector3[] vertices;
    public int[] trianangles;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        vertices = new Vector3[6];
        trianangles = new int[12];
    }

    void SetVertices()
    {
        vertices[0]= Vector3.zero;
        vertices[1]= Vvec1-Vvec0;
        vertices[2]= Wvec0;
        vertices[3]= Vvec1+Wvec1 - Vvec0;
        vertices[4]= -Wvec0;
        vertices[5]= Vvec1-Wvec1 - Vvec0;
    }

    void SetTriangles()
    {
        trianangles[0] = 0;
        trianangles[1] = 1;
        trianangles[2] = 2;

        trianangles[3] = 1;
        trianangles[4] = 3;
        trianangles[5] = 2;

        trianangles[6] = 0;
        trianangles[7] = 5;
        trianangles[8] = 1;

        trianangles[9] = 0;
        trianangles[10] = 4;
        trianangles[11] = 5;
    }

    void SetMesh()
    {
        mesh.vertices = vertices;
        mesh.triangles = trianangles;
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().sharedMesh=mesh;
    }



    // Update is called once per frame
    void Update()
    {
        transform.position = Vvec0;
        SetVertices();
        SetTriangles();
        SetMesh();
        
    }
}
