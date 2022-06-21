using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] BossController bossController;
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
            if(bossController.enemyAttackInterval > 7)
            {
                playerManager.Damage(80);
            }
            else if (bossController.enemyAttackInterval > 5)
            {
                playerManager.Damage(50);
            }
            else if (bossController.enemyAttackInterval > 3)
            {
                playerManager.Damage(30);
            }
            else if (bossController.enemyAttackInterval > 0)
            {

                playerManager.Damage(30);
            }
        }
    }
}
