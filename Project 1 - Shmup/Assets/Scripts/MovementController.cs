using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Vector3 objectPosition = Vector3.zero;

    Vector3 direction = Vector3.zero;

    Vector3 velocity = Vector3.zero;

    [SerializeField] float speed;

    [SerializeField] Camera cameraObject;

    float totalCamHeight;
    float totalCamWidth;

    [SerializeField] CollisionManager collisionManager;

    public Vector3 Direction
    {
        set { direction = value.normalized; }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Grab the GameObject’s starting position
        objectPosition = transform.position;

        totalCamHeight = cameraObject.orthographicSize;
        totalCamWidth = totalCamHeight * cameraObject.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if(collisionManager.playerHealth <= 0)
        {
            enabled = false;
        }
        else
        {
            objectPosition.x = Mathf.Clamp(objectPosition.x, -totalCamWidth, totalCamWidth);
            objectPosition.y = Mathf.Clamp(objectPosition.y, -totalCamHeight, totalCamHeight);
        }



        //Velocity is direction * speed * deltaTime
        velocity = direction * speed * Time.deltaTime;

        //Add velocity to position
        objectPosition += velocity;

        //Validate new calculated position

        // “Draw” this vehicle at that position
        transform.position = objectPosition;

        if(direction.x > 0)
        {
            transform.rotation = Quaternion.LookRotation(-Vector3.back, Vector3.up);
        }
        else if(direction.x < 0)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
        }

        /*
        if (direction != Vector3.zero)
        {
            //Set the vehicle's rotation to match the direction
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction * (Mathf.PI/2) );
        }
        */
    }



}
