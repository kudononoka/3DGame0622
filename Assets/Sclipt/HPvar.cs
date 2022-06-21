using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPvar : MonoBehaviour
{
    private RectTransform _rotate;
    private Transform _camera;
    private GameObject Slider;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        _rotate = GetComponent<RectTransform>();
        _camera = GameObject.Find("Camera").GetComponent<Transform>();
        Slider = GameObject.Find("HP");
        player = GameObject.Find("unitychan");
    }

    // Update is called once per frame
    void Update()
    {
        _rotate.rotation = _camera.transform.rotation;
        float x = player.transform.position.x;
        float y = player.transform.position.y;
        float z = player.transform.position.z;
        Slider.transform.position = new Vector3(x, y + 2, z);
    }
}
