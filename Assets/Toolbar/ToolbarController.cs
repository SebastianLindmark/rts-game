using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarController : MonoBehaviour,ItemClick {


    public enum ToolbarState {
        BUILDING,UNIT
    }


    public Texture2D toolbarIcon;

    private int toolbarWidth = 1000;

    private Rect toolbarRect;

    private ToolbarData toolbarData;

    private List<ToolbarData> availableBuildings = new List<ToolbarData>();
    private List<ToolbarData> availableUnits = new List<ToolbarData>();

    private ToolbarState toolbarState;

    //private Transform[] cells;
    private List<Transform> cells = new List<Transform>();

    void Start () {

        foreach (Transform t in gameObject.transform) {
            t.GetComponent<ToolbarItemClickRegister>().AddClickListener(cells.Count,this);
            cells.Add(t);
        }
    }


    public void AddToolbarField(BaseUnit unit, Player player, ToolbarClickListener clickListener)
    {
        availableUnits.Add(new ToolbarData(unit, player, clickListener));
    }

    public void AddToolbarField(BaseBuilding building, Player player, ToolbarClickListener clickListener) {
        availableBuildings.Add(new ToolbarData(building, player, clickListener));
    }

    public void SetDisplayState(ToolbarState state) {
        if (toolbarState != state) {
            toolbarState = state;
            RedrawToolbar();
        }
    }


    private void RedrawToolbar()
    {
        List<ToolbarData> objs;
        if (toolbarState == ToolbarState.BUILDING)
        {
            objs = availableBuildings;
        }
        else
        {
            objs = availableUnits;
        }

        for (int i = 0; i < cells.Count; i++)
        {

            if (objs.Count > i)
            {
                Text text = cells[i].GetComponentInChildren<Text>();
                text.text = objs[i].Obj.name;

                Image textBackground = cells[i].GetComponentsInChildren<Image>()[1];
                textBackground.color = new Color(0, 0, 0, 0.153f);
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


   /* public void PopulateToolbar(List<BaseObject> data, Player player, ToolbarClickListener clickListener)
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

    }*/
    
    void ToolItemClick(int index) {
        //BaseObject clickedObject = toolbarData.Data[index];
        //toolbarData.ClickListener.OnToolBarClick(clickedObject);
    }

    public void OnClicked(int id)
    {
        ToolbarData toolbarEntry = null;
        if (toolbarState == ToolbarState.BUILDING && id < availableBuildings.Count)
        {
            toolbarEntry = availableBuildings[id];
        }
        else if (toolbarState == ToolbarState.UNIT && id < availableUnits.Count) {
            toolbarEntry = availableUnits[id];
        }

        if (toolbarEntry != null) {
            toolbarEntry.ClickListener.OnToolBarClick(toolbarEntry.Obj);
        }
        
    }
}
