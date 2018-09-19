using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{

    int playerId;

    public Player() {
        playerId = Random.Range(0, 1000000000);
    }

    public int getPlayerId() {
        return playerId;
    }

}
