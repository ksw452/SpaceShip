using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Move
{
    // Start is called before the first frame update
    void Awake()
    {
        sec = new WaitForSeconds(removetime);
        dir = Vector3.back;
        speed = 50f;
        flag = EObjectFlag.powerUp;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            ObjectPool.Instance.Set(this.gameObject, flag);
            ObjectPool.Instance.get(EObjectFlag.PowerUpEffect).transform.position = this.transform.position;
        }


    }
}
