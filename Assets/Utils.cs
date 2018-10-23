using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    static Texture2D _whiteTexture;
    public static Texture2D WhiteTexture
    {
        get
        {
            if (_whiteTexture == null)
            {
                _whiteTexture = new Texture2D(1, 1);
                _whiteTexture.SetPixel(0, 0, Color.white);
                _whiteTexture.Apply();
            }

            return _whiteTexture;
        }
    }

    public static void DrawScreenRect(Rect rect, Color color)
    {
        GUI.color = color;
        
        GUI.DrawTexture(rect, WhiteTexture);
        GUI.color = Color.white;
    }

    public static void DrawScreenRectBorder(Rect rect, float thickness, Color color)
    {

        Utils.DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
        Utils.DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
        Utils.DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
        Utils.DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
    }

    public static GameObject CreateMinimapUnitCube(Color color, GameObject target) {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.layer = LayerMask.NameToLayer("Minimap");
        cube.transform.parent = target.transform;
        cube.transform.position = target.transform.position;

        Vector3 bounds = target.GetComponentInChildren<BoxCollider>().bounds.size;
        cube.transform.localScale = bounds;

        Renderer cubeRenderer = cube.GetComponent<Renderer>();
        cubeRenderer.material.color = color;
        return cube;
    }

}