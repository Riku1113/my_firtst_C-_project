using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //変数の宣言
    float x;
    float z;
    public float moveSpeed = 3;
    public Collider weaponCollider;
    public PlayerUIManager playerUIManager;
    public GameObject gameOverText;
    public Transform target;
    public int maxHp = 100;
    int hp;
    public int maxStamina = 100;
    int stamina;
    bool isDie;
    Rigidbody rb;
    Animator animator;

    // Update関数の前に1度だけ実行される (設定)
    void Start()
    {
        hp = maxHp;
        stamina = maxStamina;
        playerUIManager.Init(this);
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        HideColliderWeapon();
    }

    // 約0.02秒に1回実行される(1フレームごと) (更新)
    void Update()
    {
        //hpが0の時に移動できなくする
        if (isDie)
        {
            return;
        }
        //キーボード入力による移動
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        //スペースキーを押したら攻撃する
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
        IncreaseStamina();
    }

    //スタミナ回復関数を定義
    void IncreaseStamina()
    {
        //スタミナの時間回復
        stamina++;
        if (stamina >= maxStamina)
        {
            stamina = maxStamina;
        }
        //回復分をUIに反映
        playerUIManager.UpdateStamina(stamina);
    }

    //Attack関数を定義
    void Attack()
    {
        if (stamina >= 40)
        {
            stamina -= 40;
            playerUIManager.UpdateStamina(stamina);
            //攻撃する際に敵の方向を向く
            LookAtTarget();
            //AttackトリガーをOnに
            animator.SetTrigger("Attack");
        }
    }

    void LookAtTarget()
    {
        //enemyとplayerとの距離をdistanceとして定義
        float distance = Vector3.Distance(transform.position, target.position);
        //距離が2以下の場合に攻撃した際のみ相手の方を向く
        if (distance <= 2f)
        {
            transform.LookAt(target);
        }
    }

    private void FixedUpdate()
    {
        //hpが0の時に移動できなくする
        if (isDie)
        {
            return;
        }
        //向きの変更
        Vector3 direction = transform.position + new Vector3(x, 0, z) * moveSpeed;
        transform.LookAt(direction);
        //速度の設定
        rb.velocity = new Vector3(x, 0, z) * moveSpeed;
        //velocityの大きさをspeedのパラメータにする
        animator.SetFloat("Speed", rb.velocity.magnitude);
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
        if (hp <= 0)
        {
            hp = 0;
            isDie = true;
            //HPが0になったらDieトリガーをonにする
            animator.SetTrigger("Die");
            //GAME OVERの字幕を表示
            gameOverText.SetActive(true);
            //動きを止める
            rb.velocity = Vector3.zero;
        }
        playerUIManager.UpdateHP(hp);
        Debug.Log("Player残りHP："+hp);
    }

    //敵からの攻撃に対する処理
    private void OnTriggerEnter(Collider other)
    {
        //hpが0の時にはdamageを受け付けない
        if (isDie)
        {
            return;
        }
        //ダメージを与えるものにぶつかった場合の処理(otherがdamagerだった場合)
        Damager damager = other.GetComponent<Damager>();
        if (damager != null)
        {
            animator.SetTrigger("Hurt");
            Damage(damager.damage);
        }
    }
}
