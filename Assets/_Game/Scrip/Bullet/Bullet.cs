
using UnityEngine;

public class Bullet : GameUnit
{
    [SerializeField] protected float speed;
    [SerializeField] protected Vector3 direction = Vector3.right;
    [SerializeField] public Character targetObject;
    [SerializeField] private TrailRenderer trailRenderer;
    private float damage;
    public float Damage
    {
        get => damage;
        set => damage = value;
    }
    public void OnInit(Character target, float damage)
    {
        trailRenderer.Clear();
        targetObject = target;
        this.damage = damage;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        direction = targetObject.transform.position - transform.position;
        direction.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10);
        transform.position = Vector3.MoveTowards(transform.position, targetObject.transform.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetObject.transform.position) < 0.1f)
        {
            targetObject.OnHit(damage);
            SmartPool.Ins.Despawn(gameObject);
        }
    }

}
