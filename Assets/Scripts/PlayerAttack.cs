using System.Collections;
using System.Collections.Generic;
using UnityEngine;


////////////////////   攻撃　プレイヤーのSP増減と敵へのダメージ   /////////////////////////


public class PlayerAttack : MonoBehaviour
{
    [SerializeField] EnemyMove enemyMove;
    [SerializeField] Enemy2 enemy2;
    [SerializeField] Enemy3 enemy3;
    [SerializeField] BossController bossController;
    [SerializeField] PlayerManager playerManager;

    
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

        if (other.gameObject.CompareTag("Enemy") )
        {
                enemyMove.EnemyDamage(20);
                playerManager.Sp(20);
        }
        if (other.gameObject.CompareTag("Boss"))
        {
            bossController.EnemyDamage(20);
            playerManager.Sp(20);
        }
        if (other.gameObject.CompareTag("Enemy2"))
        {
            enemy2.EnemyDamage(20);
            playerManager.Sp(20);
        }
        if (other.gameObject.CompareTag("Enemy3"))
        {
            enemy3.EnemyDamage(20);
            playerManager.Sp(20);
        }
    }

}
