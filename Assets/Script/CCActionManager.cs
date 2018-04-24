using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, IActionManager
{

    public CCAction _throw;
    protected void Start()
    {

    }
	public void throwUFO(GameObject disk,Vector3 direction, float power) {
        _throw = CCAction.GetSSAction(direction, power);
        this.RunAction(disk, _throw, this);
    }
}
