using UnityEngine;
using System.Collections;

public class LiftButton : GameButton
{
    public Lift _lift;

    public override void TurnOn()
    {
        _lift.Move();
        base.TurnOn();
    }

    public override void TurnOff()
    {
        _lift.Move();
        base.TurnOff();
    }
}
