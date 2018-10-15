using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static int playerCount = 0;
    
    public static List<Player> playerList = new List<Player>();

    public static Player humanPlayer;

    public static Player CreatePlayer()
    {
        Player player = new Player();
        playerList.Add(player);
        return player;
    }

    public static Player CreateHumanPlayer()
    {
        Player player = CreatePlayer();
        humanPlayer = player;
        return player;
    }

    public static List<Player> GetEnemyPlayers(Player player) {
        return playerList.FindAll(p => !player.Equals(p));

    }

}
