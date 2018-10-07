using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolbarStateController: MonoBehaviour,ItemClick {


    private List<Image> toolbarStateImages = new List<Image>();
    private List<Image> childToolbarStateImages = new List<Image>();
    

    void Start () {
        ToolbarItemClickRegister[] comps = GetComponentsInChildren<ToolbarItemClickRegister>();


        Image building = transform.Find("Building").GetComponent<Image>();
        Image unit = transform.Find("Unit").GetComponent<Image>();


        toolbarStateImages.Add(building);
        toolbarStateImages.Add(unit);

        childToolbarStateImages.Add(transform.Find("Building").GetComponentsInChildren<Image>()[1]);
        childToolbarStateImages.Add(transform.Find("Unit").GetComponentsInChildren<Image>()[1]);

        for (int i = 0; i < comps.Length; i++) {
            comps[i].AddClickListener(i, this);
        }
	}
	
	
	void Update () {
		
	}


    private void changeImageBackgounds(int selected) {
        Color selectedColor = Color.clear;
        Color unSelectedColor = new Color(0.1058824f, 0.172549f, 0.1921569f, 1); //TODO make global color instead.

        if (selected == 1)
        {
            toolbarStateImages[0].color = unSelectedColor;
            toolbarStateImages[1].color = selectedColor;

            childToolbarStateImages[0].color = new Color(1, 1, 1, 0.4f);
            childToolbarStateImages[1].color = new Color(1, 1, 1);
        }
        else {
            toolbarStateImages[0].color = selectedColor;
            toolbarStateImages[1].color = unSelectedColor;

            childToolbarStateImages[0].color = new Color(1, 1, 1);
            childToolbarStateImages[1].color = new Color(1, 1, 1, 0.4f); 
        }


    }

    public void OnClicked(int id)
    {
        ToolbarController controller = GameObject.FindGameObjectWithTag("Toolbar").GetComponent<ToolbarController>();

        if (controller != null) {

            ToolbarController.ToolbarState state = id == 0 ? ToolbarController.ToolbarState.BUILDING : ToolbarController.ToolbarState.UNIT;
            controller.SetDisplayState(state);
            changeImageBackgounds(id);
        }

        

    }
}
