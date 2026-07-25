using System.Collections.Generic;
using UnityEngine;

public class SwingHitbox : MonoBehaviour
{

    public List<Collider> inside;

    void OnTriggerEnter(Collider other)
    {
        inside.Add(other);
    }

    void OnTriggerExit(Collider other)
    {
        inside.Remove(other);
    }
}
