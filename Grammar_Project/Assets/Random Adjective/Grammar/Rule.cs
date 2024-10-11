using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Rule
{
    [Serializable]
    public class Output
    {
        [SerializeField]
        public List<string> words;

        [SerializeField, Range(0f, 1f)]
        public float weigth;
    }

    [SerializeField]
    public string input = "";

    public List<Output> outputs = new List<Output>();

    // meta inf
    public bool foldout = true;

}
