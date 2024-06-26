using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteInfo : MonoBehaviour
{
    //global variables
    //AABB
    public Vector3 min, max;

    //Circle
    public float radius;

    public SpriteRenderer spriteRenderer;

    public int health = 0;
    public int damage = 0;
    public int points = 0;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        min = spriteRenderer.bounds.min;
        max = spriteRenderer.bounds.max;

        radius = spriteRenderer.bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        min = spriteRenderer.bounds.min;
        max = spriteRenderer.bounds.max;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        if (spriteRenderer != null)
        {
            Gizmos.DrawWireCube(transform.position, spriteRenderer.bounds.size);
        }


        Gizmos.color = Color.magenta;

        if (spriteRenderer != null)
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }

    }
}