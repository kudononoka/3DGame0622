using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//スライダーを常にカメラの向きと同じにする
public class HPRotate : MonoBehaviour
{
    private RectTransform _rotate;
    private Transform _camera;
    // Start is called before the first frame update
    void Start()
    {
        _rotate = GetComponent<RectTransform> ();
        _camera = GameObject.Find("MainCamera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _rotate.rotation = _camera.transform.rotation;
    }

    
}
