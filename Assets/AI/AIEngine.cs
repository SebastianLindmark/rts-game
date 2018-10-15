using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEngine : MonoBehaviour {

    private List<AIBaseHandler> AIBaseHandlers = new List<AIBaseHandler>();

    private List<AIStrategy> strategicDivisionHandlers = new List<AIStrategy>();

    private Player player;


	// Use this for initialization
	void Start () {

        player = GetComponent<PlayerInitializer>().GetPlayer();
        AIBaseHandlers = new List<AIBaseHandler>(GetComponentsInChildren<AIBaseHandler>());
        AIBaseHandlers.ForEach(elem => elem.SetPlayer(player));

        InvokeRepeating("AIBaseAdvancementLoop", 2.0f, 1f);
        InvokeRepeating("AIStrategyLoop", 3.0f, 1f);
    }


    private void AIBaseAdvancementLoop() {

        //Determine the camp advancement level

        if (AIBaseHandlers.Count <= 0) {
            return;
        }

        AIBaseHandler minHandler = AIBaseHandlers[0];
        int minDevelopmentLevel = minHandler.GetDevelopmentLevel();

        foreach (AIBaseHandler handler in AIBaseHandlers)
        {
            if (handler.GetDevelopmentLevel() < minDevelopmentLevel) {
                minHandler = handler;
                minDevelopmentLevel = handler.GetDevelopmentLevel();
            }
        }

        minHandler.MakeAction();


    }


    private void AIStrategyLoop() {

        strategicDivisionHandlers.ForEach(handler => handler.MakeAction());

        AIUnitHandler unitHandler = GetComponentInChildren<AIUnitHandler>();
        List<BaseObject> undeployedUnits = unitHandler.getUndeployedUnits();

        if(undeployedUnits.Count > AIDivision.UnitsInDivision)
        {
            List<BaseObject> units = undeployedUnits.GetRange(0, AIDivision.UnitsInDivision);
            undeployedUnits.RemoveRange(0, AIDivision.UnitsInDivision);

            AIDivision division = new AIDivision(units);
            //Add mix between offence and defence
            AIStrategy strategy = new AIOffence(player, division);

            strategicDivisionHandlers.Add(strategy);
        }


        



    }




    void Update () {
	
        

        



	}
}
