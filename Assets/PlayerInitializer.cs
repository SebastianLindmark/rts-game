using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour {

    private Player player;

	// Use this for initialization
	void Start () {
        player = PlayerManager.CreatePlayer();
	}

    public Player GetPlayer() {
        return player;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
