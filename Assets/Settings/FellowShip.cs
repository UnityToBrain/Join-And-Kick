// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using TMPro;
// using UnityEngine;
//
// public class FellowShip : MonoBehaviour
// {
//     private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
//     public Renderer character_rend;
//     public Animator character_animator;
//     private Rigidbody rb;
//     public GameObject Particle_Death;
//     private Transform Boss;
//     private int AttackAnimationPos;
//     private CapsuleCollider CapsuleColliderComponent;
//     public int life;
//     public Transform EnemyChosenTroop;
//     public bool LockToTarget,GunMode,AttackToTroop,AttackToBoss;
//
//     private Vector3 TroopDistance,targetDistance;
//     public GameObject[] gun;
//     public Rigidbody Bulet;
//     public int Ammo;
//     private List<Rigidbody> BuletLst = new List<Rigidbody>();
//     public float MaxDistanceOfEnemey,MinDistanceOfEnemey;
//     private Camera MainCamera;
//     public Vector3 PreCamPos;
//     public Quaternion MainCamRotation;
//     public ParticleSystem GrabGun;
//     private void Start()
//     {
//         MainCamera = Camera.main;
//         life = 30;
//         
//         CapsuleColliderComponent = GetComponent<CapsuleCollider>();
//         rb = GetComponent<Rigidbody>();
//         
//         if (plrManager.PlrManagerInstance.BossFight)
//         {
//             Boss = GameObject.FindGameObjectWithTag("enemy").transform;
//             MaxDistanceOfEnemey = 15f;
//             MinDistanceOfEnemey = 5f;
//         }
//         else
//         {
//             MaxDistanceOfEnemey = 15f;
//             MinDistanceOfEnemey = 4f;
//         }
//         
//         character_animator = transform.GetComponent<Animator>();
//         character_rend = transform.GetComponent<Renderer>();
//         AttackAnimationPos = 1;
//         
//     }
//
//     private void Update()
//     {
//         if (plrManager.PlrManagerInstance.StartToFight)
//         {
//               if (plrManager.PlrManagerInstance.BossFight )
//               {
//                   var EnemyDistance = Boss.position - transform.position;
//
//                   if (!GunMode)
//                   {
//                       if (EnemyDistance.magnitude < MaxDistanceOfEnemey && EnemyDistance.magnitude > MinDistanceOfEnemey )
//                       {
//                           if (!AttackToBoss | EnemyDistance.magnitude > MinDistanceOfEnemey + 1f)
//                           {
//                               transform.parent = null;
//                               transform.position = Vector3.MoveTowards(transform.position,Boss.position,10f * Time.deltaTime);  
//                           }
//                        
//                           var pos =  new Vector3(Boss.position.x,transform.position.y,Boss.position.z) - transform.position;
//            
//                           transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(pos,Vector3.up), 10f * Time.deltaTime);
//                           character_animator.SetFloat("Pos X", 1f);
//                       }
//                       if (EnemyDistance.magnitude <= MinDistanceOfEnemey)
//                       {
//                           AttackToBoss = true;
//                           var pos =  new Vector3(Boss.position.x,transform.position.y,Boss.position.z) - transform.position;
//                           transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(pos,Vector3.up), 10f * Time.deltaTime);
//
//                           if (character_animator.GetInteger("Attack") == 0)
//                           {
//                               //rb.constraints = RigidbodyConstraints.FreezeAll;
//                               CapsuleColliderComponent.center = new Vector3(0f,0.68f,0f);
//                               CapsuleColliderComponent.height = 1.34f;
//                               CapsuleColliderComponent.radius = 0.3f;
//                           }
//                          
//                           character_animator.SetInteger("Attack",Random.Range(1,3));
//                       }
//                       
//                   }
//                   else
//                   {
//                       if (EnemyDistance.magnitude < MaxDistanceOfEnemey && Ammo > 0)
//                       {
//                           transform.parent = null;
//                          var pos =  new Vector3(Boss.position.x,transform.position.y,Boss.position.z) - transform.position;
//                          transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(pos,Vector3.up), 30f * Time.deltaTime);
//                          
//                       }
//                  
//                   }
//                   
//                   if (plrManager.PlrManagerInstance.enemyTeamMate.Count == 0)
//                   {
//                       // character_animator.SetInteger("Attack", 0);
//                       // character_animator.SetFloat("Pos X", 0f);
//                       // character_animator.SetFloat("Pos Y", 0f);
//                       character_animator.SetBool("dance", true);
//                       CapsuleColliderComponent.radius = 0.1f;
//                   
//                   }
//
//               }
//               else // attack to troops
//               {
//                   if (!GunMode)
//                   {
//                       if (!LockToTarget)
//                           for (int i = 0; i < plrManager.PlrManagerInstance.enemyTeamMate.Count; i++)
//                           {
//                               TroopDistance = plrManager.PlrManagerInstance.enemyTeamMate.ElementAt(i).transform.position - transform.position;
//                             
//                               if (TroopDistance.magnitude < MaxDistanceOfEnemey && TroopDistance.magnitude > MinDistanceOfEnemey)
//                               {
//                                   EnemyChosenTroop = plrManager.PlrManagerInstance.enemyTeamMate.ElementAt(i).transform;
//                                   LockToTarget = true;
//                                   break;
//                               }
//                           }
//                       
//                       if (EnemyChosenTroop != null)
//                       {
//                           targetDistance = EnemyChosenTroop.position - transform.position;
//                           transform.parent = null;
//                           
//                       }
//                       
//                       if (LockToTarget && targetDistance.magnitude > MinDistanceOfEnemey && AttackToTroop)
//                       {
//                           transform.position = Vector3.MoveTowards(transform.position, EnemyChosenTroop.position, 10f * Time.deltaTime);
//                       
//                           var pos = new Vector3(EnemyChosenTroop.position.x, transform.position.y, EnemyChosenTroop.position.z) - transform.position;
//                       
//                           transform.rotation = Quaternion.Slerp(transform.rotation,
//                               Quaternion.LookRotation(pos, Vector3.up), 10f * Time.deltaTime);
//                           character_animator.SetFloat("Pos X", 1f);
//                          // print("run for attack");
//                       }
//                   
//                       if (LockToTarget && targetDistance.magnitude <= MinDistanceOfEnemey && plrManager.PlrManagerInstance.enemyTeamMate.Count > 0f)
//                       {
//                           var pos = new Vector3(EnemyChosenTroop.position.x, transform.position.y, EnemyChosenTroop.position.z) - transform.position;
//                           transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pos, Vector3.up), 10f * Time.deltaTime);
//                       
//                           if (character_animator.GetInteger("Attack") == 0)
//                           {
//                               CapsuleColliderComponent.center = new Vector3(0f, 0.68f, 0f);
//                               CapsuleColliderComponent.height = 1.34f;
//                               CapsuleColliderComponent.radius = 0.3f;
//                           }
//                       
//                           character_animator.SetInteger("Attack", Random.Range(1, 3));
//
//                       }
//                       
//                       // if (!LockToTarget && GameManagerCls.AutoFire) // attacks with hand and leg
//                       // {
//                       //     Character.SetInteger("Attack",0);
//                       //     Character.SetFloat("Pos Y", 0f); 
//                       //     Character.SetFloat("Pos X", 0f);
//                       // }
//                  
//                   }
//                   else
//                   {
//                       if (!LockToTarget)
//                           for (int i = 0; i < plrManager.PlrManagerInstance.enemyTeamMate.Count; i++)
//                           {
//                               TroopDistance = plrManager.PlrManagerInstance.enemyTeamMate.ElementAt(i).transform.position - transform.position;
//
//                               if (TroopDistance.magnitude < 30f | BuletLst.Count > 0  && TroopDistance.magnitude > 10f)
//                               {
//                                   EnemyChosenTroop = plrManager.PlrManagerInstance.enemyTeamMate.ElementAt(i).transform;
//                                   LockToTarget = true;
//                                   break;
//                               }
//                           }
//                    
//                       if (EnemyChosenTroop != null )
//                       {
//                           targetDistance = EnemyChosenTroop.position - transform.position;
//                           transform.parent = null;
//                           if (LockToTarget && plrManager.PlrManagerInstance.enemyTeamMate.Count > 0f)
//                           {
//                               var pos = new Vector3(EnemyChosenTroop.position.x, transform.position.y, EnemyChosenTroop.position.z) - transform.position;
//                               transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pos, Vector3.up), 30f * Time.deltaTime);
//
//                               if (character_animator.GetInteger("Attack") == 0)
//                               {
//                                   print("auto ");
//                                   CapsuleColliderComponent.center = new Vector3(0f, 0.68f, 0f);
//                                   CapsuleColliderComponent.height = 1.34f;
//                                   CapsuleColliderComponent.radius = 0.3f;
//                                   InvokeRepeating("StartShooting", 1.5f, 0.1f);
//                               }
//                               if (BuletLst.Count > 0 )
//                               {
//                                   for (int i = 0; i < BuletLst.Count; i++)
//                                   {
//                                       BuletLst.ElementAt(i).transform.position = Vector3.MoveTowards(BuletLst.ElementAt(i).transform.position,
//                                           new Vector3(EnemyChosenTroop.position.x,EnemyChosenTroop.position.y + 3f,EnemyChosenTroop.position.z), 100f * Time.deltaTime);
//                                   }
//                               }
//                           }
//                           
//                           if (!LockToTarget) // attacks with hand and leg
//                           {
//                               character_animator.SetInteger("Attack",0);
//                               character_animator.SetFloat("Pos Y", 0f); 
//                               character_animator.SetFloat("Pos X", 0f);
//                           }
//                        
//                       }
//                   }
//                   if (plrManager.PlrManagerInstance.enemyTeamMate.Count == 0)
//                   {
//                       character_animator.SetInteger("Attack", 0);
//                       character_animator.SetFloat("Pos X", 0f);
//                       character_animator.SetFloat("Pos Y", 0f);
//                       character_animator.SetBool("dance", true);
//                       CapsuleColliderComponent.radius = 0.1f;
//                     //  GameManagerCls.ChaseTheBattle = false;
//                   }
//               } 
//         }
//      
//     }
//     
//     private void OnCollisionEnter(Collision other)
//     {
//         if (other.collider.CompareTag("fellowship"))
//         {
//             other.transform.GetComponent<Renderer>().material.color = GameManagerCls.playerRenderer.material.GetColor("GColor");
//             other.transform.GetComponent<Renderer>().material.SetColor(EmissionColor, GameManagerCls.playerRenderer.material.GetColor("BColor"));
//             other.collider.tag = "free";
//             other.gameObject.layer = 8;
//             other.transform.parent = GameManagerCls.leader;
//         }
//         
//
//         if (other.collider.CompareTag("obstacle"))
//         {
//           GameObject death =  Instantiate(Particle_Death, transform.position, Quaternion.identity);
//           var mainModule = death.transform.GetChild(0).GetComponent<ParticleSystem>().main;
//           mainModule.startColor = character_rend.material.color;
//             transform.parent.gameObject.SetActive(false);
//             transform.parent.parent = null;
//
//             if (GameManagerCls.BossFight )
//                 for (int i = 0; i < GetBossManagerCls.Enemeys.Count; i++)
//                 {
//                     if (GetBossManagerCls.Enemeys.ElementAt(i).name == transform.parent.name)
//                     {
//                         GetBossManagerCls.Enemeys.RemoveAt(i);
//                         break;
//                     }
//                 }
//         }
//  
//         if (other.collider.CompareTag("sword") | other.collider.CompareTag("enemyfist") | other.collider.CompareTag("Enemybulet"))
//         {
//             if (life == 0)
//             {
//                 if (other.collider.CompareTag("sword"))
//                 {
//                     for (int i = 0; i < GetBossManagerCls.Enemeys.Count; i++)
//                     {
//                         if (GetBossManagerCls.Enemeys.ElementAt(i).name == transform.name)
//                         {
//                             GetBossManagerCls.Enemeys.RemoveAt(i);
//                             break;
//                         }
//                     }
//                     GetBossManagerCls.LockToTarget = false;
//                 }
//              
//                 if (other.collider.CompareTag("enemyfist") | other.collider.CompareTag("Enemybulet"))
//                 {
//                     // if (other.collider.CompareTag("enemyfist"))
//                     //     other.collider.transform.root.GetComponent<enemyManager1>().LockToTarget = false;
//                  
//                     for (int i = 0; i < GameManagerCls.troopsFree.Count; i++)
//                     {
//                         if (GameManagerCls.troopsFree.ElementAt(i).name == transform.name)
//                         {
//                             GameManagerCls.troopsFree.RemoveAt(i);
//                             break;
//                         }
//                     }
//                     
//                     // if (other.collider.CompareTag("Enemybulet") | other.collider.CompareTag("enemyfist"))
//                     //     // for (int i = 0; i < GameManagerCls.troopsEnemy.Count; i++)
//                     //     // {
//                     //     //     GameManagerCls.troopsEnemy.ElementAt(i).GetComponent<enemyManager1>().LockToTarget = false;
//                     //     //     GameManagerCls.troopsEnemy.ElementAt(i).GetComponent<enemyManager1>().EnemyChosenTroop = null;
//                     //     //     GameManagerCls.troopsEnemy.ElementAt(i).GetComponent<enemyManager1>().Character.SetInteger("Attack",0);
//                     //     //     GameManagerCls.troopsEnemy.ElementAt(i).GetComponent<enemyManager1>().Character.SetFloat("Pos Y", 0f); 
//                     //     //     GameManagerCls.troopsEnemy.ElementAt(i).GetComponent<enemyManager1>().Character.SetFloat("Pos X", 0f);
//                     //     // }
//                     //
//                     //     foreach (var EnemyTroops in GameObject.FindGameObjectsWithTag("enemy"))
//                     //     {
//                     //         print(EnemyTroops);
//                     //         EnemyTroops.GetComponent<enemyManager1>().EnemyChosenTroop = null;
//                     //         EnemyTroops.GetComponent<enemyManager1>().LockToTarget = false;
//                     //         EnemyTroops.GetComponent<enemyManager1>().Character.SetInteger("Attack",0);
//                     //         EnemyTroops.GetComponent<enemyManager1>().Character.SetFloat("Pos Y", 0f); 
//                     //         EnemyTroops.GetComponent<enemyManager1>().Character.SetFloat("Pos X", 0f);
//                     //         EnemyTroops.GetComponent<enemyManager1>().MaxDistanceOfEnemey = 30f;
//                     //         EnemyTroops.GetComponent<enemyManager1>().MinDistanceOfEnemey = 3f;
//                     //     }
//                 }
//
//                 GameObject death =  Instantiate(Particle_Death, transform.position, Quaternion.identity);
//                 death.GetComponent<ParticleSystem>().GetComponent<Renderer>().material.CopyPropertiesFromMaterial(character_rend.material);
//                 var mainModule = death.transform.GetChild(0).GetComponent<ParticleSystem>().main;
//                 mainModule.startColor = character_rend.material.GetColor("GColor");
//             
//                 transform.gameObject.SetActive(false);
//
//           
//             }
//
//             life--;
//         }
//         
//     }
//     
// }
