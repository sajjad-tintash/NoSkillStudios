using UnityEngine;
using System.Collections;

public class Candy : MonoBehaviour
{
    public bool _increaseMutation;

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void Reset()
    {
        gameObject.SetActive(true);
    }
}
