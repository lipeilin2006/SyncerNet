using SyncerNet.Hotfix;
using SyncerNet.Hotfix.Syncers;
using UnityEngine;
using UnityEngine.UI;

namespace HotUpdate
{
    public class UIButtons : MonoBehaviour
    {
        public InputField playerIdInput;
        public InputField tokenInput;
        public InputField worldIdInput;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPlayerIdInputChanged()
        {
            PlayerConfigs.PlayerId = uint.Parse(playerIdInput.text);
        }

        public async void JoinGameBtnClick()
        {
            PlayerConfigs.PlayerId = uint.Parse(playerIdInput.text);
            PlayerConfigs.Token = tokenInput.text;
            bool isSuccess = await Game.Instance.TryJoinGame();
        }
        public async void CreateWorldBtnClick()
        {
            (bool isSuccess, uint worldId) = await Game.Instance.CreateWorld();
            if (isSuccess)
            {
				await Game.Instance.LoadSceneForWorld("Assets/HotUpdate/Resources/Scenes/GamePlay", worldId).Task;

				(bool success, uint entityId) = await Game.Instance.CurrentWorld.TryAddEntity("Assets/HotUpdate/Resources/Prefabs/Player.prefab");
				if (success)
				{
					Entity entity = Game.Instance.CurrentWorld.GetEntity(entityId);

					entity.Initialize();
					entity.GameObject.AddComponent<LocalPlayer>();

					entity.AddSyncer<AnimatorSyncer>();
					entity.AddSyncer<TransformSyncer>();
				}
			}
        }

        public async void JoinWorldBtnClick()
        {
            uint worldId = uint.Parse(worldIdInput.text);
			if (await Game.Instance.JoinWorld(worldId))
			{
				await Game.Instance.LoadSceneForWorld("Assets/HotUpdate/Resources/Scenes/GamePlay", worldId).Task;

				(bool isSuccess, uint entityId) = await Game.Instance.CurrentWorld.TryAddEntity("Assets/HotUpdate/Resources/Prefabs/Player.prefab");
				if (isSuccess)
				{
					Entity entity = Game.Instance.CurrentWorld.GetEntity(entityId);

					entity.Initialize();
					entity.GameObject.AddComponent<LocalPlayer>();

					entity.AddSyncer<AnimatorSyncer>();
					entity.AddSyncer<TransformSyncer>();
				}
			}
        }
    }
}
