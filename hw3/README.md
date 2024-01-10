# 系統晶片設計實習
***電子四甲 C109112174 李洋誠***
## SoC_Project3 : ISR 中斷處理
> - helloworld.c : SDK程式碼
> - HW3.pptx : 作業報告(內含: ISR與主程式介紹、IRQ編號、中斷相關暫存器名稱、位址、手冊名稱與頁碼)
> - 實際動作影片 : https://youtube.com/shorts/XqiiHQfE-ak?feature=share

## HW3 作業說明
> - (1)中斷前的動作 : 可以看到欄位的Main Count LED，顯示無窮迴圈上數的數值
> - (2)按下button執行中斷後 :
> - a.可以看到欄位首先會顯示interrutpt
> - b.接者會將LED顯示的數值改為interrupt_led_data，該數值計算方式如下 : led_data = led_data + btn_value; 影片中的按鈕數值為+2，因此數值改變的方式為 : led_data = led_data + 2;
> - (3)當按鈕放開後會回復到Main Count LED，顯示無窮迴圈上數的數值的動作


 
