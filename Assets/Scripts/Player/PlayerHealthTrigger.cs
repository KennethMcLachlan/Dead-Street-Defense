using UnityEngine;

public class PlayerHealthTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _player.GetComponent<HealthHandler>().Damage(1);
        }
    }
}
