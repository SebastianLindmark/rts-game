using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {


    private Player player;

    void Start () {
        player = PlayerManager.CreatePlayer();
        
    }
    
    void Update () {
        
    }
}
