using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToolbarResourceController : MonoBehaviour {


    public Text oilResourceText;

    public Text coinResourceText;

    private int displayedOilResource = 0;
    private int displayedCoinResource = 0;

    private GameResource goldResource;
    private GameResource oilResource;

    void Start () {

        Player humanPlayer = PlayerManager.humanPlayer;
        PlayerDataEnvironment.PlayerEnvironment pEnv = PlayerDataEnvironment.GetPlayerEnvironment(humanPlayer);
        goldResource = pEnv.GetGoldResource();
        oilResource = pEnv.GetOilResource();
    }


    void Update () {

        float k = 40f;

        //p-regulator

        if ((oilResource.GetAvailableResources() - displayedOilResource) / k < 1f){
            displayedOilResource = oilResource.GetAvailableResources();
        }else{
            displayedOilResource += Mathf.RoundToInt((oilResource.GetAvailableResources() - displayedOilResource) / k);
        }


        if ((goldResource.GetAvailableResources() - displayedCoinResource) / k < 1f)
        {
            displayedCoinResource = goldResource.GetAvailableResources();
        }
        else
        {
            displayedCoinResource += Mathf.RoundToInt((goldResource.GetAvailableResources() - displayedCoinResource) / k);
        }
        


        if (oilResourceText)
        {
            oilResourceText.text = displayedOilResource + "";
        }

        if (coinResourceText)
        {
            coinResourceText.text = displayedCoinResource + "";
        }

    }
}
