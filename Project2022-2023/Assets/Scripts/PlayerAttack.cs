using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Animator animator;
    public static bool isAttacking = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EquipmentSystem.isEquiped)
            {
                animator.SetTrigger("Attack");          
                isAttacking = true;
            }
        }
    }
}
