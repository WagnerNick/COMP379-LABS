using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float timer;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            //Destroy(gameObject);
            BulletObjectPool.Instance.ReturnToPool(this);
        }
    }

    private void OnEnable()
    {
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1.5f)
        {
            BulletObjectPool.Instance.ReturnToPool(this);
        }
    }
}
