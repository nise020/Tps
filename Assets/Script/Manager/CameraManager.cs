using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Queue <float> MouseScrollQueBase => Shared.InputManager.MouseScrollQueData;
    public Queue<Vector2> MouseMoveQueBase => Shared.InputManager.MouseMoveQueData;
    List<Camera> cameras = new List<Camera>();
    Camera MainCam;

    ShakeCamera ShakeCamera;

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
    private void Start()
    {
        ShakeCamera = GetComponent<ShakeCamera>();
    }
    public void CameraChange(Camera _mainCamera) 
    {
        MainCam = _mainCamera;
    }
    public Camera MainCameraLoad() 
    {
        return MainCam;
    }
    public void CameraAdd(Camera _camera)
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
