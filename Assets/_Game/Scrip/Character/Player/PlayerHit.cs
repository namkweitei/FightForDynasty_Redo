using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] private Image imageFill;
    [SerializeField] private float maxHealth;
    [SerializeField] private float minHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private Transform hitPointBar;
    public float CurrHealth { get => currentHealth; set => currentHealth = value; }
    private void Update()
    {
        SetHealthBar();
        if (currentHealth < maxHealth)
        {
            hitPointBar.gameObject.SetActive(true);
        }
        transform.rotation = Camera.main.transform.rotation;
    }
    public void OnInit(float maxhealth)
    {
        this.maxHealth = maxhealth;
        this.currentHealth = maxHealth;
        imageFill.fillAmount = 1;
        hitPointBar.gameObject.SetActive(false);

    }

    Tween EnableTween;

    private void SetHealthBar()
    {

        EnableTween?.Kill();
        imageFill.fillAmount = currentHealth / maxHealth;
        if (currentHealth < maxHealth / 3 * 2 && currentHealth > maxHealth / 3)
        {
            EnableTween = imageFill.DOColor(Color.yellow, 2f);
        }
        else if (currentHealth < maxHealth / 3)
        {
            EnableTween = imageFill.DOColor(Color.red, 2f);
        }
        else
        {
            EnableTween = imageFill.DOColor(Color.green, 2f);
        }
    }
}
