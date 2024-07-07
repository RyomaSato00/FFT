import matplotlib.pyplot as plt
import pandas as pd

# csv読み込み
input = pd.read_csv('input.csv')
output = pd.read_csv('output.csv')
output2 = pd.read_csv('output2.csv')

# x列とy列を取得
x_input = input.iloc[:, 0]
y_input = input.iloc[:, 1]

x_output = output.iloc[:, 0]
y_output = output.iloc[:, 1]

x_output2 = output2.iloc[:, 0]
y_output2 = output2.iloc[:, 1]

# グラフを作成する
fig, (ax1, ax2) = plt.subplots(2, 1, figsize=(10, 10))  # 2行1列のサブプロットを作成

ax1.plot(x_input, y_input, label='input')
ax1.plot(x_output2, y_output2, label='output2')

ax2.plot(x_output, y_output, label='output')

# グラフのタイトルと軸ラベルを設定
ax1.set_xlabel('X Axis')
ax1.set_ylabel('Y Axis')

ax2.set_xlabel('X Axis')
ax2.set_ylabel('Y Axis')

# グリッドを表示
ax1.grid(True)
ax2.grid(True)

# 凡例の追加
ax1.legend()
ax2.legend()

# グラフを表示
plt.show()
