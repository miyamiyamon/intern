import uuid

from mlagents_envs.environment import UnityEnvironment
from mlagents_envs.side_channel.side_channel import (IncomingMessage,
                                                     OutgoingMessage,
                                                     SideChannel)


class StringLogChannel(SideChannel):  # (1)

    # コンストラクタ
    def __init__(self) -> None:
        # ChannelIDの指定
        super().__init__(uuid.UUID('621f0a70-4f87-11ea-a6bf-784f4387d1f7'))

    # Unity側からのデータの受信 (3)
    def on_message_received(self, msg: IncomingMessage) -> None:
        print(msg.read_string())

    # Unity側へのデータの送信 (4)
    def send_string(self, data: str) -> None:
        msg = OutgoingMessage()
        msg.write_string(data)
        super().queue_message_to_send(msg)

# サイドチャネル付きでUnity環境を生成 (5)
string_log_channel = StringLogChannel()
env = UnityEnvironment(side_channels=[string_log_channel])

# Unity環境のリセット
env.reset()

# 動作確認
for i in range(1000):
    # Unity側へのデータの送信 (6)
    string_log_channel.send_string('Send Data from Python : ' + str(i))

    # 1ステップ実行
    env.step()

# Unity環境のクローズ
env.close()
