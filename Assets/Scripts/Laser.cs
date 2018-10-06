using UnityEngine;
using System.Collections;

public class Laser : Obstacle
{
    public bool _on = true;
    
    public override void Move()
    {
        base.Move();

        if (_on)
        {
            gameObject.SetActive(false);
            _on = false;
        }
        else 
        {
            gameObject.SetActive(true);
            _on = true;
        }
    }
}
