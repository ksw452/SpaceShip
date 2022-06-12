using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int hp = 1000;
    public float moveSpeed = 10f;
    float shootSpeed = 0.1f;
    float angle = 45;
    float interval = 9;

    public List<GameObject> dieParticle = new List<GameObject>();
    public GameObject[] warning;
    public GameObject[] laser;


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Missile"))
        {

            hp--;
            if (hp==0)
            {
                StartCoroutine(dieParticleCoroutine());

            }
        
        }

    
    }
  
    

    IEnumerator dieParticleCoroutine()
    {
        for (int i = 0; i < dieParticle.Count; i++)
        {

            dieParticle[i].SetActive(true);
            Vector3 tempPos = Vector3.zero;
                
            tempPos.x = Random.Range(-5.0f,5.0f);
            tempPos.y = Random.Range(-5.0f, 5.0f);
            tempPos.z = Random.Range(-5.0f, 5.0f);

            dieParticle[i].transform.position = tempPos+ this.gameObject.transform.position;

            yield return new WaitForSeconds(0.05f);



        }
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);

    }



    IEnumerator StayClear()
    {
       shootSpeed = 0.01f;
       angle = 10;
       interval = 0.1f;




        for (int count = 0; count < 3; count++)
        {
            yield return new WaitForSeconds(2f);


            Vector3 playerPos = GameManager.Instance.Player.position;
            float startAngle = angle;
            while (true)
            {




                if (startAngle > 1)
                {

                    for (int i = -1; i < 2; i += 2)
                    {
                        GameObject missile = ObjectPool.Instance.get(EObjectFlag.enemyBullet);
                        missile.transform.position = this.transform.position;
                        missile.transform.LookAt(playerPos);
                        missile.transform.eulerAngles += Vector3.up * startAngle * i;
                    }
                    startAngle -= interval;
                    yield return new WaitForSeconds(shootSpeed);
                }
                else
                {
                    break;
                }
            }
        }

    }



    IEnumerator SphereShoot()
    {
        shootSpeed = 2f;
        angle = 180;
        interval = 2f;


        for (float i= 0;i<5;i++)
        {
            yield return new WaitForSeconds(shootSpeed);

            if (interval > 0)
            {

                for (float y = 180 - angle; y <= 180 + angle; y += interval)
                {
                    GameObject missile = ObjectPool.Instance.get(EObjectFlag.bossBullet);
                    missile.transform.position = this.transform.position;
                    missile.transform.eulerAngles = Vector3.up * y;

                }
            }
        }

    }

    IEnumerator OneShoot()
    {
        angle = 10f;
        shootSpeed = 0.01f;
      
        for (int count = 0; count < 200; count++)
        {
            yield return new WaitForSeconds(shootSpeed);

            for (int i = -1; i < 2; i++)
            {
           
                    GameObject missile = ObjectPool.Instance.get(EObjectFlag.enemyBullet);
                    missile.transform.position = this.transform.position;
                    missile.transform.LookAt(GameManager.Instance.Player);
                    missile.transform.eulerAngles += Vector3.up * angle * i;
                
            }

            //GameObject missile = ObjectPool.Instance.get(EObjectFlag.enemyBullet);
            //missile.transform.position = this.transform.position;
            //missile.transform.rotation = this.transform.rotation;
            //missile.transform.LookAt(GameManager.Instance.Player);
        }

    }

    IEnumerator BossLaser()
    {
        List<int> nums = new List<int>() { 0, 1, 2, 3, 4 };


        int[] randNum = new int[3];
        int count = Random.Range(0, nums.Count);

        randNum[0] = nums[count];
        nums.RemoveAt(count);
        count = Random.Range(0, nums.Count);
        randNum[1] = nums[count];
        nums.RemoveAt(count);
        count = Random.Range(0, nums.Count);
        randNum[2] = nums[count];



        for (int i = 0; i < 6; i++)
        {

            warning[randNum[0]].SetActive(!warning[randNum[0]].activeSelf);
            warning[randNum[1]].SetActive(!warning[randNum[1]].activeSelf);
            warning[randNum[2]].SetActive(!warning[randNum[2]].activeSelf);

            yield return new WaitForSeconds(0.15f);

        }

        yield return null;



        Vector3 tempPos;
        for (int i = 0; i < 3; i++)
        {
            tempPos = laser[i].transform.position;
            tempPos.x = warning[randNum[i]].transform.position.x;
            laser[i].transform.position = tempPos;
            laser[i].SetActive(true);
        }
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 3; i++)
        {
            laser[i].SetActive(false);
        }

    }

    IEnumerator AsteroidBomb()
    {
        Vector3 tempPos = Vector3.zero;
        Vector3 startRot = Vector3.up * 180;
        tempPos.z = 80;
        tempPos.x = -18;

        for (int i = 0; i < 10; i++)
        {
          
            GameObject red = ObjectPool.Instance.get(EObjectFlag.enemyY);
            red.GetComponent<Enemy>().HP = 5;
            red.transform.position = tempPos;
            red.transform.eulerAngles = startRot;
            tempPos.x += 4;

        }

        yield return new WaitForSeconds(2f);
    
    
    
    }

    IEnumerator BossAttackPattern()
    {
   

        while (true)
        {
            int ranNum;

            if (hp < 500)
            {
               ranNum = Random.Range(3, 5);

            }
            else {
                ranNum = Random.Range(0, 3);
            }
            switch (ranNum)
            {
                case 0:
                    yield return StartCoroutine(StayClear());
                    break;
                case 1:
                    yield return StartCoroutine(SphereShoot());
                    break;
                case 2:
                    yield return StartCoroutine(OneShoot());
                    break;
                case 3:
                    yield return StartCoroutine(BossLaser());
                    break;
                case 4:
                    StartCoroutine(AsteroidBomb());
                    break;
                default:
                    break;

            }

            yield return new WaitForSeconds(Random.Range(1.0f,2.0f));
        }

    }
    IEnumerator BossMovePattern()
    {
        Vector3 tempPos;
        while (true)
        {

            this.transform.position += Vector3.back * Time.deltaTime*moveSpeed;

            if (this.transform.position.z < 70)
            {
                tempPos = this.transform.position;
                tempPos.z = 70;
                this.transform.position = tempPos;
                break;
            }

            yield return null;
        }
        StartCoroutine(BossAttackPattern());

        while (true)
        {

            this.transform.position += Vector3.right * Time.deltaTime * moveSpeed;

            if (this.transform.position.x > 20)
            {
                tempPos = this.transform.position;
                tempPos.x = 20;
                this.transform.position = tempPos;
                moveSpeed *= -1;
           
            }
            else if (this.transform.position.x < -20)
            {
                tempPos = this.transform.position;
                tempPos.x = -20;
                this.transform.position = tempPos;
                moveSpeed *= -1;
            }

            yield return null;
          
        }

     
    }
    // Start is called before the first frame update
    void OnEnable()
    {

        StartCoroutine(BossMovePattern());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
