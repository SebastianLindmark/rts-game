using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarController : MonoBehaviour,ItemClick, PlayerBuildableObjects.OnBuildableObjectChange {


    public enum ToolbarState {
        BUILDING,UNIT
    }


    public Texture2D toolbarIcon;

    private int toolbarWidth = 1000;

    private Rect toolbarRect;

    private Hashtable existingObjects = new Hashtable();

    private ToolbarState toolbarState;

    private List<Transform> cells = new List<Transform>();

    private PlayerBuildableObjects buildableObjects;

    void Start () {

        foreach (Transform t in gameObject.transform) {
            t.GetComponent<ToolbarItemClickRegister>().AddClickListener(cells.Count,this);
            cells.Add(t);
        }

        Player humanPlayer = PlayerManager.humanPlayer;
        buildableObjects = PlayerDataEnvironment.GetPlayerEnvironment(humanPlayer).GetBuildableObjects();
        buildableObjects.AddChangeListener(this);
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
        List<PlayerBuildableObjectData> objs;
        if (toolbarState == ToolbarState.BUILDING)
        {
            objs = buildableObjects.getAvailableBuildings();
        }
        else {
            objs = buildableObjects.getAvailableUnits();
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

 
    public void OnClicked(int id)
    {
        PlayerBuildableObjectData toolbarEntry = null;
        if (toolbarState == ToolbarState.BUILDING && id < buildableObjects.getAvailableBuildings().Count)
        {
            toolbarEntry = buildableObjects.getAvailableBuildings()[id];
        }
        else if (toolbarState == ToolbarState.UNIT && id < buildableObjects.getAvailableUnits().Count) {
            toolbarEntry = buildableObjects.getAvailableUnits()[id];
        }

        if (toolbarEntry != null) {
            toolbarEntry.ClickListener.OnToolBarClick(toolbarEntry.Obj);
        }
        
    }

    public void OnAvailableBuildingsChanged()
    {
        RedrawToolbar();
    }
}
