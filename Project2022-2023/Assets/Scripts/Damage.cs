using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    public void ActivateDamage()
    {
        rb.isKinematic = false;
    }

    public void DeactivateDamage()
    {
        rb.isKinematic = true;
    }
}
