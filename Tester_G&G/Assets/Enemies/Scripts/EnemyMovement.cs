using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirectionE
{
    None = 0,
    Up = 1,
    Down = 2,
    Right = 3,
    Left = 4,
    CircleEllipseClockWise=5,
    CircleEllipseCounterClockWise=6
}

public class EnemyMovement : MonoBehaviour {

    public int moveType=0;
    public float radiusX=1;
    public float radiusY=1;
    public float speed=1;
    public float angledDownwards=0;
    public float timeUntilChange = 3.0f;
    //public bool fullRoration;

    private float angle;
    private Rigidbody rigidbody;
    private Vector3 beginningSpeed;
    private Vector3 Direction;
    private float timing;

    void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
        angle = 0;
        
        beginningSpeed = transform.position-new Vector3(radiusX,radiusY,0);

        if (moveType == (int)MoveDirection.Up || moveType == (int)MoveDirection.Down)
        {
            Direction = new Vector3(0, VectorDirection(moveType));
        }
        else
        {
            Direction = new Vector3(VectorDirection(moveType), 0);
        }
    }

    void Update()
    {
        timing += Time.deltaTime;

        if (timing > timeUntilChange)
        {
            Direction *= -1;
            timing -= timeUntilChange;
        }
    }


    void FixedUpdate ()
    {
		if(moveType==(int)MoveDirectionE.CircleEllipseClockWise)
        {
            Ellipse(1);
        }
        else if(moveType==(int)MoveDirectionE.CircleEllipseCounterClockWise)
        {
            Ellipse(-1);
        }
        else
        {
            LineMovement();
        }
	}

    void LineMovement()
    {
        if (speed <= 0)
        {
            speed *= -1;
        }
        rigidbody.MovePosition(transform.position + Direction * Time.deltaTime * (speed));
    }

    void Ellipse(int direction)
    {
        Vector3 location = new Vector3();

        angle += Mathf.PI / 20 * speed * Time.deltaTime*direction;

        location.x = (radiusX * Mathf.Cos(angle) * Mathf.Cos(angledDownwards)) - (radiusY * Mathf.Sin(angle)*Mathf.Sin(angledDownwards));
        location.y = (radiusX * Mathf.Cos(angle) * Mathf.Sin(angledDownwards)) + (radiusY * Mathf.Sin(angle) * Mathf.Cos(angledDownwards));
        location.z = 0;
        rigidbody.MovePosition(beginningSpeed + location);
        //a = radiusX
        //b = radiusY
        //t = angle
        //theta = angledDownwards
    }

    int VectorDirection(int number)
    {
        return number % 2 != 0 ? 1 : -1;
    }
}
