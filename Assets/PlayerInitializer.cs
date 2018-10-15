using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour {

    private Player player;

    public bool isHumanPlayer;

    public int startGold;

    public int startOil;

    public List<BaseBuilding> objectsBuildableFromStart;

    void Awake () {
        if (isHumanPlayer)
        {
            player = PlayerManager.CreateHumanPlayer();
        }
        else {
            player = PlayerManager.CreatePlayer();
        }
        

        PlayerDataEnvironment.Register(player, new GameResource(startGold), new GameResource(startOil), new PlayerBuildableObjects(), new PlayerBuiltObjects());        
	}

    public Player GetPlayer() {
        return player;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
