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

    private Dictionary<string, BaseObject> existingObjects = new Dictionary<string, BaseObject>();

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

    private void GetImage(string name) {
        //Temp function
    }


    private void RedrawToolbar()
    {
        HashSet<PlayerBuildableObjectData> uniqueItems;
        if (toolbarState == ToolbarState.BUILDING)
        {
            uniqueItems = new HashSet<PlayerBuildableObjectData>(buildableObjects.getAvailableBuildings());
        }
        else {
            uniqueItems = new HashSet<PlayerBuildableObjectData>(buildableObjects.getAvailableUnits());
        }

        List<PlayerBuildableObjectData> objs = new List<PlayerBuildableObjectData>(uniqueItems);


        for (int i = 0; i < cells.Count; i++)
        {

            if (objs.Count > i)
            {
                Text text = cells[i].GetComponentInChildren<Text>();
                text.text = objs[i].Obj.printableName.Length > 0 ? objs[i].Obj.printableName : objs[i].Obj.name;

                Image[] images = cells[i].GetComponentsInChildren<Image>();

                Image imageBackground = images[0];
                Image textBackground = images[1];

                Sprite thumbnail = objs[i].Obj.thumbnail;

                if (thumbnail) {
                    imageBackground.sprite = thumbnail;
                    imageBackground.color = new Color(1,1,1);
                }
                
                textBackground.color = new Color(0.1058824f, 0.172549f, 0.1921569f, 0.90f);
            }
            else
            {
                cells[i].GetComponentInChildren<Text>().text = "";
                Image imageBox = cells[i].GetComponentInChildren<Image>();
                imageBox.sprite = null;
                imageBox.color = new Color(0.1058824f, 0.172549f, 0.1921569f);
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
            toolbarEntry.creationObject.OnToolBarClick(toolbarEntry.Obj);
        }
        
    }


    private void UpdateItemState()
    {

    }

    public void OnBuildingOptionAdded(PlayerBuildableObjectData addedObj)
    {
        RedrawToolbar();
    }

    public void OnBuildingOptionRemoved(PlayerBuildableObjectData removedObj)
    {

        RedrawToolbar();
    }



}
