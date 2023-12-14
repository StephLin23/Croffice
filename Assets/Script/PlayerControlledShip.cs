using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlledShip : MonoBehaviour
{
    Vector3 _ShipVelocity = Vector3.zero;
    Vector3 _ShipTargetDirection = new Vector3(0.0f,0.0f,1.0f);

    public float _Speed = 10.0f;
    public Vector3 _ShipBounds = new Vector3(5.0f,0.0f,5.0f);
    public Rigidbody _Body;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateUserInput();
        UpdateMotion();
        ClampToBounds();
        FaceMotion();
        //_Body.velocity = Vector3.zero;
        //gravity
        _ShipVelocity.y = _Body.velocity.y;
        _Body.velocity = _ShipVelocity;

    }
    private void Update()
    {
        
    }

    void UpdateUserInput()
    {
        //set the horizontal part (x) of the velocity to map the the value of the horizontal input multiplied be _Speed
        _ShipVelocity.x = Input.GetAxis("Horizontal") * _Speed;
        //set the up down part of the velocity (y) to zero as we dont want the ship to move in and out at all
        //_ShipVelocity.y = 0.0f;
        //set the vertical part (z) of the velocity to map the the value of the horizontal input multiplied be _Speed
        _ShipVelocity.z = Input.GetAxis("Vertical") * _Speed;

        
    }
    void UpdateMotion()
    {
        
        transform.position = transform.position + (Time.fixedDeltaTime * _ShipVelocity);
    }
    void ClampToBounds()
    {
        //get the current position of the ship
        Vector3 clampedPostion = transform.position;

        //if the horizontal part of the position is bigger than our bounds set it to equal the bounds
        if (clampedPostion.x > _ShipBounds.x)
        {
            clampedPostion.x = _ShipBounds.x;
        }
        //if the horizontal part of the position is smaller than our negative bounds set it to equal the negative bounds
        if (clampedPostion.x < -_ShipBounds.x)
        {
            clampedPostion.x = -_ShipBounds.x;
        }
        //do this for the vertical bounds to
        if (clampedPostion.z > _ShipBounds.z)
        {
            clampedPostion.z = _ShipBounds.z;
        }
        if (clampedPostion.z < -_ShipBounds.z)
        {
            clampedPostion.z = -_ShipBounds.z;
        }

        //set the position of the ship to our new clamped position
        transform.position = clampedPostion;
    }
    void FaceMotion()
    {
        Vector3 motionVel = _ShipVelocity;
        motionVel.y = 0.0f;
        //only run this code if the ship is moving (velocity is more than zero)
        if (motionVel.magnitude>0.1f)
        {
            //is the velocity oposite to our current direction?
            if (Mathf.Abs(Vector3.Dot(_ShipTargetDirection, motionVel.normalized)) > 0)
            {
                //interpolate towards this direction
                _ShipTargetDirection = Vector3.Lerp(_ShipTargetDirection, motionVel.normalized, 0.25f);
                //make this direction a unit vector
                _ShipTargetDirection.Normalize();
            }
            else
            {
                //the velocity IS oposite to our current direction, interpolate to a perpendicular direction!
                _ShipTargetDirection = Vector3.Lerp(_ShipTargetDirection, new Vector3(-motionVel.normalized.x,0.0f, motionVel.z), 0.05f);
                //make this direction a unit vector
                _ShipTargetDirection.Normalize();
            }
        }   
        //convert the direction to an "orientation"     
        Quaternion shipRotation = Quaternion.LookRotation(_ShipTargetDirection, Vector3.up);
        //set the actual rotation
        transform.rotation = shipRotation;
    }
}
