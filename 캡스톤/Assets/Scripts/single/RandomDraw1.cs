using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
public class RandomDraw1 : MonoBehaviour
{
    //������ ������Ʈ
    public static GameObject DrawWindow; // �̱� ȭ��
    public static GameObject TenDrawIMG; // 10ȸ �̱� ��� ȭ��
    public static GameObject TenIMG; // 10ȸ �̱� ���Ե�
    public static GameObject TenIMGG;

    public GameObject cart; //�߰������� īƮ
    public GameObject ck; //�¿����� üũ����Ʈ
    //���� �̹���
    public static Sprite[] Spr; //Sprite �̹��� ����
    public int[] arr;
    public string str1;
    public int i;
    // ����
    public int RandomInt; // ���� ����

    public int DrawCount; // �̱� ī��Ʈ
    public bool TimeSet; // Ÿ�̸� Ȱ��ȭ ����
    public float Timer; // Ÿ�̸�
    public int Slotindex; // ���� �ε���(0���� ����)
    public int ChangeInt; // RandomInt�� �����ϴ� ����
    public int on = 0;

    private KartPlayer1 sw;

    private speedup sp; //�߰����� �¿������� ���߿� �迭��
    public int inum = 0;
    void Start()
    {

        // ������Ʈ �ҷ�����
        //cart = GameObject.Find("Kart2").gameObject;
        ck = GameObject.Find("Checkpoints").transform.Find("Checkpoint 3").gameObject;
        DrawWindow = GameObject.Find("Canvas2").transform.Find("Drawwin").gameObject;
        TenDrawIMG = GameObject.Find("Canvas2").transform.Find("Drawwin").transform.Find("Ten").gameObject;
        // ����ȭ�� ����ϱ�
        //DrawShop.SetActive(true);
        //DrawWindow.SetActive(false);
        //TenDrawIMG.SetActive(false);

        // Sprite �̹��� �ҷ�����
        Spr = Resources.LoadAll<Sprite>("Image"); // Asset���� ���� Resources������ ����� �̹����� ���� �ȿ� �־�� �մϴ�.
        // Resource ���� �� ��δ�� �ۼ����־�� �մϴ�.

        DrawCount = 0; // �̱� ī��Ʈ�� 0�� �ʱ�ȭ
        Timer = 0;   // Ÿ���͵� �ʱ�ȭ
        arr = new int[5];
        i = 0;
        cart = GameObject.Find("Player");
        sw = cart.GetComponent<KartPlayer1>(); //�÷��̾� �Լ��� �ҷ�����
        sp = ck.GetComponent<speedup>(); //�߰����� �޾ƿ���
        TenIMGG = GameObject.Find("Canvas2").transform.Find("Drawwin").transform.Find("Ten").gameObject;

    }

    void Update()
    {


        // TenIMG = GameObject.Find("Canvas2").transform.Find("Drawwin").transform.Find("Ten").gameObject.transform.GetChild(Slotindex).gameObject;
        TenIMG=TenIMGG.transform.GetChild(Slotindex).gameObject;
         RandomInt = Random.Range(0, 4); // RandomInt�� ������ 0 ~ 9
        //Debug.Log(str1);
        if (TimeSet == true)
        {
            Timer += Time.deltaTime;
        }
        else //Timeset�� ����Ǿ��ٸ�
        {
            if (Slotindex > 0) // �����ε����� 0 �̻��̶��
            {
                Timer += Time.deltaTime; //Ÿ�̸� �۵�
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

        if (sp.onoff == 1)//üũ����Ʈ�� ������ �߰����� Ȱ��ȭ
        {
            if (inum == 0)//üũ����Ʈ ������ ��¾ȵǰ� ����
            {
                TenDraw();
                inum = 2;
            }
            else if (inum == 1)//üũ����Ʈ ������ ��¾ȵǰ� ����
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
            /*else if (i == 1 && Timer > 0.5f)
            {
                if (sw.accel.Equals("D") && arr[i].Equals(0))
                {
                    TenIMGG.transform.GetChild(i).gameObject.SetActive(false);
                    i++;
                }
                else if (sw.accel.Equals("L") && arr[i].Equals(1))
                {
                    TenIMGG.transform.GetChild(i).gameObject.SetActive(false);
                    i++;
                }
                else if (sw.accel.Equals("R") && arr[i].Equals(2))
                {
                    TenIMGG.transform.GetChild(i).gameObject.SetActive(false);
                    i++;
                }
                else if (sw.accel.Equals("U") && arr[i].Equals(3))
                {

                    TenIMGG.transform.GetChild(i).gameObject.SetActive(false);
                    i++;
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
            else if (i == 2 && Timer > 0.5f)
            {
                if (sw.accel.Equals("D") && arr[i].Equals(0))
                {

                    TenIMGG.transform.GetChild(i).gameObject.SetActive(false);
                    i++;
                }
                else if (sw.accel.Equals("L") && arr[i].Equals(1))
                {

                    TenIMGG.transform.GetChild(i).gameObject.SetActive(false);
                    i++;
                }
                else if (sw.accel.Equals("R") && arr[i].Equals(2))
                {

                    TenIMGG.transform.GetChild(i).gameObject.SetActive(false);
                    i++;
                }
                else if (sw.accel.Equals("U") && arr[i].Equals(3))
                {

                    TenIMGG.transform.GetChild(i).gameObject.SetActive(false);
                    i++;
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
            else if (i == 3 && Timer > 0.5f)
            {
                if (sw.accel.Equals("D") && arr[i].Equals(0))
                {

                    TenIMGG.transform.GetChild(i).gameObject.SetActive(false);
                    i++;
                    CloseDraw();
                    i = 0;
                }
                else if (sw.accel.Equals("L") && arr[i].Equals(1))
                {

                    TenIMGG.transform.GetChild(i).gameObject.SetActive(false);
                    i++;
                    CloseDraw();
                    i = 0;
                }
                else if (sw.accel.Equals("R") && arr[i].Equals(2))
                {

                    TenIMGG.transform.GetChild(i).gameObject.SetActive(false);
                    i++;
                    CloseDraw();
                    i = 0;
                }
                else if (sw.accel.Equals("U") && arr[i].Equals(3))
                {

                    TenIMGG.transform.GetChild(i).gameObject.SetActive(false);
                    i++;
                    CloseDraw();
                    i = 0;
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

            }*/
        }

    }
    public void TenDraw()// 10ȸ �̱� ����
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
            Invoke("TenDraw", 0f);// TenDraw �Լ��� �ٷ� �����Ѵ�.
        }
    }
    public void re_start()
    {
        TenIMGG.transform.GetChild(0).gameObject.SetActive(false);
        //TenIMGG.transform.GetChild(1).gameObject.SetActive(false);
        //TenIMGG.transform.GetChild(2).gameObject.SetActive(false);
        //TenIMGG.transform.GetChild(3).gameObject.SetActive(false);

        TenIMGG.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        //TenIMGG.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        //TenIMGG.gameObject.transform.GetChild(2).gameObject.SetActive(true);
        //TenIMGG.gameObject.transform.GetChild(3).gameObject.SetActive(true);
        arr = new int[5];
        DrawCount = 0;
        Slotindex = 0;


    }

    public void CloseDraw() // �׸��� �ݴ´�.
    {
        DrawWindow.SetActive(false);
        TenDrawIMG.SetActive(false);
        TimeSet = false;
        sp.onoff = 0;
        StartCoroutine("kart_speed");
        inum = 1;
        Slotindex = 0;
        DrawCount = 0;
        arr = new int[5];
        i = 0;
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
