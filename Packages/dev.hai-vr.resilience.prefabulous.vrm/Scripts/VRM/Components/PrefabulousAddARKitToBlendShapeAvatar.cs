using UnityEngine;
#if PREFABULOUS_FOR_VRM_VRCHAT_IS_INSTALLED
using IPrefabulousEditorOnly = VRC.SDKBase.IEditorOnly;
#else
using Prefabulous.Universal.Shared.Runtime;
#endif

namespace Prefabulous.VRM.Runtime
{
    [AddComponentMenu("Prefabulous/PA-VRM Add ARKit to BlendShapeAvatar")]
    public class PrefabulousAddARKitToBlendShapeAvatar : MonoBehaviour, IPrefabulousEditorOnly
    {
    }
}
