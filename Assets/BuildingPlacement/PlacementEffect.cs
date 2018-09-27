using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementEffect {

    public GameObject obj;

    private Material originalMaterial;

    private Material placementMaterial;

    private int savedLayer;

    public PlacementEffect(GameObject o)
    {
        originalMaterial = new Material(o.GetComponent<Renderer>().material);
        placementMaterial = new Material(o.GetComponent<Renderer>().material);
        obj = o;
        Setup();

    }

    private void Setup() {
        savedLayer = obj.gameObject.layer;
        obj.gameObject.layer = LayerMask.NameToLayer("Placement");
        placementMaterial.shader = Shader.Find("Transparent/Diffuse");
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
        obj.GetComponent<Renderer>().material = material;
    }

    public GameObject Reset() {
        SetMaterial(originalMaterial);
        obj.gameObject.layer = savedLayer;
        return obj;
    }

}
