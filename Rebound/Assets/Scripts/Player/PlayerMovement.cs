using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _playerRigidBody;
    private float _movementSpeedAbs;
    private float _movementSpeedSign;

    private void Awake()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
    }

    public void ReversePlayerSpeedSign()
    {
        _movementSpeedSign = -_movementSpeedSign;
        _playerRigidBody.velocity = new Vector2(_movementSpeedSign * _movementSpeedAbs, 0f);
    }

    public void SetPlayerSpeedAbs(float newSpeedAbs)
    {
        _movementSpeedAbs = newSpeedAbs;
        _playerRigidBody.velocity = new Vector2(_movementSpeedSign * _movementSpeedAbs, 0f);
    }

    public void SetInitialSpeedSign(float initialSpeedSign)
    {
        _movementSpeedSign = initialSpeedSign;
    }
}
