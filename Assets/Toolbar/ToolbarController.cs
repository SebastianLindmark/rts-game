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

    private Hashtable existingObjects = new Hashtable();

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
        RedrawToolbar();
    }

    public void AddToolbarField(BaseUnit unit, Player player, ToolbarClickListener clickListener)
    {
        if (!existingObjects.Contains(unit.name))
        {
            availableUnits.Add(new ToolbarData(unit, player, clickListener));
            existingObjects.Add(unit.name, unit);
            RedrawToolbar();
        }
        
    }

    public void AddToolbarField(BaseBuilding building, Player player, ToolbarClickListener clickListener) {
        Debug.Log(building);
        Debug.Log(building.name);
        if (!existingObjects.Contains(building.name))
        {
            availableBuildings.Add(new ToolbarData(building, player, clickListener));
            existingObjects.Add(building.name, building);
            RedrawToolbar();
        }
    }

    public void RemoveElement(BaseObject baseObject) {
        if (existingObjects.Contains(baseObject.name))
        {
            existingObjects.Remove(baseObject.name);
        }

        bool found = false;
        for (int i = 0; i < availableBuildings.Count && !found; i++){
            ToolbarData td = availableBuildings[i];
            if (td.Obj == baseObject)
            {
                availableBuildings.Remove(td);
                found = true;
            }
        }
        for (int i = 0; i < availableUnits.Count && !found; i++)
        {
            ToolbarData td = availableUnits[i];
            if (td.Obj == baseObject)
            {
                availableUnits.Remove(td);
                found = true;
            }
        }
        Debug.Log("Units after " + availableUnits.Count);

        RedrawToolbar();
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
        else {
            objs = availableUnits;
        }


        for (int i = 0; i < cells.Count; i++)
        {

            if (objs.Count > i)
            {
                Text text = cells[i].GetComponentInChildren<Text>();
                text.text = objs[i].Obj.printableName.Length > 0 ? objs[i].Obj.printableName : objs[i].Obj.name;

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
