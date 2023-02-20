using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public Vector3 direction;

    private void Start()
    {
        Destroy(gameObject, 3);    
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        transform.position += direction * Time.fixedDeltaTime * Data.ThrownRocksSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CharacterKillerBot bot))
        {
            bot.Destroy();
            Destroy(gameObject);
        }
    }

}
