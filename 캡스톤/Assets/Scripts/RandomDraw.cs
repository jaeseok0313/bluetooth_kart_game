using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
public class RandomDraw : MonoBehaviour
{
    //선언할 오브젝트
    public static GameObject DrawWindow; // 뽑기 화면
    public static GameObject TenDrawIMG; // 10회 뽑기 출력 화면
    public static GameObject TenIMG; // 10회 뽑기 슬롯들
    public static GameObject TenIMGG;
    public GameManager manager;

    public GameObject cart; //추가조작할 카트
    public GameObject cart2; //추가조작할 카트
    public GameObject ck; //온오프할 체크포인트
    //숫자 이미지
    public static Sprite[] Spr; //Sprite 이미지 선언
    public int[] arr;
    public string str1;
    public int i,j;
    // 변수
    public int RandomInt; // 랜덤 변수

    public int DrawCount; // 뽑기 카운트
    public bool TimeSet; // 타이머 활성화 여부
    public float Timer; // 타이머
    public int Slotindex; // 슬롯 인덱스(0부터 시작)
    public int ChangeInt; // RandomInt를 저장하는 변수
    public int on = 0;

    private KartPlayer sw;
    private KartPlayer sw2;

    private speedup sp; //추가조작 온오프예정 나중에 배열로
    public int inum = 0;
    public int inum2 = 0;
    void Start()
    {

        // 오브젝트 불러오기
        
        ck = GameObject.Find("Checkpoints").transform.Find("Checkpoint 3").gameObject;
        DrawWindow = GameObject.Find("Canvas2").transform.Find("Drawwin").gameObject;
        TenDrawIMG = GameObject.Find("Canvas2").transform.Find("Drawwin").transform.Find("Ten").gameObject;
        // 시작화면 출력하기
        //DrawShop.SetActive(true);
        //DrawWindow.SetActive(false);
        //TenDrawIMG.SetActive(false);

        // Sprite 이미지 불러오기
        Spr = Resources.LoadAll<Sprite>("Image"); // Asset폴더 내에 Resources폴더를 만들어 이미지를 폴더 안에 넣어야 합니다.
        // Resource 폴더 내 경로대로 작성해주어야 합니다.

        DrawCount = 0; // 뽑기 카운트는 0로 초기화
        Timer = 0;   // 타이터도 초기화
        arr = new int[5];
        i = 0;
        j = 0;
        //cart = GameObject.FindWithTag("Player");
        
        sp = ck.GetComponent<speedup>(); //추가조작 받아오기
        TenIMGG = GameObject.Find("Canvas2").transform.Find("Drawwin").transform.Find("Ten").gameObject;

    }

    void Update()
    {
        if(manager.GetIndex == 0)
        {
            cart = GameObject.Find("Player0(Clone)").gameObject;
        }
        else if(manager.GetIndex==1)
        {
            cart = GameObject.Find("Player1(Clone)").gameObject;
        }
        sw = cart.GetComponent<KartPlayer>(); //플레이어 함수들 불러오기
        // TenIMG = GameObject.Find("Canvas2").transform.Find("Drawwin").transform.Find("Ten").gameObject.transform.GetChild(Slotindex).gameObject;
        TenIMG = TenIMGG.transform.GetChild(Slotindex).gameObject;
        RandomInt = Random.Range(0, 4); // RandomInt의 범위는 0 ~ 9
        //Debug.Log(str1);
        if (TimeSet == true)
        {
            Timer += Time.deltaTime;
        }
        else //Timeset이 종료되었다면
        {
            if (Slotindex > 0) // 슬롯인덱스가 0 이상이라면
            {
                Timer += Time.deltaTime; //타이머 작동
                if (Timer > 0.01f)
                {
                    for (; Slotindex > 0; Slotindex--)
                    {
                        TenIMG.gameObject.transform.GetComponent<Image>().sprite = null;
                        Debug.Log(Slotindex);
                        Timer = 0;
                        break;
                    }
                    Slotindex -= 1;
                }
            }
            TenIMG.gameObject.transform.GetComponent<Image>().sprite = null;
        }

        if (sp.onoff == 1&& manager.GetIndex==0)//체크포인트를 만나면 추가조작 활성화
        {
            if (inum == 0)//체크포인트 이전에 출력안되게 제한
            {
                TenDraw();
                inum = 2;
            }
            else if (inum == 1)//체크포인트 이전에 출력안되게 제한
            {
                TenDraw();
                re_start();
                inum = 2;
            }

            Timer += Time.deltaTime;
            if (i == 0 && Timer > 0.5f)
            {

                if (sw.accel.Equals("D") && arr[i].Equals(0))
                {

                    TenIMGG.transform.GetChild(i).gameObject.SetActive(false);
                    i++;
                    CloseDraw();
                }
                else if (sw.accel.Equals("L") && arr[i].Equals(1))
                {

                    TenIMGG.transform.GetChild(i).gameObject.SetActive(false);
                    i++;
                    CloseDraw();
                }
                else if (sw.accel.Equals("R") && arr[i].Equals(2))
                {

                    TenIMGG.transform.GetChild(i).gameObject.SetActive(false);
                    i++;
                    CloseDraw();
                }
                else if (sw.accel.Equals("U") && arr[i].Equals(3))
                {

                    TenIMGG.transform.GetChild(i).gameObject.SetActive(false);
                    i++;
                    CloseDraw();
                }
                else if (sw.accel.Equals("D") && !arr[i].Equals(0))
                {
                    i = 0;

                    TenDraw();
                    re_start();
                }
                else if (sw.accel.Equals("L") && !arr[i].Equals(1))
                {
                    i = 0;

                    TenDraw();
                    re_start();

                }
                else if (sw.accel.Equals("R") && !arr[i].Equals(2))
                {
                    i = 0;

                    TenDraw();
                    re_start();

                }
                else if (sw.accel.Equals("U") && !arr[i].Equals(3))
                {
                    i = 0;

                    TenDraw();
                    re_start();

                }
                Timer = 0;
            }

        }
        if (sp.onoff2 == 1 && manager.GetIndex == 1)//체크포인트를 만나면 추가조작 활성화
        {
            if (inum2 == 0)//체크포인트 이전에 출력안되게 제한
            {
                TenDraw();
                inum2 = 2;
            }
            else if (inum2 == 1)//체크포인트 이전에 출력안되게 제한
            {
                TenDraw();
                re_start();
                inum2 = 2;
            }

            Timer += Time.deltaTime;
            if (j == 0 && Timer > 0.5f)
            {

                if (sw.accel.Equals("D") && arr[j].Equals(0))
                {

                    TenIMGG.transform.GetChild(j).gameObject.SetActive(false);
                    j++;
                    CloseDraw();
                }
                else if (sw.accel.Equals("L") && arr[j].Equals(1))
                {

                    TenIMGG.transform.GetChild(j).gameObject.SetActive(false);
                    j++;
                    CloseDraw();
                }
                else if (sw.accel.Equals("R") && arr[j].Equals(2))
                {

                    TenIMGG.transform.GetChild(j).gameObject.SetActive(false);
                    j++;
                    CloseDraw();
                }
                else if (sw.accel.Equals("U") && arr[j].Equals(3))
                {

                    TenIMGG.transform.GetChild(j).gameObject.SetActive(false);
                    j++;
                    CloseDraw();
                }
                else if (sw.accel.Equals("D") && !arr[j].Equals(0))
                {
                    j = 0;

                    TenDraw();
                    re_start();
                }
                else if (sw.accel.Equals("L") && !arr[j].Equals(1))
                {
                    j = 0;

                    TenDraw();
                    re_start();

                }
                else if (sw.accel.Equals("R") && !arr[j].Equals(2))
                {
                    j = 0;

                    TenDraw();
                    re_start();

                }
                else if (sw.accel.Equals("U") && !arr[j].Equals(3))
                {
                    j = 0;

                    TenDraw();
                    re_start();

                }
                Timer = 0;
            }

        }
    }
    public void TenDraw()// 10회 뽑기 실행
    {
        TimeSet = true;
        //DrawShop.SetActive(false);
        DrawWindow.SetActive(true);
        TenDrawIMG.SetActive(true);

        if (DrawCount == 1)//
        {
            DrawCount = 0;
            //Invoke("CloseDraw", 3.0f);

        }
        else
        {
            if (Timer >= 0.1f)
            {

                ChangeInt = RandomInt;
                arr[DrawCount] = RandomInt;
                //Debug.Log(arr[i]);

                TenIMG.gameObject.transform.GetComponent<Image>().sprite = Spr[ChangeInt];
                Timer = 0;
                DrawCount += 1;

            }
            if (DrawCount < 1)
            {
                Slotindex = DrawCount;
            }
            //Debug.Log(arr[i]);
            Invoke("TenDraw", 0f);// TenDraw 함수를 바로 실행한다.
        }
    }
    public void re_start()
    {
        TenIMGG.transform.GetChild(0).gameObject.SetActive(false);

        TenIMGG.gameObject.transform.GetChild(0).gameObject.SetActive(true);

        arr = new int[5];
        DrawCount = 0;
        Slotindex = 0;


    }

    public void CloseDraw() // 그림을 닫는다.
    {
        DrawWindow.SetActive(false);
        TenDrawIMG.SetActive(false);
        TimeSet = false;
        sp.onoff = 0;
        StartCoroutine("kart_speed");
        //Debug.Log("뽑기 종료");
        inum = 1;
        Slotindex = 0;
        DrawCount = 0;
        arr = new int[5];
        i = 0;
        j = 0;
    }
    IEnumerator kart_speed()
    {
        sw.accelspeed = 60;
        yield return new WaitForSecondsRealtime(1);
        sw.accelspeed = 50;
        yield return new WaitForSecondsRealtime(1);
        sw.accelspeed = 40;
        yield return new WaitForSecondsRealtime(1);
        sw.accelspeed = 30;
    }
}

