import numpy as np
from mlagents_envs.environment import ActionTuple, UnityEnvironment

# Unity環境の生成
env = UnityEnvironment('3DBall')

# Unity環境のリセット
env.reset()

# BehaviorNameのリストの取得
behavior_names = list(env.behavior_specs.keys())
print('behavior_names:', behavior_names)

# BehaviorSpecの取得
behavior_spec = env.behavior_specs[behavior_names[0]]

# BehaviorSpecの情報の確認
print('\n== BehaviorSpecの情報の確認 ==')
print('observation_specs:', behavior_spec.observation_specs)
print('action_spec:', behavior_spec.action_spec)

# 動作確認
while True:
    # 現在のステップの情報の取得
    decision_steps, terminal_steps = env.get_steps(behavior_names[0])

    # DecisionStepsの情報の確認
    print('\n== DecisionStepsの情報の確認 ==')
    print('obj:', decision_steps.obs)
    print('reward:', decision_steps.reward)
    print('agent_id:', decision_steps.agent_id)
    print('action_mask:', decision_steps.action_mask)

    # TerminalStepsの情報の確認
    print('\n== TerminalStepsの情報の確認 ==')
    print('obs:', terminal_steps.obs)
    print('reward:', terminal_steps.reward)
    print('agent_id:', terminal_steps.agent_id)
    print('interrupted:', terminal_steps.interrupted)

    # 行動の決定
    for i in decision_steps.agent_id:
        action = (np.random.rand(2) * 2.0 - 1.0).reshape(1, 2).astype(np.float32)
        action_tuple = ActionTuple(continuous=action)
        env.set_action_for_agent(behavior_names[0], i, action_tuple)

    # Unity環境の1ステップ実行
    env.step()

# Unity環境のクローズ
env.close()
