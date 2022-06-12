using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{

    enum EmissilePos
        {
                left,
                midLeft,
                mid,
                midRight,
                right

        }

    Rigidbody rigid;
    float shootspeed = 0.5f;
   float speed = 10000f;
    float rotSpeed = 15f;

    int lv = 1;
    public int hp = 100;

    [SerializeField]
    private Transform[] missilePos;



    IEnumerator shoot()
    {
        while (true)
        {

  
            yield return new WaitForSeconds(shootspeed);
            ShootPower();
       

          
        }
    }

    private void ShootPower()
    {



        switch (lv)
        {
            case 1:
                GameObject missile = ObjectPool.Instance.get(EObjectFlag.missile);
                missile.transform.position = missilePos[(int)EmissilePos.mid].position;
                break;
            case 2:
                 missile = ObjectPool.Instance.get(EObjectFlag.missile);
                missile.transform.position = missilePos[(int)EmissilePos.left].position;
                GameObject missile1 = ObjectPool.Instance.get(EObjectFlag.missile);
                missile1.transform.position = missilePos[(int)EmissilePos.right].position;
                break;
            case 3:
                missile = ObjectPool.Instance.get(EObjectFlag.missile);
                missile.transform.position = missilePos[(int)EmissilePos.left].position;
                missile1 = ObjectPool.Instance.get(EObjectFlag.missile);
                missile1.transform.position = missilePos[(int)EmissilePos.right].position;
               
                GameObject missile2 = ObjectPool.Instance.get(EObjectFlag.missile);
                missile2.transform.position = missilePos[(int)EmissilePos.mid].position;

                break;
            case 4:
                missile = ObjectPool.Instance.get(EObjectFlag.missile);
                missile.transform.position = missilePos[(int)EmissilePos.left].position;
                missile1 = ObjectPool.Instance.get(EObjectFlag.missile);
                missile1.transform.position = missilePos[(int)EmissilePos.right].position;
                missile2 = ObjectPool.Instance.get(EObjectFlag.missile);
                missile2.transform.position = missilePos[(int)EmissilePos.midLeft].position;
                GameObject missile3 = ObjectPool.Instance.get(EObjectFlag.missile);
                missile3.transform.position = missilePos[(int)EmissilePos.midRight].position;
                break;
            default:
                missile = ObjectPool.Instance.get(EObjectFlag.missile);
                missile.transform.position = missilePos[(int)EmissilePos.left].position;
                missile1 = ObjectPool.Instance.get(EObjectFlag.missile);
                missile1.transform.position = missilePos[(int)EmissilePos.right].position;
                missile2 = ObjectPool.Instance.get(EObjectFlag.missile);
                missile2.transform.position = missilePos[(int)EmissilePos.midLeft].position;
                missile3 = ObjectPool.Instance.get(EObjectFlag.missile);
                missile3.transform.position = missilePos[(int)EmissilePos.midRight].position;
                GameObject missile4 = ObjectPool.Instance.get(EObjectFlag.missile);
                missile4.transform.position = missilePos[(int)EmissilePos.mid].position;
                break;



        }
    
    
    
    
    
    }





    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("EnemyBullet"))
        {
            hp -= 20;

        }
        else if (other.CompareTag("Enemy"))
        {
            hp -= 50;

        }
        else if (other.CompareTag("PowerUp"))
        {

            lv++;
        
        }

        GameManager.Instance.HpSet(hp);

        if (hp <= 0)
        {
            GameManager.Instance.GameOver();
        }



    }
    // Start is called before the first frame update
    void Awake()
    {
        rigid = this.GetComponent<Rigidbody>();

    }

     void Start()
        {

            StartCoroutine(shoot());

        }


    // Update is called once per frame
    void Update()
    {

        

        float h = Input.GetAxis("Horizontal");


        rigid.velocity =Vector3.right * h * Time.deltaTime * speed;

     

        this.transform.eulerAngles = -Vector3.forward *h*rotSpeed;




    }



    private void LateUpdate()
    {

        if (this.transform.position.x < -20)
        {

            this.transform.position = Vector3.right * -20;
        }
        else if (this.transform.position.x > 20)
        {

            this.transform.position = Vector3.right * 20;
        }


    }
}
