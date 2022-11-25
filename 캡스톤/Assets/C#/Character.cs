using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{   //Ű�����Է�
    Vector3 moveVec;

    public float speed;
    float hAxis;
    float vAxis;


    //�ִϸ��̼�
    Animator anim;
    bool wDown;


    private void Awake() //�ʱ�ȭ
    {
        anim = GetComponentInChildren<Animator>(); //�ִϸ��̼�      
    }

    // Update is called once per frame
    void Update()
    {   //ĳ�������� ���� ����

        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        //ĳ�������� �ִϸ��̼� ���� ����

        wDown = Input.GetButton("Walk");

        //ĳ���� ����

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed  *Time.deltaTime;

        //ĳ���� ���� �ִϸ��̼�

        anim.SetBool("IsRun", moveVec != Vector3.zero);//�ӵ��� 0�̾ƴϸ�
        anim.SetBool("IsWalk", wDown);

        transform.LookAt(transform.position + moveVec); //������ ���ͷ� ȸ��(�ٶ󺸴¹���)


    }
}
