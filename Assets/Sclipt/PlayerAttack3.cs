using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack3 : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] Enemy2 enemy2;
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
        if(other.gameObject.CompareTag("Enemy2"))
        {
                enemy2.EnemyDamage(20);
                playerManager.Mp(20);
        }
    }
}
