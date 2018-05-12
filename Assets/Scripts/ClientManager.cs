﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientManager : MonoBehaviour {

    public static string playerName;
    public static int playerId;

    public static Dictionary<int, PlayerInfo> playerList;
    public GameObject myPlayer;

    // Input variables
    private float axisX;
    private float axisY;
    private Vector2 mousePosition;
    private bool LeftMouseClicked = false;
    private bool fireLock;


    private void Start()
    {
        playerList = new Dictionary<int, PlayerInfo>();
        SceneManager.sceneLoaded += OnGameStart;
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        // Get WASD input
        axisX = Input.GetAxis("Horizontal");
        axisY = Input.GetAxis("Vertical");
        if (axisX > 0) axisX = 1;
        else if (axisX < 0) axisX = -1;
        if (axisY > 0) axisY = 1;
        else if (axisY < 0) axisY = -1;

        // Get Mouse Position
        LeftMouseClicked = Input.GetMouseButton(0);
        if (LeftMouseClicked && !fireLock)
        {
            mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            JsonHandler.SendFireEvent(playerId, -1, myPlayer.transform.position, mousePosition);
            StartCoroutine("FireLock");
        }        
        
    }

    void OnGameStart(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
            StartCoroutine("SendInputData");
    }
    
    public static void StartGame (GameStartEvent gameStartEvent)
    {

        foreach(NewPlayerEvent npe in gameStartEvent.PlayerList){
            PlayerInfo temp = new PlayerInfo();
            temp.playerId = npe.PlayerId;
            temp.playerHealth = 100;
            playerList.Add(temp.playerId, temp);
        }

        NewPlayerEvent myPlayerInfo = gameStartEvent.PlayerList[0];
        playerName = myPlayerInfo.PlayerName;
        playerId = myPlayerInfo.PlayerId;
        SceneManager.LoadScene(1);
        return;
    }

    IEnumerator SendInputData()
    {
        while (NetworkManager.inputSocketReady)
        {
            JsonHandler.SendInputData(playerId, axisX, axisY);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public static void HandleFireEvent (FireEvent fireEvent)
    {
        PlayerInfo pInfo = null;
        int pid = fireEvent.PlayerId;
        if (playerList.ContainsKey(pid))
            pInfo = playerList[pid];
        if (pInfo != null)
        {
            Vector2 firePosition = new Vector2(fireEvent.FirePosX, fireEvent.FirePosY);
            Vector2 mousePosition = new Vector2(fireEvent.MousePosX, fireEvent.MousePosY);
            pInfo.GetComponent<PlayerActionController>().FireWeapon(fireEvent.BulletId, firePosition, mousePosition);
        }
        
        return;
    }

    IEnumerator FireLock()
    {
        fireLock = true;
        yield return new WaitForSeconds(0.3f);
        fireLock = false;
    }

    public static void UpdatePlayer (InputData input)
    {        
        return;
    }
    
    public static void UpdateBullet (InputData input)
    {
        return;
    }

	public static void CreateMyPlayer()
    {
        return;
    }
}