using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//EnemyのUIをカメラの方向に向ける
public class LookAtCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //camera.mainで映っているカメラの情報をとる。.transformで位置情報を取得
        transform.LookAt(Camera.main.transform);
    }
}
