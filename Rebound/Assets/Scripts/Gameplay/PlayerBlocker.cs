using UnityEngine;
using Zenject;

public class PlayerBlocker : MonoBehaviour
{
    [Inject] private readonly PlayerMovement _player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player.ResetPlayerPosition();
        }
    }
}
