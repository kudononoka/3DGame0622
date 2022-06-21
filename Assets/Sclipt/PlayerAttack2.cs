using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack2 : MonoBehaviour
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

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Boss"))
        {
            bossController.EnemyDamage(20);
            playerManager.Mp(20);
        }
    }
}
