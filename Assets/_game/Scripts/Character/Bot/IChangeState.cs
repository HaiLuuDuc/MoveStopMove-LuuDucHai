using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChangeState 
{
    void ChangeState(IState currentState);
}
