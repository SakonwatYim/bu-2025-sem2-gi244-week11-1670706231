using UnityEngine;

public class BoostPowerUp : MonoBehaviour
{
    public float boostDuration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.Boost(boostDuration);
                Destroy(gameObject);
            }
        }
    }
}