using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 1f;
    [SerializeField] private float lifetime = 10.0f;
    private float currentTimer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

        currentTimer += Time.deltaTime;

        if (currentTimer >= lifetime)
        {
            Debug.LogWarning("Projectile lifetime reached: " + this.GetInstanceID());
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.tag == "Slowdown") return;

        Debug.Log("Destroying projectile: " + this.GetInstanceID());

        Destroy(gameObject);
    }
}
