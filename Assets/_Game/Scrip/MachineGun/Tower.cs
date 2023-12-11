using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] protected List<Transform> shootPoint;
    [SerializeField] protected bool loockAtEnemy = false;
    [SerializeField] protected Transform rotation;
    [SerializeField] protected Transform bow;
    [SerializeField] protected float shootTimer;
    [SerializeField] protected float shootSpeed;
    [SerializeField] protected float damage;
    public List<Enemy> enemy;
    protected Vector3 direction;
    protected Quaternion originalRotate_1;
    protected Quaternion originalRotate_2;
    public float ShootSpeed{ get => shootSpeed; }
    public float Damage{ get => damage; }
    protected virtual void Start()
    {
        enemy.Clear();
        originalRotate_1 = rotation.rotation;
        originalRotate_2 = bow.localRotation;
    }
    protected virtual void FixedUpdate()
    {
        Rotate();
    }
    protected virtual void Rotate()
    {

    }
    protected virtual void Attack(Enemy target)
    {

    }
    protected void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            enemy.Add(other.GetComponent<Enemy>());
        }

    }
    protected void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            enemy.Remove(other.GetComponent<Enemy>());
        }
    }
}
