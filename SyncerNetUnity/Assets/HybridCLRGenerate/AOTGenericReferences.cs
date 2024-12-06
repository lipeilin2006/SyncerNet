using System.Collections.Generic;
public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	public static readonly IReadOnlyList<string> PatchedAOTAssemblyList = new List<string>
	{
		"MemoryPack.Core.dll",
		"System.Runtime.CompilerServices.Unsafe.dll",
		"Unity.InputSystem.dll",
		"UnityEngine.CoreModule.dll",
		"YooAsset.dll",
		"mscorlib.dll",
	};
	// }}

	// {{ constraint implement type
	// }} 

	// {{ AOT generic types
	// MemoryPack.Formatters.ArrayFormatter<byte>
	// MemoryPack.Formatters.ArrayFormatter<float>
	// MemoryPack.Formatters.ArrayFormatter<int>
	// MemoryPack.Formatters.ArrayFormatter<object>
	// MemoryPack.Formatters.ConcurrentDictionaryFormatter<object,object>
	// MemoryPack.Formatters.ConcurrentDictionaryFormatter<uint,object>
	// MemoryPack.IMemoryPackFormatter<object>
	// MemoryPack.IMemoryPackable<object>
	// MemoryPack.MemoryPackFormatter<System.UIntPtr>
	// MemoryPack.MemoryPackFormatter<object>
	// System.Action<MemoryPack.Internal.BufferSegment>
	// System.Action<System.ArraySegment<byte>,byte>
	// System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>
	// System.Action<byte,object>
	// System.Action<object,object>
	// System.Action<object>
	// System.Action<uint>
	// System.ArraySegment.Enumerator<byte>
	// System.ArraySegment.Enumerator<float>
	// System.ArraySegment.Enumerator<int>
	// System.ArraySegment.Enumerator<ushort>
	// System.ArraySegment<byte>
	// System.ArraySegment<float>
	// System.ArraySegment<int>
	// System.ArraySegment<ushort>
	// System.Buffers.ArrayPool<byte>
	// System.Buffers.IBufferWriter<byte>
	// System.Buffers.TlsOverPerCoreLockedStacksArrayPool.LockedStack<byte>
	// System.Buffers.TlsOverPerCoreLockedStacksArrayPool.PerCoreLockedStacks<byte>
	// System.Buffers.TlsOverPerCoreLockedStacksArrayPool<byte>
	// System.ByReference<byte>
	// System.ByReference<float>
	// System.ByReference<int>
	// System.ByReference<ushort>
	// System.Collections.Concurrent.ConcurrentDictionary.<GetEnumerator>d__35<object,object>
	// System.Collections.Concurrent.ConcurrentDictionary.<GetEnumerator>d__35<uint,object>
	// System.Collections.Concurrent.ConcurrentDictionary.DictionaryEnumerator<object,object>
	// System.Collections.Concurrent.ConcurrentDictionary.DictionaryEnumerator<uint,object>
	// System.Collections.Concurrent.ConcurrentDictionary.Node<object,object>
	// System.Collections.Concurrent.ConcurrentDictionary.Node<uint,object>
	// System.Collections.Concurrent.ConcurrentDictionary.Tables<object,object>
	// System.Collections.Concurrent.ConcurrentDictionary.Tables<uint,object>
	// System.Collections.Concurrent.ConcurrentDictionary<object,object>
	// System.Collections.Concurrent.ConcurrentDictionary<uint,object>
	// System.Collections.Generic.ArraySortHelper<MemoryPack.Internal.BufferSegment>
	// System.Collections.Generic.ArraySortHelper<object>
	// System.Collections.Generic.ArraySortHelper<uint>
	// System.Collections.Generic.Comparer<MemoryPack.Internal.BufferSegment>
	// System.Collections.Generic.Comparer<byte>
	// System.Collections.Generic.Comparer<object>
	// System.Collections.Generic.Comparer<uint>
	// System.Collections.Generic.Dictionary.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.Enumerator<object,ushort>
	// System.Collections.Generic.Dictionary.Enumerator<uint,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,ushort>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<uint,object>
	// System.Collections.Generic.Dictionary.KeyCollection<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection<object,ushort>
	// System.Collections.Generic.Dictionary.KeyCollection<uint,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,ushort>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<uint,object>
	// System.Collections.Generic.Dictionary.ValueCollection<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection<object,ushort>
	// System.Collections.Generic.Dictionary.ValueCollection<uint,object>
	// System.Collections.Generic.Dictionary<object,object>
	// System.Collections.Generic.Dictionary<object,ushort>
	// System.Collections.Generic.Dictionary<uint,object>
	// System.Collections.Generic.EqualityComparer<byte>
	// System.Collections.Generic.EqualityComparer<object>
	// System.Collections.Generic.EqualityComparer<uint>
	// System.Collections.Generic.EqualityComparer<ushort>
	// System.Collections.Generic.ICollection<MemoryPack.Internal.BufferSegment>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,ushort>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<uint,object>>
	// System.Collections.Generic.ICollection<object>
	// System.Collections.Generic.ICollection<uint>
	// System.Collections.Generic.IComparer<MemoryPack.Internal.BufferSegment>
	// System.Collections.Generic.IComparer<object>
	// System.Collections.Generic.IComparer<uint>
	// System.Collections.Generic.IDictionary<object,object>
	// System.Collections.Generic.IDictionary<uint,object>
	// System.Collections.Generic.IEnumerable<MemoryPack.Internal.BufferSegment>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.UIntPtr,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,ushort>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<uint,object>>
	// System.Collections.Generic.IEnumerable<object>
	// System.Collections.Generic.IEnumerable<uint>
	// System.Collections.Generic.IEnumerator<MemoryPack.Internal.BufferSegment>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.UIntPtr,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,ushort>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<uint,object>>
	// System.Collections.Generic.IEnumerator<object>
	// System.Collections.Generic.IEnumerator<uint>
	// System.Collections.Generic.IEqualityComparer<object>
	// System.Collections.Generic.IEqualityComparer<uint>
	// System.Collections.Generic.IList<MemoryPack.Internal.BufferSegment>
	// System.Collections.Generic.IList<object>
	// System.Collections.Generic.IList<uint>
	// System.Collections.Generic.IReadOnlyDictionary<uint,object>
	// System.Collections.Generic.KeyValuePair<System.UIntPtr,object>
	// System.Collections.Generic.KeyValuePair<object,object>
	// System.Collections.Generic.KeyValuePair<object,ushort>
	// System.Collections.Generic.KeyValuePair<uint,object>
	// System.Collections.Generic.List.Enumerator<MemoryPack.Internal.BufferSegment>
	// System.Collections.Generic.List.Enumerator<object>
	// System.Collections.Generic.List.Enumerator<uint>
	// System.Collections.Generic.List<MemoryPack.Internal.BufferSegment>
	// System.Collections.Generic.List<object>
	// System.Collections.Generic.List<uint>
	// System.Collections.Generic.ObjectComparer<MemoryPack.Internal.BufferSegment>
	// System.Collections.Generic.ObjectComparer<byte>
	// System.Collections.Generic.ObjectComparer<object>
	// System.Collections.Generic.ObjectComparer<uint>
	// System.Collections.Generic.ObjectEqualityComparer<byte>
	// System.Collections.Generic.ObjectEqualityComparer<object>
	// System.Collections.Generic.ObjectEqualityComparer<uint>
	// System.Collections.Generic.ObjectEqualityComparer<ushort>
	// System.Collections.ObjectModel.ReadOnlyCollection<MemoryPack.Internal.BufferSegment>
	// System.Collections.ObjectModel.ReadOnlyCollection<object>
	// System.Collections.ObjectModel.ReadOnlyCollection<uint>
	// System.Comparison<MemoryPack.Internal.BufferSegment>
	// System.Comparison<object>
	// System.Comparison<uint>
	// System.Func<System.ValueTuple<byte,uint>>
	// System.Func<byte>
	// System.Func<object,System.ValueTuple<byte,uint>>
	// System.Func<object,byte>
	// System.Func<object,object,object>
	// System.Func<object,object>
	// System.Func<object>
	// System.Func<uint,object,object>
	// System.Func<uint,object>
	// System.Nullable<UnityEngine.InputSystem.InputAction.CallbackContext>
	// System.Predicate<MemoryPack.Internal.BufferSegment>
	// System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem>
	// System.Predicate<object>
	// System.Predicate<uint>
	// System.ReadOnlySpan.Enumerator<byte>
	// System.ReadOnlySpan.Enumerator<float>
	// System.ReadOnlySpan.Enumerator<int>
	// System.ReadOnlySpan.Enumerator<ushort>
	// System.ReadOnlySpan<byte>
	// System.ReadOnlySpan<float>
	// System.ReadOnlySpan<int>
	// System.ReadOnlySpan<ushort>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,uint>>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<byte>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<object>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<byte,uint>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<byte>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<object>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<byte,uint>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<byte>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<object>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,uint>>
	// System.Runtime.CompilerServices.TaskAwaiter<byte>
	// System.Runtime.CompilerServices.TaskAwaiter<object>
	// System.Span.Enumerator<byte>
	// System.Span.Enumerator<float>
	// System.Span.Enumerator<int>
	// System.Span.Enumerator<ushort>
	// System.Span<byte>
	// System.Span<float>
	// System.Span<int>
	// System.Span<ushort>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<byte,uint>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<byte>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<object>
	// System.Threading.Tasks.Task<System.ValueTuple<byte,uint>>
	// System.Threading.Tasks.Task<byte>
	// System.Threading.Tasks.Task<object>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<byte,uint>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<byte>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<object>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<byte,uint>>
	// System.Threading.Tasks.TaskFactory<byte>
	// System.Threading.Tasks.TaskFactory<object>
	// System.ValueTuple<byte,uint>
	// UnityEngine.InputSystem.InputBindingComposite<UnityEngine.Vector2>
	// UnityEngine.InputSystem.InputControl<UnityEngine.Vector2>
	// UnityEngine.InputSystem.InputProcessor<UnityEngine.Vector2>
	// UnityEngine.InputSystem.Utilities.InlinedArray<object>
	// }}

	public void RefMethods()
	{
		// System.Void MemoryPack.IMemoryPackFormatter<object>.Serialize<object>(MemoryPack.MemoryPackWriter<object>&,object&)
		// byte[] MemoryPack.Internal.MemoryMarshalEx.AllocateUninitializedArray<byte>(int,bool)
		// float[] MemoryPack.Internal.MemoryMarshalEx.AllocateUninitializedArray<float>(int,bool)
		// int[] MemoryPack.Internal.MemoryMarshalEx.AllocateUninitializedArray<int>(int,bool)
		// byte& MemoryPack.Internal.MemoryMarshalEx.GetArrayDataReference<byte>(byte[])
		// float& MemoryPack.Internal.MemoryMarshalEx.GetArrayDataReference<float>(float[])
		// int& MemoryPack.Internal.MemoryMarshalEx.GetArrayDataReference<int>(int[])
		// MemoryPack.MemoryPackFormatter<object> MemoryPack.MemoryPackFormatterProvider.GetFormatter<object>()
		// bool MemoryPack.MemoryPackFormatterProvider.IsRegistered<object>()
		// System.Void MemoryPack.MemoryPackFormatterProvider.Register<object>(MemoryPack.MemoryPackFormatter<object>)
		// System.Void MemoryPack.MemoryPackReader.DangerousReadUnmanagedArray<byte>(byte[]&)
		// System.Void MemoryPack.MemoryPackReader.DangerousReadUnmanagedArray<float>(float[]&)
		// System.Void MemoryPack.MemoryPackReader.DangerousReadUnmanagedArray<int>(int[]&)
		// byte[] MemoryPack.MemoryPackReader.DangerousReadUnmanagedArray<byte>()
		// float[] MemoryPack.MemoryPackReader.DangerousReadUnmanagedArray<float>()
		// int[] MemoryPack.MemoryPackReader.DangerousReadUnmanagedArray<int>()
		// MemoryPack.IMemoryPackFormatter<object> MemoryPack.MemoryPackReader.GetFormatter<object>()
		// System.Void MemoryPack.MemoryPackReader.ReadPackable<object>(object&)
		// object MemoryPack.MemoryPackReader.ReadPackable<object>()
		// System.Void MemoryPack.MemoryPackReader.ReadUnmanaged<UnityEngine.Vector3,UnityEngine.Vector3,UnityEngine.Vector3>(UnityEngine.Vector3&,UnityEngine.Vector3&,UnityEngine.Vector3&)
		// System.Void MemoryPack.MemoryPackReader.ReadUnmanaged<UnityEngine.Vector3>(UnityEngine.Vector3&)
		// System.Void MemoryPack.MemoryPackReader.ReadUnmanaged<byte,byte,uint>(byte&,byte&,uint&)
		// System.Void MemoryPack.MemoryPackReader.ReadUnmanaged<byte,byte>(byte&,byte&)
		// System.Void MemoryPack.MemoryPackReader.ReadUnmanaged<byte,uint,uint,uint>(byte&,uint&,uint&,uint&)
		// System.Void MemoryPack.MemoryPackReader.ReadUnmanaged<byte,uint,uint>(byte&,uint&,uint&)
		// System.Void MemoryPack.MemoryPackReader.ReadUnmanaged<byte,uint>(byte&,uint&)
		// System.Void MemoryPack.MemoryPackReader.ReadUnmanaged<byte>(byte&)
		// System.Void MemoryPack.MemoryPackReader.ReadUnmanaged<float>(float&)
		// System.Void MemoryPack.MemoryPackReader.ReadUnmanaged<uint,uint,uint>(uint&,uint&,uint&)
		// System.Void MemoryPack.MemoryPackReader.ReadUnmanaged<uint,uint>(uint&,uint&)
		// System.Void MemoryPack.MemoryPackReader.ReadUnmanaged<uint>(uint&)
		// System.Void MemoryPack.MemoryPackReader.ReadUnmanagedArray<byte>(byte[]&)
		// System.Void MemoryPack.MemoryPackReader.ReadUnmanagedArray<float>(float[]&)
		// System.Void MemoryPack.MemoryPackReader.ReadUnmanagedArray<int>(int[]&)
		// byte[] MemoryPack.MemoryPackReader.ReadUnmanagedArray<byte>()
		// float[] MemoryPack.MemoryPackReader.ReadUnmanagedArray<float>()
		// int[] MemoryPack.MemoryPackReader.ReadUnmanagedArray<int>()
		// System.Void MemoryPack.MemoryPackReader.ReadValue<object>(object&)
		// object MemoryPack.MemoryPackReader.ReadValue<object>()
		// int MemoryPack.MemoryPackSerializer.Deserialize<object>(System.ReadOnlySpan<byte>,object&,MemoryPack.MemoryPackSerializerOptions)
		// object MemoryPack.MemoryPackSerializer.Deserialize<object>(System.ReadOnlySpan<byte>,MemoryPack.MemoryPackSerializerOptions)
		// System.Void MemoryPack.MemoryPackSerializer.Serialize<object,object>(MemoryPack.MemoryPackWriter<object>&,object&)
		// byte[] MemoryPack.MemoryPackSerializer.Serialize<object>(object&,MemoryPack.MemoryPackSerializerOptions)
		// System.Void MemoryPack.MemoryPackWriter<object>.DangerousWriteUnmanagedArray<byte>(byte[])
		// System.Void MemoryPack.MemoryPackWriter<object>.DangerousWriteUnmanagedArray<float>(float[])
		// System.Void MemoryPack.MemoryPackWriter<object>.DangerousWriteUnmanagedArray<int>(int[])
		// MemoryPack.IMemoryPackFormatter<object> MemoryPack.MemoryPackWriter<object>.GetFormatter<object>()
		// System.Void MemoryPack.MemoryPackWriter<object>.WritePackable<object>(object&)
		// System.Void MemoryPack.MemoryPackWriter<object>.WriteUnmanaged<byte,byte,uint>(byte&,byte&,uint&)
		// System.Void MemoryPack.MemoryPackWriter<object>.WriteUnmanaged<byte,byte>(byte&,byte&)
		// System.Void MemoryPack.MemoryPackWriter<object>.WriteUnmanaged<byte,uint,uint,uint>(byte&,uint&,uint&,uint&)
		// System.Void MemoryPack.MemoryPackWriter<object>.WriteUnmanaged<byte,uint,uint>(byte&,uint&,uint&)
		// System.Void MemoryPack.MemoryPackWriter<object>.WriteUnmanaged<byte,uint>(byte&,uint&)
		// System.Void MemoryPack.MemoryPackWriter<object>.WriteUnmanaged<byte>(byte&)
		// System.Void MemoryPack.MemoryPackWriter<object>.WriteUnmanagedArray<byte>(byte[])
		// System.Void MemoryPack.MemoryPackWriter<object>.WriteUnmanagedArray<float>(float[])
		// System.Void MemoryPack.MemoryPackWriter<object>.WriteUnmanagedArray<int>(int[])
		// System.Void MemoryPack.MemoryPackWriter<object>.WriteUnmanagedWithObjectHeader<UnityEngine.Vector3,UnityEngine.Vector3,UnityEngine.Vector3>(byte,UnityEngine.Vector3&,UnityEngine.Vector3&,UnityEngine.Vector3&)
		// System.Void MemoryPack.MemoryPackWriter<object>.WriteUnmanagedWithObjectHeader<float>(byte,float&)
		// System.Void MemoryPack.MemoryPackWriter<object>.WriteUnmanagedWithObjectHeader<uint,uint,uint>(byte,uint&,uint&,uint&)
		// System.Void MemoryPack.MemoryPackWriter<object>.WriteUnmanagedWithObjectHeader<uint,uint>(byte,uint&,uint&)
		// System.Void MemoryPack.MemoryPackWriter<object>.WriteUnmanagedWithObjectHeader<uint>(byte,uint&)
		// System.Void MemoryPack.MemoryPackWriter<object>.WriteValue<object>(object&)
		// object System.Activator.CreateInstance<object>()
		// byte[] System.Array.Empty<byte>()
		// float[] System.Array.Empty<float>()
		// int[] System.Array.Empty<int>()
		// int System.Array.FindIndex<UnityEngine.LowLevel.PlayerLoopSystem>(UnityEngine.LowLevel.PlayerLoopSystem[],System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem>)
		// int System.Array.FindIndex<UnityEngine.LowLevel.PlayerLoopSystem>(UnityEngine.LowLevel.PlayerLoopSystem[],int,int,System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem>)
		// System.Void System.Array.Resize<UnityEngine.LowLevel.PlayerLoopSystem>(UnityEngine.LowLevel.PlayerLoopSystem[]&,int)
		// object System.Collections.Generic.CollectionExtensions.GetValueOrDefault<uint,object>(System.Collections.Generic.IReadOnlyDictionary<uint,object>,uint)
		// object System.Collections.Generic.CollectionExtensions.GetValueOrDefault<uint,object>(System.Collections.Generic.IReadOnlyDictionary<uint,object>,uint,object)
		// bool System.Collections.Generic.CollectionExtensions.Remove<uint,object>(System.Collections.Generic.IDictionary<uint,object>,uint,object&)
		// System.Span<byte> System.MemoryExtensions.AsSpan<byte>(byte[])
		// System.Span<float> System.MemoryExtensions.AsSpan<float>(float[])
		// System.Span<int> System.MemoryExtensions.AsSpan<int>(int[])
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,uint>>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,SyncerNet.Hotfix.Game.<CreateWorld>d__34>(System.Runtime.CompilerServices.TaskAwaiter<object>&,SyncerNet.Hotfix.Game.<CreateWorld>d__34&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,uint>>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,SyncerNet.Hotfix.World.<TryAddEntity>d__16>(System.Runtime.CompilerServices.TaskAwaiter<object>&,SyncerNet.Hotfix.World.<TryAddEntity>d__16&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,SyncerNet.Hotfix.Game.<JoinWorld>d__35>(System.Runtime.CompilerServices.TaskAwaiter<object>&,SyncerNet.Hotfix.Game.<JoinWorld>d__35&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,SyncerNet.Hotfix.Game.<TryJoinGame>d__33>(System.Runtime.CompilerServices.TaskAwaiter<object>&,SyncerNet.Hotfix.Game.<TryJoinGame>d__33&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,SyncerNet.Hotfix.World.<TryRemoveEntity>d__17>(System.Runtime.CompilerServices.TaskAwaiter<object>&,SyncerNet.Hotfix.World.<TryRemoveEntity>d__17&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,SyncerNet.Hotfix.NetworkClient.<Send>d__19>(System.Runtime.CompilerServices.TaskAwaiter&,SyncerNet.Hotfix.NetworkClient.<Send>d__19&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,uint>>.Start<SyncerNet.Hotfix.Game.<CreateWorld>d__34>(SyncerNet.Hotfix.Game.<CreateWorld>d__34&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.ValueTuple<byte,uint>>.Start<SyncerNet.Hotfix.World.<TryAddEntity>d__16>(SyncerNet.Hotfix.World.<TryAddEntity>d__16&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<byte>.Start<SyncerNet.Hotfix.Game.<JoinWorld>d__35>(SyncerNet.Hotfix.Game.<JoinWorld>d__35&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<byte>.Start<SyncerNet.Hotfix.Game.<TryJoinGame>d__33>(SyncerNet.Hotfix.Game.<TryJoinGame>d__33&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<byte>.Start<SyncerNet.Hotfix.World.<TryRemoveEntity>d__17>(SyncerNet.Hotfix.World.<TryRemoveEntity>d__17&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<object>.Start<SyncerNet.Hotfix.NetworkClient.<Send>d__19>(SyncerNet.Hotfix.NetworkClient.<Send>d__19&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,HotUpdate.UIButtons.<CreateWorldBtnClick>d__7>(System.Runtime.CompilerServices.TaskAwaiter&,HotUpdate.UIButtons.<CreateWorldBtnClick>d__7&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,HotUpdate.UIButtons.<JoinWorldBtnClick>d__8>(System.Runtime.CompilerServices.TaskAwaiter&,HotUpdate.UIButtons.<JoinWorldBtnClick>d__8&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,uint>>,HotUpdate.UIButtons.<CreateWorldBtnClick>d__7>(System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,uint>>&,HotUpdate.UIButtons.<CreateWorldBtnClick>d__7&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,uint>>,HotUpdate.UIButtons.<JoinWorldBtnClick>d__8>(System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<byte,uint>>&,HotUpdate.UIButtons.<JoinWorldBtnClick>d__8&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<byte>,HotUpdate.UIButtons.<JoinGameBtnClick>d__6>(System.Runtime.CompilerServices.TaskAwaiter<byte>&,HotUpdate.UIButtons.<JoinGameBtnClick>d__6&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<byte>,HotUpdate.UIButtons.<JoinWorldBtnClick>d__8>(System.Runtime.CompilerServices.TaskAwaiter<byte>&,HotUpdate.UIButtons.<JoinWorldBtnClick>d__8&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<HotUpdate.UIButtons.<CreateWorldBtnClick>d__7>(HotUpdate.UIButtons.<CreateWorldBtnClick>d__7&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<HotUpdate.UIButtons.<JoinGameBtnClick>d__6>(HotUpdate.UIButtons.<JoinGameBtnClick>d__6&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<HotUpdate.UIButtons.<JoinWorldBtnClick>d__8>(HotUpdate.UIButtons.<JoinWorldBtnClick>d__8&)
		// bool System.Runtime.CompilerServices.RuntimeHelpers.IsReferenceOrContainsReferences<object>()
		// byte& System.Runtime.CompilerServices.Unsafe.Add<byte>(byte&,int)
		// byte& System.Runtime.CompilerServices.Unsafe.As<byte,byte>(byte&)
		// byte& System.Runtime.CompilerServices.Unsafe.As<float,byte>(float&)
		// byte& System.Runtime.CompilerServices.Unsafe.As<int,byte>(int&)
		// object& System.Runtime.CompilerServices.Unsafe.As<object,object>(object&)
		// object& System.Runtime.CompilerServices.Unsafe.AsRef<object>(object&)
		// UnityEngine.Vector3 System.Runtime.CompilerServices.Unsafe.ReadUnaligned<UnityEngine.Vector3>(byte&)
		// byte System.Runtime.CompilerServices.Unsafe.ReadUnaligned<byte>(byte&)
		// float System.Runtime.CompilerServices.Unsafe.ReadUnaligned<float>(byte&)
		// object System.Runtime.CompilerServices.Unsafe.ReadUnaligned<object>(byte&)
		// uint System.Runtime.CompilerServices.Unsafe.ReadUnaligned<uint>(byte&)
		// int System.Runtime.CompilerServices.Unsafe.SizeOf<UnityEngine.Vector3>()
		// int System.Runtime.CompilerServices.Unsafe.SizeOf<byte>()
		// int System.Runtime.CompilerServices.Unsafe.SizeOf<float>()
		// int System.Runtime.CompilerServices.Unsafe.SizeOf<int>()
		// int System.Runtime.CompilerServices.Unsafe.SizeOf<object>()
		// int System.Runtime.CompilerServices.Unsafe.SizeOf<uint>()
		// System.Void System.Runtime.CompilerServices.Unsafe.WriteUnaligned<UnityEngine.Vector3>(byte&,UnityEngine.Vector3)
		// System.Void System.Runtime.CompilerServices.Unsafe.WriteUnaligned<byte>(byte&,byte)
		// System.Void System.Runtime.CompilerServices.Unsafe.WriteUnaligned<float>(byte&,float)
		// System.Void System.Runtime.CompilerServices.Unsafe.WriteUnaligned<int>(byte&,int)
		// System.Void System.Runtime.CompilerServices.Unsafe.WriteUnaligned<object>(byte&,object)
		// System.Void System.Runtime.CompilerServices.Unsafe.WriteUnaligned<uint>(byte&,uint)
		// byte& System.Runtime.InteropServices.MemoryMarshal.GetReference<byte>(System.ReadOnlySpan<byte>)
		// byte& System.Runtime.InteropServices.MemoryMarshal.GetReference<byte>(System.Span<byte>)
		// float& System.Runtime.InteropServices.MemoryMarshal.GetReference<float>(System.Span<float>)
		// int& System.Runtime.InteropServices.MemoryMarshal.GetReference<int>(System.Span<int>)
		// System.Void* Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf<UnityEngine.Vector2>(UnityEngine.Vector2&)
		// int Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<UnityEngine.Vector2>()
		// object UnityEngine.Component.GetComponent<object>()
		// object UnityEngine.GameObject.AddComponent<object>()
		// object UnityEngine.GameObject.GetComponent<object>()
		// bool UnityEngine.GameObject.TryGetComponent<object>(object&)
		// UnityEngine.Vector2 UnityEngine.InputSystem.InputAction.CallbackContext.ReadValue<UnityEngine.Vector2>()
		// UnityEngine.Vector2 UnityEngine.InputSystem.InputActionState.ApplyProcessors<UnityEngine.Vector2>(int,UnityEngine.Vector2,UnityEngine.InputSystem.InputControl<UnityEngine.Vector2>)
		// UnityEngine.Vector2 UnityEngine.InputSystem.InputActionState.ReadValue<UnityEngine.Vector2>(int,int,bool)
		// UnityEngine.Vector2 UnityEngine.InputSystem.InputValue.Get<UnityEngine.Vector2>()
		// YooAsset.AssetHandle YooAsset.ResourcePackage.LoadAssetSync<object>(string)
	}
}