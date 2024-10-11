using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.IO;
using System;
using Debug = UnityEngine.Debug;
using System.Linq;

[InitializeOnLoad]
public class BeforeAssemblyReload
{
    private static string antlrPath = @"Assets/ANTLR-4/antlr-4.13.2-complete.jar";

    static BeforeAssemblyReload()
    {
        //AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;
    }

    private static void OnBeforeAssemblyReload()
    {
        Debug.Log("Crear scripts G4");



        string[] guids = AssetDatabase.FindAssets("t:Grammar"); // Reemplaza con el tipo de tu ScriptableObject
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Grammar grammar = AssetDatabase.LoadAssetAtPath<Grammar>(path);

            if (grammar != null)
            {
                var content = grammar.GetG4();
                var tempG4 = Path.Combine(Path.GetTempPath(), "tempGrammar.g4");
                File.WriteAllText(tempG4, content);

                GenerateLexerAndParser(tempG4);
            }
        }

    }

    private static void GenerateLexerAndParser(string G4Path)
    {
        string outputDirectory = Path.GetDirectoryName(G4Path); // Directorio donde se guardarán los archivos generados

        // Crear el comando para ejecutar ANTLR
        string arguments = $"-Dlanguage=CSharp -o \"{outputDirectory}\" \"{G4Path}\"";

        // Configurar el proceso para ejecutar ANTLR
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "java", // Utiliza Java para ejecutar ANTLR
            Arguments = $"-jar \"{antlrPath}\" {arguments}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        try
        {
            using (Process process = new Process())
            {
                process.StartInfo = startInfo;

                // Inicia el proceso
                process.Start();

                // Lee la salida y errores
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                // Espera a que el proceso termine
                process.WaitForExit();

                // Imprimir la salida y errores para depuración
                if (!string.IsNullOrEmpty(output))
                {
                    Console.WriteLine("Salida de ANTLR: " + output);
                }

                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine("Error de ANTLR: " + error);
                }

                // Verifica el código de salida para asegurarte de que se ejecutó correctamente
                if (process.ExitCode != 0)
                {
                    throw new Exception("Error al generar el lexer y parser. Código de salida: " + process.ExitCode);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocurrió un error al ejecutar ANTLR: " + ex.Message);
        }
    }

}