using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelection : MonoBehaviour
{
    private Renderer objRenderer;
    private Color originalColor;
    public Color highlightColor = new Color(1f, 1f, 1f, 0.5f);
    private bool isSelected = false;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        if (objRenderer != null)
        {
            originalColor = objRenderer.material.color; // save the original color of the object
        }
    }

    void OnMouseDown()
    {
        if (objRenderer != null)
        {
            if (isSelected)
            {
                objRenderer.material.color = originalColor; // back to the default color
            }
            else
            {
                objRenderer.material.color = highlightColor;
            }

            isSelected = !isSelected; // reverse the selected state
        }
    }
}