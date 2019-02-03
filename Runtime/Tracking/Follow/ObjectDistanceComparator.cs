﻿namespace Zinnia.Tracking.Follow
{
    using UnityEngine;
    using UnityEngine.Events;
    using System;
    using Malimbe.BehaviourStateRequirementMethod;
    using Malimbe.MemberClearanceMethod;
    using Malimbe.XmlDocumentationAttribute;
    using Zinnia.Process;

    /// <summary>
    /// Compares the distance between two GameObjects and emits an event when a given threshold is exceeded or falls within it.
    /// </summary>
    /// <remarks>
    /// If the <see cref="source"/> and the <see cref="target"/> are the same <see cref="GameObject"/> then the initial position of the <see cref="target"/> is used as the <see cref="source"/> position.
    /// </remarks>
    public class ObjectDistanceComparator : MonoBehaviour, IProcessable
    {
        /// <summary>
        /// Holds data about a <see cref="ObjectDistanceComparator"/> event.
        /// </summary>
        [Serializable]
        public class EventData
        {
            /// <summary>
            /// The difference of the positions of the target and source.
            /// </summary>
            [DocumentedByXml]
            public Vector3 difference;
            /// <summary>
            /// The distance between the source and target.
            /// </summary>
            [DocumentedByXml]
            public float distance;

            public EventData Set(EventData source)
            {
                return Set(source.difference, source.distance);
            }

            public EventData Set(Vector3 difference, float distance)
            {
                this.difference = difference;
                this.distance = distance;
                return this;
            }

            public void Clear()
            {
                Set(default, default);
            }
        }

        /// <summary>
        /// Defines the event with the <see cref="EventData"/>.
        /// </summary>
        [Serializable]
        public class UnityEvent : UnityEvent<EventData>
        {
        }

        /// <summary>
        /// The source of the distance measurement.
        /// </summary>
        [DocumentedByXml, Cleared]
        public GameObject source;
        /// <summary>
        /// The target of the distance measurement.
        /// </summary>
        [DocumentedByXml, Cleared]
        public GameObject target;
        /// <summary>
        /// The distance between the source and target that is considered to be exceeding the given threshold.
        /// </summary>
        [DocumentedByXml]
        public float distanceThreshold = 1f;

        /// <summary>
        /// Emitted when the distance between the source and the target exceeds the threshold.
        /// </summary>
        [DocumentedByXml]
        public UnityEvent ThresholdExceeded = new UnityEvent();
        /// <summary>
        /// Emitted when the distance between the source and the target falls back within the threshold.
        /// </summary>
        [DocumentedByXml]
        public UnityEvent ThresholdResumed = new UnityEvent();

        /// <summary>
        /// The difference of the positions of the target and source.
        /// </summary>
        public Vector3 Difference
        {
            get;
            protected set;
        }

        /// <summary>
        /// The distance between the source and target.
        /// </summary>
        public float Distance
        {
            get;
            protected set;
        }

        /// <summary>
        /// Determines if the distance between the source and target is exceeding the threshold.
        /// </summary>
        public bool Exceeding
        {
            get;
            protected set;
        }

        protected bool previousState;
        protected Vector3 sourcePosition;
        protected EventData eventData = new EventData();

        /// <summary>
        /// Checks to see if the distance between the source and target exceed the threshold.
        /// </summary>
        [RequiresBehaviourState]
        public virtual void Process()
        {
            if (source == null || target == null)
            {
                return;
            }

            Difference = target.transform.position - GetSourcePosition();
            Distance = Difference.magnitude;
            Exceeding = Distance >= distanceThreshold;

            bool didStateChange = previousState != Exceeding;
            previousState = Exceeding;

            if (!didStateChange && distanceThreshold > 0f)
            {
                return;
            }

            eventData.Set(Difference, Distance);

            if (Exceeding)
            {
                ThresholdExceeded?.Invoke(eventData);
            }
            else
            {
                ThresholdResumed?.Invoke(eventData);
            }
        }

        /// <summary>
        /// Sets the <see cref="source"/> parameter.
        /// </summary>
        /// <param name="source">The new source value.</param>
        public virtual void SetSource(GameObject source)
        {
            this.source = source;
            SavePosition();
        }

        /// <summary>
        /// Attempts to save the current <see cref="target"/> position as the initial position if the <see cref="source"/> and the <see cref="target"/> are the same <see cref="GameObject"/>.
        /// </summary>
        public virtual void SavePosition()
        {
            if (source == null || source != target)
            {
                return;
            }

            sourcePosition = target.transform.position;
            previousState = false;
        }

        /// <summary>
        /// Sets the <see cref="target"/> parameter.
        /// </summary>
        /// <param name="target">The new target value.</param>
        public virtual void SetTarget(GameObject target)
        {
            this.target = target;
            SavePosition();
        }

        protected virtual void OnEnable()
        {
            SavePosition();
        }

        /// <summary>
        /// Gets the actual position for the <see cref="source"/> based on whether it's a different <see cref="GameObject"/> or whether it is set up to use the initial position of the <see cref="target"/>.
        /// </summary>
        /// <returns>The appropriate position.</returns>
        protected virtual Vector3 GetSourcePosition()
        {
            if (source == null)
            {
                return Vector3.zero;
            }

            return (source == target ? sourcePosition : source.transform.position);
        }
    }
}