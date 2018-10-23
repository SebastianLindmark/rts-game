using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementEffect : MonoBehaviour{

    private Material[] originalMaterials;

    private Material[] placementMaterials;

    private Renderer[] placementRenderers;

    private int savedLayer;


    void Awake()
    {

        placementRenderers = GetComponentsInChildren<Renderer>();

        if (placementRenderers.Length == 0)
        {
            Debug.LogError("Material must have a renderer");
            return;
        }

        placementMaterials = new Material[placementRenderers.Length];
        originalMaterials = new Material[placementRenderers.Length];

        for (int i = 0; i < placementRenderers.Length; i++)
        {
            originalMaterials[i] = new Material(placementRenderers[i].material);
            placementMaterials[i] = new Material(placementRenderers[i].material);

            placementMaterials[i].shader = Shader.Find("Transparent/Diffuse");
        }

        Debug.Log("The renderers are " + placementRenderers.Length);

    }

    void Start()
    {
       
    }

    public void Setup() {
        savedLayer = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("Placement");
        //EnableCollider(false);
        
    }

    void EnableCollider(bool state)
    {
        Collider collider = gameObject.GetComponentInChildren<BoxCollider>();
        if (collider)
        {
            collider.enabled = state;
        }

    }

    public void ApplyColorToOriginal(Color color) {


        for (int i = 0; i < originalMaterials.Length; i++)
        {
            Material material = new Material(originalMaterials[i]);
            material.color = new Color(color.r, color.g, color.b, color.a);
            originalMaterials[i] = material;
            SetMaterial(i, material);
        }

    }

    public void ApplyValidEffect()
    {

        for (int i = 0; i < placementMaterials.Length; i++) {

            Material material = new Material(placementMaterials[i]);
            material.color = new Color(material.color.r, material.color.g, material.color.b, 0.2f);
            SetMaterial(i,material);
        }
        
    }

    public void ApplyInvalidEffect() {
        for (int i = 0; i < placementMaterials.Length; i++) {
            Material material = new Material(placementMaterials[i]);
            material.color = new Color(255, 0, 0, 0.2f);
            SetMaterial(i,material);
        }
        
    }

    public void SetMaterial(int index, Material material) {
        placementRenderers[index].material = material;
    }

    public void Reset() {

        for (int i = 0; i < originalMaterials.Length; i++) {

            SetMaterial(i,originalMaterials[i]);
            gameObject.layer = savedLayer;
        }
        EnableCollider(true);


    }

}
