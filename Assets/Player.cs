using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{

    private int playerId;
  
    public Player() {
        playerId = Random.Range(0, 1000000000);
    }


    public int getPlayerId() {
        return playerId;
    }

    public void setPlayerId(int id) {
        playerId = id;
    }

    public override bool Equals(object obj)
    {
        Player player = obj as Player;

        if (player == null)
        {
            Debug.LogError("Comparing invalid structure");
            return false;
        }
        return getPlayerId() == player.getPlayerId();
    }

}
