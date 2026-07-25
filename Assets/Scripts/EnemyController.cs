using System;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    PlayerController target;
    CharacterController character;

    public float speed;
    public float health;

    MeshRenderer meshRenderer;

    [Header("Damage Effects")]
    public float damageEffectDecay;
    public AudioClip deathSound;
    float damageEffectValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = FindObjectsByType<PlayerController>(FindObjectsSortMode.None).First();
        character = GetComponent<CharacterController>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Look rotation
        float angle = Mathf.Atan2(target.transform.position.x - transform.position.x, target.transform.position.z - transform.position.z);
        transform.rotation = Quaternion.Euler(Vector3.up * angle * Mathf.Rad2Deg);

        // Move towards target
        character.SimpleMove(transform.forward * speed);

        // Apply damage effects
        meshRenderer.material.SetColor("_EmissionColor", new Color(1, 1, 0) * damageEffectValue);
        damageEffectValue = Mathf.Lerp(damageEffectValue, 0, damageEffectDecay * Time.deltaTime);
    }

    internal void ApplyDamage(DamageData damage)
    {
        health -= damage.amount;
        damageEffectValue = 1f;

        if (health <= 0)
        {
            Die();
        }
    }

    internal void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Destroy(gameObject);
    }
}

public struct DamageData
{
    public GameObject source;
    public float amount;

    public DamageData(GameObject source, float amount)
    {
        this.source = source;
        this.amount = amount;
    }
}