using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataEnvironment  {
    
    public class PlayerEnvironment
    {

        public PlayerEnvironment(Player player, GameResource gR, GameResource oR, PlayerBuildableObjects buildableObjects) {
            this.player = player;
            this.goldResource = gR;
            this.oilResource = oR;
            this.buildableObjects = buildableObjects;
        }
        private Player player;
        private GameResource goldResource;
        private GameResource oilResource;
        private PlayerBuildableObjects buildableObjects;

        public Player GetPlayer() { return player; }
        public GameResource GetGoldResource() { return goldResource; }
        public GameResource GetOilResource() { return oilResource; }
        public PlayerBuildableObjects GetBuildableObjects() { return buildableObjects; }
    }

    private static Dictionary<int, PlayerEnvironment> dataEnvironment = new Dictionary<int, PlayerEnvironment>();


    public static void Register(Player player, GameResource goldResource, GameResource oilResource, PlayerBuildableObjects buildableObjects)
    {
        if (!dataEnvironment.ContainsKey(player.getPlayerId()))
        {
            dataEnvironment[player.getPlayerId()] = new PlayerEnvironment(player, goldResource, oilResource, buildableObjects);
        }
    }


    public static PlayerEnvironment GetPlayerEnvironment(Player player)
    {
        return dataEnvironment[player.getPlayerId()];
    }



}
