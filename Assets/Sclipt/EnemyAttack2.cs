using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack2 : MonoBehaviour
{
    [SerializeField] PlayerManager _playerManager;
    [SerializeField] Enemy2 _enemy2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            if (_enemy2.enemyAttackInterval > 8)
            {
                _playerManager.Damage(70);
            }
            else if (_enemy2.enemyAttackInterval > 4)
            {
                _playerManager.Damage(30);
            }
            else if (_enemy2.enemyAttackInterval > 0)
            {
                
                _playerManager.Damage(30);
            }
        }
    }
}
