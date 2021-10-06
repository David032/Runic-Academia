using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Cardinal.Adjustor
{
    public class Adjustor : CardinalSingleton<Adjustor>
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        #region Messages

        public void Message() { }
        public void Message(ResponseSubject actor, ResponseAction action, ResponseLocation location) 
        {
        }
        public void Message(ResponseSubject actor, ResponseAction action, ResponseSubject subject)
        {

        }

        #endregion
    }
}

