using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.UI;
using PN = Photon.Pun.PhotonNetwork;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.SceneManagement;

public enum State { None, QuickMatching, QuickMatchDone, RacingStart, RacingDone }

public class GameManager : MonoBehaviourPunCallbacks
{
	
	public static GameManager Inst { get; private set; }
	public static GameManager instance = null;
	public bool isConnect = false;
	public PhotonView pv;
	public KartPlayer kp;


	void Awake()
	{
		Inst = this;

		ShowPanel("ConnectPanel");

		PN.SendRate = 30;
		PN.SerializationRate = 15;

		if (autoJoin)
			ConnectClick(null);

	}

	[Header("Debug")]
	[SerializeField] bool autoJoin;
	[SerializeField] byte autoMaxPlayer = 2;

	[Header("Panel")]
	[SerializeField] GameObject connectPanel;
	[SerializeField] GameObject lobbyPanel;
	[SerializeField] GameObject gamePanel;

	[Header("Lobby")]
	[SerializeField] Text quickMatchText;

	[Header("Game")]
	[SerializeField] Transform[] spawnPoints;
	[SerializeField] Text timeText;
	[SerializeField] Text timeText1st;
	[SerializeField] Text countdownText;

	public KartAgent kart;
	public KartAgent kart2;
	//public KartPlayer player;
	public static GameObject player;
	public time_record raptime;

	public float baseSpeed;

	public Transform Target;

	public int Player_rap = 0;
	public int Ai_rap = 0;
	public int Ai_rap2 = 0;

	public TextMeshProUGUI countText;
	public TextMeshProUGUI[] rap;
	public TextMeshProUGUI cur_speed;
	public TextMeshProUGUI first;
	public TextMeshProUGUI second;
	public GameObject startMenu;
	public string str = "Player";
	int nameck = 0;
	int racingStartTime;
	public State state;

	public int GetIndex
	{
		get
		{
			for (int i = 0; i < PN.PlayerList.Length; i++)
			{
				if (PN.PlayerList[i] == PN.LocalPlayer)
					return i;
			}
			return -1;
		}
	}
	WaitForSeconds one = new WaitForSeconds(1);


	public void ShowPanel(string panelName)
	{
		connectPanel.SetActive(false);
		lobbyPanel.SetActive(false);
		gamePanel.SetActive(false);

		if (panelName == connectPanel.name)
			connectPanel.SetActive(true);
		else if (panelName == lobbyPanel.name)
			lobbyPanel.SetActive(true);
		else if (panelName == gamePanel.name)
			gamePanel.SetActive(true);
	}


	public void ConnectClick(InputField nickInput)
	{
		PN.ConnectUsingSettings();

		string nick = nickInput == null ? $"Player{Random.Range(0, 100)}" : nickInput.text;
		PN.NickName = nick;
	}

	public override void OnConnectedToMaster() => PN.JoinLobby();

	public override void OnJoinedLobby()
	{
		ShowPanel("LobbyPanel");

		if (autoJoin)
			QuickMatchClick();
	}

	public void QuickMatchClick()
	{
		if (state == State.None)
		{
			state = State.QuickMatching;
			quickMatchText.gameObject.SetActive(true);
			PN.JoinRandomOrCreateRoom(null, autoMaxPlayer, MatchmakingMode.FillRoom, null, null,
				$"room{Random.Range(0, 10000)}", new RoomOptions { MaxPlayers = autoMaxPlayer });
		}
		else if (state == State.QuickMatching)
		{
			state = State.None;
			quickMatchText.gameObject.SetActive(false);
			PN.LeaveRoom();
		}
	}

	public override void OnJoinedRoom()
	{
		PlayerChanged();
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		PlayerChanged();
	}

	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
		PlayerChanged();
	}

	void PlayerChanged()
	{
		if (PN.CurrentRoom.PlayerCount == autoMaxPlayer) { }
		else if (PN.CurrentRoom.PlayerCount != PN.CurrentRoom.MaxPlayers)
			return;

		StartCoroutine(GameStartCo());
	}

	IEnumerator GameStartCo()
	{
		
		GameObject.Find("Canvas").transform.Find("ui").gameObject.SetActive(true);
		ShowPanel("GamePanel");
		init_();
		SpawnCar();
		Debug.Log(GetIndex);
		yield return one;
		countdownText.text = "5";
		yield return one;
		countdownText.text = "4";
		yield return one;
		countdownText.text = "3";
		yield return one;
		countdownText.text = "2";
		yield return one;
		countdownText.text = "1";
		yield return one;
		countdownText.text = "GO";
		state = State.RacingStart;
		racingStartTime = PN.ServerTimestamp;
		yield return one;
		yield return one;
		countdownText.text = "";
		
	}

	void SpawnCar()
	{
		GameObject carObj = PN.Instantiate("Player"+GetIndex, spawnPoints[GetIndex].position, spawnPoints[GetIndex].rotation);
	}


	void Update()
	{
		if (state == State.QuickMatching && PN.InRoom)
		{
			quickMatchText.text = $"{PN.CurrentRoom.PlayerCount} / {PN.CurrentRoom.MaxPlayers}";
		}
		//if(nameck==0)
		//Name();
		TimeUpdate();
	}

	

	public void playerGameEnd() //게임종료화면 등등 만들기
	{
		GameObject.Find("Canvas").transform.Find("ui").gameObject.SetActive(false);
		GameObject.Find("Canvas").transform.Find("Rank").gameObject.SetActive(true);
		first.text = "Player 1";
		second.text = "Player 2";
		timeText1st.text = timeText.text;
	}
	public void aiGameEnd() //게임종료화면 등등 만들기
	{
		GameObject.Find("Canvas").transform.Find("ui").gameObject.SetActive(false);
		GameObject.Find("Canvas").transform.Find("Rank").gameObject.SetActive(true);
		first.text = "Player 2";
		second.text = "Player 1";
		timeText1st.text = timeText.text;
	}
	/*void Name()
	{
		GameObject.Find("Player").gameObject.name = "Player 1";
		nameck = 1;
	}*/
	public void Lobby()
	{
		SceneManager.LoadScene("Lobby");
		nameck = 0;
		OnDisconnect();
		//Application.Quit();
	}
	public void OnDisconnect()
	{
		// 접속 종료 함수
		PhotonNetwork.Disconnect();
	}
    public void init_()
    {
		Player_rap = 0;
		Ai_rap = 0;
    }
	void TimeUpdate()
	{
		if (state != State.RacingStart)
			return;

		TimeSpan elapsedTime = TimeSpan.FromMilliseconds(PN.ServerTimestamp - racingStartTime);
		int milliseconds = (int)(elapsedTime.Milliseconds * 0.1f);
		timeText.text = $"{elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}:{milliseconds:D2}";
		

	}
}