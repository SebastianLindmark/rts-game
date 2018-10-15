using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataEnvironment  {
    
    public class PlayerEnvironment
    {

        private Player player;
        private GameResource goldResource;
        private GameResource oilResource;
        private PlayerBuildableObjects buildableObjects;
        private PlayerBuiltObjects builtObjects;


        public PlayerEnvironment(Player player, GameResource gR, GameResource oR, PlayerBuildableObjects buildableObjects, PlayerBuiltObjects builtObjects) {
            this.player = player;
            this.goldResource = gR;
            this.oilResource = oR;
            this.buildableObjects = buildableObjects;
            this.builtObjects = builtObjects;
        }
       

        public Player GetPlayer() { return player; }
        public GameResource GetGoldResource() { return goldResource; }
        public GameResource GetOilResource() { return oilResource; }
        public PlayerBuildableObjects GetBuildableObjects() { return buildableObjects; }
        public PlayerBuiltObjects GetBuiltObjects() { return builtObjects;  }
    }

    private static Dictionary<int, PlayerEnvironment> dataEnvironment = new Dictionary<int, PlayerEnvironment>();


    public static void Register(Player player, GameResource goldResource, GameResource oilResource, PlayerBuildableObjects buildableObjects, PlayerBuiltObjects builtObjects)
    {
        if (!dataEnvironment.ContainsKey(player.getPlayerId()))
        {
            dataEnvironment[player.getPlayerId()] = new PlayerEnvironment(player, goldResource, oilResource, buildableObjects,builtObjects);
        }
    }


    public static PlayerEnvironment GetPlayerEnvironment(Player player)
    {
        if (dataEnvironment.ContainsKey(player.getPlayerId()))
        {
            return dataEnvironment[player.getPlayerId()];
        }
        else {
            return null;
        }
        
    }



}
