﻿using UnityEngine;

namespace Impact.Triggers
{
    [AddComponentMenu("Impact/2D Collision Triggers/Impact On Trigger Stay 2D", 0)]
    public class ImpactOnTriggerStay2D : ImpactSlideAndRollTriggerBase<ImpactCollisionSingleContactWrapper, ImpactContactPoint>
    {
        private void OnTriggerStay2D(Collider2D collider)
        {
            if (!Enabled || (!HighPriority && ImpactManagerInstance.HasReachedPhysicsInteractionsLimit()))
                return;

            ImpactManagerInstance.IncrementPhysicsInteractionsLimit();

            int otherPhysicsMaterialID = 0;
            if (collider.sharedMaterial != null)
                otherPhysicsMaterialID = collider.sharedMaterial.GetInstanceID();

            ImpactCollisionSingleContactWrapper c = new ImpactCollisionSingleContactWrapper(new ImpactContactPoint()
            {
                Point = transform.position,
                Normal = Vector3.zero,
                ThisObject = this.gameObject,
                OtherObject = collider.gameObject,
                PhysicsType = PhysicsType.Physics3D,
                ThisPhysicsMaterialID = 0,
                OtherPhysicsMaterialID = otherPhysicsMaterialID
            }, PhysicsType.Physics3D);

            //Ensure these settings are always overridden
            SlideMode = SlideMode.Normal;
            RollMode = RollMode.None;

            processCollision(c);
        }
    }
}