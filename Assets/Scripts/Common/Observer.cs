using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

namespace QuangDM.Common
{
    public static class EventName
    {
        public static readonly string BeginChain = "BeginChain";
        public static readonly string EnterCell = "EnterCell";
        public static readonly string ExitCell = "ExitCell";
        public static readonly string MatchChain = "MatchChain";
        public static readonly string ResetChain = "ResetChain";
        public static readonly string AddToChain = "AddToChain";
        public static readonly string RemoveFromChain = "RemoveFromChain";
        public static readonly string DoAnim = "DoAnim";
    }
    public class Observer : MonoBehaviour
    {

        public static Observer Instance;
        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        public delegate void CallBackObserver(System.Object data);

        Dictionary<string, HashSet<CallBackObserver>> dictObserver = new Dictionary<string, HashSet<CallBackObserver>>();
        // Use this for initialization
        public void AddObserver(string topicName, CallBackObserver callbackObserver)
        {
            HashSet<CallBackObserver> listObserver = CreateListObserverForTopic(topicName);
            listObserver.Add(callbackObserver);
        }

        public void RemoveObserver(string topicName, CallBackObserver callbackObserver)
        {
            HashSet<CallBackObserver> listObserver = CreateListObserverForTopic(topicName);
            listObserver.Remove(callbackObserver);
        }

        public void Notify<OData>(string topicName, OData Data) where OData : MonoBehaviour
        {
            HashSet<CallBackObserver> listObserver = CreateListObserverForTopic(topicName);
            foreach (CallBackObserver observer in listObserver)
            {
                observer(Data);
            }
        }
        public void Notify(string topicName, System.Object Data)
        {
            HashSet<CallBackObserver> listObserver = CreateListObserverForTopic(topicName);
            foreach (CallBackObserver observer in listObserver)
            {
                observer(Data);
            }
        }
        public void Notify(string topicName)
        {
            HashSet<CallBackObserver> listObserver = CreateListObserverForTopic(topicName);
            foreach (CallBackObserver observer in listObserver)
            {
                observer(null);
            }
        }

        protected HashSet<CallBackObserver> CreateListObserverForTopic(string topicName)
        {
            if (!dictObserver.ContainsKey(topicName))
                dictObserver.Add(topicName, new HashSet<CallBackObserver>());
            return dictObserver[topicName];
        }

    }
}