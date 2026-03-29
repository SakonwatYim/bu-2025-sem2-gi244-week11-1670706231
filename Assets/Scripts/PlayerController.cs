using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Transform focalPoint;
    public bool hasPowerUp; // Default = false
    public GameObject powerUpIndicator;

    private Rigidbody rb;
    private Coroutine boostCoroutine;
    private GameObject tempEffectIndicator;


    private InputAction moveAction;
    private InputAction smashAction;
    private InputAction breakAction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        smashAction = InputSystem.actions.FindAction("Smash");
        breakAction = InputSystem.actions.FindAction("Break");
    }

    // Update is called once per frame
    void Update()
    {
        var move = moveAction.ReadValue<Vector2>();
        rb.AddForce(move.y * speed * focalPoint.forward);
        if (breakAction.IsPressed())
        {
            rb.linearVelocity = Vector3.zero; // new Vector3(0, 0, 0)
        }

        if (tempEffectIndicator != null)
        {
            tempEffectIndicator.transform.position = transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (hasPowerUp)
            {
                var enemyRb = collision.gameObject.GetComponent<Rigidbody>();
                //var v = enemyRb.linearVelocity;
                //v.Normalize();
                var dir = enemyRb.transform.position - transform.position;
                dir.Normalize();
                enemyRb.AddForce(dir * 10, ForceMode.Impulse);
            }
        }
    }

    public void Boost(float duration)
    {
        if (boostCoroutine != null)
        {
            StopCoroutine(boostCoroutine);
        }
        boostCoroutine = StartCoroutine(BoostRoutine(duration));
    }

    IEnumerator BoostRoutine(float duration)
    {
        if (tempEffectIndicator != null)
        {
            Destroy(tempEffectIndicator);
        }
        tempEffectIndicator = Instantiate(powerUpIndicator, transform.position, Quaternion.identity);
        Debug.Log("Boost Activated");
        hasPowerUp = true;
        yield return new WaitForSeconds(duration);
        Destroy(tempEffectIndicator);
        Debug.Log("Boost Ended");
        hasPowerUp = false;
    }
}