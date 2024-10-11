using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New grammar", menuName = "Grammar/New Grammar...")]
public class Grammar : ScriptableObject
{
    private const int MAX_ITERATIONS = int.MaxValue; // int.MaxValue = 2.147.483.647

    [SerializeField]
    public List<Rule> rules = new List<Rule>();

    public string lexerRef;
    public string parserRef;

    public string rootWord;

    public List<string> ExpandSentence(List<string> sentence, int maxIteration = MAX_ITERATIONS) // FIX: change name to DeriveSentence
    {
        var toR = new List<string>(sentence);
        for (int i = 0; i < toR.Count; i++)
        {
            var word = toR[i];

            if (IsExpandable(word))
            {
                var result = ExpandWord(word);
                toR.RemoveAt(i);
                toR.InsertRange(i, result);

                maxIteration--;

                if (maxIteration <= 0)
                {
                    Debug.Log("[Warning]: Max iterations reached");
                    break;
                }
                i--;
            }
        }

        return toR;
    }

    public List<string> ExpandWord(string word) // FIX: change name to DeriveWord
    {
        try
        {
            var rule = rules.Find(x => x.input == word);
            return rule.outputs.RandomRullete(r => r.weigth).words;
        }
        catch(Exception e)
        {
            Debug.Log("Word: '" +word+ "' cannot be Expanded.");
            return new List<string>() { "-" };
        }
    } 

    public bool IsExpandable(string word) // FIX: change name to isDerivable
    {
        try
        {
            var rule = rules.Find(x => x.input.Equals(word));
            return rule.outputs.Count > 0;
        }
        catch (Exception e)
        {
            return false;
        }

        return char.IsUpper(word[0]); 
    } 

    public void GenerateANTLR_code()
    { 
    
    }

    public string GetG4()
    {
        var value = "";

        value += "grammar " + name + ";\n\n";

        foreach (var rule in rules)
        {
            value += rule.input + " : ";
            var output = rule.outputs;

            foreach (var o in output)
            {
                var words = o.words;
                foreach (var w in words)
                {
                    if (IsExpandable(w))
                    {
                        value += w;
                    }
                    else
                    {
                        value += "'" + w + "'";
                    }
                    value += " ";
                }

                if(o != output.Last()) value += "|";
            }
            value = value.TrimEnd();
            value += ";\n";
        }

        return value;
    }
}