using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarData {

    public List<BaseObject> Data { get; }
    public Player Player { get; }
    public ToolbarClickListener ClickListener { get; }

    public ToolbarData(List<BaseObject> data, Player player, ToolbarClickListener clickListener) {
        Data = data;
        Player = player;
        ClickListener = clickListener;
    }
}
