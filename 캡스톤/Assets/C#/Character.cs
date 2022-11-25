using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{   //키보드입력
    Vector3 moveVec;

    public float speed;
    float hAxis;
    float vAxis;


    //애니메이션
    Animator anim;
    bool wDown;


    private void Awake() //초기화
    {
        anim = GetComponentInChildren<Animator>(); //애니메이션      
    }

    // Update is called once per frame
    void Update()
    {   //캐릭터조작 변수 선언

        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        //캐릭터조작 애니메이션 변수 선언

        wDown = Input.GetButton("Walk");

        //캐릭터 조작

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed  *Time.deltaTime;

        //캐릭터 조작 애니메이션

        anim.SetBool("IsRun", moveVec != Vector3.zero);//속도가 0이아니면
        anim.SetBool("IsWalk", wDown);

        transform.LookAt(transform.position + moveVec); //지정된 백터로 회전(바라보는방향)


    }
}
