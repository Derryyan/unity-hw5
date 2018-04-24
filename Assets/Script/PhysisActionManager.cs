using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysisActionManager : SSActionManager,  IActionManager
{

    public PhysisAction _throw;

    protected void Start()
    {
    }
    public void throwUFO (GameObject disk,Vector3 direction, float power) {
        _throw = PhysisAction.GetSSAction(direction, power);
		disk.GetComponent<Rigidbody>().AddForce(direction*power,ForceMode.Impulse);
        disk.GetComponent<Rigidbody>().useGravity = true;
        this.RunAction(disk, _throw, this);
    }
}
