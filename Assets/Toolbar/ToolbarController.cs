using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarController : MonoBehaviour {


    public Texture2D toolbarIcon;

    private int toolbarWidth = 1000;

    private Rect toolbarRect;

    private ToolbarData toolbarData;

    void Start () {
        toolbarRect = new Rect(Screen.width / 2 - toolbarWidth / 2, Screen.height - 80,toolbarWidth,80);
    }

    public void PopulateToolbar(List<BaseObject> data, Player player, ToolbarClickListener clickListener)
    {
        toolbarData = new ToolbarData(data, player, clickListener);
    }
    
    void Update () {
        
    }

    void OnGUI()
    {
        GUIStyle container = new GUIStyle();
        container.normal.background = toolbarIcon;
            
        GUI.DrawTexture(toolbarRect, toolbarIcon,ScaleMode.StretchToFill);

        GUI.BeginGroup(new Rect(toolbarRect.x, toolbarRect.y, toolbarRect.width, toolbarRect.height));

        if (toolbarData != null && toolbarData.Data.Count != 0)
        {
            float startX = 0;
            for (int k = 0; k < toolbarData.Data.Count; k++) {

                GUIStyle icon = new GUIStyle();
                icon.normal.textColor = Color.red;

                Rect subIcon = new Rect(startX + 5, 5, (toolbarWidth / 10) - 10, toolbarRect.height - 10);
                string name = toolbarData.Data[k].name;
                if (GUI.Button(subIcon, name))
                {
                    ToolItemClick(k);
                }
                

                //GUI.Button(subIcon, ,);


                startX += toolbarWidth / 10;
            }
        }

        GUI.EndGroup();
    }

    void ToolItemClick(int index) {
        BaseObject clickedObject = toolbarData.Data[index];
        toolbarData.ClickListener.OnToolBarClick(clickedObject);
    }
    
}
