using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STAGE {
    stage1,
    stage2,
    stage3
}

public class GameManager : MonoBehaviour {
    private STAGE currentStage;
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

    private void Update () {
        QualifiedForNextStage ();
    }

    private void QualifiedForNextStage () {
        switch (this.currentStage) {
            case STAGE.stage1:
                if (numberOfPlayers == NumberOfPlayersForEachStage (this.currentStage)) {
                    // TODO: play 1st scene
                    currentStage = STAGE.stage2;
                }
                break;
            case STAGE.stage2:
                if (numberOfPlayers == NumberOfPlayersForEachStage (this.currentStage)) {
                    // TODO: play 2nd scene
                    currentStage = STAGE.stage3;
                }
                break;
            case STAGE.stage3:
                if (numberOfPlayers == NumberOfPlayersForEachStage (this.currentStage)) {
                    // TODO: play 3rd scene
                }
                break;
        }
    }

    public int NumberOfPlayersForEachStage (STAGE stage) {
        switch (stage) {
            case STAGE.stage1:
                return 7;
                break;
            case STAGE.stage2:
                return 6;
                break;
            case STAGE.stage3:
                return 4;
                break;
            default:
                return 7;
                break;
        }
    }
}