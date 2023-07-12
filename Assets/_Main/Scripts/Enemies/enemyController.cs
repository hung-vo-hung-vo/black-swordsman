using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    [SerializeField] EnemyDataSO _enemyData;
    [SerializeField] Rigidbody2D _body;
    [SerializeField] Animator _animator;
    [SerializeField] SpriteRenderer _avatar;
    public float HealthPoint { get; private set; }

    void Start()
    {
        // _enemyData = GetComponent<EnemyDataSO>();
        HealthPoint = _enemyData.MaxHealthPoint;
        _body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (IsFacingRight())
        {
            _body.velocity = new Vector2(_enemyData.RunSpeed, _body.velocity.y);
        }
        else
        {
            _body.velocity = new Vector2(-_enemyData.RunSpeed, _body.velocity.y);
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Debug.Log("Trigger Exit");
        transform.localScale = new Vector2(-(Mathf.Sign(_body.velocity.x)), transform.localScale.y);
    }
}
