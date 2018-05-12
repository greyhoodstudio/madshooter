using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientManager : MonoBehaviour {

    public static string playerName; 
    public static int playerId;

    public static Dictionary<int, PlayerInfo> playerList;
    public static GameStartEvent gameStartEvent; //preload gameStartEvent

    private float axisX;
    private float axisY;

    private void Start()
    {
        playerList = new Dictionary<int,PlayerInfo>();
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
        {
            foreach (NewPlayerEvent npe in gameStartEvent.PlayerList)
            { 
                //set player hashtable
                GameObject player = Instantiate(Resources.Load("Prefabs/Player")) as GameObject;
                playerId = npe.PlayerId; //myPlayerId
                playerList.Add(npe.PlayerId, player.GetComponent<PlayerInfo>());
            }

            StartCoroutine("SendInputData");
        }
    }
    
    public static void StartGame (GameStartEvent _gameStartEvent)
    {
        gameStartEvent =_gameStartEvent;
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
        if (playerList.ContainsKey(input.player_id))
        {
            MovementController p = playerList[input.player_id].GetComponent<MovementController>();
            p.axisX = input.axis_x;
            p.axisY = input.axis_y;
        }
        return;
    }
}
