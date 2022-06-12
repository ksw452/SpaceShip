using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Move //소행성
{

    public EObjectFlag myflag;
    Vector3 rot = Vector3.zero;
    Vector3 startPos= Vector3.zero;
   
    // Start is called before the first frame update

    private int score = 10; // 소행성 처치 시 획득 점수

    private void Awake()
    {
        speed = 25;
        removetime = 10f;
        sec = new WaitForSeconds(removetime);
        if (this.gameObject.name[0] == '1')
        {
            flag = EObjectFlag.asteroidBlue;
        }
        else {

            flag = EObjectFlag.asteroidRed;
        }

    }



    private void Die()
    {
      
        ObjectPool.Instance.Set(this.gameObject, myflag);

        GameObject effect = ObjectPool.Instance.get(EObjectFlag.EnemyEffect);

        effect.transform.position = this.transform.position;
        effect.transform.localScale = Vector3.one * 1;


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Missile"))// 소행성 미사일로 파괴 시 
        {
            Die();
            GameManager.Instance.AddScore = score; // 점수 추가


            if (Random.Range(0.0f, 1.0f) < 0.3) // 0.3 보다 작으면 파워업 생성
            {
                GameObject powerUp = ObjectPool.Instance.get(EObjectFlag.powerUp);
                powerUp.transform.position = this.transform.position;
            }
        }
        else if (other.CompareTag("Player"))
        {
            Die();
        }

    }


    protected override void OnEnable()
    {

        base.OnEnable(); // 부모 함수 호출
        speed = Random.Range(20f, 30f); //랜덤 속도
        rot.x = Random.Range(0f, 360f); // 회전 방향 랜덤
        rot.y = Random.Range(0f, 360f);
        rot.z = Random.Range(0f, 360f);

        startPos.x = Random.Range(-20f,20f); // 시작 위치 랜덤
        startPos.z = 85f;
        this.transform.position = startPos; 

        Vector3 target = Vector3.right*Random.Range(-20f,20f); // 도착 위치 랜덤

        dir = target - this.transform.position;
        Debug.Log(dir);
        dir = dir.normalized; // 방향 구해주기
       

    }



    // Update is called once per frame
    protected override void Update() //소행성에서는 다르게 움직임
    {
        this.transform.eulerAngles += rot*Time.deltaTime; // 소행성 회전
        this.transform.position += dir * speed * Time.deltaTime; // 절대좌표 움직임
    }
}
