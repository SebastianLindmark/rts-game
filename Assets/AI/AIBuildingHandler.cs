using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBuildingHandler : AIBaseHandler {


    public List<BaseObject> neccesaryObjects; //Will be Tank builder and an Ore refinery

    //private List<BaseObject> builtObjects;

    private Dictionary<string,int> builtObjects = new Dictionary<string,int>();

    private int advancementLevel = 0;

    void Start () {
		
	}
	

	void Update () {
		
	}

    public override void Advance()
    {
        advancementLevel++;
    }

    private bool HasBuiltObject(BaseObject target)
    {

        return builtObjects.ContainsKey(target.name) && builtObjects[target.name] > 0;
        
    }


    public override int GetDevelopmentLevel()
    {
        foreach(BaseObject target in neccesaryObjects)
        {
            if (!HasBuiltObject(target))
            {
                return 0;
            }
        }

        //We have built all the neccessary buildings. TODO Add extra defence
        return advancementLevel;
    }

    public override void MakeAction()
    {
        int level = GetDevelopmentLevel();
        if (level == 0)
        {
            foreach (BaseObject target in neccesaryObjects)
            {
                if (!HasBuiltObject(target))
                {
                    BuildBuilding(target);
                    return;
                }
            }
        }
    }

    private void BuildBuilding(BaseObject target)
    {
        AICamp camp = GameObject.Find("AIHandler").GetComponent<AICamp>();
        Vector3 campLocation = camp.GetCampLocation();
        float campRadius = camp.GetCampRadius();

        
        Vector3 possibleBuildingPlacement = (Random.insideUnitCircle * campRadius);
        possibleBuildingPlacement += campLocation;

        //Try to place object around camp max 10 times.
        for (int i = 0; i < 10 && !BuildingPlacer.HitsObstacle(possibleBuildingPlacement, target.transform); i++)
        {
            possibleBuildingPlacement = (Random.insideUnitCircle * campRadius);
            possibleBuildingPlacement += campLocation;
        }




        if (!BuildingPlacer.HitsObstacle(possibleBuildingPlacement, target.transform))
        {

            Player aiPlayer= GameObject.Find("AIHandler").GetComponent<Player>();
            BaseObject newObject = new BaseFactory().CreateUnit(aiPlayer, target, possibleBuildingPlacement);
            if (builtObjects.ContainsKey(target.name))
            {
                builtObjects[target.name]++;
            }
            else
            {
                builtObjects.Add(target.name, 1);
            }
        }
        else
        {
            Debug.LogError("A placement was not found");
        }


    }


}
