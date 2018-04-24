using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhysisAction : SSAction
{
    private Vector3 direction;
    public float power;
    private PhysisAction() { }
    public static PhysisAction GetSSAction(Vector3 direction, float power)
    {
        PhysisAction action = CreateInstance<PhysisAction>();
        action.direction = direction;
        action.power = power;
        return action;
    }

    public override void Update()
    {
        if (this.transform.position.y < -6)
        {
            this.destroy = true;
            this.callback.SSActionEvent(this);
        }
    }
}
