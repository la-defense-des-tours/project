using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 75f;
    private Transform target;
    private Collider targetCollider;

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 targetCenter = targetCollider.bounds.center;
        Vector3 direction = targetCenter - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    public void Seek(Transform _target)
    {
        target = _target;
        targetCollider = _target.GetComponent<Collider>();
    }

    void HitTarget()
    {
        Destroy(gameObject);
    }
}
