using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Entity : ApcsNetworkBehaviour, IHudable
{
    public DataEntity data;
    public FiniteStateMachine FSM;
    public int facingDirection { get; private set; }
    public Rigidbody2D body { get; private set; }
    public Animator animator { get; private set; }
    public GameObject avatar { get; private set; }
    public Anim2State a2s { get; private set; }
    protected int lastDamageDirection { get; private set; }

    [field: SerializeField] public ItemDropper Dropper { get; private set; }
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform playerCheck;
    [SerializeField]
    private Transform groundCheck;
    private Vector2 velocity;

    [SerializeField] Canvas _hud;
    [SerializeField] Slider _healthSlider;

    float _curHealth;
    private float CurHealth
    {
        get => _curHealth;
        set
        {
            _curHealth = value;
            OnHealthChanged()?.Invoke(_curHealth);
        }
    }

    private float lastDamageTime;
    protected bool isDead;

    UnityEvent<float> _onHealthChanged = new UnityEvent<float>();

    public UnityEvent<float> OnHealthChanged() => _onHealthChanged;
    public UnityEvent<float> OnManaChanged() => null;

    private void Awake()
    {
        _hud.worldCamera = Camera.main;
        OnHealthChanged().AddListener((curHP) =>
        {
            _healthSlider.value = curHP / data.maxHealth;
        });
    }

    public virtual void Start()
    {
        facingDirection = 1;

        avatar = transform.Find("Avatar").gameObject;
        body = avatar.GetComponent<Rigidbody2D>();
        animator = avatar.GetComponent<Animator>();
        a2s = avatar.GetComponent<Anim2State>();

        CurHealth = data.maxHealth;
        lastDamageTime = Time.time;

        isDead = false;

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

    public virtual void Knockback(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        this.velocity.Set(angle.x * velocity * direction, angle.y * velocity);
        body.velocity = this.velocity;

        // body.AddForce(new Vector2(angle.x * velocity * direction, angle.y * velocity), ForceMode2D.Impulse);

        if (direction == facingDirection)
        {
            Flip();
        }
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, avatar.transform.right, data.wallCheckDistance, data.groundLayer);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, data.ledgeCheckDistance, data.groundLayer);
    }

    public virtual bool CheckPlayerInAgroRange()
    {
        var hit = Physics2D.Raycast(playerCheck.position, avatar.transform.right, data.agroRange, data.playerLayer);
        return CheckPlayerHit(hit);
    }

    public virtual bool CheckGround()
    {
        return Physics2D.Raycast(groundCheck.position,
                                 Vector2.down, data.groundCheckLength, data.groundLayer
                                 );
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        var hit = Physics2D.Raycast(playerCheck.position, avatar.transform.right, data.closeRangeActionDistance, data.playerLayer);
        return CheckPlayerHit(hit);
    }

    // public virtual void Hop(float velocity)
    // {
    //     this.velocity.Set(this.velocity.x, velocity);
    //     body.velocity = this.velocity;
    // }

    public virtual void ReceiveDamage(AttackStats attackStats)
    {
        CurHealth -= attackStats.damage;

        lastDamageTime = Time.time;
        lastDamageDirection = attackStats.position.x > avatar.transform.position.x ? -1 : 1;

        // Hop(data.damageHopSpeed);
        // Knockback(data.damageKnockbackSpeed, data.damageKnockbackAngle, lastDamageDirection)

        isDead = CurHealth <= 0;

        // TODO: Hit particle
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        avatar.transform.Rotate(0f, 180f, 0f);
        _hud.transform.Rotate(0f, 180f, 0f);
    }

    public virtual void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * data.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * data.ledgeCheckDistance));

        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * data.closeRangeActionDistance), 1f);
        Gizmos.DrawLine(groundCheck.position,
                        groundCheck.position + (Vector3)(Vector2.down * data.groundCheckLength)
                        );
#endif
    }

    public virtual void InitIcon(GameObject icon)
    {
        if (!icon)
        {
            return;
        }

        GameObject myIcon = Instantiate(icon,
                                        avatar.transform.position,
                                        Quaternion.Euler(0f, 0f, 0f)
                                        );

        myIcon.transform.SetParent(avatar.transform);
        // myIcon.transform.rotation = avatar.transform.rotation;
    }

    // public virtual void FinishedDeath()
    // {
    //     gameObject.SetActive(false);
    // }

    bool CheckPlayerHit(RaycastHit2D hit)
    {
        if (hit.collider == null)
        {
            return false;
        }

        var player = hit.collider.gameObject.GetComponent<Player>();
        if (player == null)
        {
            return false;
        }

        return !player.IsDead();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(ApcsLayerMask.DEATH))
        {
            FSM.ChangeState(a2s.deadState);
        }
    }
}