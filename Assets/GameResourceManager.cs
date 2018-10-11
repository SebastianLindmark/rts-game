using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResourceManager : MonoBehaviour {


    public Text oilResourceText;
    public Text coinResourceText;

    public int startOilResource;
    public int startCoinResource;

    private int targetOilResource = 0;
    private int targetCoinResource = 0;

    private int displayedOilResource = 0;
    private int displayedCoinResource = 0;

    void Start () {

        targetOilResource = startOilResource;
        targetCoinResource = startCoinResource;
        //InvokeRepeating("test", 0, 1.0f);
        //InvokeRepeating("test2", 0, 2.0f);

    }


    void removeResource(int amountOil, int amountCoin)
    {
        targetOilResource -= amountOil;
        targetCoinResource -= amountCoin;


        if (targetOilResource < 0)
        {
            targetOilResource = 0;
        }

        if (targetCoinResource < 0)
        {
            targetCoinResource = 0;
        }
    }

    public void addResource(int amountOil, int amountCoin)
    {
        //protect against negative numbers

        targetOilResource += amountOil;
        targetCoinResource += amountCoin;


        if (targetOilResource > 9999)
        {
            targetOilResource = 9999;
        }

        if (targetCoinResource > 9999)
        {
            targetCoinResource = 9999;
        }
    }

    public void test() {
        addResource(Random.Range(0, 50), Random.Range(0, 50));
    }

    public void test2()
    {
        removeResource(Random.Range(-200, 0), Random.Range(-200, 0));
    }


    public int getOilResourceCount()
    {
        return targetOilResource;
    }

    public int getCoinResourceCount()
    {
        return targetCoinResource;
    }


    void Update () {

        float k = 20f;
        
        if((targetOilResource - displayedOilResource) / k < 1f){
            displayedOilResource = targetOilResource;
        }else{
            displayedOilResource += Mathf.RoundToInt((targetOilResource - displayedOilResource) / k);
        }

        if ((targetCoinResource - displayedCoinResource) / k < 1f)
        {
            displayedCoinResource = targetCoinResource;
        }
        else
        {
            displayedCoinResource += Mathf.RoundToInt((targetCoinResource - displayedCoinResource) / k);
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
