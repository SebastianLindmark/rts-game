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

    private List<BaseObject> undeployedUnits = new List<BaseObject>();


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
        if (hasBuiltFactory)
        {

            //Check how many units we are missing to advance one level

            int requiredUnits = unitsPerLevel * (advancementLevel + 1);
            int unitsLeft = requiredUnits - availableBattleUnits.Count;
            //Debug.Log("Units left " + unitsLeft);

            //Another approach is to map different units from beginning. Problably more control.
            List<PlayerBuildableObjectData> buildableObjectList = pEnv.GetBuildableObjects().getAvailableUnits();
            List<PlayerBuildableObjectData> attackUnits = buildableObjectList.FindAll(elem => elem.Obj.GetComponent<AttackHandler>() != null);

            if (attackUnits.Count > 0)
            {
                for (int i = 0; i < unitsLeft; i++)
                {
                    CreateUnit(attackUnits[0]);
                }
            }
        }

    }
    

    public void OnBuildingOptionAdded(PlayerBuildableObjectData addedObj)
    {
        if(addedObj.Obj.printableName == factoryToSearchFor.printableName)
        {
            hasBuiltFactory = true;
        }
     
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
        if (createdObject != null) {
            availableBattleUnits.Add(createdObject);
            undeployedUnits.Add(createdObject);
        }
        
    }

    public List<BaseObject> getUndeployedUnits() {
        
        return undeployedUnits;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
