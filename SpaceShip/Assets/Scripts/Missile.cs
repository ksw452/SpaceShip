using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Move
{




    private void Start()
    {
        speed = 50;
        dir = Vector3.forward;
        flag = EObjectFlag.missile;
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            ObjectPool.Instance.Set(this.gameObject, EObjectFlag.missile);
            ObjectPool.Instance.get(EObjectFlag.missileEffect).transform.position = this.transform.position;
        }


    }

    // Start is called before the first frame update
  
    

 
}
