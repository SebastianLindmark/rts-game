using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{

    private int playerId;
    public int availableFunds = 10000;

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
        BaseObject item = obj as BaseObject;

        if (item == null)
        {
            return false;
        }

        return getPlayerId() == item.GetPlayer().getPlayerId();
    }

}
