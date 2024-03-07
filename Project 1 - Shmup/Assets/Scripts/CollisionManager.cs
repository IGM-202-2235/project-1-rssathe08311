using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollisionManager : MonoBehaviour
{
    //list of sprites with spriteinfo class
    [SerializeField] List<SpriteInfo> sprites = new List<SpriteInfo>();//player should be the first index for simplicities sake
    [SerializeField] SpriteInfo player;

    [SerializeField] TextMesh instruction;

    [SerializeField] EnemySpawner spawner;

    [SerializeField] WeaponController weaponController;
    [SerializeField] List<SpriteInfo> bullets = new List<SpriteInfo>();

    //camera
    [SerializeField] Camera cameraObject;
    float totalCamHeight;
    float totalCamWidth;

    public int playerHealth = 100;

    public int score = 0;


    //stronger enemies have a hit counter so like they only die after 3 collisions, have a counter, can have tint to show this injury
    //1 really stong monster at the end that moves hella slow, defeating it will end the game

    // Start is called before the first frame update
    void Start()
    {
        totalCamHeight = (cameraObject.orthographicSize * 2f) / 2;
        totalCamWidth = (totalCamHeight * cameraObject.aspect);

        sprites.Add(player);

    }

    // Update is called once per frame
    void Update()
    {
        List<int> indicesToRemove = new List<int>();

        foreach (SpriteInfo sprite in sprites)
        {
            if(sprite.spriteRenderer != null)
            {
                sprite.spriteRenderer.color = Color.white;
            }
        }

        foreach (SpriteInfo bullet in bullets)
        {
            if (bullet.spriteRenderer != null)
            {
                bullet.spriteRenderer.color = Color.white;
            }
        }

        for (int i = 1; i < sprites.Count; i++)
        {
            if (sprites[i].spriteRenderer != null)
            {
                if (AABBCollisionCheck(sprites[0], sprites[i]))
                {
                    sprites[0].spriteRenderer.color = Color.red;
                    sprites[i].spriteRenderer.color = Color.red;

                    sprites[i].transform.position = new Vector3(-totalCamWidth - 1, 0, 0);

                    playerHealth -= 20;
                }
            }

        }

        for(int b = 0; b < bullets.Count; b++)
        {
            for (int i = 1; i < sprites.Count; i++)
            {
                if (sprites[i].spriteRenderer != null)
                {
                    if (AABBCollisionCheck(bullets[b], sprites[i]))
                    {
                        bullets[b].spriteRenderer.color = Color.red;
                        sprites[i].spriteRenderer.color = Color.red;

                        sprites[i].transform.position = new Vector3(-totalCamWidth - 1, 0, 0);

                        weaponController.DestroyBullet(bullets[b]);

                        bullets.RemoveAt(b);
                        b--;
                        score += 10;
                        break;


                    }
                }
                else
                {
                    sprites.RemoveAt(i);
                    i--;
                }

            }
        }

        
        for (int i = 1; i < sprites.Count; i++)
        {
            if (sprites[i] == null || sprites[i].gameObject == null)
            {
                indicesToRemove.Add(i);
            }
        }
        for (int i = indicesToRemove.Count - 1; i >= 0; i--)
        {
            Destroy(sprites[indicesToRemove[i]]);
            sprites.RemoveAt(indicesToRemove[i]);
        }


        //adds objects to the lists respectively
        foreach(SpriteInfo bullet in weaponController.spriteInfos)
        {
            if (!bullets.Contains(bullet)){
                bullets.Add(bullet);
            }
        }

        //Update sprites list with new sprites
        foreach (SpriteInfo newSprite in spawner.sprites)
        {
            if (!sprites.Contains(newSprite))
            {
                sprites.Add(newSprite);
            }
        }


    }

    bool AABBCollisionCheck(SpriteInfo spriteA, SpriteInfo spriteB)
    {
        bool collision = false;

        if ((spriteB.min.x < spriteA.max.x) && (spriteB.max.x > spriteA.min.x) && (spriteB.max.y > spriteA.min.y) && (spriteB.min.y < spriteA.max.y))
        {
            collision = true;
        }

        return collision;
    }

    bool CircleCollision(SpriteInfo spriteA, SpriteInfo spriteB)
    {
        //spriteA.transform.position = center of the object
        bool collision = false;

        float xDist = Mathf.Pow(Mathf.Abs(spriteA.transform.position.x - spriteB.transform.position.x), 2);
        float yDist = Mathf.Pow(Mathf.Abs(spriteA.transform.position.y - spriteB.transform.position.y), 2);

        float distance = Mathf.Sqrt(xDist + yDist);

        float minDist = spriteA.radius + spriteB.radius;

        if (distance < minDist)
        {
            collision = true;
        }

        return collision;
    }
}