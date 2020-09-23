using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks {
  string gameVersion = "1";

  private void Awake () {
    // this make sure PhotonNetwork.LoadLevel() on master and all clients in the same room sync their level automatically
    PhotonNetwork.AutomaticallySyncScene = true;
  }

  // Start is called before the first frame update
  void Start () {
    Connect ();
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

  #region MonoBehaviourPunCallbacks Callbacks

  public override void OnConnectedToMaster () {
    Debug.Log (" OnConnectedToMaster() was called by PUN");
    PhotonNetwork.JoinRandomRoom ();
  }

  public override void OnDisconnected (DisconnectCause cause) {
    Debug.LogWarningFormat (" OnDisconnected() was called by PUN with reason {0}", cause);
  }

  public override void OnJoinRandomFailed (short returnCode, string message) {
    Debug.Log ("No random room available, so we create one");

    PhotonNetwork.CreateRoom (null, new RoomOptions ());
  }

  public override void OnJoinedRoom () {
    Debug.Log ("Now this client is in a room.");
  }

  #endregion
}