using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vertex : MonoBehaviour
{
    public Vector3 Vcoord; //球の中心座標
    public Vector3 WVector; //帯の幅方向ベクトル
    public Vector3 VVector; //帯の軸方向ベクトル
    public Vector3 NVector; //帯の垂直方向ベクトル
    // Start is called before the first frame update
    void Start()
    {
        Vcoord = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vcoord;
    }
}
