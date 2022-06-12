using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Move
{



   


    // Start is called before the first frame update
    private void Start()
    {
        
        flag = EObjectFlag.enemyBullet;
        speed = 40;
        dir = Vector3.forward;
       
        
    }
    
    
}
