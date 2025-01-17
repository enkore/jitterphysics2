using System.Collections.Generic;
using Jitter2;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Jitter2.LinearMath;
using JitterDemo.Renderer;

namespace JitterDemo;

public class Demo10 : IDemo
{
    public string Name => "Stacked Cubes";

    public void Build()
    {
        var pg = (Playground)RenderWindow.Instance;
        World world = pg.World;

        pg.ResetScene();

        List<RigidBody> bodies = new();

        for (int i = 0; i < 32; i++)
        {
            var body = world.CreateRigidBody();
            bodies.Add(body);

            body.Position = new JVector(0, 0.5f + i * 0.999f, 0);
            body.AddShape(new BoxShape(1));
            body.Damping = (0.998f, 0.998f);
        }

        for (int i = 0; i < 32; i++)
        {
            var body = world.CreateRigidBody();
            bodies.Add(body);

            body.Position = new JVector(10, 0.5f + i * 0.999f, 0);
            body.AddShape(new ConeShape());
            body.Damping = (0.998f, 0.998f);
        }


        world.SolverIterations = 4;
        world.NumberSubsteps = 3;
    }

    public void Draw()
    {
    }
}