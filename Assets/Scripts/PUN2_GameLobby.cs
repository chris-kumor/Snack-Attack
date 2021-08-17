using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PUN2_GameLobby : MonoBehaviourPunCallbacks
{
    public Slider CharacterSlider, InputSlider;

    public GameObject[] PlayerPrefabs;

    public Transform[] spawnPoints;

    SinputSystems.InputDeviceSlot[] playerControls = new SinputSystems.InputDeviceSlot[] {SinputSystems.InputDeviceSlot.keyboardAndMouse, SinputSystems.InputDeviceSlot.gamepad1};
    //Player being controlled by user
    string playerName = "Player 1";
    //Help to distinguish users, "allows breaking changes"
    string gameVersion = "0.9";
    bool canJoinRoom;
    List<RoomInfo> createdRooms = new List<RoomInfo>();
    //Default+Placeholder RoomName
    string roomName = "Room 1";
    Vector2 roomListScroll = Vector2.zero;
    bool isJoining = false;


    // Start is called before the first frame update
    void Start(){
        PhotonNetwork.AutomaticallySyncScene = true;
        if(!PhotonNetwork.IsConnected){
            //Photon Rooms game version = Users game version
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = gameVersion;
            //Connects to master-server using ^ settings stored in PhotonServerSettings within asset file 
            PhotonNetwork.ConnectUsingSettings();
        }
        

    }

    void Update(){
            GameStats.playerPrefab = PlayerPrefabs[(int)CharacterSlider.value];
            GameStats.spawnPoint = spawnPoints[(int)CharacterSlider.value];
            GameStats.localPlayerSlot = playerControls[(int)InputSlider.value];
    }

    public override void OnDisconnected(DisconnectCause cause){
        Debug.Log("OnFailedToConnectToPhoton.StatusCode: " + cause.ToString() + " ServerAddress: " + PhotonNetwork.ServerAddress);
    }

    public override void OnConnectedToMaster(){
        Debug.Log("OnConnectedToMaster");
        //Next Step Join Lobby
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList){
        Debug.Log("Room list aquired.");
        //Updates contents of Room List displayed to user
        createdRooms = roomList;
    }
    void OnGUI(){
        GUI.Window(0, new Rect(Screen.width/2 - 400, Screen.height/2 - 400, 900, 400), LobbyWindow, "Lobby");
    }

    void LobbyWindow(int index){
        //Create GUI Column layout to MultiplayerMenu
        GUILayout.BeginHorizontal();
        GUILayout.Label("Status: "+PhotonNetwork.NetworkClientState);
        if(isJoining||!PhotonNetwork.IsConnected||PhotonNetwork.NetworkClientState!=ClientState.JoinedLobby)
            GUI.enabled = false;
        GUILayout.FlexibleSpace();
        //textField for assignning Room Name
        roomName = GUILayout.TextField(roomName, GUILayout.Width(250));
        if(GUILayout.Button("Create Room", GUILayout.Width(125))){
            if(roomName != ""){
                isJoining = true;
                RoomOptions roomOptions = new RoomOptions();
                roomOptions.IsOpen = true;
                roomOptions.IsVisible = true;
                roomOptions.MaxPlayers = (byte)2;
                PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
            }
        }
        GUILayout.EndHorizontal();
        //Begin to layout scrollView for available rooms
        roomListScroll = GUILayout.BeginScrollView(roomListScroll, true, true);
        if(createdRooms.Count == 0)
            GUILayout.Label("No Rooms atm.");
        else{
            for(int i = 0; i < createdRooms.Count; i++){
                GUILayout.BeginHorizontal("box");
                GUILayout.Label(createdRooms[i].Name, GUILayout.Width(400));
                GUILayout.Label(createdRooms[i].PlayerCount+"/"+createdRooms[i].MaxPlayers);
                GUILayout.FlexibleSpace();
                if(GUILayout.Button("Join Room"))
                {
                    isJoining = true;
                    PhotonNetwork.NickName=playerName;
                    if(GameStats.playerPrefab != null && GameStats.localPlayerSlot != null){
                        GameStats.bothPlayersKB = false;
                        GameStats.isOnline = true;
                        PhotonNetwork.JoinRoom(createdRooms[i].Name);
                    }
                    

                }
                GUILayout.EndHorizontal();
            }
        }
        GUILayout.EndScrollView();
        //Setplayer Name(temp feature) layout refresh button
        GUILayout.BeginHorizontal();
        //Label+Textfield for player name
        GUILayout.Label("Player Name: ", GUILayout.Width(85));
        playerName = GUILayout.TextField(playerName,GUILayout.Width(250));
        GUILayout.FlexibleSpace();
        //Once user has joined a room, Room Selection No longer needed to be seen
        GUI.enabled = (PhotonNetwork.NetworkClientState==ClientState.JoinedLobby||PhotonNetwork.NetworkClientState==ClientState.Disconnected) && !isJoining;
        if(GUILayout.Button("Refresh", GUILayout.Width(100)))
        {
            if(PhotonNetwork.IsConnected)
                PhotonNetwork.ConnectUsingSettings();
            else    
                PhotonNetwork.ConnectUsingSettings();
        }
        GUILayout.EndHorizontal();
        if(isJoining)
        {
            GUI.enabled = true;
            GUI.Label(new Rect(900/2-50, 400/2-10, 100, 20), "Joining room...");
        }
    }

    public override void OnCreateRoomFailed(short returncode, string message)
    {
        Debug.Log("OnCreateRoomFailed called. Can occur when room exists, Try another name.");
        isJoining = false;
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed called. Room doesnt exist, is full or closed.");
        isJoining = false;
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
        PhotonNetwork.NickName = playerName;
        GameStats.isOnline = true;
        PhotonNetwork.LoadLevel("GameLevel");
    }



}
