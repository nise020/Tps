using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Player : Charactor
{
    
    protected void inputrocessing() 
    {
        int keyinPut = Shared.InputManager.keyinPutQueBase.Count;
        int mouseinPut = Shared.InputManager.mouseQueBase.Count;
        int moveinPut = Shared.InputManager.moveQueBase.Count;

        if (keyinPut == 0 && mouseinPut == 0 && moveinPut == 0) { return; }

        while (keyinPut > 0)//key 
        {
            KeyCode type = Shared.InputManager.keyinPutQueBase.Dequeue();
            switch (type)
            {
                case KeyCode.Mouse1:
                    WalkStateChange(runState);
                    break;
                case KeyCode.R:
                    reloding(playerType);//리로드;
                    break;
                case KeyCode.Q:
                    skillAttack1(playerType);//SkillQ
                    break;
                case KeyCode.E:
                    skillAttack2(playerType);//SkillE
                    break;
            }
        }
        while (mouseinPut > 0)//mouseClick
        {
            MouseInputType type = Shared.InputManager.mouseQueBase.Dequeue();
            MouseInputType type2 = Shared.InputManager.mouseQueBase.Peek();
            switch (type) 
            {
                case MouseInputType.Click://mouseClick
                    ;
                    break;
                case MouseInputType.Release://mouseClickUp
                    ;
                    break;
                case MouseInputType.Hold://mouseClickDown
                    ;
                    break;
            }
                
        }
        while (Shared.InputManager.moveQueBase.Count > 0)//move
        {
            Vector3 type = Shared.InputManager.moveQueBase.Dequeue();
            move(charctorState,type);
            //Shared.InputManager.moveQueBase.Dequeue();
        }
    }




    InputManager input_Base = new InputManager();
    protected Queue<KeyCode> keyinPutQue => input_Base.keyinPutQueBase;
    protected Queue<MouseInputType> mouseQue => input_Base.mouseQueBase;
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
