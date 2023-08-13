using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class JumpPlayer : MonoBehaviour
{
    PlayerDataSO _playerData;
    UnityAction _jumpFunc;

    enum JumpState { onGround, singleJump, fall }
    JumpState _jumpState;
    StatAgent _stat;

    public void Init(StatAgent stat, PlayerDataSO playerData, UnityAction jumpFunc)
    {
        _stat = stat;
        _playerData = playerData;
        _jumpFunc = jumpFunc;
    }

    public void Do()
    {
        if (_jumpState == JumpState.fall)
        {
            return;
        }

        if (_jumpState == JumpState.onGround)
        {
            _jumpFunc?.Invoke();
            StartCoroutine(IESingleJump());
        }
        else
        {
            if (_stat.ManaPoint < _playerData.DoubleJumpManaCost)
            {
                return;
            }

            _stat.UpdateMana(-_playerData.DoubleJumpManaCost);
            _jumpFunc?.Invoke();
            _jumpState = JumpState.fall;
        }
    }

    IEnumerator IESingleJump()
    {
        _jumpState = JumpState.singleJump;

        yield return new WaitForSeconds(_playerData.DoubleJumpWaitingTime);

        if (_jumpState == JumpState.singleJump)
        {
            _jumpState = JumpState.fall;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(ApcsLayerMask.FOREGROUND))
        {
            _jumpState = JumpState.onGround;
        }
    }
}