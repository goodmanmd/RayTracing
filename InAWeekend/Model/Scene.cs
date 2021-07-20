using System.Collections.Generic;
using InAWeekend.Geometry;

namespace InAWeekend.Model
{
    class Scene : IHittable
    {
        private readonly List<IHittable> _objects = new List<IHittable>();

        public void Add(IHittable hittableObject) => _objects.Add(hittableObject);
        public void Clear() => _objects.Clear();
        
        public bool HitBy(Ray r, float min, float max, out HitRecord hit)
        {
            var closestHit = max;
            var hitSomething = false;
            hit = default;

            foreach (var obj in _objects)
            {
                if (!obj.HitBy(r, min, closestHit, out var hitRecord)) continue;

                closestHit = hitRecord.T;
                hit = hitRecord;
                hitSomething = true;
            }

            return hitSomething;
        }
    }
}
