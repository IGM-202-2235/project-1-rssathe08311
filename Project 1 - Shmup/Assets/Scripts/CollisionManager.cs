using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollisionManager : MonoBehaviour
{
    //list of sprites with spriteinfo class
    [SerializeField] List<SpriteInfo> sprites = new List<SpriteInfo>();//player should be the first index for simplicities sake

    //[SerializeField] bool aabbCol;
    [SerializeField] SpriteInfo player;

    [SerializeField] TextMesh instruction;

    [SerializeField] EnemySpawner spawner;

    //camera
    [SerializeField] Camera cameraObject;
    float totalCamHeight;
    float totalCamWidth;


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

        foreach (var sprite in sprites)
        {
            if(sprite.spriteRenderer != null)
            {
                sprite.spriteRenderer.color = Color.white;
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
                }
            }

        }

        List<int> indicesToRemove = new List<int>();
        for (int i = 1; i < sprites.Count; i++)
        {
            if (sprites[i] == null || sprites[i].gameObject == null)
            {
                indicesToRemove.Add(i);
            }
        }
        for (int i = indicesToRemove.Count - 1; i >= 0; i--)
        {
            Destroy(sprites[i]);
            sprites.RemoveAt(indicesToRemove[i]);
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