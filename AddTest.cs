using Godot;
using System;
using Efesus;

public class AddTest : Button
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    [Efesus.Profiler.Profile]
    public void _on_Button_pressed()
    {
        Efesus.Profiler.ProfilingCollection.add("btnTest");
    }
}