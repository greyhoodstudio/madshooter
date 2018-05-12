using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientManager : MonoBehaviour {

    public static string playerName;
    public static int playerId;

    public static PlayerInfo updatePlayerInfo;
    public static Hashtable playerList;
    public GameObject myPlayer;

    private float axisX;
    private float axisY;

    private void Start()
    {
        playerList = new Hashtable();
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
