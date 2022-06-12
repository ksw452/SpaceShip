using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    int hp = 2;
    float moveSpeed = 20f;
    float shootSpeed = 1f;

    int score = 50;
    // Start is called before the first frame update


    IEnumerator Timeout()
    {
        yield return new WaitForSeconds(8f);
        ObjectPool.Instance.Set(this.gameObject, EObjectFlag.enemyY);



    }

    public int HP
    {
        
        set {

            hp = value;
        
        }
    
    }



    IEnumerator shoot()
    {
        for(int i=0;i<3;i++)
        {
            yield return new WaitForSeconds(shootSpeed);
            GameObject missile = ObjectPool.Instance.get(EObjectFlag.enemyBullet);
            missile.transform.position = this.transform.position;
            missile.transform.rotation = this.transform.rotation;
            missile.transform.LookAt(GameManager.Instance.Player);

        }



    }
    private void Die()
    {
        this.transform.gameObject.SetActive(false);
        ObjectPool.Instance.Set(this.gameObject, EObjectFlag.enemyY);
        GameObject effect = ObjectPool.Instance.get(EObjectFlag.EnemyEffect);

        effect.transform.position = this.transform.position;
        effect.transform.localScale = Vector3.one * 3;


    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Missile"))
        {
        
            hp--;

            if (hp <= 0)
            {
                Die();
                GameManager.Instance.AddScore = score;
            }
        }
        else if (other.CompareTag("Player"))
        {
            Die();
        }

     
    }
    void OnEnable()
    {

        hp = 2;
        //Vector3 startPos = Vector3.zero;
        //startPos.x = Random.Range(-20f, 20f);
        //startPos.z = 85f;
        //this.transform.position = startPos;
        //this.transform.eulerAngles = Vector3.up * 180;
        StartCoroutine(shoot());
        StartCoroutine(Timeout());



    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }
}
