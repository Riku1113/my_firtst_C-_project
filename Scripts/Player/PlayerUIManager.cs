using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerUIManager : MonoBehaviour
{
    public Slider hpSlider;
    public Slider staminaSlider;

    public void Init(PlayerManager playerManager)
    {
        hpSlider.maxValue = playerManager.maxHp;
        hpSlider.value = playerManager.maxHp;
        //スタミナの初期値の設定
        staminaSlider.maxValue = playerManager.maxStamina;
        staminaSlider.value = playerManager.maxStamina;
    }
    
    public void UpdateHP(int hp)
    {
        //hpSlider.value = hp;
        //HPを0.5秒かけて減らす
        hpSlider.DOValue(hp, 0.5f);
    }

        public void UpdateStamina(int stamina)
    {
        //スタミナを0.5秒かけて減らす
        staminaSlider.DOValue(stamina, 0.5f);
    }
}
