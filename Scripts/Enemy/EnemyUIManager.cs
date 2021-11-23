using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//EnemyのHPゲージの管理を行う
public class EnemyUIManager : MonoBehaviour
{
    //HPゲージを取得
    public Slider hpSlider;
    
    //enemyの初期HPをmaxHPにする関数(実行はEnemyManagerで実行)
    public void Init(EnemyManager enemyManager)
    {
        hpSlider.maxValue = enemyManager.maxHp;
        hpSlider.value = enemyManager.maxHp;
    }
    //HPを変更
    public void UpdateHP(int hp)
    {
        //hpを減らす
        //hpSlider.value = hp;
        //o.3秒かけてhpを減らす
        hpSlider.DOValue(hp, 0.5f);
    }
}
