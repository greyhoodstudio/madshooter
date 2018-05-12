using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientManager : MonoBehaviour {

    public static string playerName; 
    public static int playerId;
    public GameObject myPlayer;

    public static Dictionary<int, PlayerInfo> playerList;
    

    //preload gameStartEvent
    public static GameStartEvent gameStartEvent; 


    // Input variables
    private float axisX;
    private float axisY;
    private Vector2 mousePosition;
    private bool LeftMouseClicked = false;
    private bool fireLock = false;


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
            JsonHandler.SendFireEvent(playerId, -1, myPlayer.transform.GetChild(0).transform.position, mousePosition);
            StartCoroutine("FireLock");
        }        
        
    }

    void OnGameStart(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            for (int i = 0; i<gameStartEvent.PlayerList.Count; i++)
            {
                //set player hashtable
                NewPlayerEvent npe = gameStartEvent.PlayerList[i];
                GameObject player = Instantiate(Resources.Load("Prefabs/Player")) as GameObject;
                playerList.Add(npe.PlayerId, player.GetComponent<PlayerInfo>());

                if (i == 0)
                {
                    playerId = npe.PlayerId; //myPlayerId
                    myPlayer = player; //my player object
                }
            }

            StartCoroutine("SendInputData");
        }
    }
    
    public static void StartGame (GameStartEvent _gameStartEvent)
    {
        gameStartEvent = _gameStartEvent;
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
        if (playerList.ContainsKey(input.player_id))
        {
            MovementController p = playerList[input.player_id].GetComponent<MovementController>();
            p.axisX = input.axis_x;
            p.axisY = input.axis_y;
        }
        return;
    }

    public static void HandleNewPlayerEvent(NewPlayerEvent _newPlayerEvent){

        GameObject player = Instantiate(Resources.Load("Prefabs/Player")) as GameObject;
        player.transform.position = new Vector3(30,30,1);
        playerList.Add(_newPlayerEvent.PlayerId, player.GetComponent<PlayerInfo>());
    }
}
