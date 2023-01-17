using UnityEngine;

public class DrawGizmos : MonoBehaviour
{
    public PlayerConfig config;

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, config.radius.Amount.Value);
    }
}
