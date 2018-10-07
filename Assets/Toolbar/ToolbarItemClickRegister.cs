using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolbarItemClickRegister : MonoBehaviour,IPointerClickHandler {

    private int id;
    private ItemClick clickedListener;

    public void AddClickListener(int id,ItemClick l) {
        this.id = id;
        this.clickedListener = l;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if(clickedListener != null)
        {
            clickedListener.OnClicked(id);
        }
            
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
