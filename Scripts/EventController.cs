using Godot;
using System;
using System.Collections.Generic;

public partial class EventController : Node2D
{
    List<Event> events = new List<Event>();
    public override void _Ready()
    {
        events.Add(new Event() { Name = "It's noon" });
    }

    
}
