using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

    private Transform target;
    private float BulletTimeToLive = 3.5f;

    public void SetTarget(Transform _target) {
        target = _target;
    }

    private void FixedUpdate() {
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 180));
    }
/*     private void OnCollisionEnter2D(Collision2D other) {
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        Destroy(gameObject, 0f);
    } */

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform == target) {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null) {
            health.TakeDamage(bulletDamage);
        }
        Destroy(gameObject);
        }
    }

    private void Start () {
        Destroy (gameObject, BulletTimeToLive);
    }
}
