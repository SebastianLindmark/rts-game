using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AIBuildingHandler : AIBaseHandler,ObjectLifecycleListener {


    public List<BaseObject> neccesaryObjects; //Will be Tank builder and an Ore refinery

    

    private Dictionary<string, List<BaseObject>> builtObjects = new Dictionary<string,List<BaseObject>>();

    private int advancementLevel = 0;

    private Player player;
    

    public override void SetPlayer(Player player)
    {
        this.player = player;
    }


    public override void Advance()
    {
        advancementLevel++;
    }

    private bool HasBuiltObject(BaseObject target)
    {
        
        //Debug.Log(PrefabUtility.GetCorrespondingObjectFromSource(target.gameObject));
        //Debug.Log("Checking if " + target.name + " exists");
        //Debug.Log(builtObjects.ContainsKey(target.printableName) && builtObjects[target.printableName].Count > 0);
        return builtObjects.ContainsKey(target.printableName) && builtObjects[target.printableName].Count > 0;
        
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
        return advancementLevel + 1;
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
        else {
            //Debug.Log("All neccesary buildings has been built");
        }
    }

    private Vector3 GetRandomPositionInsideCamp(Vector3 campLocation, float campRadius) {

        Vector3 possibleBuildingPlacement = Vector3.zero;
        Vector2 temp = (Random.insideUnitCircle * campRadius);
        possibleBuildingPlacement.x = temp.x;
        possibleBuildingPlacement.z = temp.y;
        possibleBuildingPlacement += campLocation;
        return possibleBuildingPlacement;

    }


    private void BuildBuilding(BaseObject target)
    {
        AICamp camp = GameObject.Find("AIHandler").GetComponent<AICamp>();
        Vector3 campLocation = camp.GetCampLocation();
        float campRadius = camp.GetCampRadius();


        Vector3 possibleBuildingPlacement = GetRandomPositionInsideCamp(campLocation, campRadius);

        //Try to place object around camp max 10 times.
        for (int i = 0; i < 10 && !BuildingPlacer.HitsObstacle(possibleBuildingPlacement, target.transform); i++)
        {
            possibleBuildingPlacement = GetRandomPositionInsideCamp(campLocation, campRadius);
        }


        if (!BuildingPlacer.HitsObstacle(possibleBuildingPlacement, target.transform))
        {
            possibleBuildingPlacement.y += target.transform.position.y; //Offset the building to be above ground

            BaseBuilding newObject = Instantiate<BaseObject>(target, possibleBuildingPlacement, Quaternion.Euler(Vector3.zero)) as BaseBuilding;
            newObject.SetPlayer(player);
            newObject.AddLifecycleListener(this);
            newObject.OnCreated();
        }
        else
        {
            Debug.LogError("A placement was not found");
        }


    }

    public void onCreated(BaseObject baseObject)
    {
        if (!builtObjects.ContainsKey(baseObject.printableName))
        {
            builtObjects[baseObject.printableName] = new List<BaseObject>();
        }
        builtObjects[baseObject.printableName].Add(baseObject);
    }

    public void onRemoved(BaseObject baseObject)
    {
        List<BaseObject> builtObjs = builtObjects[baseObject.printableName];
        

        for (int i = 0; i < builtObjs.Count; i++)
        {
            BaseObject built = builtObjs[i];

            if (GameObject.ReferenceEquals(baseObject,built)) {
                builtObjs.Remove(built);
                break;
            }
        }
    }

   
}
