using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static int playerCount = 0;
    
    public static List<Player> playerList = new List<Player>();

    public static Player CreatePlayer()
    {
        Player player = new Player();
        playerList.Add(player);
        return player;
    } 
    
    void Start () {
        
    }
    
    
    void Update () {
        
    }
}
