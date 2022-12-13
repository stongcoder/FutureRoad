using MechanicControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder: LevelInteractBase
{
    public Collider downCol;
    public Collider upCol;
    public CommandEntity entityDown;
    public CommandEntity entityUp;
    public override void ExecuteInit()
    {
        base.ExecuteInit();
    }
    public override IEnumerator ItorInteract()
    {
        if (inCol == downCol)
        {
            entityDown.GetTween().Start();
        }
        if (inCol == upCol)
        {
            entityUp.GetTween().Start();
        }
        yield break;
    }
}
