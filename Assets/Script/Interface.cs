using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionManager
{
    void throwUFO(GameObject disk, Vector3 direction, float power);
}

public interface ISceneController
{
    void LoadResources();
}

public interface IUserAction
{
	int ShowScore();
    int ShowRound();
	void gameStart();
    void gameOver();
    void switchActionType();
}

public enum SSActionEventType : int { Started, Competeted }
public interface ISSActionCallback
{
void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0, string strParam = null, Object objectParam = null);
}
