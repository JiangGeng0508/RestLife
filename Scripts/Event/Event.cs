using Godot;
using System;
using System.Collections.Generic;

public class Event
{
    public string Name { get; set; }
    

    public void Trigger()
    {
        Godot.GD.Print("Triggered event: " + Name);
    }
}