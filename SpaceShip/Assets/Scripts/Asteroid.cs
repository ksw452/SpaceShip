using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Move //���༺
{

    public EObjectFlag myflag;
    Vector3 rot = Vector3.zero;
    Vector3 startPos= Vector3.zero;
   
    // Start is called before the first frame update

    private int score = 10; // ���༺ óġ �� ȹ�� ����

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
        if (other.CompareTag("Missile"))// ���༺ �̻��Ϸ� �ı� �� 
        {
            Die();
            GameManager.Instance.AddScore = score; // ���� �߰�


            if (Random.Range(0.0f, 1.0f) < 0.3) // 0.3 ���� ������ �Ŀ��� ����
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

        base.OnEnable(); // �θ� �Լ� ȣ��
        speed = Random.Range(20f, 30f); //���� �ӵ�
        rot.x = Random.Range(0f, 360f); // ȸ�� ���� ����
        rot.y = Random.Range(0f, 360f);
        rot.z = Random.Range(0f, 360f);

        startPos.x = Random.Range(-20f,20f); // ���� ��ġ ����
        startPos.z = 85f;
        this.transform.position = startPos; 

        Vector3 target = Vector3.right*Random.Range(-20f,20f); // ���� ��ġ ����

        dir = target - this.transform.position;
        Debug.Log(dir);
        dir = dir.normalized; // ���� �����ֱ�
       

    }



    // Update is called once per frame
    protected override void Update() //���༺������ �ٸ��� ������
    {
        this.transform.eulerAngles += rot*Time.deltaTime; // ���༺ ȸ��
        this.transform.position += dir * speed * Time.deltaTime; // ������ǥ ������
    }
}
