using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class CharacterHit : MonoBehaviour
{
    [SerializeField] protected Image imageFill;
    [SerializeField] protected float maxHp;
    [SerializeField] protected GameObject hitPointBar;
    public GameObject HitPointBar { get => hitPointBar; set => hitPointBar = value; }
    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
    public virtual void OnInit(float maxhealth)
    {
        this.maxHp = maxhealth;
        imageFill.fillAmount = 1;
        hitPointBar.gameObject.SetActive(false);

    }

    Tween EnableTween;

    public void SetHp(float hp)
    {
        hitPointBar.gameObject.SetActive(true);
        EnableTween?.Kill();
        imageFill.fillAmount = hp / maxHp;
        if (hp < maxHp / 3 * 2 && hp > maxHp / 3)
        {
            EnableTween = imageFill.DOColor(Color.yellow, 2f);
        }
        else if (hp < maxHp / 3)
        {
            EnableTween = imageFill.DOColor(Color.red, 2f);
        }
        else
        {
            EnableTween = imageFill.DOColor(Color.green, 2f);
        }
    }
}
