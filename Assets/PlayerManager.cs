using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    private static int maxPlayers = 4;

    public static int playerCount = 0;
    
    public static List<Player> playerList = new List<Player>();

    public static Player humanPlayer;

    private static List<Color> availableTeamColors = new List<Color>() {new Color(1,0.71f,0.71f,1), new Color(0.714f, 0.786f, 0.99f, 1), new Color(0.767f, 0.99f, 0.71f, 1), new Color(0.94f, 0.71f, 0.99f, 1) };

    public static Player CreatePlayer()
    {

        if (playerCount < maxPlayers)
        {
            Player player = new Player();
            Color color = GetColor();
            player.SetTeamColor(color);
            playerList.Add(player);
            playerCount++;
            return player;
        }
        else {
            Debug.LogError("Trying to add more players than maxPlayers defined");
            return null;
        }
        
        
    }

    private static Color GetColor() {

        int index = UnityEngine.Random.Range(0, availableTeamColors.Count);
        Color color = availableTeamColors[index];
        availableTeamColors.RemoveAt(index);
        return color;
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
