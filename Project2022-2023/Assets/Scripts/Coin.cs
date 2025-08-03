using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Transform target = null;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float xp = 30f;    
    PlayerHealth player = null;

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.Slerp(transform.position, target.position + Vector3.up, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, target.position) < 1f)
            {
                player.Level(xp);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerHealth>();
            target = other.transform;
            GetComponentInChildren<MeshCollider>().enabled = false;
        }
    }
}
