from gym_unity.envs import UnityToGymWrapper
from mlagents_envs.environment import UnityEnvironment

# Unity環境の生成（1）
unity_env = UnityEnvironment('3DBall')

# Gymラッパーでラップ（2）
env = UnityToGymWrapper(unity_env, allow_multiple_obs=True)

# 動作確認（3）
state = env.reset()
while True:
    # 環境の描画
    env.render()

    # 行動の取得
    action = env.action_space.sample()
    print('action:', action)

    # 1ステップ実行
    state, reward, done, info = env.step(action)
    print('state:', state)
    print('reward:', reward)

    # エピソード完了
    if done:
        print('done')
        state = env.reset()
