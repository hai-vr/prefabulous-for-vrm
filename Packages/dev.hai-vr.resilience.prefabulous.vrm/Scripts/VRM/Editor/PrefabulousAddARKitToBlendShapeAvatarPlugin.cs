
using System.Collections.Generic;
using System.Linq;
using nadena.dev.ndmf;
using Prefabulous.Universal.Shared.Editor;
using Prefabulous.VRM.Editor;
using Prefabulous.VRM.Runtime;
using UnityEngine;
using VRM;

[assembly: ExportsPlugin(typeof(PrefabulousAddARKitToBlendShapeAvatarPlugin))]
namespace Prefabulous.VRM.Editor
{
    public class PrefabulousAddARKitToBlendShapeAvatarPlugin : Plugin<PrefabulousAddARKitToBlendShapeAvatarPlugin>
    {
        public override string QualifiedName => "dev.hai-vr.prefabulous.vrm.AddARKitToBlendShapeAvatar";
        public override string DisplayName => "Prefabulous for VRM and VTubing - Add ARKit to BlendShapeAvatar";
        
        private static readonly Dictionary<string, string> unifiedExpressionsShapesToARKitClips = new Dictionary<string, string>
        {
            // Added by hand
            { "MouthLeft", "MouthLeft" },
            { "MouthRight", "MouthRight" },
            // Stock
            { "EyeLookUpRight", "EyeLookUpRight" },
            { "EyeLookDownRight", "EyeLookDownRight" },
            { "EyeLookInRight", "EyeLookInRight" },
            { "EyeLookOutRight", "EyeLookOutRight" },
            { "EyeLookUpLeft", "EyeLookUpLeft" },
            { "EyeLookDownLeft", "EyeLookDownLeft" },
            { "EyeLookInLeft", "EyeLookInLeft" },
            { "EyeLookOutLeft", "EyeLookOutLeft" },
            { "EyeClosedRight", "EyeBlinkRight" },
            { "EyeClosedLeft", "EyeBlinkLeft" },
            { "EyeSquintRight", "EyeSquintRight" },
            { "EyeSquintLeft", "EyeSquintLeft" },
            { "EyeWideRight", "EyeWideRight" },
            { "EyeWideLeft", "EyeWideLeft" },
            { "BrowDownRight", "BrowDownRight" },
            { "BrowDownLeft", "BrowDownLeft" },
            { "BrowInnerUp", "BrowInnerUp" },
            { "BrowOuterUpRight", "BrowOuterUpRight" },
            { "BrowOuterUpLeft", "BrowOuterUpLeft" },
            { "NoseSneerRight", "NoseSneerRight" },
            { "NoseSneerLeft", "NoseSneerLeft" },
            { "CheekSquintRight", "CheekSquintRight" },
            { "CheekSquintLeft", "CheekSquintLeft" },
            { "CheekPuff", "CheekPuff" },
            { "JawOpen", "JawOpen" },
            { "MouthClosed", "MouthClose" },
            { "JawRight", "JawRight" },
            { "JawLeft", "JawLeft" },
            { "JawForward", "JawForward" },
            { "LipSuckUpper", "MouthRollUpper" },
            { "LipSuckLower", "MouthRollLower" },
            { "LipFunnel", "MouthFunnel" },
            { "LipPucker", "MouthPucker" },
            { "MouthUpperUpRight", "MouthUpperUpRight" },
            { "MouthUpperUpLeft", "MouthUpperUpLeft" },
            { "MouthLowerDownRight", "MouthLowerDownRight" },
            { "MouthLowerDownLeft", "MouthLowerDownLeft" },
            { "MouthSmileRight", "MouthSmileRight" },
            { "MouthSmileLeft", "MouthSmileLeft" },
            { "MouthFrownRight", "MouthFrownRight" },
            { "MouthFrownLeft", "MouthFrownLeft" },
            { "MouthStretchRight", "MouthStretchRight" },
            { "MouthStretchLeft", "MouthStretchLeft" },
            { "MouthDimpleRight", "MouthDimpleRight" },
            { "MouthDimpleLeft", "MouthDimpleLeft" },
            { "MouthRaiserUpper", "MouthShrugUpper" },
            { "MouthRaiserLower", "MouthShrugLower" },
            { "MouthPressRight", "MouthPressRight" },
            { "MouthPressLeft", "MouthPressLeft" },
            { "TongueOut", "TongueOut" },
        };
        private static readonly Dictionary<string, string> arKitShapesToARKitClips = new Dictionary<string, string>
        {
            // Added by hand
            { "mouthLeft", "MouthLeft" },
            { "mouthRight", "MouthRight" },
            // Stock
            { "eyeLookUpRight", "EyeLookUpRight" },
            { "eyeLookDownRight", "EyeLookDownRight" },
            { "eyeLookInRight", "EyeLookInRight" },
            { "eyeLookOutRight", "EyeLookOutRight" },
            { "eyeLookUpLeft", "EyeLookUpLeft" },
            { "eyeLookDownLeft", "EyeLookDownLeft" },
            { "eyeLookInLeft", "EyeLookInLeft" },
            { "eyeLookOutLeft", "EyeLookOutLeft" },
            { "eyeBlinkRight", "EyeBlinkRight" },
            { "eyeBlinkLeft", "EyeBlinkLeft" },
            { "eyeSquintRight", "EyeSquintRight" },
            { "eyeSquintLeft", "EyeSquintLeft" },
            { "eyeWideRight", "EyeWideRight" },
            { "eyeWideLeft", "EyeWideLeft" },
            { "browDownRight", "BrowDownRight" },
            { "browDownLeft", "BrowDownLeft" },
            { "browInnerUp", "BrowInnerUp" },
            { "browOuterUpRight", "BrowOuterUpRight" },
            { "browOuterUpLeft", "BrowOuterUpLeft" },
            { "noseSneerRight", "NoseSneerRight" },
            { "noseSneerLeft", "NoseSneerLeft" },
            { "cheekSquintRight", "CheekSquintRight" },
            { "cheekSquintLeft", "CheekSquintLeft" },
            { "cheekPuff", "CheekPuff" },
            { "jawOpen", "JawOpen" },
            { "mouthClose", "MouthClose" },
            { "jawRight", "JawRight" },
            { "jawLeft", "JawLeft" },
            { "jawForward", "JawForward" },
            { "mouthRollUpper", "MouthRollUpper" },
            { "mouthRollLower", "MouthRollLower" },
            { "mouthFunnel", "MouthFunnel" },
            { "mouthPucker", "MouthPucker" },
            { "mouthUpperUpRight", "MouthUpperUpRight" },
            { "mouthUpperUpLeft", "MouthUpperUpLeft" },
            { "mouthLowerDownRight", "MouthLowerDownRight" },
            { "mouthLowerDownLeft", "MouthLowerDownLeft" },
            { "mouthSmileRight", "MouthSmileRight" },
            { "mouthSmileLeft", "MouthSmileLeft" },
            { "mouthFrownRight", "MouthFrownRight" },
            { "mouthFrownLeft", "MouthFrownLeft" },
            { "mouthStretchRight", "MouthStretchRight" },
            { "mouthStretchLeft", "MouthStretchLeft" },
            { "mouthDimpleRight", "MouthDimpleRight" },
            { "mouthDimpleLeft", "MouthDimpleLeft" },
            { "mouthShrugUpper", "MouthShrugUpper" },
            { "mouthShrugLower", "MouthShrugLower" },
            { "mouthPressRight", "MouthPressRight" },
            { "mouthPressLeft", "MouthPressLeft" },
            { "tongueOut", "TongueOut" },
        };

        protected override void Configure()
        {
            var seq = InPhase(BuildPhase.Transforming)
                .AfterPlugin("Prefabulous.Universal.Shared.Editor.PrefabulousConvertBlendshapeConventionsPlugin");
            
            seq.Run("Add ARKit to BlendShapeAvatar", AddARKitToBlendShapeAvatar);
        }

        private void AddARKitToBlendShapeAvatar(BuildContext context)
        {
            var avatar = context.AvatarRootTransform;
            var comps = avatar.GetComponentsInChildren<PrefabulousAddARKitToBlendShapeAvatar>(true);
            if (comps.Length == 0) return;

            GenerateFor(avatar);
            
            PrefabulousUtil.DestroyAllAfterBake<PrefabulousAddARKitToBlendShapeAvatar>(context);
        }

        private void GenerateFor(Transform avatar)
        {
            var proxy = avatar.GetComponentInChildren<VRMBlendShapeProxy>(true);
            if (proxy == null) return;

            var copy = Object.Instantiate(proxy.BlendShapeAvatar);

            var clips = new Dictionary<string, BlendShapeClip>();
            
            var smrs = avatar.GetComponentsInChildren<SkinnedMeshRenderer>(true);
            foreach (var smr in smrs)
            {
                var smrPath = ResolveRelativePath(avatar, smr.transform);
                var mesh = smr.sharedMesh;

                var alreadyAddedClipsForThisMesh = new HashSet<string>();

                var matchers = ProvideShapeToClipMatchers();
                foreach (var shapeToARKitClips in matchers)
                {
                    for (var blendshapeIndex = 0; blendshapeIndex < mesh.blendShapeCount; blendshapeIndex++)
                    {
                        var name = mesh.GetBlendShapeName(blendshapeIndex);
                    
                        var clipName = AsClipNameOrNull(name, shapeToARKitClips);
                        if (clipName != null && !alreadyAddedClipsForThisMesh.Contains(clipName))
                        {
                            // Avoid adding both Unified Expressions and ARKit shapes to a single BlendShapeClip.
                            alreadyAddedClipsForThisMesh.Add(clipName);
                            if (!clips.ContainsKey(clipName))
                            {
                                var script = ScriptableObject.CreateInstance<BlendShapeClip>();
                                script.name = clipName;
                                script.BlendShapeName = clipName;
                                script.Values = new[]
                                {
                                    new BlendShapeBinding
                                    {
                                        Index = blendshapeIndex,
                                        Weight = 100,
                                        RelativePath = smrPath
                                    }
                                };
                                clips[name] = script;
                            }
                            else
                            {
                                var clip = clips[name];
                                clip.Values = clip.Values.Concat(new[]
                                {
                                    new BlendShapeBinding
                                    {
                                        Index = blendshapeIndex,
                                        Weight = 100,
                                        RelativePath = smrPath
                                    }

                                }).ToArray();
                            }
                        }
                    }
                }
            }

            foreach (var clip in clips.Values)
            {
                copy.SetClip(BlendShapeKey.CreateFromClip(clip), clip);
            }

            proxy.BlendShapeAvatar = copy;
        }

        private static Dictionary<string, string>[] ProvideShapeToClipMatchers()
        {
            // We want to search for all Unified Expressions shapes first, and then all ARKit shapes.
            // If an avatar has both Unified Expressions shapes and ARKit shapes, then the resulting VRMBlendShapeClip must
            // only reference one of them, not both, so that the expression doesn't get incorrectly amplified by twice the amount.
            return new [] { unifiedExpressionsShapesToARKitClips, arKitShapesToARKitClips };
        }

        private static string ResolveRelativePath(Transform avatar, Transform item)
        {
            if (avatar == item)
            {
                return "";
            }
            
            if (item.parent != avatar && item.parent != null)
            {
                return ResolveRelativePath(avatar, item.parent) + "/" + item.name;
            }

            return item.name;
        }

        private string AsClipNameOrNull(string name, Dictionary<string, string> shapeToARKitClips)
        {
            return shapeToARKitClips.TryGetValue(name, out var result) ? result : null;
        }
    }
}