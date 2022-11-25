using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum State1 { None, QuickMatching, QuickMatchDone, RacingStart, RacingDone }

public class GameManager1 : MonoBehaviour
{

	public static GameManager1 Inst { get; private set; }
	public static GameManager1 instance = null;
	public bool isConnect = false;
	public KartPlayer1 player;
	public int start = 0;


	void Start()
	{
		StartCoroutine(GameStart());
	}


	[Header("Panel")]

	[SerializeField] GameObject gamePanel;

	[Header("Lobby")]
	[SerializeField] Text quickMatchText;

	[Header("Game")]
	[SerializeField] Transform[] spawnPoints;
	[SerializeField] Text timeText;
	[SerializeField] Text countdownText;

	public KartAgent kart;
	public KartAgent kart2;
	//public KartPlayer player;
	public time_record1 raptime;

	public float baseSpeed;

	public Transform Target;

	public int Player_rap = 0;
	public int Ai_rap = 0;
	public int Ai_rap2 = 0;

	public TextMeshProUGUI countText;
	public TextMeshProUGUI cur_Rap;
	public TextMeshProUGUI cur_speed;
	public TextMeshProUGUI first;
	public TextMeshProUGUI second;
	public TextMeshProUGUI third;
	public GameObject startMenu;
	public GameObject carObj;
	public string str = "Player";

	public State1 state1;

	WaitForSeconds one = new WaitForSeconds(1);
	int racingStartTime;

	void Awake()
	{
		Inst = this;
	}

	public void ShowPanel(string panelName)
	{
		gamePanel.SetActive(false);
		if (panelName == gamePanel.name)
			gamePanel.SetActive(true);

	}



	IEnumerator GameStart()
	{
		GameObject.Find("Canvas").transform.Find("ui").gameObject.SetActive(true);
		ShowPanel("GamePanel");

		yield return one;
		countdownText.text = "3";
		yield return one;
		countdownText.text = "2";
		yield return one;
		countdownText.text = "1";
		yield return one;
		countdownText.text = "GO";
		state1 = State1.RacingStart;
		raptime.time_on();
		kart.ai_start();
		kart2.ai_start();
		yield return one;
		yield return one;
		countdownText.text = "";
		
	}

	public void playerGameEnd() //게임종료화면 등등 만들기
	{
		GameObject.Find("Canvas").transform.Find("ui").gameObject.SetActive(false);
		GameObject.Find("Canvas").transform.Find("Rank").gameObject.SetActive(true);
		int m = maxx(raptime.playercheck, raptime.aicheck, raptime.aicheck2);
		if (m == 1)
		{
			first.text = "Player";
			int n = max2(raptime.aicheck, raptime.aicheck2);
			if (n == 2)
			{
				second.text = "AI_1";
				third.text = "AI_2";
			}
			else
			{
				second.text = "AI_2";
				third.text = "AI_1";
			}
		}
		if (m == 2)
		{
			first.text = "AI_1";
			int n = max2(raptime.playercheck, raptime.aicheck2);
			if (n == 2)
			{
				second.text = "Player";
				third.text = "AI_2";
			}
			else
			{
				second.text = "AI_2";
				third.text = "Player";
			}
		}
		if (m == 3)
		{
			first.text = "AI_2";
			int n = max2(raptime.playercheck, raptime.aicheck);
			if (n == 2)
			{
				second.text = "Player";
				third.text = "AI_1";
			}
			else
			{
				second.text = "AI_1";
				third.text = "Player";
			}
		}
		raptime.time_off();
		kart.ai_end();
		kart2.ai_end();

	}
	public void aiGameEnd() //게임종료화면 등등 만들기
	{
		GameObject.Find("Canvas").transform.Find("ui").gameObject.SetActive(false);
		GameObject.Find("Canvas").transform.Find("Rank").gameObject.SetActive(true);
		first.text = "AI";
		second.text = "Player";
	}
	int maxx(int n, int n2, int n3)
	{
		if (n > n2 && n > n3)
		{
			return 1;
		}
		else if (n2 > n && n2 > n3)
		{
			return 2;
		}
		else
			return 3;
	}
	int max2(int n, int n2)
	{
		if (n > n2)
		{
			return 2;

		}
		else
			return 3;
	}
	public void Lobby()
	{
		SceneManager.LoadScene("Lobby");
		//Application.Quit();
	}

}