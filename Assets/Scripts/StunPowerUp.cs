// StunPowerUp.cs
using System.Collections;
using UnityEngine;

public class StunPowerUp : MonoBehaviour
{
    public float stunDuration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // หา Enemy ทุกตัวในฉาก แล้ว Stun
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (Enemy enemy in enemies)
            {
                enemy.Stun(stunDuration);
            }

            Destroy(gameObject);
        }
    }
}