using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript  {


    private Player player;

    void Start () {
        player = PlayerManager.CreatePlayer();
        
    }

    public Player GetPlayer()
    {
        return player;
    }
}
