using System.Collections.Generic;

namespace InAWeekend.Geometry
{
    class Scene : IHittable
    {
        private readonly List<IHittable> _objects = new List<IHittable>();

        public void Add(IHittable hittableObject) => _objects.Add(hittableObject);
        public void Clear() => _objects.Clear();
        
        public bool Hit(Ray r, float min, float max, out HitRecord hit)
        {
            var closestHit = max;
            var hitSomething = false;
            hit = default;

            foreach (var hittable in _objects)
            {
                if (hittable.Hit(r, min, max, out var hitRecord) && hitRecord.T < closestHit)
                {
                    closestHit = hitRecord.T;
                    hit = hitRecord;
                    hitSomething = true;
                }
            }

            return hitSomething;
        }
    }
}
