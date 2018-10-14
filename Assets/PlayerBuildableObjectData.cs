using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildableObjectData {

    public BaseObject Obj { get; }
    public Player Player { get; }
    public ToolbarClickListener creationObject { get; }

    public PlayerBuildableObjectData(Player player, BaseObject o, ToolbarClickListener responsibleForCreation) {
        Obj = o;
        Player = player;
        creationObject = responsibleForCreation;
    }
}
