using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArduinoBluetoothAPI;
using System;
using System.Text;

public class mymanager : MonoBehaviour
{

	// Use this for initialization
	BluetoothHelper bluetoothHelper;
	string deviceName;

	public Text text;
	public GameObject sphere;
	int buffer = 7;
	public string received_message;

	void Start()
	{
		deviceName = "HC-06"; //bluetooth should be turned ON;
		try
		{
			bluetoothHelper = BluetoothHelper.GetInstance(deviceName);
			bluetoothHelper.OnConnected += OnConnected;
			bluetoothHelper.OnConnectionFailed += OnConnectionFailed;
			//bluetoothHelper.OnDataReceived = OnMessageReceived(bluetoothHelper); //read the data

			bluetoothHelper.setFixedLengthBasedStream(6);
			bluetoothHelper.setLengthBasedStream();

			LinkedList<BluetoothDevice> ds = bluetoothHelper.getPairedDevicesList();

			foreach (BluetoothDevice d in ds)
			{
				Debug.Log($"{d.DeviceName} {d.DeviceAddress}");
			}

		}
		catch (Exception ex)
		{
			sphere.GetComponent<Renderer>().material.color = Color.yellow;
			Debug.Log(ex.Message);
			text.text = ex.Message;
			//BlueToothNotEnabledException == bluetooth Not turned ON
			//BlueToothNotSupportedException == device doesn't support bluetooth
			//BlueToothNotReadyException == the device name you chose is not paired with your android or you are not connected to the bluetooth device;
			//								bluetoothHelper.Connect () returned false;
		}
	}

	IEnumerator blinkSphere()
	{
		sphere.GetComponent<Renderer>().material.color = Color.cyan;
		yield return new WaitForSeconds(0.5f);
		sphere.GetComponent<Renderer>().material.color = Color.green;
	}

	// Update is called once per frame
	void FixedUpdate()
	{


		//OnMessageReceived(bluetoothHelper);
		//Synchronous method to receive messages
		if (bluetoothHelper != null)
			if (bluetoothHelper.Available)
			{

				OnMessageReceived(bluetoothHelper);

				received_message = bluetoothHelper.Read();
				buffer = received_message.Length;

			}






	}

	//Asynchronous method to receive messages
	void OnMessageReceived(BluetoothHelper helper)
	{
		//StartCoroutine(blinkSphere());
		received_message = helper.Read();
		//Debug.Log(received_message);
		//text.text = received_message;

	}

	void OnConnected(BluetoothHelper helper)
	{
		sphere.GetComponent<Renderer>().material.color = Color.green;
		try
		{
			helper.StartListening();
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}

	}

	void OnConnectionFailed(BluetoothHelper helper)
	{
		sphere.GetComponent<Renderer>().material.color = Color.red;
		Debug.Log("Connection Failed");
	}


	//Call this function to emulate message receiving from bluetooth while debugging on your PC.
	void OnGUI()
	{
		if (bluetoothHelper != null)
			bluetoothHelper.DrawGUI();
		else
			return;

		if (!bluetoothHelper.isConnected())
			if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 10, Screen.height / 10, Screen.width / 5, Screen.height / 10), "Connect"))
			{
				if (bluetoothHelper.isDevicePaired())
					bluetoothHelper.Connect(); // tries to connect
				else
					sphere.GetComponent<Renderer>().material.color = Color.magenta;
			}

		if (bluetoothHelper.isConnected())
			if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 10, Screen.height - 2 * Screen.height / 10, Screen.width / 5, Screen.height / 10), "Disconnect"))
			{
				bluetoothHelper.Disconnect();
				sphere.GetComponent<Renderer>().material.color = Color.blue;
			}

		if (bluetoothHelper.isConnected())
			if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 10, Screen.height / 10, Screen.width / 5, Screen.height / 10), "Send text"))
			{
				bluetoothHelper.SendData(new Byte[] { 0, 0, 85, 0, 85 });
				// bluetoothHelper.SendData("This is a very long long long long text");
			}
	}


	void OnDestroy()
	{
		if (bluetoothHelper != null)
			bluetoothHelper.Disconnect();
	}
}
