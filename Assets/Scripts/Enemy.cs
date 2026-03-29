using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody rb;
    private GameObject player;
    private bool isStunned = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        // ถ้าติดสถานะ Stun อยู่ ให้ข้ามการทำงาน
        if (isStunned) return;

        // เช็คผู้เล่น
        if (player != null)
        {
            Vector3 dir = player.transform.position - transform.position;
            dir.Normalize();
            rb.AddForce(dir * speed);
        }
    }

    public void Stun(float duration)
    {
        StartCoroutine(StunCoroutine(duration));
    }

    private IEnumerator StunCoroutine(float duration)
    {
        isStunned = true;

        // ความเร็วในการเคลื่อนที่และการหมุนเป็น 0 ทันที
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // รอ
        yield return new WaitForSeconds(duration);

        // ยกเลิก
        isStunned = false;
    }
}