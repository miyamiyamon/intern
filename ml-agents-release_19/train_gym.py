from gym_unity.envs import UnityToGymWrapper
from mlagents_envs.environment import UnityEnvironment
from mlagents_envs.side_channel.engine_configuration_channel import \
    EngineConfigurationChannel
from stable_baselines3 import PPO

# Unity環境の生成
channel = EngineConfigurationChannel()
unity_env = UnityEnvironment('3DBall', side_channels=[channel])
channel.set_configuration_parameters(time_scale=20.0)  # 高速化

# Gymラッパーでラップ
env = UnityToGymWrapper(unity_env)

# モデルの準備
model = PPO('MlpPolicy', env, verbose=1)

# 学習の実行
model.learn(total_timesteps=30000)

# 動作確認
state = env.reset()
while True:
    # 環境の描画
    env.render()

    # 行動の取得
    action, _ = model.predict(state)

    # 1ステップ実行
    state, reward, done, info = env.step(action)
    print('reward:', reward)

    # エピソード完了
    if done:
        print('done')
        state = env.reset()
