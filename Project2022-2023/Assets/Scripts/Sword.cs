using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Sword : MonoBehaviour
{
    [SerializeField] private float damage = 10f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null && PlayerAttack.isAttacking)
            {
                health.TakeDamage(damage);
            }            
        }
        PlayerAttack.isAttacking = false;
    }
}
