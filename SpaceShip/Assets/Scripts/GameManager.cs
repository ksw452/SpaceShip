using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    // ���������� act, move ���� �ϳ��� Ŭ������ ����� ��ӽ�Ű�� ����
    // �̱��� �ϳ��� ����ƽ �ν��Ͻ��� Ŭ������ ����� ��𼭵� ����ϰ� ����� ���� �� �̿�
    public static GameManager Instance;  // �̱��� ����  
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
            ObjectPool.Instance.get(EObjectFlag.asteroidRed); // ���� ���༺
            ObjectPool.Instance.get(EObjectFlag.asteroidBlue); // ���� ���༺
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
        // ���� ��ġ ���� ���� ���� Ÿ�̹�

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
