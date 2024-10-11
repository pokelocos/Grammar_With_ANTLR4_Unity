using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;
using System.Linq;
using System.Linq.Expressions;
using System.IO;

[CustomEditor(typeof(Grammar))]
public class GrammarEditor : Editor
{
    private static string antlrPath = @"Assets/ANTLR-4/antlr-4.13.2-complete.jar";

    private Grammar grammar;

    private bool showRules = true;
    private VisualElement containerRules;
    private VisualElement containerMetadata;

    private void OnEnable()
    {
        grammar = target as Grammar;
    }

    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();

        // Root word
        var rootWord = new TextField("Root Word");
        rootWord.value = grammar.rootWord;
        rootWord.RegisterValueChangedCallback(evt =>
        {
            grammar.rootWord = evt.newValue;
            EditorUtility.SetDirty(grammar);
        });
        root.Add(rootWord);

        // Rules
        containerRules = new VisualElement();
        root.Add(containerRules);

        for (int i = 0; i < grammar.rules.Count; i++)
        {
            var rule = grammar.rules[i];
            AddRuleContainer(containerRules, rule);
        }

        var button = new Button(() =>
        {
            var rule = new Rule();
            rule.input = "inputWord";
            var output = new Rule.Output() { words = new List<string>() { "outputWord" }, weigth = 1f };
            rule.outputs.Add(output);
            grammar.rules.Add(rule);

            AddRuleContainer(containerRules, rule);

            EditorUtility.SetDirty(grammar);
        });
        button.text = "Add Rule";
        root.Add(button);

        containerMetadata = Metadata();
        root.Add(containerMetadata);

        var alphabetic = Alphabetic();
        root.Add(alphabetic);

        // Create ANTLR files
        var genANTRL = new Button(() =>
        {
            var content = grammar.GetG4();

            // get root path
            var rootPath = Application.dataPath;
            var path = rootPath + "/" + target.name + " (ANTLR)";
            var name = target.name + ".g4";
            var filePath = path + "/" + name;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            System.IO.File.WriteAllText(filePath, content);

            var arguments = $"-Dlanguage=CSharp -o \"{path}\" \"{filePath}\"";
            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "java",
                Arguments = $"-jar \"{antlrPath}\" {arguments}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = new System.Diagnostics.Process
            {
                StartInfo = startInfo
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            Debug.Log($"Output: {output}");
            Debug.Log($"Error: {error}");

            AssetDatabase.Refresh();
        });
        genANTRL.text = "Generate ANTLR";
        root.Add(genANTRL);

        return root;
    }

    private void ShowRules(bool value)
    {
        containerRules.style.display = value ? DisplayStyle.Flex : DisplayStyle.None;
        containerMetadata.style.display = !value ? DisplayStyle.Flex : DisplayStyle.None;
        showRules = value;
    }

    private VisualElement Metadata()
    {
        var r = new VisualElement();
        
        var l = new Label("Rules: " + grammar.rules.Count);
        r.Add(l);

        var variables = new List<string>();
        var terminals = new List<string>();
        for (int i = 0; i < grammar.rules.Count(); i++)
        {
            var rule = grammar.rules[i];
            if (grammar.IsExpandable(rule.input) && !variables.Contains(rule.input))
            {
                variables.Add(rule.input);
            }

            for (int j = 0; j < rule.outputs.Count(); j++)
            {
                var output = rule.outputs[j];

                for (int k = 0; k < output.words.Count(); k++)
                {
                    var o = output.words[k];
                    if (!terminals.Contains(o))
                    {
                        terminals.Add(o);
                    }
                }
            }
        }   
        var l2 = new Label("Variables: " + grammar.rules.Count(x => grammar.IsExpandable(x.input)));
        r.Add(l2);

        var l3 = new Label("Terminals: " + grammar.rules.Count(x => !grammar.IsExpandable(x.input)));
        r.Add(l3);

        return r;
    }

    private VisualElement Alphabetic()
    {
        var words = new List<string>();
        for (int i = 0; i < grammar.rules.Count(); i++)
        {
            var rule = grammar.rules[i];

            if (!words.Contains(rule.input))
            {
                words.Add(rule.input);
            }

            for (int j = 0; j < rule.outputs.Count(); j++)
            {
                var output = rule.outputs[j];

                for (int k = 0; k < output.words.Count(); k++)
                {
                    var o = output.words[k];
                    if (!words.Contains(o))
                    {
                        words.Add(o);
                    }
                }
            }
        }

        words.Sort();

        var l = new Label("Alphabetic: \n");
        for (int i = 0; i < words.Count; i++)
        {
            l.text += words[i] + "\n";
        }
        return l;
    }

    private void AddRuleContainer(VisualElement root, Rule rule, int index = 0)
    {
        // rule view
        var view = new RuleView(rule, grammar);

        // container
        var container = new SimpleContainerView(view);
        view.input.RegisterValueChangedCallback(evt =>
        {
            container.SetName(rule.input);
            EditorUtility.SetDirty(grammar);
        });
        container.OnRemove += (element) =>
        {
            //Undo.RecordObject(grammar, "Remove Rule");
            grammar.rules.Remove(rule);

            var c = container;
            root.Remove(c);

        };
        container.SetName(rule.input);

        // foldout
        container.foldout.RegisterValueChangedCallback(evt =>
        {
            rule.foldout = evt.newValue;
            container.SetFoldout(rule.foldout);

            EditorUtility.SetDirty(grammar);
        });
        container.foldout.value = rule.foldout;
        container.SetFoldout(rule.foldout);

        root.Insert(index, container);
    }
}
