using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEngine : MonoBehaviour {

    private List<AIBaseHandler> aiBaseHandlers = new List<AIBaseHandler>();

    private List<AIStrategy> strategicDivisionHandlers = new List<AIStrategy>();

    private Player player;


	// Use this for initialization
	void Start () {

        player = GetComponent<PlayerInitializer>().GetPlayer();
        aiBaseHandlers = new List<AIBaseHandler>(GetComponentsInChildren<AIBaseHandler>());
        aiBaseHandlers.ForEach(elem => elem.SetPlayer(player));

        InvokeRepeating("AIBaseAdvancementLoop", 2.0f, 5f);
        InvokeRepeating("AIStrategyLoop", 10.0f, 1f);
    }


    private void AIBaseAdvancementLoop() {

        //Determine the camp advancement level

        if (aiBaseHandlers.Count <= 0) {
            return;
        }

        AIBaseHandler minHandler = aiBaseHandlers[0];
        int minDevelopmentLevel = minHandler.GetDevelopmentLevel();

        bool sameLevel = true;

        foreach (AIBaseHandler handler in aiBaseHandlers)
        {
            if (handler.GetDevelopmentLevel() < minDevelopmentLevel) {
                minHandler = handler;
                minDevelopmentLevel = handler.GetDevelopmentLevel();
                sameLevel = false;
            }
        }

        if (sameLevel) {
            aiBaseHandlers.ForEach(handler => handler.Advance());
        }
        
        minHandler.MakeAction();
        
        


    }


    private void AIStrategyLoop() {

        strategicDivisionHandlers.ForEach(handler => handler.MakeAction());

        AIUnitHandler unitHandler = GetComponentInChildren<AIUnitHandler>();
        List<BaseObject> undeployedUnits = unitHandler.getUndeployedUnits();

        if(undeployedUnits.Count >= AIDivision.UnitsInDivision)
        {
            List<BaseObject> units = undeployedUnits.GetRange(0, AIDivision.UnitsInDivision);
            undeployedUnits.RemoveRange(0, AIDivision.UnitsInDivision);

            AIDivision division = new AIDivision(units);
            //Add mix between offence and defence
            AIStrategy strategy = new AIOffence(player, division);
            Debug.Log("Adding strategy");
            strategicDivisionHandlers.Add(strategy);
        }


        



    }




    void Update () {
	
        

        



	}
}
