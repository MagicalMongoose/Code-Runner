using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : Enemy
{
    const string LEFT = "left";
    const string RIGHT = "right";

    [SerializeField] protected Transform castPos;
    [SerializeField] protected float baseCastDist;

    [SerializeField] private float chargeSpeedMultiplier;
    [SerializeField] private float chargeDuration;
    [SerializeField] private float jumpForce;

    string facingDirection;

    Vector3 baseScale;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float detectionDistance = 5f;


    enum ChargerStates
    {
        Charger_Idle,
        Charger_Surprised,
        Charger_Charge
    }
    ChargerStates currentChargerState;

    // Start is called before the first frame update
    protected override void Start()
    {
        baseScale = transform.localScale;

        facingDirection = RIGHT;

    }
    // Update is called once per frame
    protected override void Update()
    {

    }

    protected void FixedUpdate()
    {
        float vX = moveSpeed;

        if (facingDirection == LEFT)
        {
            vX = -moveSpeed;
        }

        //move the game object
        rb.velocity = new Vector2(vX, rb.velocity.y);

        if (IsHittingWall() || IsNearEdge())
        {
            if (facingDirection == LEFT)
            {
                ChangeFacingDirection(RIGHT);
            }
            else if (facingDirection == RIGHT)
            {
                ChangeFacingDirection(LEFT);
            }
        }
    }

    protected void ChangeFacingDirection(string newDirection)
    {
        Vector3 newScale = baseScale;

        if (newDirection == LEFT)
        {
            newScale.x = -baseScale.x;
        }
        else if (newDirection == RIGHT)
        {
            newScale.x = baseScale.x;
        }

        transform.localScale = newScale;

        facingDirection = newDirection;
    }

    protected bool IsHittingWall()
    {
        bool val = false;

        float castDist = baseCastDist;

        // define the cast distance for left and right
        if (facingDirection == LEFT)
        {
            castDist = -baseCastDist;
        }

        // determine the target destination based on the cst distance
        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.blue);

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }

    protected bool IsNearEdge()
    {
        bool val = true;

        float castDist = baseCastDist;


        // determine the target destination based on the cst distance
        Vector3 targetPos = castPos.position;
        targetPos.y -= castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.red);

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = false;
        }
        else
        {
            val = true;
        }

        return val;
    }
}
