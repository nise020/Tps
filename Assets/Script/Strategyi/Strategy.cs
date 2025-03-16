using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Strategy 
{
    protected State State;
    abstract public void Skill(int _number);
    public void init(State _state) 
    {
        State = _state;
    }
}
