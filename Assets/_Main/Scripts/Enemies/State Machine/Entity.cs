using UnityEngine;

public class Entity : MonoBehaviour
{
    public DataEntity data;
    public FiniteStateMachine FSM;
    public int facingDirection { get; private set; }
    public Rigidbody2D body { get; private set; }
    public Animator animator { get; private set; }
    public GameObject avatar { get; private set; }

    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    private Vector2 velocity;

    public virtual void Start()
    {
        facingDirection = 1;

        avatar = transform.Find("Avatar").gameObject;
        body = avatar.GetComponent<Rigidbody2D>();
        animator = avatar.GetComponent<Animator>();

        FSM = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        FSM.currState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        FSM.currState.PhysicsUpdate();
    }

    public virtual void SetVelocityX(float velocityX)
    {
        velocity.Set(facingDirection * velocityX, velocity.y);
        body.velocity = velocity;
    }

    public virtual void SetVelocityY(float velocityY)
    {
        velocity.y = velocityY;
        body.velocity = velocity;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, avatar.transform.right, data.wallCheckDistance, data.groundLayer);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, data.ledgeCheckDistance, data.groundLayer);
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        avatar.transform.Rotate(0f, 180f, 0f);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * data.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * data.ledgeCheckDistance));
    }
}
