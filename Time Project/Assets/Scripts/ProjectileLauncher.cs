using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public Transform spawnPoint;
    public Projectile projectile;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Launching Projectile.");
            LaunchProjectile();
        }
    }

    private void LaunchProjectile()
    {
        // Create projectile at spawnPoint.
        GameObject.Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
    }
}
