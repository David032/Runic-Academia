using Runic.Characteristics;
using Runic.Characteristics.Titles;
using Runic.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Runic.Entities
{
    [RequireComponent(typeof(EntityInventory))]
    public class Entity : MonoBehaviour
    {
        [Header("Base")]
        public string Name = "";
        public EntityFlag Flag = EntityFlag.Friendly;
        public TypeOfEntity typeOfEntity = TypeOfEntity.All;
        public BaseTitle Title;
        [Header("Attributes")]
        public Health Health;
        public Mana Mana;
        public Stamina Stamina;
        [Header("Characteristics")]
        public float VisibilityRange = 15f;
        public float Fov = 90f;
        [Header("Resources")]
        public EntityInventory inventory;

        protected NavMeshAgent entityAgent;
        protected Animator entityAnimator;
        protected GameObject playerRef 
        {
            get => player;
        }
        GameObject player;

        Vector3 lastPosition;
        float speed;
        void Start()
        {
            if (Title is CharacteristicTitle)
            {
                CharacteristicTitle titleData = (CharacteristicTitle)Title;
                //Apply modifier here
            }
            entityAnimator = GetComponent<Animator>();
        }
        void Awake()
        {
            inventory = GetComponent<EntityInventory>();
            entityAgent = GetComponent<NavMeshAgent>();
            entityAnimator = GetComponent<Animator>();
            Health = GetComponent<Health>();
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void Update()
        {

        }

        public float GetSpeed()
        {
            speed = Mathf.Lerp(speed, (transform.position - lastPosition).magnitude / Time.deltaTime, 0.75f);
            lastPosition = transform.position;
            return speed;
        }

        public Health GetHealth() { return Health; }

        public bool CanSeePlayer() 
        {
            GameObject origin = gameObject;
            Vector3 startPoint = new Vector3(transform.position.x,
                transform.position.y + 1, transform.position.z);
            RaycastHit hitinfo;
            Vector3 playerLoc = new Vector3(player.transform.position.x, 
                player.transform.position.y + 1, player.transform.position.z);
            Vector3 direction = (playerLoc - startPoint).normalized;

            if (Physics.Raycast(new Ray(startPoint, direction),out hitinfo, 
                VisibilityRange))
            {
                if (Vector3.Angle
                    (origin.transform.forward, direction) < Fov / 2)
                {
                    if (hitinfo.collider.gameObject.tag == "Player" || 
                        hitinfo.collider.GetComponentInParent<PlayerControls>())
                    {
                        Debug.DrawLine(startPoint, playerLoc, Color.green);
                        return true;
                    }
                    else
                    {
                        Debug.DrawLine(startPoint, playerLoc, Color.yellow);
                        //print(gameObject.name + " hit " +
                        //    hitinfo.collider.gameObject.name);
                        return false;
                    }
                }
                else
                {
                    //print(gameObject.name + " can't see the player");
                    return false;
                }
            }
            else
            {
                print("Failed to raycast?");
                return false;
            }
        }

        public float GetDistanceToPlayer() 
        { return Vector3.Distance(transform.position, playerRef.transform.position); }

        public IEnumerator TimedTonicUse(Tonic thisTonic)
        {
            if (Health != null)
            {
                Health.current += thisTonic.HealthModifier;
            }
            if (Mana != null)
            {
                Mana.current += thisTonic.ManaModifier;
            }
            if (Stamina != null)
            {
                Stamina.current += thisTonic.StaminaModifier;
            }
            yield return new WaitForSeconds(thisTonic.Duration);
            if (Health != null)
            {
                Health.current -= thisTonic.HealthModifier;
            }
            if (Mana != null)
            {
                Mana.current -= thisTonic.ManaModifier;
            }
            if (Stamina != null)
            {
                Stamina.current -= thisTonic.StaminaModifier;
            }
        }

        public void ExecuteDie()
        {
            int randomDeath = Random.Range(1, 3);
            GetComponent<Animator>().SetInteger("DeathType_int", randomDeath);
            GetComponent<Animator>().SetBool("IsDead_b", true);
            GetComponent<Animator>().SetBool("Death_b", true);
        }
    }

}
