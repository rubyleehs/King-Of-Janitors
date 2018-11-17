using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerManager : MonoBehaviour{

    public CharacterAnimator[] animators;
    public Transform bodyTransform;
    public Transform[] legsTransform;

    public float walkingSpeed = 1.0f;
    public float acceleration;
    public float velocityLoss;

    private bool isAlive = true;
    private Vector2 velocity;

    private float lookAngle;
    private float moveAngle;

    new Rigidbody2D rigidbody;
    new Transform transform;

    private void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
        Initialize();
    }

    private void Initialize()
    {
        transform = this.GetComponent<Transform>();
        isAlive = true;
    }

    public void Update()
    {
        float dt = Time.deltaTime;

        if (isAlive)
        {
            Move(dt);
            Look(dt);
        }
        else
        {
            velocity -= velocity * (velocityLoss * dt);
        }

        rigidbody.velocity = velocity;
    }

    //Make move and look system
    void Look(float dt)
    {
        Vector2 mouseDelta = MainCameraLogic.mousePos - (Vector2)transform.position;
        lookAngle = Mathf.Atan2(mouseDelta.y, mouseDelta.x) * Mathf.Rad2Deg;
        bodyTransform.eulerAngles = new Vector3(0, 0, lookAngle);
    }

    void Move(float dt)
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 input = new Vector2(h, v).normalized;

        velocity = Vector2.ClampMagnitude(velocity - velocity * (velocityLoss * dt) + input * acceleration, walkingSpeed);
        moveAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        //transform.eulerAngles = new Vector3(0, 0, moveAngle);

        for (int i = 0; i < legsTransform.Length; i++)
        {
            legsTransform[i].eulerAngles = Vector3.forward * moveAngle;
        }

        if (Mathf.Abs(h) < Mathf.Epsilon && Mathf.Abs(v) < Mathf.Epsilon)
        {
            for (int i = 0; i < animators.Length; i++)
            {
                animators[i].Halt();
            }
        }
        else
        {
            for (int i = 0; i < animators.Length; i++)
            {
                animators[i].Move(transform.position, dt);
            }
        }
    }
}
