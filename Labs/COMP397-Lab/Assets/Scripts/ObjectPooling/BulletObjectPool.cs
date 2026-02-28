using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : PersistentSingleton<BulletObjectPool>
{
    [SerializeField] private Bullet bulletPrefab;
    private Queue<Bullet> pool = new Queue<Bullet>();

    public Bullet Get()
    {
        if (pool.Count == 0)
        {
            AddBullet(1);
        }
        return pool.Dequeue();
    }

    private void AddBullet(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var prefab = Instantiate(bulletPrefab);
            prefab.gameObject.SetActive(false);
            pool.Enqueue(prefab);
        }
    }

    public void ReturnToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        pool.Enqueue(bullet);
    }
}
