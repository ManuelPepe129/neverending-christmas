using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public void OnDeath()
    {
        Destroy(gameObject);
    }
}
