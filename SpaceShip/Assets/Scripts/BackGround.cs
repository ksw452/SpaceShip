using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{

    Material mat; // ��Ƽ���� ���� 

    float speed = -0.01f;
    // Start is called before the first frame update
    void Start()
    {

        mat = this.GetComponent<MeshRenderer>().material;// �޽� �������� ���� ��Ƽ���� ��������

        mat.SetTextureScale("_MainTex", Vector2.one * 2); // ��Ƽ���� �۰� ���̱�
    }

    // Update is called once per frame
    void Update()
    {


        mat.SetTextureOffset("_MainTex", new Vector2(0, Time.time * speed)); // ������ �ٲ� �����̱�
        
        
    }
}
