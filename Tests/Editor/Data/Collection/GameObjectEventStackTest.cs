﻿using VRTK.Core.Data.Collection;

namespace Test.VRTK.Core.Data.Collection
{
    using UnityEngine;
    using NUnit.Framework;
    using System.Collections.Generic;
    using Test.VRTK.Core.Utility.Mock;

    public class GameObjectEventStackTest
    {
        private GameObject containingObject;
        private GameObjectEventStack subject;

        [SetUp]
        public void SetUp()
        {
            containingObject = new GameObject();
            subject = containingObject.AddComponent<GameObjectEventStack>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(subject);
            Object.DestroyImmediate(containingObject);
        }

        [Test]
        public void Push()
        {
            UnityEventListenerMock elementOnePushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOnePoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOneForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsOne = new GameObjectEventStack.ElementEvents();
            eventsOne.Pushed.AddListener(elementOnePushedMock.Listen);
            eventsOne.Popped.AddListener(elementOnePoppedMock.Listen);
            eventsOne.ForcePopped.AddListener(elementOneForcePoppedMock.Listen);

            UnityEventListenerMock elementTwoPushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementTwoPoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementTwoForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsTwo = new GameObjectEventStack.ElementEvents();
            eventsTwo.Pushed.AddListener(elementTwoPushedMock.Listen);
            eventsTwo.Popped.AddListener(elementTwoPoppedMock.Listen);
            eventsTwo.ForcePopped.AddListener(elementTwoForcePoppedMock.Listen);

            UnityEventListenerMock elementThreePushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementThreePoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementThreeForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsThree = new GameObjectEventStack.ElementEvents();
            eventsThree.Pushed.AddListener(elementThreePushedMock.Listen);
            eventsThree.Popped.AddListener(elementThreePoppedMock.Listen);
            eventsThree.ForcePopped.AddListener(elementThreeForcePoppedMock.Listen);

            GameObject objectOne = new GameObject();
            GameObject objectTwo = new GameObject();
            GameObject objectThree = new GameObject();

            subject.elementEvents.Add(eventsOne);
            subject.elementEvents.Add(eventsTwo);
            subject.elementEvents.Add(eventsThree);

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);
            Assert.IsFalse(elementTwoPushedMock.Received);
            Assert.IsFalse(elementTwoPoppedMock.Received);
            Assert.IsFalse(elementTwoForcePoppedMock.Received);
            Assert.IsFalse(elementThreePushedMock.Received);
            Assert.IsFalse(elementThreePoppedMock.Received);
            Assert.IsFalse(elementThreeForcePoppedMock.Received);

            subject.Push(objectOne);

            Assert.IsTrue(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);
            Assert.IsFalse(elementTwoPushedMock.Received);
            Assert.IsFalse(elementTwoPoppedMock.Received);
            Assert.IsFalse(elementTwoForcePoppedMock.Received);
            Assert.IsFalse(elementThreePushedMock.Received);
            Assert.IsFalse(elementThreePoppedMock.Received);
            Assert.IsFalse(elementThreeForcePoppedMock.Received);

            elementOnePushedMock.Reset();
            elementOnePoppedMock.Reset();
            elementOneForcePoppedMock.Reset();
            elementTwoPushedMock.Reset();
            elementTwoPoppedMock.Reset();
            elementTwoForcePoppedMock.Reset();
            elementThreePushedMock.Reset();
            elementThreePoppedMock.Reset();
            elementThreeForcePoppedMock.Reset();

            subject.Push(objectTwo);

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);
            Assert.IsTrue(elementTwoPushedMock.Received);
            Assert.IsFalse(elementTwoPoppedMock.Received);
            Assert.IsFalse(elementTwoForcePoppedMock.Received);
            Assert.IsFalse(elementThreePushedMock.Received);
            Assert.IsFalse(elementThreePoppedMock.Received);
            Assert.IsFalse(elementThreeForcePoppedMock.Received);

            elementOnePushedMock.Reset();
            elementOnePoppedMock.Reset();
            elementOneForcePoppedMock.Reset();
            elementTwoPushedMock.Reset();
            elementTwoPoppedMock.Reset();
            elementTwoForcePoppedMock.Reset();
            elementThreePushedMock.Reset();
            elementThreePoppedMock.Reset();
            elementThreeForcePoppedMock.Reset();

            subject.Push(objectThree);

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);
            Assert.IsFalse(elementTwoPushedMock.Received);
            Assert.IsFalse(elementTwoPoppedMock.Received);
            Assert.IsFalse(elementTwoForcePoppedMock.Received);
            Assert.IsTrue(elementThreePushedMock.Received);
            Assert.IsFalse(elementThreePoppedMock.Received);
            Assert.IsFalse(elementThreeForcePoppedMock.Received);

            Object.DestroyImmediate(objectOne);
            Object.DestroyImmediate(objectTwo);
            Object.DestroyImmediate(objectThree);
        }

        [Test]
        public void PushDuplicate()
        {
            UnityEventListenerMock elementOnePushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOnePoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOneForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsOne = new GameObjectEventStack.ElementEvents();
            eventsOne.Pushed.AddListener(elementOnePushedMock.Listen);
            eventsOne.Popped.AddListener(elementOnePoppedMock.Listen);
            eventsOne.ForcePopped.AddListener(elementOneForcePoppedMock.Listen);

            UnityEventListenerMock elementTwoPushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementTwoPoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementTwoForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsTwo = new GameObjectEventStack.ElementEvents();
            eventsTwo.Pushed.AddListener(elementTwoPushedMock.Listen);
            eventsTwo.Popped.AddListener(elementTwoPoppedMock.Listen);
            eventsTwo.ForcePopped.AddListener(elementTwoForcePoppedMock.Listen);

            GameObject objectOne = new GameObject();

            subject.elementEvents.Add(eventsOne);
            subject.elementEvents.Add(eventsTwo);

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);
            Assert.IsFalse(elementTwoPushedMock.Received);
            Assert.IsFalse(elementTwoPoppedMock.Received);
            Assert.IsFalse(elementTwoForcePoppedMock.Received);

            subject.Push(objectOne);

            Assert.IsTrue(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);
            Assert.IsFalse(elementTwoPushedMock.Received);
            Assert.IsFalse(elementTwoPoppedMock.Received);
            Assert.IsFalse(elementTwoForcePoppedMock.Received);

            elementOnePushedMock.Reset();
            elementOnePoppedMock.Reset();
            elementOneForcePoppedMock.Reset();
            elementTwoPushedMock.Reset();
            elementTwoPoppedMock.Reset();
            elementTwoForcePoppedMock.Reset();

            subject.Push(objectOne);

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);
            Assert.IsFalse(elementTwoPushedMock.Received);
            Assert.IsFalse(elementTwoPoppedMock.Received);
            Assert.IsFalse(elementTwoForcePoppedMock.Received);

            Object.DestroyImmediate(objectOne);
        }

        [Test]
        public void PushExceedsEventCount()
        {
            UnityEventListenerMock elementOnePushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOnePoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOneForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsOne = new GameObjectEventStack.ElementEvents();
            eventsOne.Pushed.AddListener(elementOnePushedMock.Listen);
            eventsOne.Popped.AddListener(elementOnePoppedMock.Listen);
            eventsOne.ForcePopped.AddListener(elementOneForcePoppedMock.Listen);

            GameObject objectOne = new GameObject();
            GameObject objectTwo = new GameObject();

            subject.elementEvents.Add(eventsOne);

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);

            subject.Push(objectOne);

            Assert.IsTrue(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);

            elementOnePushedMock.Reset();
            elementOnePoppedMock.Reset();
            elementOneForcePoppedMock.Reset();

            subject.Push(objectTwo);

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);

            Object.DestroyImmediate(objectOne);
            Object.DestroyImmediate(objectTwo);
        }

        [Test]
        public void PushInactiveGameObject()
        {
            UnityEventListenerMock elementOnePushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOnePoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOneForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsOne = new GameObjectEventStack.ElementEvents();
            eventsOne.Pushed.AddListener(elementOnePushedMock.Listen);
            eventsOne.Popped.AddListener(elementOnePoppedMock.Listen);
            eventsOne.ForcePopped.AddListener(elementOneForcePoppedMock.Listen);

            GameObject objectOne = new GameObject();

            subject.elementEvents.Add(eventsOne);
            subject.gameObject.SetActive(false);
            subject.Push(objectOne);

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);

            Object.DestroyImmediate(objectOne);
        }

        [Test]
        public void PushInactiveComponent()
        {
            UnityEventListenerMock elementOnePushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOnePoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOneForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsOne = new GameObjectEventStack.ElementEvents();
            eventsOne.Pushed.AddListener(elementOnePushedMock.Listen);
            eventsOne.Popped.AddListener(elementOnePoppedMock.Listen);
            eventsOne.ForcePopped.AddListener(elementOneForcePoppedMock.Listen);

            GameObject objectOne = new GameObject();

            subject.elementEvents.Add(eventsOne);
            subject.enabled = false;
            subject.Push(objectOne);

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);

            Object.DestroyImmediate(objectOne);
        }

        [Test]
        public void Pop()
        {
            UnityEventListenerMock elementOnePushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOnePoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOneForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsOne = new GameObjectEventStack.ElementEvents();
            eventsOne.Pushed.AddListener(elementOnePushedMock.Listen);
            eventsOne.Popped.AddListener(elementOnePoppedMock.Listen);
            eventsOne.ForcePopped.AddListener(elementOneForcePoppedMock.Listen);

            UnityEventListenerMock elementTwoPushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementTwoPoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementTwoForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsTwo = new GameObjectEventStack.ElementEvents();
            eventsTwo.Pushed.AddListener(elementTwoPushedMock.Listen);
            eventsTwo.Popped.AddListener(elementTwoPoppedMock.Listen);
            eventsTwo.ForcePopped.AddListener(elementTwoForcePoppedMock.Listen);

            UnityEventListenerMock elementThreePushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementThreePoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementThreeForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsThree = new GameObjectEventStack.ElementEvents();
            eventsThree.Pushed.AddListener(elementThreePushedMock.Listen);
            eventsThree.Popped.AddListener(elementThreePoppedMock.Listen);
            eventsThree.ForcePopped.AddListener(elementThreeForcePoppedMock.Listen);

            GameObject objectOne = new GameObject();
            GameObject objectTwo = new GameObject();
            GameObject objectThree = new GameObject();

            subject.elementEvents.Add(eventsOne);
            subject.elementEvents.Add(eventsTwo);
            subject.elementEvents.Add(eventsThree);

            subject.Push(objectOne);
            subject.Push(objectTwo);
            subject.Push(objectThree);

            elementOnePushedMock.Reset();
            elementOnePoppedMock.Reset();
            elementOneForcePoppedMock.Reset();
            elementTwoPushedMock.Reset();
            elementTwoPoppedMock.Reset();
            elementTwoForcePoppedMock.Reset();
            elementThreePushedMock.Reset();
            elementThreePoppedMock.Reset();
            elementThreeForcePoppedMock.Reset();

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);
            Assert.IsFalse(elementTwoPushedMock.Received);
            Assert.IsFalse(elementTwoPoppedMock.Received);
            Assert.IsFalse(elementTwoForcePoppedMock.Received);
            Assert.IsFalse(elementThreePushedMock.Received);
            Assert.IsFalse(elementThreePoppedMock.Received);
            Assert.IsFalse(elementThreeForcePoppedMock.Received);

            subject.Pop();

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);
            Assert.IsFalse(elementTwoPushedMock.Received);
            Assert.IsFalse(elementTwoPoppedMock.Received);
            Assert.IsFalse(elementTwoForcePoppedMock.Received);
            Assert.IsFalse(elementThreePushedMock.Received);
            Assert.IsTrue(elementThreePoppedMock.Received);
            Assert.IsFalse(elementThreeForcePoppedMock.Received);

            Object.DestroyImmediate(objectOne);
            Object.DestroyImmediate(objectTwo);
            Object.DestroyImmediate(objectThree);
        }

        [Test]
        public void PopAtMiddle()
        {
            UnityEventListenerMock elementOnePushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOnePoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOneForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsOne = new GameObjectEventStack.ElementEvents();
            eventsOne.Pushed.AddListener(elementOnePushedMock.Listen);
            eventsOne.Popped.AddListener(elementOnePoppedMock.Listen);
            eventsOne.ForcePopped.AddListener(elementOneForcePoppedMock.Listen);

            UnityEventListenerMock elementTwoPushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementTwoPoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementTwoForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsTwo = new GameObjectEventStack.ElementEvents();
            eventsTwo.Pushed.AddListener(elementTwoPushedMock.Listen);
            eventsTwo.Popped.AddListener(elementTwoPoppedMock.Listen);
            eventsTwo.ForcePopped.AddListener(elementTwoForcePoppedMock.Listen);

            UnityEventListenerMock elementThreePushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementThreePoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementThreeForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsThree = new GameObjectEventStack.ElementEvents();
            eventsThree.Pushed.AddListener(elementThreePushedMock.Listen);
            eventsThree.Popped.AddListener(elementThreePoppedMock.Listen);
            eventsThree.ForcePopped.AddListener(elementThreeForcePoppedMock.Listen);

            GameObject objectOne = new GameObject();
            GameObject objectTwo = new GameObject();
            GameObject objectThree = new GameObject();

            subject.elementEvents.Add(eventsOne);
            subject.elementEvents.Add(eventsTwo);
            subject.elementEvents.Add(eventsThree);

            subject.Push(objectOne);
            subject.Push(objectTwo);
            subject.Push(objectThree);

            elementOnePushedMock.Reset();
            elementOnePoppedMock.Reset();
            elementOneForcePoppedMock.Reset();
            elementTwoPushedMock.Reset();
            elementTwoPoppedMock.Reset();
            elementTwoForcePoppedMock.Reset();
            elementThreePushedMock.Reset();
            elementThreePoppedMock.Reset();
            elementThreeForcePoppedMock.Reset();

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);
            Assert.IsFalse(elementTwoPushedMock.Received);
            Assert.IsFalse(elementTwoPoppedMock.Received);
            Assert.IsFalse(elementTwoForcePoppedMock.Received);
            Assert.IsFalse(elementThreePushedMock.Received);
            Assert.IsFalse(elementThreePoppedMock.Received);
            Assert.IsFalse(elementThreeForcePoppedMock.Received);

            subject.PopAt(objectTwo);

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);
            Assert.IsFalse(elementTwoPushedMock.Received);
            Assert.IsTrue(elementTwoPoppedMock.Received);
            Assert.IsFalse(elementTwoForcePoppedMock.Received);
            Assert.IsFalse(elementThreePushedMock.Received);
            Assert.IsFalse(elementThreePoppedMock.Received);
            Assert.IsTrue(elementThreeForcePoppedMock.Received);

            Object.DestroyImmediate(objectOne);
            Object.DestroyImmediate(objectTwo);
            Object.DestroyImmediate(objectThree);
        }

        [Test]
        public void PopAtStart()
        {
            UnityEventListenerMock elementOnePushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOnePoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOneForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsOne = new GameObjectEventStack.ElementEvents();
            eventsOne.Pushed.AddListener(elementOnePushedMock.Listen);
            eventsOne.Popped.AddListener(elementOnePoppedMock.Listen);
            eventsOne.ForcePopped.AddListener(elementOneForcePoppedMock.Listen);

            UnityEventListenerMock elementTwoPushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementTwoPoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementTwoForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsTwo = new GameObjectEventStack.ElementEvents();
            eventsTwo.Pushed.AddListener(elementTwoPushedMock.Listen);
            eventsTwo.Popped.AddListener(elementTwoPoppedMock.Listen);
            eventsTwo.ForcePopped.AddListener(elementTwoForcePoppedMock.Listen);

            UnityEventListenerMock elementThreePushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementThreePoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementThreeForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsThree = new GameObjectEventStack.ElementEvents();
            eventsThree.Pushed.AddListener(elementThreePushedMock.Listen);
            eventsThree.Popped.AddListener(elementThreePoppedMock.Listen);
            eventsThree.ForcePopped.AddListener(elementThreeForcePoppedMock.Listen);

            GameObject objectOne = new GameObject();
            GameObject objectTwo = new GameObject();
            GameObject objectThree = new GameObject();

            subject.elementEvents.Add(eventsOne);
            subject.elementEvents.Add(eventsTwo);
            subject.elementEvents.Add(eventsThree);

            subject.Push(objectOne);
            subject.Push(objectTwo);
            subject.Push(objectThree);

            elementOnePushedMock.Reset();
            elementOnePoppedMock.Reset();
            elementOneForcePoppedMock.Reset();
            elementTwoPushedMock.Reset();
            elementTwoPoppedMock.Reset();
            elementTwoForcePoppedMock.Reset();
            elementThreePushedMock.Reset();
            elementThreePoppedMock.Reset();
            elementThreeForcePoppedMock.Reset();

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);
            Assert.IsFalse(elementTwoPushedMock.Received);
            Assert.IsFalse(elementTwoPoppedMock.Received);
            Assert.IsFalse(elementTwoForcePoppedMock.Received);
            Assert.IsFalse(elementThreePushedMock.Received);
            Assert.IsFalse(elementThreePoppedMock.Received);
            Assert.IsFalse(elementThreeForcePoppedMock.Received);

            subject.PopAt(objectOne);

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsTrue(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);
            Assert.IsFalse(elementTwoPushedMock.Received);
            Assert.IsFalse(elementTwoPoppedMock.Received);
            Assert.IsTrue(elementTwoForcePoppedMock.Received);
            Assert.IsFalse(elementThreePushedMock.Received);
            Assert.IsFalse(elementThreePoppedMock.Received);
            Assert.IsTrue(elementThreeForcePoppedMock.Received);

            Object.DestroyImmediate(objectOne);
            Object.DestroyImmediate(objectTwo);
            Object.DestroyImmediate(objectThree);
        }

        [Test]
        public void PopAtInvalid()
        {
            UnityEventListenerMock elementOnePushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOnePoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOneForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsOne = new GameObjectEventStack.ElementEvents();
            eventsOne.Pushed.AddListener(elementOnePushedMock.Listen);
            eventsOne.Popped.AddListener(elementOnePoppedMock.Listen);
            eventsOne.ForcePopped.AddListener(elementOneForcePoppedMock.Listen);

            GameObject objectOne = new GameObject();
            GameObject objectTwo = new GameObject();

            subject.elementEvents.Add(eventsOne);

            subject.Push(objectOne);

            elementOnePushedMock.Reset();
            elementOnePoppedMock.Reset();
            elementOneForcePoppedMock.Reset();

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);

            subject.PopAt(objectTwo);

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);

            Object.DestroyImmediate(objectOne);
            Object.DestroyImmediate(objectTwo);
        }

        [Test]
        public void PopAtEmptyStack()
        {
            UnityEventListenerMock elementOnePushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOnePoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOneForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsOne = new GameObjectEventStack.ElementEvents();
            eventsOne.Pushed.AddListener(elementOnePushedMock.Listen);
            eventsOne.Popped.AddListener(elementOnePoppedMock.Listen);
            eventsOne.ForcePopped.AddListener(elementOneForcePoppedMock.Listen);

            GameObject objectOne = new GameObject();
            GameObject objectTwo = new GameObject();

            subject.elementEvents.Add(eventsOne);

            elementOnePushedMock.Reset();
            elementOnePoppedMock.Reset();
            elementOneForcePoppedMock.Reset();

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);

            subject.PopAt(objectTwo);

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);

            Object.DestroyImmediate(objectOne);
            Object.DestroyImmediate(objectTwo);
        }

        [Test]
        public void PopAtInactiveGameObject()
        {
            UnityEventListenerMock elementOnePushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOnePoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOneForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsOne = new GameObjectEventStack.ElementEvents();
            eventsOne.Pushed.AddListener(elementOnePushedMock.Listen);
            eventsOne.Popped.AddListener(elementOnePoppedMock.Listen);
            eventsOne.ForcePopped.AddListener(elementOneForcePoppedMock.Listen);

            GameObject objectOne = new GameObject();

            subject.elementEvents.Add(eventsOne);

            elementOnePushedMock.Reset();
            elementOnePoppedMock.Reset();
            elementOneForcePoppedMock.Reset();

            subject.gameObject.SetActive(false);
            subject.PopAt(objectOne);

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);

            Object.DestroyImmediate(objectOne);
        }

        [Test]
        public void PopAtInactiveComponent()
        {
            UnityEventListenerMock elementOnePushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOnePoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOneForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsOne = new GameObjectEventStack.ElementEvents();
            eventsOne.Pushed.AddListener(elementOnePushedMock.Listen);
            eventsOne.Popped.AddListener(elementOnePoppedMock.Listen);
            eventsOne.ForcePopped.AddListener(elementOneForcePoppedMock.Listen);

            GameObject objectOne = new GameObject();

            subject.elementEvents.Add(eventsOne);

            elementOnePushedMock.Reset();
            elementOnePoppedMock.Reset();
            elementOneForcePoppedMock.Reset();

            subject.enabled = false;
            subject.PopAt(objectOne);

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);

            Object.DestroyImmediate(objectOne);
        }

        [Test]
        public void PopAtIndexMiddle()
        {
            UnityEventListenerMock elementOnePushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOnePoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOneForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsOne = new GameObjectEventStack.ElementEvents();
            eventsOne.Pushed.AddListener(elementOnePushedMock.Listen);
            eventsOne.Popped.AddListener(elementOnePoppedMock.Listen);
            eventsOne.ForcePopped.AddListener(elementOneForcePoppedMock.Listen);

            UnityEventListenerMock elementTwoPushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementTwoPoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementTwoForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsTwo = new GameObjectEventStack.ElementEvents();
            eventsTwo.Pushed.AddListener(elementTwoPushedMock.Listen);
            eventsTwo.Popped.AddListener(elementTwoPoppedMock.Listen);
            eventsTwo.ForcePopped.AddListener(elementTwoForcePoppedMock.Listen);

            UnityEventListenerMock elementThreePushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementThreePoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementThreeForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsThree = new GameObjectEventStack.ElementEvents();
            eventsThree.Pushed.AddListener(elementThreePushedMock.Listen);
            eventsThree.Popped.AddListener(elementThreePoppedMock.Listen);
            eventsThree.ForcePopped.AddListener(elementThreeForcePoppedMock.Listen);

            GameObject objectOne = new GameObject();
            GameObject objectTwo = new GameObject();
            GameObject objectThree = new GameObject();

            subject.elementEvents.Add(eventsOne);
            subject.elementEvents.Add(eventsTwo);
            subject.elementEvents.Add(eventsThree);

            subject.Push(objectOne);
            subject.Push(objectTwo);
            subject.Push(objectThree);

            elementOnePushedMock.Reset();
            elementOnePoppedMock.Reset();
            elementOneForcePoppedMock.Reset();
            elementTwoPushedMock.Reset();
            elementTwoPoppedMock.Reset();
            elementTwoForcePoppedMock.Reset();
            elementThreePushedMock.Reset();
            elementThreePoppedMock.Reset();
            elementThreeForcePoppedMock.Reset();

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);
            Assert.IsFalse(elementTwoPushedMock.Received);
            Assert.IsFalse(elementTwoPoppedMock.Received);
            Assert.IsFalse(elementTwoForcePoppedMock.Received);
            Assert.IsFalse(elementThreePushedMock.Received);
            Assert.IsFalse(elementThreePoppedMock.Received);
            Assert.IsFalse(elementThreeForcePoppedMock.Received);

            subject.PopAt(1);

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);
            Assert.IsFalse(elementTwoPushedMock.Received);
            Assert.IsTrue(elementTwoPoppedMock.Received);
            Assert.IsFalse(elementTwoForcePoppedMock.Received);
            Assert.IsFalse(elementThreePushedMock.Received);
            Assert.IsFalse(elementThreePoppedMock.Received);
            Assert.IsTrue(elementThreeForcePoppedMock.Received);

            Object.DestroyImmediate(objectOne);
            Object.DestroyImmediate(objectTwo);
            Object.DestroyImmediate(objectThree);
        }

        [Test]
        public void PopAtIndexInvalidOutOfBounds()
        {
            UnityEventListenerMock elementOnePushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOnePoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOneForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsOne = new GameObjectEventStack.ElementEvents();
            eventsOne.Pushed.AddListener(elementOnePushedMock.Listen);
            eventsOne.Popped.AddListener(elementOnePoppedMock.Listen);
            eventsOne.ForcePopped.AddListener(elementOneForcePoppedMock.Listen);

            GameObject objectOne = new GameObject();

            subject.elementEvents.Add(eventsOne); ;

            subject.Push(objectOne);

            elementOnePushedMock.Reset();
            elementOnePoppedMock.Reset();
            elementOneForcePoppedMock.Reset();

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);

            subject.PopAt(1);

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);

            Object.DestroyImmediate(objectOne);
        }

        [Test]
        public void PopAtIndexAlreadyPopped()
        {
            UnityEventListenerMock elementOnePushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOnePoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementOneForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsOne = new GameObjectEventStack.ElementEvents();
            eventsOne.Pushed.AddListener(elementOnePushedMock.Listen);
            eventsOne.Popped.AddListener(elementOnePoppedMock.Listen);
            eventsOne.ForcePopped.AddListener(elementOneForcePoppedMock.Listen);

            UnityEventListenerMock elementTwoPushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementTwoPoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementTwoForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsTwo = new GameObjectEventStack.ElementEvents();
            eventsTwo.Pushed.AddListener(elementTwoPushedMock.Listen);
            eventsTwo.Popped.AddListener(elementTwoPoppedMock.Listen);
            eventsTwo.ForcePopped.AddListener(elementTwoForcePoppedMock.Listen);

            UnityEventListenerMock elementThreePushedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementThreePoppedMock = new UnityEventListenerMock();
            UnityEventListenerMock elementThreeForcePoppedMock = new UnityEventListenerMock();
            GameObjectEventStack.ElementEvents eventsThree = new GameObjectEventStack.ElementEvents();
            eventsThree.Pushed.AddListener(elementThreePushedMock.Listen);
            eventsThree.Popped.AddListener(elementThreePoppedMock.Listen);
            eventsThree.ForcePopped.AddListener(elementThreeForcePoppedMock.Listen);

            GameObject objectOne = new GameObject();
            GameObject objectTwo = new GameObject();
            GameObject objectThree = new GameObject();

            subject.elementEvents.Add(eventsOne);
            subject.elementEvents.Add(eventsTwo);
            subject.elementEvents.Add(eventsThree);

            subject.Push(objectOne);
            subject.Push(objectTwo);
            subject.Push(objectThree);

            subject.PopAt(objectTwo);

            elementOnePushedMock.Reset();
            elementOnePoppedMock.Reset();
            elementOneForcePoppedMock.Reset();
            elementTwoPushedMock.Reset();
            elementTwoPoppedMock.Reset();
            elementTwoForcePoppedMock.Reset();
            elementThreePushedMock.Reset();
            elementThreePoppedMock.Reset();
            elementThreeForcePoppedMock.Reset();

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);
            Assert.IsFalse(elementTwoPushedMock.Received);
            Assert.IsFalse(elementTwoPoppedMock.Received);
            Assert.IsFalse(elementTwoForcePoppedMock.Received);
            Assert.IsFalse(elementThreePushedMock.Received);
            Assert.IsFalse(elementThreePoppedMock.Received);
            Assert.IsFalse(elementThreeForcePoppedMock.Received);

            subject.PopAt(1);

            Assert.IsFalse(elementOnePushedMock.Received);
            Assert.IsFalse(elementOnePoppedMock.Received);
            Assert.IsFalse(elementOneForcePoppedMock.Received);
            Assert.IsFalse(elementTwoPushedMock.Received);
            Assert.IsFalse(elementTwoPoppedMock.Received);
            Assert.IsFalse(elementTwoForcePoppedMock.Received);
            Assert.IsFalse(elementThreePushedMock.Received);
            Assert.IsFalse(elementThreePoppedMock.Received);
            Assert.IsFalse(elementThreeForcePoppedMock.Received);

            Object.DestroyImmediate(objectOne);
            Object.DestroyImmediate(objectTwo);
            Object.DestroyImmediate(objectThree);
        }
    }
}
