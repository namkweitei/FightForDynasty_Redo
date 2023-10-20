
using System;
using UnityEngine;


public class Flag : MonoBehaviour
{
    [SerializeField] private Transform flag;
    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform bottomPoint;
    [SerializeField] private bool isActive;
    [SerializeField] private bool isDown;
    [SerializeField] private Transform playerFlag;
    [SerializeField] private Transform enemyFlag;
    [SerializeField] private GameObject ring;
    [SerializeField] private float timeActive = 1f;
    Map map;
    public Map Map { get => map; set => map = value; }
    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        ring.SetActive(true);
        isDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            timeActive -= Time.deltaTime;
            if (timeActive <= 0)
            {
                ActivePlayerFlag();
            }
        }
    }
    private void ActivePlayerFlag()
    {
        if (isDown)
        {
            enemyFlag.position = Vector3.MoveTowards(enemyFlag.position, bottomPoint.position, Time.deltaTime);
            if (Vector3.Distance(enemyFlag.position, bottomPoint.position) < Constants.Distance_Zero)
            {
                isDown = false;
                playerFlag.gameObject.SetActive(true);
                enemyFlag.gameObject.SetActive(false);
            }
        }
        else
        {
            playerFlag.position = Vector3.MoveTowards(playerFlag.position, topPoint.position, Time.deltaTime);
            if (Vector3.Distance(playerFlag.position, topPoint.position) < Constants.Distance_Zero)
            {
                map.CloseMap();
                ring.SetActive(false);
                isActive = false;
                this.enabled = false;
                gameObject.SetActive(false);
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = false;

        }
    }
}
