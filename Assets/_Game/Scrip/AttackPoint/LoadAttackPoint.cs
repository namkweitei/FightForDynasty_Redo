
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class LoadAttackPoint : MonoBehaviour
{
    [SerializeField] public Image loadCircle;
    [SerializeField] public float loadTime = 2.5f;
    [SerializeField] private bool isLoad;
    [SerializeField] private Map map;
    public Map Map { get => map; set => map = value; }
    public float shrinkDuration = 1.0f;

    private void Update()
    {
        if (isLoad && !Player.Ins.GetMove())
        {
            loadCircle.fillAmount += 1 / loadTime * Time.fixedDeltaTime * 2;
            if (loadCircle.fillAmount >= 1)
            {
                isLoad = false;
                transform.DOScale(0, 0.5f).OnComplete(() => map.OpenMap());
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        isLoad = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isLoad = false;
        loadCircle.fillAmount = 0;
    }

}
