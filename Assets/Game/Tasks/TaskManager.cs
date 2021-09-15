using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Tasks
{
    public class TaskManager : MonoSingleton<TaskManager>
    {
        public List<Endeavour> ActiveEndeavours = new List<Endeavour>();
        public List<Job> ActiveJobs = new List<Job>();
        public Quest ActiveQuest;
        public List<Task> CompletedTasks = new List<Task>();

        private void Start()
        {
            
        }

        void CheckEndeavours() 
        {
            
        }

        void CheckJobs() 
        {
        
        }

        void CheckQuests() 
        {
        
        }
    }
}

