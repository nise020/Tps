using System;
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
    //Player
    public Queue<KeyCode> KeyinPutQueData = new Queue<KeyCode>();
    public Queue<MouseInputType> MouseInputQueData = new Queue<MouseInputType>();
    public Queue<Vector2> MouseMoveQueData = new Queue<Vector2>();

    public Queue<Vector3> MoveQueData = new Queue<Vector3>();
    public Queue<float> MouseScrollQueData = new Queue<float>();
    float Speed = 10.0f;
    //Ui
    public Queue<KeyCode> UiKeyinPutQueData = new Queue<KeyCode>();
    public void inputEvent() 
    {
        PlayerInput();
        UiButtenInput();
    }

    private void UiButtenInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            UiKeyinPutQueData.Enqueue(KeyCode.Alpha1);//CharactorChangeButten1
        if (Input.GetKeyDown(KeyCode.Alpha2))
            UiKeyinPutQueData.Enqueue(KeyCode.Alpha2);//CharactorChangeButten2
    }

    public void PlayerInput() 
    {
        if (Input.GetMouseButton(0))
            MouseInputQueData.Enqueue(MouseInputType.Click);//mouseClick

        if (Input.GetMouseButtonUp(0))
            MouseInputQueData.Enqueue(MouseInputType.Release);//mouseClickUp

        if (Input.GetMouseButtonDown(0))
            MouseInputQueData.Enqueue(MouseInputType.Hold);//mouseClickDown

        if (Input.GetKeyDown(KeyCode.Mouse1))
            KeyinPutQueData.Enqueue(KeyCode.Mouse1);//RunCheck

        if (Input.GetKeyDown(KeyCode.R))
            KeyinPutQueData.Enqueue(KeyCode.R);//reloadOn

        if (Input.GetKeyDown(KeyCode.Q))
            KeyinPutQueData.Enqueue(KeyCode.Q);//Skill1

        if (Input.GetKeyDown(KeyCode.E))
            KeyinPutQueData.Enqueue(KeyCode.E);//Skill2

        if (Input.GetKeyDown(KeyCode.Z))
            KeyinPutQueData.Enqueue(KeyCode.Z);//shitdown

        if (Input.GetKeyDown(KeyCode.Space))
            KeyinPutQueData.Enqueue(KeyCode.Space);//Space

        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (move.magnitude > 0.1f) MoveQueData.Enqueue(move);

        float scroll = Input.GetAxis("Mouse ScrollWheel") * Speed;
        if (scroll != 0.0f) MouseScrollQueData.Enqueue(scroll);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (Mathf.Abs(mouseX) > 0.01f || Mathf.Abs(mouseY) > 0.01f)
        {
            MouseMoveQueData.Enqueue(new Vector2(mouseX, mouseY));
        }
    }
}
