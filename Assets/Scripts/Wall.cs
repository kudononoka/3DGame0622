using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//////////////////        ボスステージ前の壁　        /////////////////////
//////////////////  エリア内の敵３匹倒すと壁が消える  /////////////////////

public class Wall : MonoBehaviour
{
    [SerializeField] EnemyMove enemyMove;
    [SerializeField] Enemy2 enemy2;
    [SerializeField] Enemy3 enemy3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if(enemyMove._die && enemy2._die && enemy3._die )
        {
            Destroy(gameObject);
        }
    }
}
