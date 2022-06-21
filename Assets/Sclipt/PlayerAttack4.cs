using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack4 : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] Enemy3 enemy3;
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
        if (other.gameObject.CompareTag("Enemy3"))
        {
                enemy3.EnemyDamage(20);
                playerManager.Mp(20);
        }
    }
}
