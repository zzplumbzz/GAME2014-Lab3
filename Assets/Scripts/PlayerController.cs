using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public BulletManager bulletManager;

    public float horizontalBoundary;
    public float horizontalSpeed;
    public float maxSpeed;
    public float horizontalTValue;


    private Rigidbody2D m_rigidbody;
    private Vector3 touchesEnd;

    // Start is called before the first frame update
    void Start()
    {
        touchesEnd = new Vector3();

        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _FireBullet();
        _Move();
        _CheckBounds();
    }

    private void _FireBullet()
    {
        if(Time.frameCount % 40 == 0)
        {
            bulletManager.GetBullet(transform.position);
        }
        
    }

    private void _Reset()
    {
       
    }

    private void _Move()
    {
        float direction = 0.0f;
        //Touch firstTouch;


        foreach (var touch in Input.touches)
        {
            var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

            if (worldTouch.x > transform.position.x)
            {//direction is positive
                direction = 1.0f;
            }

            if (worldTouch.x < transform.position.x)
            {// direction is negative
                direction = -1.0f;
            }

            touchesEnd = worldTouch;
        }

           // firstTouch = touch;




            if (Input.GetAxis("Horizontal") >= 0.1f) 
            {//direction is positive
                direction = 1.0f;
            }

            if (Input.GetAxis("Horizontal") <= -0.1f)
            {// direction is negative
                direction = -1.0f;
            }
        
        Vector2 newVelocity = m_rigidbody.velocity + new Vector2(direction * horizontalSpeed, 0.0f);
        m_rigidbody.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
        m_rigidbody.velocity *= 0.99f;

       // Vector2.Lerp(transform.position, Touch.position, 0.1f);
       if (touchesEnd.x != 0.0f)
        {
            transform.position = new Vector2(Mathf.Lerp(transform.position.x, touchesEnd.x, horizontalTValue), transform.position.y);
            //Vector2.Lerp(transform.position, touchesEnd, 0.1f);
        }
    }

    private void _CheckBounds()
    {
        //check right bounds
        if(transform.position.x >= horizontalBoundary)
        {
            transform.position = new Vector3(horizontalBoundary, transform.position.y, 0.0f);
        }
        //check left bounds
        if (transform.position.x <= -horizontalBoundary)
        {
            transform.position = new Vector3(-horizontalBoundary, transform.position.y, 0.0f);
        }
    }
}
