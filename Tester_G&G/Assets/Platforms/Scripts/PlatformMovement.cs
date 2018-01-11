using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection
{
    None = 0,
    Up = 1,
    Down = 2,
    Right = 3,
    Left = 4,
}

public class PlatformMovement : MonoBehaviour {

    public int UpDownOrLeftRight;
    private Vector3 Direction;
    

    private Rigidbody rigidbody;
    private float timing;
    public float timeUntilChange=3.0f;
    public float speed=1.0f;

	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
        if (UpDownOrLeftRight == (int)MoveDirection.Up || UpDownOrLeftRight == (int)MoveDirection.Down)
        {
            Direction = new Vector3(0, VectorDirection(UpDownOrLeftRight));
        }
        else
        {
            Direction = new Vector3(VectorDirection(UpDownOrLeftRight), 0);
        }
    }

    void Update()
    {
        timing += Time.deltaTime;

        if(timing> timeUntilChange)
        {
            Direction *= -1;
            timing -= timeUntilChange;
        }
    }

    void FixedUpdate()
    {
        if(speed<=0)
        {
            speed *= -1;
        }
        rigidbody.MovePosition(transform.position + Direction * Time.deltaTime * (speed));
    }

    int VectorDirection(int number)
    {
        return number % 2 != 0 ? 1 : -1;
    }
}
