using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePainter : Painter
{
    public float minRadius = 0.05f;
    public float maxRadius = 0.2f;
    [Space]
    ParticleSystem part;
    List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        Paintable p = other.GetComponent<Paintable>();
        if (p != null)
        {
            for (int i = 0; i < numCollisionEvents; i++)
            {
                Vector3 pos = collisionEvents[i].intersection;
                float radius = Random.Range(minRadius, maxRadius);
                PaintManager.Instance.Paint(p, pos, radius, hardness, strength, paintColor);
            }
        }
    }
}
