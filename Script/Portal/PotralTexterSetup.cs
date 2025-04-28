using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotralTexterSetup : MonoBehaviour
{
    public Camera CameraB;

    public Material CameraMatB;

    private void Start()
    {
        if (CameraB.targetTexture != null)
        {
            CameraB.targetTexture.Release();
        }
        CameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        CameraMatB.mainTexture = CameraB.targetTexture;
    }
}

