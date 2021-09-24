using Cardinal.Appraiser;
using Runic.Characteristics;
using Runic.Entities;
using Runic.Entities.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public GameObject LastEnemy;
    public GameObject CurrentRoom;

    private void Start()
    {
        print(GetComponent<Collider>());
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            LastEnemy = hit.gameObject;
        }
        else if (hit.gameObject.GetComponentInParent<BaseEnemy>())
        {
            LastEnemy = hit.gameObject.GetComponentInParent<BaseEnemy>().gameObject;
        }
    }

    public void OnDeath() 
    {
        PlayerDeathEvent @event = ScriptableObject.CreateInstance<PlayerDeathEvent>();
        @event.Name = "Player Died!";
        @event.Time = Time.time.ToString();
        @event.RoomOfDeath = CurrentRoom;
        @event.Slayer = LastEnemy.GetComponent<Entity>();
        Cardinal.Analyser.Analyser.Instance.RegisterEvent(@event);

        GameObject PlayerHoldingLocation = GameObject.Find("PlayerStartPosition");
        GetComponent<CharacterController>().enabled = false;
        transform.position = PlayerHoldingLocation.transform.position;
        GetComponent<CharacterController>().enabled = true;
        GetComponent<Health>().Restore();
    }
}
