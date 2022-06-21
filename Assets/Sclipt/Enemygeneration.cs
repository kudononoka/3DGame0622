using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemygeneration : MonoBehaviour
{
    [SerializeField] List<GameObject> _enemyPrefabs;
    private float _time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if( _time > 100)
        {
            Vector3 _pos = new Vector3(Random.Range(-7, 13), 0, Random.Range(6,-12));
            GameObject _enemy = _enemyPrefabs[0];
            Instantiate(_enemy, _pos, _enemy.transform.rotation);
            _time = 0;
        }
    }
}
