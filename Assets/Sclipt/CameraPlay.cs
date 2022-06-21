using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlay : MonoBehaviour
{
    private GameObject _player;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("unitychan");
    }

    // Update is called once per frame
    void Update()
    {
        float x = _player.transform.position.x;
        float y = _player.transform.position.y;
        float z = _player.transform.position.z;
        transform.position = new Vector3(x, y + 4, z - 6);
    }
}
