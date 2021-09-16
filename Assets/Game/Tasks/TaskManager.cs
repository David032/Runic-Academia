using Runic.Tasks.Jobs;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Runic.Entities;

namespace Runic.Tasks
{
    public class TaskManager : MonoSingleton<TaskManager>
    {
        public List<Endeavour> ActiveEndeavours = new List<Endeavour>();
        public List<Job> ActiveJobs = new List<Job>();
        public Quest ActiveQuest;
        public List<Task> CompletedTasks = new List<Task>();

        Entity Player;
        EntityInventory playerInventory;

        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player")
                .GetComponent<Entity>();
            playerInventory = Player.GetComponent<EntityInventory>();
            InvokeRepeating("CheckJobs", 5, 5);
            InvokeRepeating("CheckEndeavours", 5, 5);
        }

        public void CheckForUpdates() 
        {
            CheckEndeavours();
            CheckJobs();
        }

        void CheckEndeavours() 
        {
            foreach (Endeavour item in ActiveEndeavours)
            {
                if (item is ItemEndeavour)
                {
                    var ItemEndeavour = (ItemEndeavour)item;
                    if (playerInventory.Inventory.Contains(ItemEndeavour.DesiredItem))
                    {
                        ItemEndeavour.OnCompletion();
                    }
                }
            }
            if (ActiveQuest != null)
            {
                foreach (Task item in ActiveQuest.TasksToComplete.ToList())
                {
                    if (item is ItemEndeavour)
                    {
                        var ItemEndeavour = (ItemEndeavour)item;
                        if (playerInventory.Inventory.Contains(ItemEndeavour.DesiredItem))
                        {
                            ItemEndeavour.OnCompletion();
                        }
                    }
                }
            }
        }

        void CheckJobs() 
        {
            foreach (Job item in ActiveJobs)
            {
                if (item is ItemJob)
                {
                    var itemCollection = (ItemJob)item;
                    itemCollection.CountItems();
                }
            }
            if (ActiveQuest != null)
            {
                foreach (Task item in ActiveQuest.TasksToComplete)
                {
                    if (item is ItemJob)
                    {
                        var itemCollection = (ItemJob)item;
                        itemCollection.CountItems();
                    }
                }
            }
        }

        void CheckQuests() 
        {
        
        }
    }
}

