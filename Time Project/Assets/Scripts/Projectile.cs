using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        Debug.Log("Destroying projectile: " + this.GetInstanceID());

        Destroy(gameObject);
    }
}
