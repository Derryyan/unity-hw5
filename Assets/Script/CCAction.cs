using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCAction : SSAction
{
    private Vector3 direction;
	private Vector3 fall;
	public float gravity = 9.8F;
    private CCAction() { }
    public static CCAction GetSSAction(Vector3 direc, float power)
    {
        CCAction action = CreateInstance<CCAction>();
        action.direction = direc * power;
        return action;
    }

    public override void Update()
    {
        Vector3 newFall = fall + gravity * Time.deltaTime * Vector3.down;
		this.transform.position += (0.5F * (newFall + fall) * Time.deltaTime);
		fall = newFall;
		this.transform.position += direction*Time.deltaTime;

        if (this.transform.position.y < -6)
        {
            this.destroy = true;
            this.callback.SSActionEvent(this);
        }
    }
}
