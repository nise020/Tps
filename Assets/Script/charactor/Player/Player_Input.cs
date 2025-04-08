using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Charactor
{
    protected bool mouseClick => Input.GetMouseButton(0);
    protected bool mouseClickUp => Input.GetMouseButtonUp(0);
    protected bool mouseClickDown => Input.GetMouseButtonDown(0);
    protected bool RunCheck => Input.GetKeyDown(KeyCode.Mouse1);
    protected bool reloadOn => Input.GetKeyDown(KeyCode.R);
    protected Vector3 inPutPos => new Vector3(Input.GetAxisRaw("Horizontal"), 0,
        Input.GetAxisRaw("Vertical"));
    protected bool Skill1 => Input.GetKeyDown(KeyCode.Q);
    protected bool Skill2 => Input.GetKeyDown(KeyCode.E);

    Queue<bool> inPutQueValue = new Queue<bool>();
    public void InputEventAdd(bool _input) 
    {
        //좀 더 수정이 필요함
        if (!_input) { return; }
        if (_input) 
        {
            inPutQueValue.Contains(_input);
        }
        fsmPosQue.Dequeue();
    }
}
