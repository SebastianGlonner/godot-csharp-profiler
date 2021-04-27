using cSharpProfiler.csharp.graph;
using Godot;
using System;
using System.Collections.Generic;

public class ProfilerMetricsViewerGraph : Control
{
    static private Color SYSTEM_LINE = new Color(0, 0, 0);
    static private Color GRAPH_LINE = new Color(127, 0, 0);

    private List<Vector2> data = new List<Vector2>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.data.Add(new Vector2(0, 0));
    }

    public override void _Draw()
    {
        _DrawSystem();
        _DrawData();
    }

    public void addPoint(Vector2 p)
    {
        this.data.Add(p);
        Update();
    }

    private void _DrawSystem()
    {
        DrawLine(new Vector2(0, 0), new Vector2(0, 150), SYSTEM_LINE, 1);
        DrawLine(new Vector2(0, 150), new Vector2(300, 150), SYSTEM_LINE, 1);
    }

    private void _DrawData()
    {
        for(int i = 1; i < this.data.Count; i++ )
        {
            DrawLine(this.data[i-1], this.data[i], GRAPH_LINE, 1);
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
