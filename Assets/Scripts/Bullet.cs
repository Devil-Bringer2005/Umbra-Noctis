using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //private Transform player;
    //private float speed;
    //private float lifetime = 2f; // Bullet will be destroyed after 2 seconds

    //public void Initialize(Transform target, float bulletSpeed)
    //{
    //  player = target;
    //speed = bulletSpeed;
    //StartCoroutine(DestroyAfterTime());
    //}

    //void Update()
    //{
    //if (player != null)
    //{
    //  Vector3 direction = (player.position - transform.position).normalized;
    //transform.position += direction * speed * Time.deltaTime;
    //}
    //}

    // private IEnumerator DestroyAfterTime()
    //{
    //  yield return new WaitForSeconds(lifetime);
    //Destroy(gameObject);
    // }
    public LayerMask playerLayer;
    public Health playerHealth;
    public float damageTakenPlayer = 2;
    public SphereCollider sphereCollider;












    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            //print("Hit");
            playerHealth.TakeDamage(damageTakenPlayer);
        }
    }
}
