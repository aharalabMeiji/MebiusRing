using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beads : MonoBehaviour
{
    public GameObject[] Vertices;
    public GameObject[] edges;
    public GameObject[] longedges;
    public GameObject[] RibbonsA;
    public GameObject[] RibbonsB;
    public Vector3[] Vcoord;
    public Vector3[] WVector;
    public int BeadsLength;
    // Start is called before the first frame update
    void Start()
    {
        BeadsLength = 30;

        Vertices = new GameObject[BeadsLength];

        edges = new GameObject[BeadsLength];
        longedges=new GameObject[BeadsLength];
        RibbonsA = new GameObject[BeadsLength];
        RibbonsB = new GameObject[BeadsLength];


        //球を配置する
        GameObject prefab;
        prefab = Resources.Load<GameObject>("Prefabs/Sphere");
        for(int i = 0; i < BeadsLength; i++)
        {
            Vector3 vec= new Vector3(20 * Mathf.Cos(2f * Mathf.PI * i / BeadsLength), 1.5f, 20 * Mathf.Sin(2f * Mathf.PI * i / BeadsLength));
            Vertices[i] = Instantiate<GameObject>(prefab, vec, Quaternion.identity, transform);
            vertex vtx = Vertices[i].GetComponent<vertex>();
            //球の中心座標を決める
            vtx.Vcoord = vec;
            //帯の幅方向のベクトルを初期化する
            vtx.WVector = new Vector3(0f, 1f, 0f);

        }

        //コントロールを決める
        controll ctrl = FindObjectOfType<controll>();
        ctrl.cursor = Vertices[0];

        //エッジを配置する
        GameObject shortEdgePrefab = Resources.Load<GameObject>("Prefabs/edge");
        GameObject longEdgePrefab = Resources.Load<GameObject>("Prefabs/longedge");
        edge edge;
        for(int i = 0; i < BeadsLength - 1; i++)
        {
            edges[i] = Instantiate<GameObject>(shortEdgePrefab, transform);
            edge = edges[i].GetComponent<edge>();
            edge.Vertex0 = Vertices[i];
            edge.Vertex1 = Vertices[i + 1];
        }
        edges[BeadsLength-1] = Instantiate<GameObject>(shortEdgePrefab, transform);
        edge = edges[BeadsLength-1].GetComponent<edge>();
        edge.Vertex0 = Vertices[BeadsLength-1];
        edge.Vertex1 = Vertices[0];

        for(int i = 0; i < BeadsLength - 2; i++)
        {
            longedges[i] = Instantiate<GameObject>(longEdgePrefab, transform);
            edge = longedges[i].GetComponent<edge>();
            edge.Vertex0 = Vertices[i];
            edge.Vertex1 = Vertices[i + 2];
        }
        longedges[BeadsLength-2] = Instantiate<GameObject>(longEdgePrefab, transform);
        edge = longedges[BeadsLength-2].GetComponent<edge>();
        edge.Vertex0 = Vertices[BeadsLength-2];
        edge.Vertex1 = Vertices[0];
        longedges[BeadsLength - 1] = Instantiate<GameObject>(longEdgePrefab, transform);
        edge = longedges[BeadsLength - 1].GetComponent<edge>();
        edge.Vertex0 = Vertices[BeadsLength - 1];
        edge.Vertex1 = Vertices[1];

        //リボンを配置する
        GameObject ribbonPrefab = Resources.Load<GameObject>("Prefabs/ribbon");

        ribbon ribbon;
        for (int i = 0; i < BeadsLength - 1; i++)
        {
            RibbonsA[i] = Instantiate<GameObject>(ribbonPrefab, transform);
            ribbon  = RibbonsA[i].GetComponent<ribbon>();
            ribbon.Vvec0 = Vertices[i].GetComponent<vertex>().Vcoord;
            ribbon.Vvec1 = Vertices[i + 1].GetComponent<vertex>().Vcoord;
            ribbon.Wvec0 = Vertices[i].GetComponent<vertex>().WVector;
            ribbon.Wvec1 = Vertices[i+1].GetComponent<vertex>().WVector;
        }
        RibbonsA[BeadsLength-1] = Instantiate<GameObject>(ribbonPrefab, transform);
        ribbon = RibbonsA[BeadsLength-1].GetComponent<ribbon>();
        ribbon.Vvec0 = Vertices[BeadsLength-1].GetComponent<vertex>().Vcoord;
        ribbon.Vvec1 = Vertices[0].GetComponent<vertex>().Vcoord;
        ribbon.Wvec0 = Vertices[BeadsLength - 1].GetComponent<vertex>().WVector; 
        ribbon.Wvec1 = Vertices[0].GetComponent<vertex>().WVector;

        for (int i = 0; i < BeadsLength - 1; i++)
        {
            RibbonsB[i] = Instantiate<GameObject>(ribbonPrefab, transform);
            ribbon = RibbonsB[i].GetComponent<ribbon>();
            ribbon.Vvec0 = Vertices[i+1].GetComponent<vertex>().Vcoord;
            ribbon.Vvec1 = Vertices[i].GetComponent<vertex>().Vcoord;
            ribbon.Wvec0 = Vertices[i + 1].GetComponent<vertex>().WVector;
            ribbon.Wvec1 = Vertices[i].GetComponent<vertex>().WVector;
        }
        RibbonsB[BeadsLength - 1] = Instantiate<GameObject>(ribbonPrefab, transform);
        ribbon = RibbonsB[BeadsLength - 1].GetComponent<ribbon>();
        ribbon.Vvec0 = Vertices[0].GetComponent<vertex>().Vcoord;
        ribbon.Vvec1 = Vertices[BeadsLength-1].GetComponent<vertex>().Vcoord;
        ribbon.Wvec0 = Vertices[0].GetComponent<vertex>().WVector;
        ribbon.Wvec1 = Vertices[BeadsLength - 1].GetComponent<vertex>().WVector;

    }

    // Update is called once per frame
    void Update()
    {
        //軸方向を更新する
        vertex vtx;
        for(int i = 0; i < BeadsLength - 1; i++)
        {
            vtx = Vertices[i].GetComponent<vertex>();
            vtx.VVector = Vertices[i + 1].GetComponent<vertex>().Vcoord - Vertices[i].GetComponent<vertex>().Vcoord;
            vtx.NVector = Vector3.Cross(vtx.VVector,vtx.WVector);
            vtx.NVector.Normalize();
        }
        vtx = Vertices[BeadsLength - 1].GetComponent<vertex>();
        vtx.VVector = Vertices[0].GetComponent<vertex>().Vcoord - Vertices[BeadsLength - 1].GetComponent<vertex>().Vcoord;
        vtx.NVector = Vector3.Cross(vtx.VVector, vtx.WVector);
        vtx.NVector.Normalize();

        //隣り合ったWVectorをできるだけ同じ向きに
        for(int i = 0; i < BeadsLength ; i++)
        {
            int i0 = i;
            int i1 = i + 1;
            if (i1 == BeadsLength) i1 = 0;
            vertex vtx0 = Vertices[i0].GetComponent<vertex>();
            vertex vtx1 = Vertices[i1].GetComponent<vertex>();
            Vector3 new_wvec0 = 0.9f*vtx0.WVector+0.1f*vtx1.WVector;
            Vector3 new_wvec1 = 0.1f*vtx0.WVector+0.9f*vtx1.WVector;
            //WVectorを軸方向に直交するように
            Vector3 vvec0 = vtx0.VVector;
            Vector3 vvec1 = vtx1.VVector;
            new_wvec0 -= Vector3.Dot(vvec0, new_wvec0) / Vector3.Dot(vvec0, vvec0) * vvec0;
            new_wvec1 -= Vector3.Dot(vvec1, new_wvec1) / Vector3.Dot(vvec1, vvec1) * vvec1;
            new_wvec0.Normalize();
            new_wvec1.Normalize();
            vtx0.WVector = new_wvec0;
            vtx1.WVector = new_wvec1;

        }
        //リボン位置を更新する
        ribbon ribbon;
        for (int i = 0; i < BeadsLength - 1; i++)
        {
            
            ribbon = RibbonsA[i].GetComponent<ribbon>();
            ribbon.Vvec0 = Vertices[i].GetComponent<vertex>().Vcoord;
            ribbon.Vvec1 = Vertices[i + 1].GetComponent<vertex>().Vcoord;
            ribbon.Wvec0 = Vertices[i].GetComponent<vertex>().WVector;
            ribbon.Wvec1 = Vertices[i + 1].GetComponent<vertex>().WVector;
        }
        
        ribbon = RibbonsA[BeadsLength - 1].GetComponent<ribbon>();
        ribbon.Vvec0 = Vertices[BeadsLength - 1].GetComponent<vertex>().Vcoord;
        ribbon.Vvec1 = Vertices[0].GetComponent<vertex>().Vcoord;
        ribbon.Wvec0 = Vertices[BeadsLength - 1].GetComponent<vertex>().WVector;
        ribbon.Wvec1 = Vertices[0].GetComponent<vertex>().WVector;

        for (int i = 0; i < BeadsLength - 1; i++)
        {

            ribbon = RibbonsB[i].GetComponent<ribbon>();
            ribbon.Vvec0 = Vertices[i + 1].GetComponent<vertex>().Vcoord;
            ribbon.Vvec1 = Vertices[i].GetComponent<vertex>().Vcoord;
            ribbon.Wvec0 = Vertices[i + 1].GetComponent<vertex>().WVector;
            ribbon.Wvec1 = Vertices[i].GetComponent<vertex>().WVector;
        }
        
        ribbon = RibbonsB[BeadsLength - 1].GetComponent<ribbon>();
        ribbon.Vvec0 = Vertices[0].GetComponent<vertex>().Vcoord;
        ribbon.Vvec1 = Vertices[BeadsLength - 1].GetComponent<vertex>().Vcoord;
        ribbon.Wvec0 = Vertices[0].GetComponent<vertex>().WVector;
        ribbon.Wvec1 = Vertices[BeadsLength - 1].GetComponent<vertex>().WVector;
    }
}
