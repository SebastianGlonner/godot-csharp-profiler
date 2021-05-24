using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpProfiler.csharp.graph
{
    public class Projection
    {
        private float maxHeight;
        private float maxValue;

        public Projection(float maxHeight, float maxValue)
        {
            this.maxHeight = maxHeight;
            this.maxValue = maxValue;
        }

        public float getMaxHeight()
        {
            return maxHeight;
        }

        public Vector2 project(Vector2 p)
        {
            float yPercent = (100 * p.y) / maxValue;
            float yProjected = (yPercent * maxHeight) / 100;

            return new Vector2(p.x, Math.Max(maxHeight - yProjected, 0));
        }
    }
}
