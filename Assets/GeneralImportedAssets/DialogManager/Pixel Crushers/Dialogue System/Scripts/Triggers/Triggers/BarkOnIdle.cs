// Copyright (c) Pixel Crushers. All rights reserved.

using UnityEngine;
using System.Collections;

namespace PixelCrushers.DialogueSystem
{

    /// <summary>
    /// The Bark On Idle component can be used to make an NPC bark on timed intervals.
    /// Barks don't occur while a conversation is active.
    /// </summary>
    [AddComponentMenu("")] // Use wrapper.
    public class BarkOnIdle : BarkStarter
    {

        [Tooltip("Bark as soon as this component starts the first time.")]
        public bool barkOnStart = false;

        [Tooltip("Bark when the component is enabled. If disabled and reenabled, barks again.")]
        public bool barkOnEnable = false;

        /// <summary>
        /// The minimum seconds between barks.
        /// </summary>
        [Tooltip("Minimum seconds between barks.")]
        public float minSeconds = 5f;

        /// <summary>
        /// The maximum seconds between barks.
        /// </summary>
        [Tooltip("Maximum seconds between barks.")]
        public float maxSeconds = 10f;

        /// <summary>
        /// The target to bark at. Leave unassigned to just bark into the air.
        /// </summary>
        [Tooltip("Target to whom bark is addressed. Leave unassigned to just bark into the air.")]
        public Transform target;

        protected override bool useOnce { get { return false; } } // Removed confusing Once checkbox.

        private bool started = false;

        protected override void Start()
        {
            base.Start();
            started = true;
            StartBarkLoop();
            if (barkOnStart && !barkOnEnable) TryIdleBark();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            StartBarkLoop();
            if (barkOnEnable) TryIdleBark();
        }

        /// <summary>
        /// Starts the bark loop. Normally this is started in the Start() method. If you need to
        /// restart it for some reason, call this method.
        /// </summary>
        public virtual void StartBarkLoop()
        {
            if (!started) return;
            StopAllCoroutines();
            StartCoroutine(BarkLoop());
        }

        protected virtual IEnumerator BarkLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(minSeconds, maxSeconds));
                TryIdleBark();
            }
        }

        protected virtual void TryIdleBark()
        {
            if (enabled && (!DialogueManager.isConversationActive || allowDuringConversations) && !DialogueTime.isPaused)
            {
                TryBark(target);
            }
        }

    }

}
