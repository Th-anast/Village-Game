using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWave : MonoBehaviour
{
    private Animator animator;
    private new BoxCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider>();
    }      

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("Wave");
            collider.enabled = false;
        }
    }
}
