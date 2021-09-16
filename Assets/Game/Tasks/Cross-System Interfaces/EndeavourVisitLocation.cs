using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Runic.Tasks.Interfaces
{
    public class EndeavourVisitLocation : MonoBehaviour
    {
        public string Id = "";

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                foreach (Endeavour item in TaskManager.Instance.ActiveEndeavours.ToList())
                {
                    if (item is VisitEndeavour)
                    {
                        VisitEndeavour visitation = (VisitEndeavour)item;
                        if (visitation.LocationId == Id)
                        {
                            visitation.GiveReward();
                            visitation.OnCompletion();
                        }
                    }
                }
                if (TaskManager.Instance.ActiveQuest != null)
                {
                    foreach (Task item in 
                        TaskManager.Instance.ActiveQuest.TasksToComplete.ToList())
                    {
                        if (item is VisitEndeavour)
                        {
                            VisitEndeavour visitation = (VisitEndeavour)item;
                            if (visitation.LocationId == Id)
                            {
                                visitation.GiveReward();
                                visitation.OnCompletion();
                            }
                        }
                    }
                }
            }
        }
    }
}

