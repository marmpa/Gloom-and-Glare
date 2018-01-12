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

    public int moveType;
    public float radiusX;
    public float radiusY;
    public float speed;
    public float angledDownwards;
    public bool fullRoration;

    private float angle;
    private Rigidbody rigidbody;
    private Vector3 beginningSpeed;
	
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
        angle = 0;

        beginningSpeed = transform.position;
	}
	
	
	void FixedUpdate ()
    {
		if(moveType==(int)MoveDirectionE.CircleEllipseClockWise)
        {
            Ellipse();
        }
	}

    void Ellipse()
    {
        Vector3 location = new Vector3();

        angle += Mathf.PI / 20 * speed * Time.deltaTime;

        location.x = (radiusX * Mathf.Cos(angle) * Mathf.Cos(angledDownwards)) - (radiusY * Mathf.Sin(angle)*Mathf.Sin(angledDownwards));
        location.y = (radiusX * Mathf.Cos(angle) * Mathf.Sin(angledDownwards)) + (radiusY * Mathf.Sin(angle) * Mathf.Cos(angledDownwards));
        location.z = 0;

        rigidbody.MovePosition(beginningSpeed + location);


        //a = radiusX
        //b = radiusY
        //t = angle
        //theta = angledDownwards

    }
}
