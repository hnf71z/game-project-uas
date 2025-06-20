using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class MeleeBabi : MonoBehaviour
// {
//     [SerializeField] private float attackCooldown;
//     [SerializeField] private int damage;
//     private float cooldownTimer; = Mathf.Infinity;
//     [SerializeField] private BoxCollider2D boxCollider;
//     [SerializeField] private LayerMask playerLayer;
//     private void Update()
//     {
//         cooldownTimer += Time.deltaTime;

//         if PlayerInSight()
//         {
//             if (cooldownTimer >= attackCooldown)
//             {

//             }  
//         }
//     }

//     private bool PlayerInSight()
//     {
//         RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 
//             0, Vector2.left, 0, LayerMask.GetMask("playerLayer"));
//         return hit.collider != null; // Placeholder return value
//     }

//     private void OnDrawGizmos()
//     {
//         Gizmos.color = Color.red;
//         Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.bounds.size);
//     }

// }


