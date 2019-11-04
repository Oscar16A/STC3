using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement
{
    public float xScroll; // Scroll speed of level
    protected Vector2 velocity;
    protected float xAccel = 0, yAccel = 0;

    public Movement(float xScroll)
    {
        this.xScroll = xScroll;
        velocity = new Vector2(0, 0);
    }

    public Movement(float xScroll, Vector2 velocity)
    {
        this.xScroll = xScroll;
        this.velocity = velocity;
    }

    public abstract Vector2 Execute();

    public Vector2 TrueVelocity()
    {
        return new Vector2(xScroll + velocity.x, velocity.y);
    }

    public Vector2 Velocity()
    {
        return velocity;
    }
}

public class Static : Movement
{
    public Static(float xScroll) : base(xScroll)
    {

    }

    public override Vector2 Execute()
    {
        return new Vector2(xScroll, 0);
    }
}

// Don't use, does not work properly
public class ZigZag : Movement
{
    public ZigZag(float xScroll, float amplitude, float yVel) : base(xScroll)
    {

    }

    public ZigZag(float xScroll, Vector2 velocity, float amplitude, float yVel) : base(xScroll, velocity)
    {

    }

    public override Vector2 Execute()
    {
        return new Vector2();
    }
}

// Don't use, does not work properly
public class SineWave : Movement
{
    protected float amplitude;
    protected float period;
    private float time = 0;

    public SineWave(float xScroll, float amplitude) : base (xScroll)
    {
        this.amplitude = amplitude;
        velocity = Calculate();
    }

    public override Vector2 Execute()
    {
        return Calculate();
    }

    private Vector2 Calculate()
    {
        time += 0.01f;
        return new Vector2(xScroll + velocity.x, xScroll * amplitude * Mathf.Cos(xScroll * time) + velocity.y);
    }
}