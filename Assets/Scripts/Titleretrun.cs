using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Titleretrun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Change();
    }

    void Change()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log(1);
            SceneManager.LoadScene("StartScene");
        }
    }
}
