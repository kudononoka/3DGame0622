using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] EnemyMove enemyMove;
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
    }

}
