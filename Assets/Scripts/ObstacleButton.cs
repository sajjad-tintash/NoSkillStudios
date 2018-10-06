using UnityEngine;
using System.Collections;

public class ObstacleButton : GameButton
{
    public Obstacle _obstacle;

    public override void TurnOn()
    {
        _obstacle.Move();
        base.TurnOn();
    }

    public override void TurnOff()
    {
        _obstacle.Move();
        base.TurnOff();
    }
}
