using System.Collections;
using System.Collections.Generic;
using UnityEngine;


///////////////////////////// 攻撃  プレイヤーへのダメージ　/////////////////////////////////

public class EnemyAttack1 : MonoBehaviour
{
    [SerializeField] PlayerManager _playerManager;
    [SerializeField] EnemyMove _enemyMove;
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

            if (_enemyMove.enemyAttackInterval > 8)
            {
                _playerManager.Damage(70);
            }
            else if (_enemyMove.enemyAttackInterval > 4)
            {
                _playerManager.Damage(30);
            }
            else if (_enemyMove.enemyAttackInterval > 0)
            {
                
                _playerManager.Damage(30);
            }
        }
    }
}
