using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponController : MonoBehaviour
{
    [SerializeField] SpriteRenderer bulletPrefab;

    [SerializeField] SpriteInfo bulletInfo;

    [SerializeField] Transform fireOrigin;

    [SerializeField] float speed = 4;
    [SerializeField] CollisionManager collisionManager;

    bool shootPossible = true;

    public List<SpriteRenderer> bullets = new List<SpriteRenderer>();

    public List<SpriteInfo> spriteInfos = new List<SpriteInfo>();

    

    IEnumerator BulletTime(SpriteRenderer bullet, SpriteInfo spriteInfo)
    {
        yield return new WaitForSeconds(4f);

        if (bullet != null && spriteInfo != null) 
        {
            spriteInfos.Remove(spriteInfo);
            bullets.Remove(bullet);
            Destroy(spriteInfo.gameObject);
            Destroy(bullet.gameObject);
        }
        
    }

    public void DestroyBullet(SpriteInfo bullet)
    {
        if(bullet != null)
        {
            bullets.Remove(bullet.spriteRenderer);
            spriteInfos.Remove(bullet);
            Destroy(bullet.spriteRenderer.gameObject);
            Destroy(bullet.gameObject);
        }
        
    }

    public void Shoot()
    {
        if(!shootPossible || !enabled)
        {
            return;
        }

        SpriteInfo bullet = Instantiate(bulletInfo, fireOrigin.position, fireOrigin.rotation);
        bullets.Add(bullet.spriteRenderer);
        spriteInfos.Add(bullet);
        StartCoroutine(CanShoot());
        StartCoroutine(BulletTime(bullet.spriteRenderer, bullet));
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
        if (collisionManager != null && collisionManager.playerHealth > 0)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i] != null)
                {
                    bullets[i].transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
            }
        }
        else
        {

            foreach (var bullet in spriteInfos)
            {
                if (bullet != null)
                {
                    DestroyBullet(bullet);
                }
            }
            // Disable the WeaponController if collisionManager or playerHealth is null or <= 0
            enabled = false;
        }


    }
}
