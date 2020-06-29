using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MoonSharp.Interpreter;
using System;
using System.Collections;
using System.IO;

/// <summary>
/// Attached to the disclaimer screen so you can skip it.
/// </summary>
public class DisclaimerScript : MonoBehaviour
{
    private static bool initial = false;

    private void Start()
    {

        if (!initial)
        {
            StaticInits.Start();
            SaveLoad.Start();
            new ControlPanel();
            new PlayerCharacter();
            SaveLoad.LoadAlMighty();
            StaticInits.MODFOLDER = "Undertale Disbelief Papyrus";
            StaticInits.ENCOUNTER = "by Cezar Andrade";
            StartCoroutine(LaunchMod());
        }
    }
    IEnumerator LaunchMod()
    {
        // First: make sure the mod is still here and can be opened
        if (!(new DirectoryInfo(Path.Combine(FileLoader.DataRoot, "Mods/" + StaticInits.MODFOLDER + "/Lua/Encounters/"))).Exists
         || !File.Exists(Path.Combine(FileLoader.DataRoot, "Mods/" + StaticInits.MODFOLDER + "/Lua/Encounters/" + StaticInits.ENCOUNTER + ".lua")))
        {
            Debug.LogWarning("Error detected! Mod or Encounter not found!");
            Application.Quit();
            yield break;
        }/**/

        yield return new WaitForEndOfFrame();
        StaticInits.Initialized = false;
        try
        {
            StaticInits.InitAll();
            Debug.Log("Loading: " + StaticInits.ENCOUNTER);
            GlobalControls.isInFight = true;
            SceneManager.LoadScene("Battle");
        }
        catch
        {
            Debug.LogError("it doesnt work");
        }
    }
}