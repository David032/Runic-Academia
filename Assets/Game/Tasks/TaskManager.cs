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
            Invoke("SetUp", 5f);
            InvokeRepeating("CheckJobs", 5, 5);
            InvokeRepeating("CheckEndeavours", 5, 5);
        }

        void SetUp() 
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
            playerInventory = Player.GetComponent<EntityInventory>();
        }

        private void Update()
        {
            if (Player is null)
            {
                Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
            }
            if (playerInventory is null)
            {
                playerInventory = Player.GetComponent<EntityInventory>();
            }
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

        public bool HasItemTasks() 
        {
            foreach (Endeavour endeavour in ActiveEndeavours)
            {
                if (endeavour is ItemEndeavour)
                {
                    return true;
                }
            }

            foreach (Job job in ActiveJobs)
            {
                if (job is ItemJob)
                {
                    return true;
                }
            }
            if (ActiveQuest != null)
            {
                foreach (Task task in ActiveQuest.TasksToComplete)
                {
                    if (task is ItemEndeavour || task is ItemJob)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }
        public bool HasCompleteDungeonTasks() 
        {
            foreach (Job job in ActiveJobs)
            {
                if (job is ProgressiveJob progressiveJob && progressiveJob.ProgressCriteria == ProgressCriteria.DungeonCompletion)
                {
                    return true;
                }
            }
            if (ActiveQuest != null)
            {
                foreach (Task task in ActiveQuest.TasksToComplete)
                {
                    if (task is ProgressiveJob progressiveJob && progressiveJob.ProgressCriteria == ProgressCriteria.DungeonCompletion)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }
        public bool HasKillTasks()
        {
            foreach (Job job in ActiveJobs)
            {
                if (job is KillJob)
                {
                    return true;
                }
            }
            if (ActiveQuest != null)
            {
                foreach (Task task in ActiveQuest.TasksToComplete)
                {
                    if (task is KillJob)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        public bool HasKillTasks(TypeOfEntity requiredType)
        {
            foreach (Job job in ActiveJobs)
            {
                if (job is KillJob killJob && killJob.TypeToTrack == requiredType)
                {
                    return true;
                }
            }
            if (ActiveQuest != null)
            {
                foreach (Task task in ActiveQuest.TasksToComplete)
                {
                    if (task is KillJob killJob && killJob.TypeToTrack == requiredType)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        public void IncrementProgressJobs(ProgressCriteria criteria) 
        {
            foreach (Job job in ActiveJobs.ToList())
            {
                if (job is ProgressiveJob progressiveJob && progressiveJob.ProgressCriteria == ProgressCriteria.DungeonCompletion)
                {
                    progressiveJob.IncrementValue();
                }
            }
            if (ActiveQuest != null)
            {
                foreach (Task task in ActiveQuest.TasksToComplete)
                {
                    if (task is ProgressiveJob progressiveJob && progressiveJob.ProgressCriteria == ProgressCriteria.DungeonCompletion)
                    {
                        progressiveJob.IncrementValue();
                    }
                }
            }
        }
    }
}

