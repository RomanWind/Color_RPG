using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed;

    public Vector3 direction;
    public float Damage {get; set;}
    
    private void Update()
    {
        transform.Translate(direction * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<IDamagable>()?.TakeDamage(Damage);
        Destroy(gameObject);
    }
}
