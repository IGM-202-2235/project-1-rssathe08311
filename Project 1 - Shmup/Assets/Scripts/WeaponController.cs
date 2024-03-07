using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] SpriteRenderer bulletPrefab;

    [SerializeField] Transform fireOrigin;

    [SerializeField] float speed = 4;

    bool shootPossible = true;

    List<SpriteRenderer> bullets = new List<SpriteRenderer>();

    IEnumerator BulletTime(SpriteRenderer bullet)
    {
        yield return new WaitForSeconds(4f);
        Destroy(bullet.gameObject);
        bullets.Remove(bullet);
    }

    public void Shoot()
    {
        if(!shootPossible)
        {
            return;
        }

        SpriteRenderer bullet = Instantiate(bulletPrefab, fireOrigin.position, fireOrigin.rotation);
        bullets.Add(bullet);
        StartCoroutine(CanShoot());
        StartCoroutine(BulletTime(bullet));
    }

    // Update is called once per frame
    IEnumerator CanShoot()
    {
        shootPossible = false;
        yield return new WaitForSeconds(.5f);
        shootPossible = true;
    }

    private void Update()
    {
        for(int i = 0; i < bullets.Count; i++)
        {
            bullets[i].transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}
