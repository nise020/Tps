using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour 
{
    private void Awake()
    {
        if (Shared.InputManager == null)
        {
            Shared.InputManager = this;
        }
        else 
        {
            Destroy(this);
        }
    }
    private void Update()
    {
        inputEvent();
    }
    public Queue<KeyCode> KeyinPutQueBase = new Queue<KeyCode>();
    public Queue<MouseInputType> MouseInputQueBase = new Queue<MouseInputType>();
    public Queue<Vector2> MouseMoveQueBase = new Queue<Vector2>();

    public Queue<Vector3> MoveQueBase = new Queue<Vector3>();
    public Queue<float> MouseScrollQueBase = new Queue<float>();
    float Speed = 10.0f;
    public void inputEvent() 
    {
        if (Input.GetMouseButton(0)) 
            MouseInputQueBase.Enqueue(MouseInputType.Click);//mouseClick

        if (Input.GetMouseButtonUp(0)) 
            MouseInputQueBase.Enqueue(MouseInputType.Release);//mouseClickUp

        if (Input.GetMouseButtonDown(0)) 
            MouseInputQueBase.Enqueue(MouseInputType.Hold);//mouseClickDown

        if (Input.GetKeyDown(KeyCode.Mouse1)) 
            KeyinPutQueBase.Enqueue(KeyCode.Mouse1);//RunCheck

        if (Input.GetKeyDown(KeyCode.R)) 
            KeyinPutQueBase.Enqueue(KeyCode.R);//reloadOn

        if (Input.GetKeyDown(KeyCode.Q)) 
            KeyinPutQueBase.Enqueue(KeyCode.Q);//Skill1

        if (Input.GetKeyDown(KeyCode.E)) 
            KeyinPutQueBase.Enqueue(KeyCode.E);//Skill2

        if (Input.GetKeyDown(KeyCode.Z)) 
            KeyinPutQueBase.Enqueue(KeyCode.Z);//shitdown

        if (Input.GetKeyDown(KeyCode.Space)) 
            KeyinPutQueBase.Enqueue(KeyCode.Space);//Space

        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (move.magnitude > 0.1f) MoveQueBase.Enqueue(move);

        float scroll = Input.GetAxis("Mouse ScrollWheel") * Speed;
        if (scroll != 0.0f) MouseScrollQueBase.Enqueue(scroll);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (Mathf.Abs(mouseX) > 0.01f || Mathf.Abs(mouseY) > 0.01f)
        {
            MouseMoveQueBase.Enqueue(new Vector2(mouseX, mouseY));
        }
    }
}
