
using UnityEngine;

public class Bullet : GameUnit
{
    [SerializeField] protected float speed;
    [SerializeField] protected Vector3 direction = Vector3.right;
    [SerializeField] public Character targetObject;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] bool isTargetEnemy;
    [SerializeField] Vector3 indexPos;
    private float damage;
    public bool IsTargetEnemy { get => isTargetEnemy; set => isTargetEnemy = value; }


    public void OnInit(Character target, float damage)
    {
        indexPos = transform.position;
        trailRenderer.Clear();
        targetObject = target;
        this.damage = damage;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(targetObject != null){
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
        }else{
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, indexPos) > 7f)
            {
                SmartPool.Ins.Despawn(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(targetObject == null){
            other.GetComponent<Character>().OnHit(damage);
            SmartPool.Ins.Despawn(gameObject);
        }
    }
}
