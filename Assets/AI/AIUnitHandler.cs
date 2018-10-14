using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIUnitHandler : AIBaseHandler, PlayerBuildableObjects.OnBuildableObjectChange{

    private int advancementLevel = 0;

    private int unitsPerLevel = 5;

    private Player player;

    private bool hasBuiltFactory = false;

    public BaseObject factoryToSearchFor;

    private PlayerDataEnvironment.PlayerEnvironment pEnv;

    private List<BaseObject> availableBattleUnits = new List<BaseObject>();

    public override void Advance()
    {
        advancementLevel++;
    }

    public override int GetDevelopmentLevel()
    {
        return availableBattleUnits.Count / unitsPerLevel; 
    }

    public override void MakeAction()
    {
        Debug.Log("Making action");
        if (hasBuiltFactory) {

            //Check how many units we are missing to advance one level

            int requiredUnits = unitsPerLevel * (advancementLevel + 1);
            int unitsLeft = requiredUnits - availableBattleUnits.Count;
            Debug.Log("Units left " + unitsLeft);

            PlayerBuildableObjectData pbod = pEnv.GetBuildableObjects().GetBuildableObject(factoryToSearchFor);
            for (int i = 0; i < unitsLeft; i++) {    
                CreateUnit(pbod);
            }            
        }

    }


    public void OnBuildingOptionAdded(PlayerBuildableObjectData addedObj)
    {
        if(addedObj.Obj.printableName == factoryToSearchFor.printableName)
        {
            Debug.Log("Found building we are looking for");
            hasBuiltFactory = true;
        }

        addedObj.creationObject.OnToolBarClick(addedObj.Obj);
        

    }

    public void OnBuildingOptionRemoved(PlayerBuildableObjectData removedObj)
    {

        if (removedObj.Obj.printableName == factoryToSearchFor.printableName) {
            hasBuiltFactory = pEnv.GetBuildableObjects().GetBuildableObject(removedObj.Obj) != null;
        }
    }

    public override void SetPlayer(Player player)
    {
        this.player = player;
        pEnv = PlayerDataEnvironment.GetPlayerEnvironment(player);
        PlayerBuildableObjects pbo = pEnv.GetBuildableObjects();
        pbo.AddChangeListener(this);
    }


    private void CreateUnit(PlayerBuildableObjectData pbod) {
        BaseObject createdObject = pbod.creationObject.OnToolBarClick(pbod.Obj);
        availableBattleUnits.Add(createdObject);
        Debug.Log("Created unit");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
