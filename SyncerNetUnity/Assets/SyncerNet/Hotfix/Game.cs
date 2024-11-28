using SyncerNet.Hotfix.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using YooAsset;

#nullable enable
namespace SyncerNet.Hotfix
{
    public class Game
    {
        public Dictionary<uint, World> Worlds { get; set; } = new();
        public World? CurrentWorld { get; private set; }
        public int Timeout { get; set; } = 10000;

        public bool IsReady { get; private set; } = false;

        private static Game? _instance;
        public NetworkClient Client { get; private set; }

        public Func<IEnumerator, Coroutine> StartCoroutineFunc { get; private set; }
        public static Game Instance
        {
            get
            {
                if (_instance == null)
                {
                    NetworkClient client = GameObject.Find("NetworkClient").GetComponent<NetworkClient>();
                    _instance = new Game(client, client.StartCoroutine);
                }
                return _instance;
            }
        }

        private Game(NetworkClient client, Func<IEnumerator, Coroutine> startCoroutineFunc)
        {
            Client = client;
            StartCoroutineFunc = startCoroutineFunc;
        }

        public void NetworkEarlyUpdate()
        {
            CurrentWorld?.NetworkEarlyUpdate();
        }

        public void NetworkLateUpdate()
        {
            CurrentWorld?.NetworkLateUpdate();
        }

        /// <summary>
        /// ���п��޵�Э�̣������ò���
        /// </summary>
        /// <param name="routine"></param>
        /// <returns></returns>
        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return StartCoroutineFunc.Invoke(routine);
        }

        /// <summary>
        /// �첽���س���ͬʱ����World
        /// </summary>
        /// <param name="sceneLocation">����·��</param>
        /// <param name="worldId"></param>
        /// <returns>SceneHandle�����ڻ�ȡ�������ؽ���</returns>
        public SceneHandle? LoadSceneForWorld(string sceneLocation, uint worldId)
        {
            World? world = GetWorld(worldId);
            if (world != null)
            {
                IsReady = false;
                CurrentWorld?.SetActive(false);
                CurrentWorld = world;

                SceneHandle handle = YooAssets.GetPackage("DefaultPackage").LoadSceneAsync(sceneLocation);
                handle.Completed += Handle_Completed;
                return handle;
            }
            return null;
        }

        /// <summary>
        /// ����������ɺ�Ļص�
        /// </summary>
        /// <param name="obj"></param>
        private void Handle_Completed(SceneHandle obj)
        {
            CurrentWorld?.SetActive(true);
            IsReady = true;
            var package = YooAssets.GetPackage("DefaultPackage");
            package.UnloadUnusedAssetsAsync();
            Debug.Log($"Loaded Scene {obj.SceneName}");
        }

        /// <summary>
        /// ������Ϸ�������κβ���֮ǰ��Ӧ���ȼ�����Ϸ��
        /// </summary>
        /// <returns></returns>
        public async Task<bool> TryJoinGame()
        {
            JoinGameRespMessage? response = await Client.Send(new JoinGameReqMessage(), true) as JoinGameRespMessage;
            if (response != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// ����World
        /// </summary>
        /// <returns>(isSuccess,WorldId)</returns>
        public async Task<(bool, uint)> CreateWorld()
        {
            CreateWorldRespMessage? response = await Client.Send(new CreateWorldReqMessage(), true) as CreateWorldRespMessage;
            if (response != null)
            {
                Worlds.Add(response.WorldId, new World() { WorldId = response.WorldId });
                return (response.Success, response.WorldId);
            }
            return (false, 0);
        }

        /// <summary>
        /// ����World
        /// </summary>
        /// <param name="worldId"></param>
        /// <returns>isSuccess</returns>
        public async Task<bool> JoinWorld(uint worldId)
        {
            JoinWorldRespMessage? response = await Client.Send(new JoinWorldReqMessage(worldId), true) as JoinWorldRespMessage;
            World? world = response?.World;
            if (response != null && world != null)
            {
                if (response.Success)
                {
                    Worlds.Add(world.WorldId, world);
                    foreach (Entity entity in world.Entities.Values)
                    {
                        entity.World = world;
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// ��ȡWorld,ǰ�����Ѿ��������World
        /// </summary>
        /// <param name="worldId">WorldId</param>
        /// <returns></returns>
        public World? GetWorld(uint worldId)
        {
            return Worlds.GetValueOrDefault(worldId);
        }
    }
}
