using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/FullLife")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "FullLife", message: "[Zombie] has recovered [threshold] hp", category: "Events", id: "241d2e61599b3dc93e13e6c5f7716e7a")]
public sealed partial class FullLife : EventChannel<GameObject, float> { }

