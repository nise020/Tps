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
    public Queue<KeyCode> keyinPutQueBase = new Queue<KeyCode>();
    public Queue<MouseInputType> mouseQueBase = new Queue<MouseInputType>();

    public Queue<Vector3> moveQueBase = new Queue<Vector3>();
    public void inputEvent() 
    {
        if (Input.GetMouseButton(0)) mouseQueBase.Enqueue(MouseInputType.Click);//mouseClick
        if (Input.GetMouseButtonUp(0)) mouseQueBase.Enqueue(MouseInputType.Release);//mouseClickUp
        if (Input.GetMouseButtonDown(0)) mouseQueBase.Enqueue(MouseInputType.Hold);//mouseClickDown
        if (Input.GetKeyDown(KeyCode.Mouse1)) keyinPutQueBase.Enqueue(KeyCode.Mouse1);//RunCheck
        if (Input.GetKeyDown(KeyCode.R)) keyinPutQueBase.Enqueue(KeyCode.R);//reloadOn
        if (Input.GetKeyDown(KeyCode.Q)) keyinPutQueBase.Enqueue(KeyCode.Q);//Skill1
        if (Input.GetKeyDown(KeyCode.E)) keyinPutQueBase.Enqueue(KeyCode.E);//Skill2

        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (move.magnitude > 0.1f) moveQueBase.Enqueue(move);
    }
}
