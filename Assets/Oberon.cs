using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oberon : MonoBehaviour {

    private Vector3 pos;

    void Start()
    {

        // MoveFloorオブジェクトの位置情報をposの中に代入する。
        pos = transform.position;
    }

    void Update()
    {

        // （ポイント）
        // Mathf.PingPong(float t, float length);
        // tの値を0からlengthの範囲内で行ったりきたりさせる。
        this.gameObject.transform.position = new Vector3(pos.x, pos.y + Mathf.PingPong(1, 0.2f), pos.z);
    }
}
