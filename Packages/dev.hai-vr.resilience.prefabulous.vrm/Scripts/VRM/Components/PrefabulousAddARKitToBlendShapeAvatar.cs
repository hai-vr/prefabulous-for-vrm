using UnityEngine;
#if VRC_SDK_VRCSDK3
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
