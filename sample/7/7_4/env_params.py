import numpy as np
from mlagents_envs.environment import ActionTuple, UnityEnvironment
from mlagents_envs.side_channel.environment_parameters_channel import \
    EnvironmentParametersChannel

# Unity環境の生成
channel = EnvironmentParametersChannel()
env = UnityEnvironment('3DBall', side_channels=[channel])
channel.set_float_parameter("scale", 3.0)  # scaleを3

# Unity環境のリセット
env.reset()

# BehaviorSpecの取得
behavior_names = list(env.behavior_specs.keys())
behavior_spec = env.behavior_specs[behavior_names[0]]

# 動作確認
while True:
    # 現在のステップの情報の取得
    decision_steps, terminal_steps = env.get_steps(behavior_names[0])

    # 行動の決定
    for i in decision_steps.agent_id:
        action = (np.random.rand(2) * 2.0 - 1.0).reshape(1, 2).astype(np.float32)
        action_tuple = ActionTuple(continuous=action)
        env.set_action_for_agent(behavior_names[0], i, action_tuple)

    # Unity環境の1ステップ実行
    env.step()

# Unity環境のクローズ
env.close()
