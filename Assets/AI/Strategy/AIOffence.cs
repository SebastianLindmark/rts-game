﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOffence : AIStrategy, ObjectLifecycleListener
{

    private AIDivision division;

    private Player player;

    private Player targetEnemy;

    private BaseObject targetObject;

    public AIOffence(Player player, AIDivision division)
    {
        this.division = division;
        this.player = player;
        targetEnemy = PlayerManager.GetEnemyPlayers(player)[0];
    }


    public void MakeAction()
    {

        if (targetObject == null)
        {
            targetObject = GetTargetObject();
        }
        else {
            Debug.Log("Attacking target");
            division.getDivision().ForEach(unit => unit.Attack(targetObject));
        }
        
    }

    public void onCreated(BaseObject baseObject)
    {
        
    }

    public void onRemoved(BaseObject baseObject)
    {
        if (baseObject == targetObject) {
            //Building has been shot down
            targetObject = null;
        }
    }

    private BaseObject GetTargetObject() {
        PlayerDataEnvironment.PlayerEnvironment pEnv = PlayerDataEnvironment.GetPlayerEnvironment(targetEnemy);
        List<BaseObject> builtBuildings = pEnv.GetBuiltObjects().GetBuildings(); //This one returns null

        //Logic for targeting specific buildings can be placed here
        if (builtBuildings.Count > 0)
        {
            BaseObject selectedBuilding = builtBuildings[0];
            selectedBuilding.AddLifecycleListener(this);
            return builtBuildings[0];
        }

        return null;
    }

}
