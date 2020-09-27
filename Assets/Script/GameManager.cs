﻿using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum STAGE {
    stage1,
    stage2,
    stage3
}

public class GameManager : MonoBehaviour {
    static bool IsStarted = false;
    public static bool canAdvanced = false;
    private STAGE currentStage;
    private STAGE nextStages;
    private static int numberOfPlayers = 0;
    public void Qualified () {
        Debug.Log ("Qualified!");
    }

    public static void addPlayer () {
        numberOfPlayers += 1;
    }

    public static void removePlayer () {
        numberOfPlayers -= 1;
    }
    public static int getNumberOfPlayers () {
        return numberOfPlayers;
    }
    private void Start () {
        currentStage = STAGE.stage1;
        nextStages = currentStage + 1;
    }
    private void Update () {
        StartGame ();
        LoadNextLevel ();
    }

    private void LoadNextLevel () {
        if (numberOfPlayers == 0 && IsStarted == true) {
            if (PhotonNetwork.IsMasterClient) {
                canAdvanced = true;
                switch (this.currentStage) {
                    case STAGE.stage1:
                        currentStage = STAGE.stage2;
                        // PhotonNetwork.LoadLevel (2);
                        IsStarted = false;
                        break;
                    case STAGE.stage2:
                        currentStage = STAGE.stage3;
                        // PhotonNetwork.LoadLevel (2);
                        break;

                }
                nextStages = currentStage + 1;
            }
        }
    }

    public int NumberOfPlayersForEachStage (STAGE stage) {
        switch (stage) {
            case STAGE.stage1:
                return 1;
                break;
            case STAGE.stage2:
                return 1;
                break;
            case STAGE.stage3:
                return 1;
                break;
            default:
                return 1;
                break;
        }
    }

    private void StartGame () {
        if (numberOfPlayers == NumberOfPlayersForEachStage (this.currentStage)) {
            IsStarted = true;
            nextStages = currentStage + 1;
        }
    }

    void OnLeftRoom () {
        switch (currentStage) {
            case STAGE.stage2:
                SceneManager.LoadScene (2);
                break;
            default:
                SceneManager.LoadScene (2);
                break;
        }
    }

}