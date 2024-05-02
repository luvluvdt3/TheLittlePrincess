// Contributed by @xick

using UnityEngine;
using System.Collections.Generic;

namespace PixelCrushers.DialogueSystem
{
    [AddComponentMenu("")] // Use wrapper.
    public class PersistentActiveDataMultiple : MonoBehaviour
    {
        [System.Serializable]
        public class TargetConditionPair
        {
            public GameObject target;
            public Condition condition;
        }

        public List<TargetConditionPair> targetsAndConditions = new List<TargetConditionPair>();
        public bool checkOnStart = true;

        protected virtual void Start()
        {
            if (checkOnStart) CheckAllTargets();
        }

        protected virtual void OnEnable()
        {
            PersistentDataManager.RegisterPersistentData(gameObject);
        }

        protected virtual void OnDisable()
        {
            PersistentDataManager.UnregisterPersistentData(gameObject);
        }

        public void OnApplyPersistentData()
        {
            CheckAllTargets();
        }

        public virtual void CheckAllTargets()
        {
            if (enabled)
            {
                foreach (var targetConditionPair in targetsAndConditions)
                {
                    if (targetConditionPair.target == null)
                    {
                        if (DialogueDebug.logWarnings)
                        {
                            Debug.LogWarning("Dialogue System: No target is assigned to Persistent Active Data Multiple component on " + name + ".", this);
                        }
                    }
                    else
                    {
                        targetConditionPair.target.SetActive(targetConditionPair.condition.IsTrue(null));
                    }
                }
            }
        }
    }
}
