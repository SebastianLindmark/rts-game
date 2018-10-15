using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildableObjects {

    public interface OnBuildableObjectChange {
        void OnBuildingOptionAdded(PlayerBuildableObjectData baseObject);
        void OnBuildingOptionRemoved(PlayerBuildableObjectData baseObject);
    }

    private List<PlayerBuildableObjectData> availableBuildings = new List<PlayerBuildableObjectData>();
    private List<PlayerBuildableObjectData> availableUnits = new List<PlayerBuildableObjectData>();

    private List<OnBuildableObjectChange> changeListeners = new List<OnBuildableObjectChange>();

    public void AddObject(Player player, BaseBuilding obj, ToolbarClickListener responsibleForCreation) {
        PlayerBuildableObjectData pbo = new PlayerBuildableObjectData(player, obj, responsibleForCreation);
        availableBuildings.Add(pbo);
        changeListeners.ForEach(elem => elem.OnBuildingOptionAdded(pbo));
    }


    //Player argument can be removed since this class is unique for each player.
    public void AddObject(Player player, BaseUnit obj, ToolbarClickListener responsibleForCreation)
    {
        PlayerBuildableObjectData pbo = new PlayerBuildableObjectData(player, obj, responsibleForCreation);
        availableUnits.Add(pbo);
        changeListeners.ForEach(elem => elem.OnBuildingOptionAdded(pbo));
    }

    public PlayerBuildableObjectData GetBuildableObject(BaseObject baseObject) {
        
        PlayerBuildableObjectData pbod = availableBuildings.Find(elem => elem.Obj.printableName == baseObject.printableName);
        if (pbod != null) {
            return pbod;
        }
        return availableUnits.Find(elem => elem.Obj.printableName == baseObject.printableName);
    }
   

    public List<PlayerBuildableObjectData> getAvailableBuildings() {
        return availableBuildings;
    }

    public List<PlayerBuildableObjectData> getAvailableUnits()
    {
        return availableUnits;
    }

    public void AddChangeListener(OnBuildableObjectChange boc) {
        changeListeners.Add(boc);
    }

    
    public void RemoveElement(BaseObject baseObject)
    {

        bool found = false;
        PlayerBuildableObjectData removed = null;

        for (int i = 0; i < availableBuildings.Count && !found; i++)
        {
            removed = availableBuildings[i];
            if (removed.Obj == baseObject)
            {
                availableBuildings.Remove(removed);
                found = true;
            }
        }
        for (int i = 0; i < availableUnits.Count && !found; i++)
        {
            removed = availableUnits[i];
            if (removed.Obj == baseObject)
            {
                availableUnits.Remove(removed);
                found = true;
            }
        }

        if (found) {
            changeListeners.ForEach(elem => elem.OnBuildingOptionRemoved(removed));
        }

        
    }

}
