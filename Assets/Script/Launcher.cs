using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks {

    public GameObject playerModel;

    public GameObject[] SpawnPoints;
    string gameVersion = "1";

    private void Awake () {
        // this make sure PhotonNetwork.LoadLevel() on master and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Start is called before the first frame update
    void Start () {
        Connect ();
        // Init ();
    }

    // Update is called once per frame
    void Update () {

    }
    void Init () {
        if (playerModel == null) { } else {
            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            PhotonNetwork.Instantiate (this.playerModel.name, new Vector3 (-9.09f, 5.21f, 26.59f), Quaternion.identity, 0);
        }
    }
    public void Connect () {
        if (PhotonNetwork.IsConnected) {
            PhotonNetwork.JoinRandomRoom ();
        } else {
            PhotonNetwork.ConnectUsingSettings ();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    public override void OnConnectedToMaster () {
        // PhotonNetwork.JoinOrCreateRoom ("Test", new RoomOptions { IsOpen = true, MaxPlayers = 4 }, TypedLobby.Default);
        PhotonNetwork.JoinRandomRoom ();
    }

    public override void OnJoinedRoom () {
        Debug.Log ("Now this client is in a room.");
        Debug.Log ("Number of players:" + PhotonNetwork.LocalPlayer.ActorNumber);

        if (Player.LocalPlayerInstance == null) {
            PhotonNetwork.Instantiate (this.playerModel.name, SpawnPoints[PhotonNetwork.LocalPlayer.ActorNumber].transform.position, Quaternion.identity, 0);
        }

    }
    public override void OnJoinRandomFailed (short returnCode, string message) {
        PhotonNetwork.CreateRoom ("game", new RoomOptions ());
    }
}