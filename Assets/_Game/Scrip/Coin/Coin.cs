
using UnityEngine;

public class Coin : GameUnit
{
    [SerializeField] private Vector3 rotaion;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool collect = false;
    [SerializeField] public Transform target;
    [SerializeField] private float timer;
    [SerializeField] private float time;
    // Start is called before the first frame update
    public void OnInit()
    {
        rb.isKinematic = false;
        collect = false;
        timer = time;
        AddTorque(350);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 1f)
        {
            if (!rb.isKinematic)
            {
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
            }
        }
        transform.Rotate(rotaion);
        if (collect == true)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, 10 * Time.deltaTime);
                if (Vector3.Distance(target.position, transform.position) <= 1f)
                {
                    SmartPool.Ins.Despawn(gameObject);
                    SaveLoadData.Ins.PlayerData.Coin++;
                    target = null;
                    collect = false;
                    timer = time;
                }
            }

        }

    }

    public void AddTorque(float force)
    {
        rb.AddForce(Vector3.up * force);
    }
    private void OnTriggerEnter(Collider other)
    {
        target = Player.Ins.transform;
        collect = true;
    }
}
