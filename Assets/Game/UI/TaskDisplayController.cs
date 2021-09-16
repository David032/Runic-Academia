using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Runic.Tasks.Jobs;

namespace Runic.UI
{
    public class TaskDisplayController : MonoBehaviour
    {
        public TextMeshProUGUI TasksList;
        public GameObject TasksWindow;

        Tasks.TaskManager TaskManager;

        private void Start()
        {
            TaskManager = Tasks.TaskManager.Instance;
        }
        public void ToggleTaskWindow()
        {
            TasksWindow.SetActive(!TasksWindow.activeSelf);
            if (TasksWindow.activeSelf)
            {
                foreach (Tasks.Endeavour endeavour in TaskManager.ActiveEndeavours)
                {
                    TasksList.text += "\nEndavour: " + endeavour.Name + " - " + endeavour.Description;
                }
                foreach (Tasks.Job job in TaskManager.ActiveJobs)
                {
                    if (job is ProgressiveJob || job is KillJob || job is ItemJob)
                    {
                        var progJob = (ProgressiveJob)job;
                        TasksList.text += "\nJob: " + job.Name + " - "
                            + progJob.CurrentValue.ToString() + "/"
                            + progJob.TargetValue.ToString();
                    }
                }
                if (TaskManager.ActiveQuest != null)
                {
                    TasksList.text += "\nQuest: " + TaskManager.ActiveQuest.Name 
                        + " - " + TaskManager.ActiveQuest.Description;
                }
            }
            else
            {
                TasksList.text = "";
            }
        }


    }
}

