using UnityEngine;
using System.Collections;

public class Candy : MonoBehaviour
{
    public bool _increaseMutation;
    public float rotateSpeed = 10f;

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void Reset()
    {
        gameObject.SetActive(true);
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate ()
    {
        transform.Rotate(0, 0, Time.deltaTime * rotateSpeed, Space.Self);
    }
}
