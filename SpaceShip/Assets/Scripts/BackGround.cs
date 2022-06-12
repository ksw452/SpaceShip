using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{

    Material mat; // 머티리얼 접근 

    float speed = -0.01f;
    // Start is called before the first frame update
    void Start()
    {

        mat = this.GetComponent<MeshRenderer>().material;// 메쉬 렌더러로 부터 머티리얼 가져오기

        mat.SetTextureScale("_MainTex", Vector2.one * 2); // 머티리얼 작게 보이기
    }

    // Update is called once per frame
    void Update()
    {


        mat.SetTextureOffset("_MainTex", new Vector2(0, Time.time * speed)); // 오프셋 바꿔 움직이기
        
        
    }
}
