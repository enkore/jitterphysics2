using System;
using Jitter2.Collision;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Jitter2.LinearMath;
using JitterDemo.Renderer;
using JitterDemo.Renderer.OpenGL;

namespace JitterDemo;

public partial class Playground : RenderWindow
{
    private bool debugDrawIslands;
    private bool debugDrawContacts;
    private bool debugDrawShapes;
    private bool debugDrawTree;
    private int debugDrawTreeDepth = 1;

    private readonly Action<JBBox, int> drawBox;

    private void DrawBox(JBBox box, int depth)
    {
        if (depth != debugDrawTreeDepth) return;
        DebugRenderer.PushBox(DebugRenderer.Color.Green, Conversion.FromJitter(box.Min),
            Conversion.FromJitter(box.Max));
    }

    public Vector3 rayHitPoint = Vector3.Zero;

    public void DebugDraw()
    {
        //DebugRenderer.PushPoint(DebugRenderer.Color.White, rayHitPoint);

        if (debugDrawTree)
        {
            World.DynamicTree.EnumerateAll(drawBox);
        }


        if (debugDrawShapes)
        {
            foreach (RigidBody body in World.RigidBodies)
            {
                foreach (Shape s in body.Shapes)
                {
                    var bb = s.WorldBoundingBox;
                    DebugRenderer.PushBox(DebugRenderer.Color.Green, Conversion.FromJitter(bb.Min),
                        Conversion.FromJitter(bb.Max));
                }
            }
        }

        if (debugDrawIslands)
        {
            for (int i = 0; i < World.Islands.Count; i++)
            {
                Island island = World.Islands[i];

                JBBox box = JBBox.SmallBox;
                foreach (RigidBody body in island.Bodies)
                {
                    foreach (Shape s in body.Shapes)
                        JBBox.CreateMerged(box, s.WorldBoundingBox, out box);
                }

                DebugRenderer.PushBox(DebugRenderer.Color.Red, Conversion.FromJitter(box.Min),
                    Conversion.FromJitter(box.Max));
            }
        }

        if (debugDrawContacts)
        {
            var contacts = World.RawData.ActiveContacts;

            for (int i = 0; i < contacts.Length; i++)
            {
                ref var cq = ref contacts[i];

                void DrawContact(in ContactData cq, in ContactData.Contact c)
                {
                    JVector v1 = c.RelativePos1 + cq.Body1.Data.Position;
                    JVector v2 = c.RelativePos2 + cq.Body2.Data.Position;

                    DebugRenderer.PushPoint(DebugRenderer.Color.Green, Conversion.FromJitter(v1), 0.1f);
                    DebugRenderer.PushPoint(DebugRenderer.Color.White, Conversion.FromJitter(v2), 0.1f);
                }

                if ((cq.UsageMask & 0b0001) != 0) DrawContact(cq, cq.Contact0);
                if ((cq.UsageMask & 0b0010) != 0) DrawContact(cq, cq.Contact1);
                if ((cq.UsageMask & 0b0100) != 0) DrawContact(cq, cq.Contact2);
                if ((cq.UsageMask & 0b1000) != 0) DrawContact(cq, cq.Contact3);
            }
        }
    }
}