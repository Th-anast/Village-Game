using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb : MonoBehaviour
{
    Animator animator;
    public float value = 50f;
    bool taken;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        taken = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F) && !taken)
            {
                Heal(other);
                taken = true;
            }
        }
    }

    void Heal(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Heal(value);
            animator.SetTrigger("Interact");
            Destroy(gameObject, 1f);
        }
    }
}
