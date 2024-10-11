# General Description

This project is a custom implementation of generative grammars as an extension in the Unity 3D game engine. In this project, you can create and modify your own grammars, generate sentences based on them, and also check if a sentence belongs to the grammar thanks to the integration of the ANTLR4 library.

## Requirements
* This project uses ANTLR 4.13.2, which requires the installation of Java 11 to execute some of its functions.
* This project makes use of UIBuilder and Unity Toolkit features, so it requires Unity 2022.3.15. I cannot guarantee it will work on versions lower or higher than this.

# Creation of Grammar Assets
In this implementation, the grammar is an object inherited from ScriptableObject, so you can create your own grammar very similarly to how most assets are created in Unity. Simply right-click in a folder, then select **Create > Grammar > New Grammar...**. This will create a grammar with the default name `New_Grammar`, and you should rename it appropriately to avoid conflicts between files.

**Caution**: Some systems in this tool may fail if the grammar name contains spaces or special characters. It is recommended to use underscores ("_") instead.

## Modifying Grammars
* Grammars contain a main word called **RootWord**. Make sure to select it and create rules that can derive from this main word.
* Grammars are composed of sets of rules. Each rule consists of a variable called **input** and a group of derivations called **outputs**.
* This system is weighted, meaning each **output** has a weight that translates into its probability of being derived compared to others.

## Generating Sentences Based on the Grammar

* Grammars can derive both specific words and complete sentences. You can do this by calling the methods **yourGrammar.ExpandSentence** or **yourGrammar.ExpandWord**.
* This project includes a demo scene called **SampleScene**, where you can assign a grammar and generate sentences derived from it through the game interface.

![image](https://github.com/user-attachments/assets/5cd86c85-2588-4f33-a03f-35c425711d0a)
* You can add a new rule by pressing the **New Rule** button and delete it with the trash icon button next to it.
* You can add a new output with the **+** button for each rule and delete it with the **-** button next to it.
* Outputs can be strings with more than one word. Make sure to separate these with commas (",") so the system can identify them as such.

**Caution**: We recommend not adding words that contain spaces. Instead, use underscores ("_").

## Checking if a Sentence Belongs to a Grammar

* This step is more complex as it uses the ANTLR4 library. To perform this step, you must generate the code that handles this functionality by pressing the "Generate ANTLR Code" button found in the grammar files.
* In **SampleScene**, you will find a system you can review and execute to check if a sentence belongs to the selected grammar. Make sure the corresponding **Lexer** and **Parser** are properly assigned. If they are not, the checking process will fail.

![image](https://github.com/user-attachments/assets/2acb7780-daed-497b-916a-de7eeb3075cc)
