using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolboxController : MonoBehaviour {


    public Texture2D toolboxIcon;

    private int toolboxWidth = 1000;

    private Rect toolboxRect;

	void Start () {
        toolboxRect = new Rect(Screen.width / 2 - toolboxWidth / 2, Screen.height - 80,toolboxWidth,80);
	}
	
	void Update () {
		
	}

    void OnGUI()
    {
        GUIStyle container = new GUIStyle();
        container.normal.background = toolboxIcon;
            
        GUI.Box(toolboxRect, toolboxIcon);

        GUI.BeginGroup(new Rect(toolboxRect.x, toolboxRect.y, toolboxRect.width, toolboxRect.height));
        for (int i = 0; i < toolboxWidth; i += toolboxWidth / 10)
        {
            
            GUIStyle icon = new GUIStyle();
            icon.normal.textColor = Color.red;

            Rect subIcon = new Rect(i + 5, 5, (toolboxWidth / 10) - 10, toolboxRect.height - 10);
            GUI.Box(subIcon, "Blah");
        }
        GUI.EndGroup();


    }
}
