using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class RuleView : VisualElement
{
    public TextField input;

    public RuleView(Rule rule, Grammar owner)
    {
        var visualTree = Resources.Load<VisualTreeAsset>("rule_view");
        visualTree.CloneTree(this);

        // container
        var container = this.Q<VisualElement>("output_continer");

        // input
        input = this.Q<TextField>("input_field");
        input.value = rule.input;
        input.RegisterValueChangedCallback(evt =>
        {
            rule.input = evt.newValue;
        });

        // outputs
        for (int i = 0; i < rule.outputs.Count; i++)
        {
            var output = rule.outputs[i];
            var outputView = new OutputView(output, rule, owner);

            container.Insert(container.childCount - 1, outputView);
        }

        // add outputs
        var addbtn = this.Q<Button>("add");
        addbtn.clicked += () =>
        {
            var output = new Rule.Output() { words = new List<string>() { "outputWord" }, weigth = 1f };
            rule.outputs.Add(output);
            container.Insert(container.childCount - 1, new OutputView(output, rule, owner));
        };

    }



}

public class OutputView : VisualElement
{
    public static string  split = ",";

    public OutputView(Rule.Output output, Rule ruleOwner, Grammar grammarOwner)
    {
        var visualTree = Resources.Load<VisualTreeAsset>("output_view");
        visualTree.CloneTree(this);

        // words
        var outputField = this.Q<TextField>("output_field");
        outputField.value = string.Join(split, output.words);
        outputField.RegisterValueChangedCallback(evt =>
        {
            var outputs = evt.newValue.Split(split);

            var v = "";
            for (int i = 0; i < outputs.Length - 1; i++)
            {
                var o = outputs[i];
                v += o + split;
            }
            v += outputs[outputs.Length - 1];

            output.words = outputs.ToList();

            EditorUtility.SetDirty(grammarOwner);
        });

        // weight
        var weightField = this.Q<Slider>("weight_slider");
        weightField.value = output.weigth;
        weightField.RegisterValueChangedCallback(evt =>
        {
            output.weigth = evt.newValue;
            EditorUtility.SetDirty(grammarOwner);
        });

        // delete
        var delete = this.Q<Button>("delete");
        delete.clicked += () =>
        {
            ruleOwner.outputs.Remove(output);
            EditorUtility.SetDirty(grammarOwner);

            this.RemoveFromHierarchy();
        };
    }
}
