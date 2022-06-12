using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum EObjectFlag
{
    missile,
    asteroidBlue,
    asteroidRed,
    enemyY,
    enemyBullet,
    missileEffect,
    EnemyEffect,
    powerUp,
    PowerUpEffect,
    bossBullet
}

public class ObjectPool : MonoBehaviour
{

 

    public static ObjectPool Instance;
    public int[] initCount;
    public List<Queue<GameObject>> queList = new List<Queue<GameObject>>();

    public GameObject[] cpyObject;




    public int missileStartCount = 10;
    public int asteroidStartCount = 10;
    //public Missile missile; // 汗力且 固荤老
    //public Asteroid asteroid; // 家青己






    private void init(int count,GameObject gb, int flag)
    {

        for (int i = 0; i < count; i++)
        {

            GameObject tempGB = GameObject.Instantiate(gb, this.transform);
            tempGB.gameObject.SetActive(false);
            queList[flag].Enqueue(tempGB);
        }

    }

    public GameObject get(EObjectFlag flag)
    {

        int index = (int)flag;

        GameObject tempGB;
        if (queList[index].Count > 0)
        {

          tempGB = queList[index].Dequeue();

            tempGB.transform.SetParent(null);
            tempGB.SetActive(true);
        



        }
        else
        { 
            tempGB = GameObject.Instantiate(cpyObject[index], this.transform);
      
        }
        return tempGB;

    }

    public GameObject get(EObjectFlag flag,Vector3 pos, Vector3 rot)
    {
        GameObject tempGB;
        int index = (int)flag;


        if (queList[index].Count > 0)
        {

            tempGB = queList[index].Dequeue();

            tempGB.transform.SetParent(null);
            tempGB.SetActive(true);

        }
        else
        {
            tempGB = GameObject.Instantiate(cpyObject[index], this.transform);
          
         
        }
        tempGB.transform.position = pos;
        tempGB.transform.eulerAngles = rot;
        return tempGB;

    }


    //switch (flag)
    //{

    //    case EObjectFlag.missile:
    //        if (missiles.Count > 0)
    //        {
    //            Missile tempMissile = missiles.Dequeue();
    //            tempMissile.transform.SetParent(null);
    //            tempMissile.gameObject.SetActive(true);
    //            return tempMissile;
    //        }
    //        else
    //        {

    //            Missile tempMissile = GameObject.Instantiate(missile, this.transform);
    //            tempMissile.transform.SetParent(null);
    //            return tempMissile;
    //        }
    //        break;

    //    case EObjectFlag.asteroid:
    //        if (missiles.Count > 0)
    //        {
    //            Asteroid tempAsteroid = asteroids.Dequeue();
    //            tempAsteroid.transform.SetParent(null);
    //            tempAsteroid.gameObject.SetActive(true);
    //            return tempAsteroid;
    //        }
    //        else
    //        {

    //            Missile tempMissile = GameObject.Instantiate(missile, this.transform);
    //            tempMissile.transform.SetParent(null);
    //            return tempMissile;
    //        }
    //        break;
    //    default:
    //        break;

    //}




    //}

    public void Set(GameObject gb, EObjectFlag flag)
{
        int index = (int)flag;
    gb.gameObject.SetActive(false);
    gb.transform.SetParent(this.transform);
    queList[index].Enqueue(gb);

}






    void Awake()
    {
        Instance = this;

        for (int i = 0; i < cpyObject.Length; i++)
        {
            queList.Add(new Queue<GameObject>());

            init(initCount[i], cpyObject[i], i);
        }
      
    }

    private void Start()
    {
      
    }
    // Update is called once per frame
    void Update()
    {

    }
}
