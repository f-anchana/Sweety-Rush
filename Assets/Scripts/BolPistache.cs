using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolPistache : MonoBehaviour
{
    private void OnMouseDown()
    {
        GateauInteractif gateau = TrouverGateauSurAssiette();
        if (gateau != null && gateau.EstSurAssiette)
        {
            gateau.AppliquerPistache();
        }
    }

    private GateauInteractif TrouverGateauSurAssiette()
    {
        GateauInteractif[] g�teaux = FindObjectsOfType<GateauInteractif>();
        foreach (var g in g�teaux)
        {
            if (g.EstSurAssiette)
                return g;
        }
        return null;
    }
}
