using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] GameObject weaponHolder;
    [SerializeField] Animator animator;
    [SerializeField] GameObject weaponSheath;
    public static bool isEquiped = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        StartCoroutine(Sword());
    }

    IEnumerator Sword()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            if (!isEquiped)
            {
                animator.SetTrigger("drawWeapon");
                yield return new WaitForSeconds(1);
                weaponHolder.SetActive(true);
                weaponSheath.SetActive(false);
                isEquiped = true;
            }
            else
            {
                animator.SetTrigger("sheathWeapon");
                yield return new WaitForSeconds(1);
                weaponHolder.SetActive(false);
                weaponSheath.SetActive(true);
                isEquiped = false;
            }
        }
    }
}
