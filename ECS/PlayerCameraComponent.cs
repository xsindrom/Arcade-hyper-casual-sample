using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraComponent : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;
    public static Camera PlayerCamera = null;

    private void Awake()
    {
        if (!PlayerCamera)
        {
            PlayerCamera = playerCamera;
        }
    }

    private void OnDestroy()
    {
        if(PlayerCamera == playerCamera)
        {
            PlayerCamera = null;
        }
    }
}
