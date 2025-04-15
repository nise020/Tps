using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Queue <float> MouseScrollQueBase => Shared.InputManager.MouseScrollQueBase;
    public Queue<Vector2> MouseMoveQueBase => Shared.InputManager.MouseMoveQueBase;
    List<PlayerCamera> cameras = new List<PlayerCamera>();
    Camera MainCam = null;
    private void Awake()
    {
        if (Shared.CameraManager == null)
        {
            Shared.CameraManager = this;
        }
        else
        {
            Destroy(this);
        }
        //MainCam = Camera.main;
    }
    public void CameraAdd(PlayerCamera _camera)
    {
        cameras.Add(_camera);
    }
    public void MainCamerainitEvent() 
    {
        while (MouseScrollQueBase.Count > 0)//key 
        {
            float type = MouseScrollQueBase.Dequeue();
        }
        while (MouseMoveQueBase.Count > 0)//key 
        {
            Vector3 type = MouseMoveQueBase.Dequeue();
        }
    }

}
