// Copyright (c) Pixel Crushers. All rights reserved.

using System;
using UnityEngine;

namespace PixelCrushers.DialogueSystem
{

    /// <summary>
    /// This optional attribute specifies a submenu name in which to organize 
    /// a custom sequencer command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SequencerCommandGroupAttribute : System.Attribute
    {

        public string submenu;

        public SequencerCommandGroupAttribute(string submenu)
        {
            this.submenu = submenu;
        }
    }

}
