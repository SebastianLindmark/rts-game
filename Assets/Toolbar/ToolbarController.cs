using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarController : MonoBehaviour {


    public Texture2D toolbarIcon;

    private int toolbarWidth = 1000;

    private Rect toolbarRect;

    private ToolbarData toolbarData;

    //private Transform[] cells;
    private List<Transform> cells = new List<Transform>();

    void Start () {

        foreach (Transform t in gameObject.transform) {
            cells.Add(t);
        }
    }

    public void PopulateToolbar(List<BaseObject> data, Player player, ToolbarClickListener clickListener)
    {
        toolbarData = new ToolbarData(data, player, clickListener);
        List<BaseObject> objs = toolbarData.Data;

        for (int i = 0; i < cells.Count; i++)
        {
            
            if (objs.Count > i) {
                Text text = cells[i].GetComponentInChildren<Text>();
                text.text = objs[i].name;
                
                Image textBackground = cells[i].GetComponentsInChildren<Image>()[1];
                textBackground.color = new Color(0,0,0,0.153f);
            }
            else
            {
                cells[i].GetComponentInChildren<Text>().text = "";
                Image imageBox = cells[i].GetComponentInChildren<Image>();
                Image textBackground = imageBox.GetComponentsInChildren<Image>()[1]; //Gives the parent component at index 0
                textBackground.color = Color.clear;
            }
            
        }

    }
    
    void ToolItemClick(int index) {
        BaseObject clickedObject = toolbarData.Data[index];
        toolbarData.ClickListener.OnToolBarClick(clickedObject);
    }
    
}
