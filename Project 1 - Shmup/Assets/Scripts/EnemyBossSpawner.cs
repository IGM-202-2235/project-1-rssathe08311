using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject creaturePrefab;

    [SerializeField]
    SpriteInfo creatureSprite;

    [SerializeField] Camera cameraObject;
    float totalCamHeight;
    float totalCamWidth;

    //List of enemies
    public List<GameObject> enemies = new List<GameObject>();

    public List<SpriteInfo> sprites = new List<SpriteInfo>();

    //Enemy movement variables
    Vector3 direction = Vector3.left;

    Vector3 velocity = Vector3.zero;

    [SerializeField] float speed;

    //Countdown variables
    static float duration = 15f;
    float time = duration;




    // Start is called before the first frame update
    void Start()
    {
        //SpawnEnemy();

        totalCamHeight = (cameraObject.orthographicSize * 2f) / 2;
        totalCamWidth = (totalCamHeight * cameraObject.aspect);

    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0.0f)
        {
            SpawnEnemy();
            time = duration;
        }

        time -= Time.deltaTime;

        velocity = direction * speed * Time.deltaTime;

        if (enemies.Count > 0)
        {
            for (int i = (enemies.Count - 1); i >= 0; i--)
            {
                if (enemies[i].transform.position.x <= -totalCamWidth)
                {
                    Destroy(enemies[i]);
                    enemies.RemoveAt(i);
                    // Remove corresponding SpriteInfo from the sprites list
                    if (i < sprites.Count)
                    {
                        Destroy(sprites[i].gameObject);
                        sprites.RemoveAt(i);
                    }
                }
                else
                {
                    enemies[i].transform.position += velocity;
                    sprites[i].transform.position += velocity;
                }
            }
        }

    }

    void SpawnEnemy()
    {
        float yPos = Random.Range(-5f, 3f);
        Vector3 position = new Vector3(totalCamWidth, yPos, 0f);

        //enemies.Add(Instantiate(creaturePrefab, position, Quaternion.identity));
        SpriteInfo newCreature = Instantiate(creatureSprite, position, Quaternion.identity);
        sprites.Add(newCreature);
        enemies.Add(newCreature.gameObject);
    }

    public void DestroyEnemy(int index)
    {
        Destroy(enemies[index]);
        enemies.RemoveAt(index);
        // Remove corresponding SpriteInfo from the sprites list
        if (index < sprites.Count)
        {
            Destroy(sprites[index].gameObject);
            sprites.RemoveAt(index);
        }
    }
}
