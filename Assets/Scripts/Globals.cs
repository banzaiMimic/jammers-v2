using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType
{
    MouseAndKeyboard,
    Controller,
}

/**
  makeshift singleton
*/
public sealed class Globals {

    private static readonly Globals instance = new Globals();

    public InputType inputType { get; set; }

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static Globals() {}
    private Globals(){}

    public static Globals Instance {
        get {
        return instance;
        }
    }
}