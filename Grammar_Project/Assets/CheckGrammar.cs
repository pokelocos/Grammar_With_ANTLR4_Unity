using Antlr4.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Antlr4.Runtime.Tree;
using System.IO;
using System;
using System.Linq;
using UnityEditor;
using Antlr4.Runtime.Misc;

public class CheckGrammar : MonoBehaviour
{
    private Grammar grammar;

    public TMP_Dropdown grammarDropdown;
    public TMP_Dropdown lexerDropdown;
    public TMP_Dropdown parserDropdown;
    public TMP_InputField inputField;
    public Button check;

    private Type lexerType;
    private Type parserType;

    private List<Grammar> allGrammars;
    private List<Type> lexerclasses;
    private List<Type> parserclasses;

    private void Start()
    {
        allGrammars = TypeFinder.FindAll<Grammar>();
        grammarDropdown.ClearOptions();
        grammarDropdown.AddOptions(allGrammars.Select(g => g.name).ToList());
        grammarDropdown.onValueChanged.AddListener((n) => { grammar = allGrammars[n];  Debug.Log(grammar.GetG4()); });
        grammar = allGrammars[0];
        Debug.Log(grammar.GetG4());

        lexerclasses = TypeFinder.GetAllSubclassesOf(typeof(Lexer));
        lexerDropdown.ClearOptions();
        lexerDropdown.AddOptions(lexerclasses.Select(l => l.Name).ToList());
        lexerDropdown.onValueChanged.AddListener((n) => { lexerType = lexerclasses[n]; });
        lexerType = lexerclasses[0];

        parserclasses = TypeFinder.GetAllSubclassesOf(typeof(Parser));
        parserDropdown.ClearOptions();
        parserDropdown.AddOptions(parserclasses.Select(p => p.Name).ToList());
        parserDropdown.onValueChanged.AddListener((n) => { parserType = parserclasses[n]; });
        parserType = parserclasses[0];

    }

    public void CheckSentence()
    {
        // transform grmar in usable format
        var content = grammar.GetG4();
        var tempG4 = Path.Combine(Path.GetTempPath(), "tempGrammar.g4");
        File.WriteAllText(tempG4, content);

        // get the sentence from the input field
        string sentence = inputField.text;

        if (string.IsNullOrEmpty(sentence))
        {
            Debug.Log("The sentence is empty");
            return;
        }

        // check if the sentence is correct
        try
        {
            var antlerStream = new AntlrInputStream(sentence);
            var lexer = Activator.CreateInstance(lexerType, antlerStream) as Lexer;
            var tokenStream = new CommonTokenStream(lexer);
            var parser = Activator.CreateInstance(parserType, tokenStream) as Parser;

            //var errorLexer = new LexerErrorListener();
            var errorLexer = new ErrorLexer();
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(errorLexer);

            //var errorParser = new ParserErrorListener();
            var errorParser = new ErrorParser();
            parser.RemoveErrorListeners();
            parser.AddErrorListener(errorParser);
            //parser.AddParseListener(new aaaa());

            var parseMethod = parserType.GetMethod("start");
            //var parseMethod = parserType.GetMethod(grammar.rootWord);
            var tree = parseMethod.Invoke(parser, null) as IParseTree;
            Debug.Log(tree.ToStringTree(parser));

            var hasErrors = errorLexer.HasErrors || errorParser.HasErrors;
            Debug.Log("The sentence is: " + !hasErrors);
            check.GetComponent<Image>().color = hasErrors ? Color.red : Color.green;
        }
        catch (Exception e)
        {
            Debug.Log("ERROR: " + e);
        }
    }

    public void GenerateRand()
    {
        var sentece = new List<string> { grammar.rootWord };
        var _out = grammar.ExpandSentence(sentece);
        inputField.text = string.Join("", _out);
    }

    public void ShowG4Grammar()
    {
        string g4Grammar = grammar.GetG4();
        Debug.Log(g4Grammar);
    }



}

public class aaaa : IParseTreeListener
{
    public void EnterEveryRule(ParserRuleContext ctx)
    {
        Debug.Log("AAA");
    }

    public void ExitEveryRule(ParserRuleContext ctx)
    {
        Debug.Log("BBB");
    }

    public void VisitErrorNode(IErrorNode node)
    {
        Debug.Log("CCC");
    }

    public void VisitTerminal(ITerminalNode node)
    {
        Debug.Log("DDD");
    }
}

public class ParserErrorListener : BaseErrorListener
{
    public override void SyntaxError(
        TextWriter output, IRecognizer recognizer,
        IToken offendingSymbol, int line,
        int charPositionInLine, string msg,
        RecognitionException e)
    {
        string sourceName = recognizer.InputStream.SourceName;
        Console.WriteLine("line:{0} col:{1} src:{2} msg:{3}", line, charPositionInLine, sourceName, msg);
        Console.WriteLine("--------------------");
        Console.WriteLine(e);
        Console.WriteLine("--------------------");
    }
}

public class LexerErrorListener : IAntlrErrorListener<int>
{
    public void SyntaxError(
        TextWriter output, IRecognizer recognizer,
        int offendingSymbol, int line,
        int charPositionInLine, string msg,
        RecognitionException e)
    {
        string sourceName = recognizer.InputStream.SourceName;
        Console.WriteLine("line:{0} col:{1} src:{2} msg:{3}", line, charPositionInLine, sourceName, msg);
        Console.WriteLine("--------------------");
        Console.WriteLine(e);
        Console.WriteLine("--------------------");
    }
}


internal class ErrorLexer : IAntlrErrorListener<int>
{
    public bool HasErrors { get; private set; } = false;

    public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        HasErrors = true;
        Debug.Log("Error: " + msg);
    }
}



public class ErrorParser : IAntlrErrorListener<IToken>
{
    public bool HasErrors { get; private set; } = false;

    public void SyntaxError(
        TextWriter output,
        IRecognizer recognizer,
        IToken offendingSymbol,
        int line,
        int charPositionInLine,
        string msg,
        RecognitionException e)
    {

        HasErrors = true;
        Debug.Log("Error: " + msg);
    }
}

public class TypeFinder
{
    public static List<Type> GetAllSubclassesOf(Type baseType)
    {
        // Obtener todos los tipos de todos los ensamblados cargados
        var types = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(assembly => assembly.GetTypes())
                        .Where(type => type.IsClass && !type.IsAbstract && baseType.IsAssignableFrom(type))
                        .ToList();

        return types;
    }

    public static List<T> FindAll<T>() where T : ScriptableObject
    {
        List<T> result = new List<T>();

        // Buscar todos los assets de tipo T en el proyecto
        string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");

        // Cargar cada asset y añadirlo a la lista de resultados
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            T asset = AssetDatabase.LoadAssetAtPath<T>(path);
            if (asset != null)
            {
                result.Add(asset);
            }
        }

        return result;
    }
}