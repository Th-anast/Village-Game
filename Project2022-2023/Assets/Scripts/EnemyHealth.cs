using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UI;
using UnityEngine.VFX;

public class EnemyHealth : Health, IDataPersistence
{
    [SerializeField] private string id;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject[] loots;
    private float force = 100f;
    private bool dead = false;

    public void LoadData(GameData data)
    {
        data.deadEnemies.TryGetValue(id, out dead);
        if (dead) Dead(0);
    }

    public void SaveData(ref GameData data)
    {
        if (data.deadEnemies.ContainsKey(id))
        {
            data.deadEnemies.Remove(id);
        }
        data.deadEnemies.Add(id, dead);
    }

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    protected override void Start()
    {
        base.Start();
        healthBar.value = currentHealth / totalHealth;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        healthBar.value = currentHealth / totalHealth;
        animator.SetTrigger("Hit");
    }    

    protected override void Die()
    {
        base.Die();
        dead = true;
        animator.SetTrigger("Death");
        Invoke("DropLoot", 3f);
        Dead(6f);
    }

    void DropLoot()
    {
        foreach (GameObject item in loots)
        {
            Instantiate(item, transform.position + new Vector3(0,0.5f,0), transform.rotation);
            if (GetComponent<Rigidbody>() != null)
            {
                GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, 4f);
            }            
        }
    }

    void Dead(float sec)
    {
        GetComponentInParent<Enemy>().enabled = false;
        GetComponentInParent<UnityEngine.AI.NavMeshAgent>().destination = transform.position;
        GetComponentInParent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        foreach (Collider collider in GetComponentsInChildren<Collider>())
        {
            Destroy(collider);
        }
        enabled = false; //enemyhealth
        Canvas canvas = GetComponentInChildren<Canvas>();
        if (canvas != null)
        {
            // Disable the CanvasScaler and GraphicRaycaster components
            CanvasScaler canvasScaler = canvas.GetComponent<CanvasScaler>();
            if (canvasScaler != null) Destroy(canvasScaler);
            GraphicRaycaster graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
            if (graphicRaycaster != null) Destroy(graphicRaycaster);
            Destroy(canvas);    // Destroy the Canvas component
        }
        Destroy(gameObject, sec);
    }    
}
