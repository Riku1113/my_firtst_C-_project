using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Playerを追跡する
//idle……距離7以上 (speedを0)
//run……距離2以上 (speedを2)
//attack……距離2未満 (speedを0)

public class EnemyManager : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
    Animator animator;
    public Collider weaponCollider;
    public EnemyUIManager enemyUIManager;
    public GameObject gameClearText;
    public int maxHp = 100;
    int hp;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        //thisは自分自身のデータ
        enemyUIManager.Init(this);
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //agentの目的地をtargetの位置にする
        agent.destination = target.position;
        //最初は武器の当たり判定を消す
        HideColliderWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        //毎回追跡する
        agent.destination = target.position;
        //Distanceパラメータをtargetとの距離に設定
        animator.SetFloat("Distance", agent.remainingDistance);
    }

    //playerの方を向く関数
    public void LookAtTarget()
    {
        transform.LookAt(target);
    }

    //武器の当たり判定を、振り下ろしている最中のみ有効にする
    public void HideColliderWeapon()
    {
        weaponCollider.enabled = false;
    }
    public void ShowColliderWeapon()
    {
        weaponCollider.enabled = true;
    }

    void Damage(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            hp = 0;
            //hpが0の時にDieトリガーをOnにする
            animator.SetTrigger("Die");
            //2秒後に戦闘不能enemyを消去
            Destroy(gameObject, 2f);
            //GAME CLEARの字幕を表示
            gameClearText.SetActive(true);
        }
        //EnemyUIManagerのHPを更新
        enemyUIManager.UpdateHP(hp);
        Debug.Log("Enemy残りHP："+hp);
    }

    //剣がぶつかったときの処理
    //当たり判定=colliderを使う
    //OnTriggerEnterはモノがぶつかった際に呼ばれる関数
    //ぶつかったモノはotherに入る
    private void OnTriggerEnter(Collider other)
    {
        //ダメージを与えるものにぶつかった場合の処理(otherがdamagerだった場合)
        Damager damager = other.GetComponent<Damager>();
        if (damager != null)
        {
            animator.SetTrigger("Hurt");
            Damage(damager.damage);
        }
    }
}
