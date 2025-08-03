using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageDeal : MonoBehaviour
{
    [SerializeField] GameObject damageDealer;

    public void ActivateDamageDealer()
    {
        if (!PlayerAttack.isAttacking)
        {
            damageDealer.SetActive(true);
        }
        else return;
    }

    public void DeactivateDamageDealer()
    {
        if (!PlayerAttack.isAttacking)
        {
            damageDealer.SetActive(false);
        }
        else return;
    }
}
