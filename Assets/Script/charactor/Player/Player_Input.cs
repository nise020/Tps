using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Charactor
{
    Input_Base input_Base = new Input_Base();
    protected Queue<KeyCode> keyinPutQue => input_Base.keyinPutQueBase;
    protected Queue<InputType> mouseQue => input_Base.mouseQueBase;
    protected Queue<Vector3> moveQue => input_Base.moveQueBase;





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
    Queue<Vector3> moveQueValue = new Queue<Vector3>();
    public void InputEventAdd(bool _input) 
    {
        //좀 더 수정이 필요함
        if (!_input) { return; }
        if (_input) 
        {
            inPutQueValue.Enqueue(_input);
        }
        bool vector = inPutQueValue.Peek();
        fsmPosQue.Dequeue();
    }
    public void InputMoveAdd(Vector3 _input)
    {
        //좀 더 수정이 필요함
        if (_input.magnitude > 0.1f) { return; }
        if (_input.magnitude < 0.1f)
        {
            moveQueValue.Enqueue(_input);
        }
        Vector3 vector = moveQueValue.Peek();
        moveQueValue.Dequeue();
    }
}
