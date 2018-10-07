using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarData {

    public BaseObject Obj { get; }
    public Player Player { get; }
    public ToolbarClickListener ClickListener { get; }

    public ToolbarData(BaseObject o, Player player, ToolbarClickListener clickListener) {
        Obj = o;
        Player = player;
        ClickListener = clickListener;
    }
}
