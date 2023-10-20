using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CampCharacterHit : MonoBehaviour
{
    [SerializeField] protected Image imageFill;
    [SerializeField] protected float maxHp;
    [SerializeField] private TextMeshPro textLevel;
    public void OnInit(float maxHealth, int level)
    {
        this.maxHp = maxHealth;
        imageFill.fillAmount = 1;
        textLevel.text ="Level " + level.ToString();
    }
    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
    public void SetLevel(int level)
    {

        textLevel.text ="Level " + level.ToString();
    }
    Tween EnableTween;

    public void SetHp(float hp)
    {
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
