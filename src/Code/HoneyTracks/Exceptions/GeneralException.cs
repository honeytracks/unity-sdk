using UnityEngine;
using System.Collections;
using System;

namespace HoneyTracks.Exceptions
{
    public class GeneralException : Exception
    {
        public GeneralException(string message) : base(message) { }
    }
}
