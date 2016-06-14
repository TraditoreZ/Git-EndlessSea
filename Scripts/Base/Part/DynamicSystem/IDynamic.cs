using UnityEngine;

namespace Assets.Scripts.Base.Part.DynamicSystem
{
    public interface IDynamic
    {
        Vector3 Dynamic { get; set; }
        Vector3 CenterMass { get; set; }
    }
}
