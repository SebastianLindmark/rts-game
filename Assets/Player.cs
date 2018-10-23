using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    private int playerId;

    private Color teamColor;

    public Player() {
        playerId = Random.Range(0, 1000000000);
    }


    public int getPlayerId() {
        return playerId;
    }

    public void setPlayerId(int id) {
        playerId = id;
    }

    public void SetTeamColor(Color color)
    {
        Debug.Log("Setting color " + color);
        teamColor = color;
    }

    public Color GetTeamColor() {
        Debug.Log("Returning color " + teamColor);
        return teamColor;
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
