using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;
    private PlayerAnim player;
    private Skeleton skeleton;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerAnim>();
        skeleton = GetComponentInParent<Skeleton>(); //procura no objeto pai para puxar o script Skeleton
    }

    public void PlayerAnim(int value)
    {
        anim.SetInteger("transition", value);
    }

    public void Attack()
    {
        if (!skeleton.isDead)
        {
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);

            if(hit != null)
            {//detecta colisão com player
                player.OnHit();
            }
        }
        
    }
    public void OnHit()
    {
        if(skeleton.currenHealth <= 0)
        {
            skeleton.isDead = true;
            anim.SetTrigger("death");
            //aqui destoi todoo objeto não so o gameobject
            Destroy(skeleton.gameObject, 1f);
        }
        else
        {
            anim.SetTrigger("hit");
            skeleton.currenHealth--;

            skeleton.healthBar.fillAmount = skeleton.currenHealth / skeleton.totalHealth;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
