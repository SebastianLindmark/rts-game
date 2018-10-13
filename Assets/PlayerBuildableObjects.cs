using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildableObjects {

    public interface OnBuildableObjectChange {
        void OnAvailableBuildingsChanged();
    }

    private List<PlayerBuildableObjectData> availableBuildings = new List<PlayerBuildableObjectData>();
    private List<PlayerBuildableObjectData> availableUnits = new List<PlayerBuildableObjectData>();

    private List<OnBuildableObjectChange> changeListeners = new List<OnBuildableObjectChange>();

    public void AddObject(Player player, BaseBuilding obj, ToolbarClickListener responsibleForCreation) {
        availableBuildings.Add(new PlayerBuildableObjectData(player, obj, responsibleForCreation));
        NotifyChange();
    }


    public void AddObject(Player player, BaseUnit obj, ToolbarClickListener responsibleForCreation)
    {
        availableUnits.Add(new PlayerBuildableObjectData(player, obj, responsibleForCreation));
        NotifyChange();
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

    public void NotifyChange() {
        changeListeners.ForEach(elem => elem.OnAvailableBuildingsChanged());
    } 

    public void RemoveElement(BaseObject baseObject)
    {

        bool found = false;
        for (int i = 0; i < availableBuildings.Count && !found; i++)
        {
            PlayerBuildableObjectData td = availableBuildings[i];
            if (td.Obj == baseObject)
            {
                availableBuildings.Remove(td);
                found = true;
            }
        }
        for (int i = 0; i < availableUnits.Count && !found; i++)
        {
            PlayerBuildableObjectData td = availableUnits[i];
            if (td.Obj == baseObject)
            {
                availableUnits.Remove(td);
                found = true;
            }
        }

        if (found) {
            NotifyChange();
        }

        
    }

}
