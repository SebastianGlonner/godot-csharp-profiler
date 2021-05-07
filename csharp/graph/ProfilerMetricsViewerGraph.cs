using cSharpProfiler.csharp.graph;
using Godot;
using System;
using System.Collections.Generic;

public class ProfilerMetricsViewerGraph : Control
{
    static private Color SYSTEM_LINE = new Color(0, 0, 0);
    static private Color GRAPH_LINE = new Color(127, 0, 0);

    private float height = 1;
    private int width = 300;

    private float maxY = 0;

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
        DrawLine(new Vector2(0, 0), new Vector2(0, height), SYSTEM_LINE, 1);
        DrawLine(new Vector2(0, height), new Vector2(width, height), SYSTEM_LINE, 1);
    }

    private void _DrawData()
    {
        for(int i = 1; i < this.data.Count; i++ )
        {
            Vector2 p = this.data[i];
            this.calcHeight(p);
            DrawLine(this.projectY(this.data[i-1]), this.projectY(p), GRAPH_LINE, 1);
        }
    }

    private void calcHeight(Vector2 p)
    {
        this.maxY = Math.Max(this.maxY, p.y);
        this.height = (int)Math.Ceiling(this.maxY * 1.1f);
    }

    private Vector2 projectY(Vector2 p)
    {
        return new Vector2(p.x, height - p.y);
    }
}
