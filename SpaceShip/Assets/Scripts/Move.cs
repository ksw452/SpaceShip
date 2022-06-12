using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
   
    protected Vector3 dir;
    protected float speed; // protected 자식만 가능 public 인스턴스로 만들면 모든 곳에서 사용 가능  자식에서 인스턴스로 만든다해서 protected 사용 불가능 Move move = new Move() move.dir 불가능 그냥 dir 가능 protected 로 만든 변수는 자식이라해도 인스턴스로 사용 불가능 그냥 사용해야함

    protected float removetime = 5f;
    protected EObjectFlag flag;
    protected WaitForSeconds sec;

    private void Awake()
    {
        sec = new WaitForSeconds(removetime);    
    }
    protected virtual IEnumerator Remove()
    {

        yield return sec;
        ObjectPool.Instance.Set(this.gameObject,flag);

    }


        protected virtual void OnEnable()
      {
            StartCoroutine(Remove());
        }

    protected virtual void Update()
    {
        this.transform.Translate(dir* speed * Time.deltaTime);
    }
}
