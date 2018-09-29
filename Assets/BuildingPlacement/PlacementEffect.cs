using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementEffect : MonoBehaviour{

    private Material originalMaterial;

    private Material placementMaterial;

    private int savedLayer;



    void Start()
    {
        originalMaterial = new Material(GetComponent<Renderer>().material);
        placementMaterial = new Material(GetComponent<Renderer>().material);
        placementMaterial.shader = Shader.Find("Transparent/Diffuse");
    }

    public void Setup() {
        savedLayer = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("Placement");
        
    }

    public void ApplyValidEffect()
    {
        
        Material material = new Material(placementMaterial);
        material.color = new Color(material.color.r, material.color.g, material.color.b, 0.2f);
        SetMaterial(material);
    }

    public void ApplyInvalidEffect() {
        Material material = new Material(placementMaterial);
        material.color = new Color(255, 0, 0, 0.2f);
        SetMaterial(material);
    }

    public void SetMaterial(Material material) {
        GetComponent<Renderer>().material = material;
    }

    public void Reset() {
        SetMaterial(originalMaterial);
        gameObject.layer = savedLayer;
    }

}
