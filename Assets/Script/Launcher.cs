using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks {

    //public GameObject playerModel;

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
    public void Connect () {
        if (PhotonNetwork.IsConnected) {
            PhotonNetwork.JoinRandomRoom ();
        } else {
            PhotonNetwork.ConnectUsingSettings ();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    public override void OnConnectedToMaster () {
        PhotonNetwork.JoinRandomRoom ();
    }

    public override void OnJoinedRoom () {
        Debug.Log ("Now this client is in a room.");
        Debug.Log ("Number of players:" + PhotonNetwork.LocalPlayer.ActorNumber);
        LoadGame ();
    }
    public override void OnJoinRandomFailed (short returnCode, string message) {
        PhotonNetwork.CreateRoom ("game", new RoomOptions ());
    }

    public override void OnPlayerLeftRoom (Photon.Realtime.Player otherPlayer) {
        if (PhotonNetwork.IsMasterClient) {
            LoadGame ();
        }
    }

    private void LoadGame () {
        if (Player.LocalPlayerInstance == null) {
            Vector3 spawns = SpawnPoints[PhotonNetwork.LocalPlayer.ActorNumber].transform.position;
            // Vector3 spawns = new Vector3 (-18.7f, 124.45f, 327.01f);
            switch (PassMat.index) {
                case 1:
                    PhotonNetwork.Instantiate ("BLUE", spawns, Quaternion.identity, 0);
                    break;
                case 2:
                    PhotonNetwork.Instantiate ("PURPLE", spawns, Quaternion.identity, 0);
                    break;
                case 3:
                    PhotonNetwork.Instantiate ("RED", spawns, Quaternion.identity, 0);
                    break;
                case 4:
                    PhotonNetwork.Instantiate ("GREEN", spawns, Quaternion.identity, 0);
                    break;
                default:
                    PhotonNetwork.Instantiate ("PURPLE", spawns, Quaternion.identity, 0);
                    break;
            }

        }
    }
}