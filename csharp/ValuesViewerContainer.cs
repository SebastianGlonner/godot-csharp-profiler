using Godot;
using System;
using System.Collections.Generic;

namespace Efesus.Profiler
{
    public class ValuesViewerContainer : GridContainer
    {

        private ProfilerMetricsViewerGraph graphControl;

        private int columns = 3;

        private Dictionary<string, Label[]> valueLabels = new Dictionary<string, Label[]>();

        private float timePassed = 0;
        private int seconds = 0;
        private int frameCount = 0;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            graphControl = GetTree().Root.FindNode("ProfilerMetricsViewerGraph", true, false) as ProfilerMetricsViewerGraph;

            this.Columns = this.columns;
            ProfilingCollection.NewMetric += NewMetric;
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(float delta)
        {
            ProfilingCollection.add("frame");

            ProfilingCollection.ProcessTime(delta);

            ProcessGraph(delta);

            foreach (KeyValuePair<string, Metric> entry in ProfilingCollection.values)
            {
                var labels = valueLabels[entry.Key];
                var metric = entry.Value;
                labels[0].Text = "" + metric.currentSum();
                labels[1].Text = "" + metric.currentInterval();
            }
        }

        public void ProcessGraph(float delta)
        {

            Metric metric;
            if (this.graphControl != null && ProfilingCollection.values.TryGetValue("frame", out metric))
            {
                this.graphControl.addPoint(new Vector2(frameCount++, (float)metric.currentInterval()));
            }
            //timePassed += delta;
            //if (timePassed >= 1)
            //{
            //    seconds++;
            //    Metric metric;
            //    if (this.graphControl != null && ProfilingCollection.values.TryGetValue("frame", out metric))
            //    {
            //        this.graphControl.addPoint(new Vector2(seconds, (float)metric.currentInterval()));
            //    }

            //    timePassed = 0;
            //}

        }

        private void NewMetric(object sender, ProfilingCollection.NewMetricArgs args)
        {
            this.newAttachedLabel(args.name);

            int bufferLength = this.columns - 1;

            Label[] labels = new Label[bufferLength];
            Metric[] buffers = new Metric[bufferLength];
            for ( int i = 0; i < bufferLength; i++)
            {
                labels[i] = this.newAttachedLabel("" + 0);
            }

            valueLabels[args.name] = labels;
        }

        private Label newAttachedLabel(string name)
        {
            Label newLabel = new Label();
            newLabel.Text = name;
            AddChild(newLabel);
            return newLabel;
        }
    }
}