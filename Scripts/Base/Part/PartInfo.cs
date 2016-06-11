using UnityEngine;
using System.Collections;

public class PartInfo : CubeInfo
{
    private Transform particle;
    public bool Use { get { return _use; } }
    [SerializeField]
    private bool _use;

    public override void Instance(ItemType type, CubeType materialType)
    {
        mtype = type;
        var collider = transform.GetComponentsInChildren<Collider>();
        if (collider != null)
        {
            for (int i = 0; i < collider.Length; i++)
            {
                collider[i].transform.tag = "Part";
                collider[i].gameObject.AddComponent<PartLocationParent>().parent = transform;
            }
        }
        particle = Global.FindChild(transform, "Particle");
    }

    public override Vector3 GetParticle()
    {
        return particle.position;
    }

}
