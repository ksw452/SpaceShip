using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    // 상태패턴은 act, move 등을 하나의 클래스로 만들고 상속시키는 패턴
    // 싱글톤 하나의 스태틱 인스턴스로 클래스를 만들어 어디서든 사용하고 저장된 같은 값 이용
    public static GameManager Instance;  // 싱글톤 패턴  
    private bool isAsteroidPlay = true;
    [SerializeField]
    private GameObject boss;

    public Transform Player;
    [SerializeField]
    private GameObject gameOverText;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text Hp;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject bossText;


    public void HpSet(int hp)
    {

        Hp.text = hp.ToString();
        slider.value = hp;

    }
    public int AddScore
    {
        get {

            return myScore;
        }
        set {

            myScore += value;
            scoreText.text = myScore.ToString();
        }
    
    
    }



    int myScore = 0;
    public void GameOver()
    {
        gameOverText.SetActive(true);
    
    }


    IEnumerator CreaateAsteroid()
    {


        while (isAsteroidPlay)
        {
            ObjectPool.Instance.get(EObjectFlag.asteroidRed); // 생성 소행성
            ObjectPool.Instance.get(EObjectFlag.asteroidBlue); // 생성 소행성
            yield return new WaitForSeconds(1f);
        }
    
    
    }

    IEnumerator Boss()
    {

        for (int i = 0; i < 6; i++)
        {

            bossText.SetActive(!bossText.activeSelf);

            yield return new WaitForSeconds(0.5f);

        }

        yield return null;

        boss.SetActive(true);
    
    }

    IEnumerator Main()
    {
        // 생성 위치 생성 개수 생성 타이밍

        Vector3 startPos = Vector3.zero;
        startPos.x = 15;
        startPos.z = 85;

        Vector3 startRot = Vector3.up * 180;

        yield return new WaitForSeconds(5f);
        for (int i = 0; i < 5; i++)
        {
            ObjectPool.Instance.get(EObjectFlag.enemyY,startPos,startRot);
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(3f);
        startPos.x = 0;
        for (int i = 0; i < 5; i++)
        {
            ObjectPool.Instance.get(EObjectFlag.enemyY, startPos, startRot);
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(3f);
        startPos.x = -15;
        for (int i = 0; i < 5; i++)
        {
            ObjectPool.Instance.get(EObjectFlag.enemyY, startPos, startRot);
            yield return new WaitForSeconds(1f);
        }

        isAsteroidPlay = false;
        yield return new WaitForSeconds(7f);

        StartCoroutine(Boss());
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        StartCoroutine(CreaateAsteroid());
        StartCoroutine(Main());
    }

    // Update is called once per frame
  
}
