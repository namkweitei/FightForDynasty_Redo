using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WavePanel : MonoBehaviour
{
    [SerializeField] RectTransform waveStage;
    [SerializeField] RectTransform waveStageHolder;
    [SerializeField] Image fill;
    [SerializeField] float currentWave;
    [SerializeField] Animator anim;
    [SerializeField] TextMeshProUGUI texWave;
    float currentFill;
    [Button]
    public void SetWaveStage(float count)
    {
        currentWave = count;
        foreach (RectTransform item in waveStageHolder)
        {
            Destroy(item.gameObject);
        }
        for (int i = 0; i < count - 1; i++)
        {
            RectTransform newStage = Instantiate(waveStage,waveStageHolder);
        
            newStage.localScale = new Vector3(1, 1, 1);
        }
    }
    public void SetTextWave(int wave){
        texWave.text = "Wave " + wave.ToString();
    }
    public void SetFill(float curWave)
    {
        fill.fillAmount = 1 / this.currentWave * curWave;
        currentFill = fill.fillAmount;
    }
    public void Fill(float fil)
    {
        fill.fillAmount = fil * (1 / this.currentWave) + currentFill;
    }
    public void Show()
    {
        anim.SetTrigger("Show");
    }
}
