using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet :Move
{
    // Start is called before the first frame update
    void Awake()
    {
        speed = 50;
        dir = Vector3.forward;
        removetime = 0.5f;
        flag = EObjectFlag.bossBullet;
        sec = new WaitForSeconds(removetime);

    }


    protected override IEnumerator Remove()
    {

        yield return sec;
        ObjectPool.Instance.Set(this.gameObject, flag);
       GameObject bullet =  ObjectPool.Instance.get(EObjectFlag.enemyBullet);
        bullet.transform.position = this.transform.position;
        bullet.transform.LookAt(GameManager.Instance.Player);

   
    }


  
}
