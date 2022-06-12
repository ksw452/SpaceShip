using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
   
    protected Vector3 dir;
    protected float speed; // protected �ڽĸ� ���� public �ν��Ͻ��� ����� ��� ������ ��� ����  �ڽĿ��� �ν��Ͻ��� ������ؼ� protected ��� �Ұ��� Move move = new Move() move.dir �Ұ��� �׳� dir ���� protected �� ���� ������ �ڽ��̶��ص� �ν��Ͻ��� ��� �Ұ��� �׳� ����ؾ���

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
