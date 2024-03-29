/******************************************************************************
*
* Copyright (C) 2009 - 2014 Xilinx, Inc.  All rights reserved.
*
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
*
* Use of the Software is limited solely to applications:
* (a) running on a Xilinx device, or
* (b) that interact with a Xilinx device through a bus or interconnect.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
* XILINX  BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
* WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF
* OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*
* Except as contained in this notice, the name of the Xilinx shall not be used
* in advertising or otherwise to promote the sale, use or other dealings in
* this Software without prior written authorization from Xilinx.
*
******************************************************************************/

/*
 * helloworld.c: simple test application
 *
 * This application configures UART 16550 to baud rate 9600.
 * PS7 UART (Zynq) is not initialized by this application, since
 * bootrom/bsp configures it to baud rate 115200
 *
 * ------------------------------------------------
 * | UART TYPE   BAUD RATE                        |
 * ------------------------------------------------
 *   uartns550   9600
 *   uartlite    Configurable only in HW design
 *   ps7_uart    115200 (configured by bootrom/bsp)
 */

//initial head file
#include <stdio.h>
#include "platform.h"
#include "xil_printf.h"

//additional head file
#include "xparameters.h"
#include "xscugic.h"		//包含中斷管理和處理有關的函數
#include "xil_exception.h"	//異常處理和中斷處裡的函數與定義
#include "xgpio.h"			//
#include "xuartps.h"		//包含UART有關得函數
#include "sleep.h"			//延遲時間函數


#include "Sokoban.h"		//推箱子遊戲的函示庫


// Parameter definitions
#define INTC_DEVICE_ID 			XPAR_PS7_SCUGIC_0_DEVICE_ID
#define LEDS_DEVICE_ID			XPAR_AXI_GPIO_0_DEVICE_ID					//led8bits
#define BTNS_DEVICE_ID			XPAR_AXI_GPIO_1_DEVICE_ID					//btn5bits

#define INTC_GPIO_INTERRUPT_ID 	XPAR_FABRIC_AXI_GPIO_1_IP2INTC_IRPT_INTR	//我猜是[PS]設定的中斷接口
#define BTN_INT 				XGPIO_IR_CH1_MASK

//volatile定義 : 每次訪問時從記憶體中重新讀取其值
volatile static u32 ByteSend = 0 , TotalByteSend = 0;

static int Sokoban_Game_State = 0;											//未結束:0   結束:1
static int map[Map1_HEIGHT][Map1_WIDTH]= {0};								//存放地圖陣列資料的資料
static int Remaining_Box_led;												//顯示剩餘目的地數量的led
static int btn_value;
static u8 TransmitBuffer[Map1_HEIGHT * Map1_WIDTH + 1] = {0};				//儲存轉換後[map的一維陣列]+[判斷遊戲是否結束]，共有:73bits

static int Person_X;														//X軸的人物座標
static int Person_Y;														//Y軸人物座標


XUartPs_Config *Config_1;													//宣告"Config_1"的指標變數，並使用「XUartPs_Config」的結構 (UART設備的設定)
XUartPs Uart_PS_1;															//宣告Uart_PS_1為XUartPs的結構 (XUartPs驅動程式實例資料結構)

XGpio LEDInst, BTNInst;														//宣告LEDInst,BTNInst為XGpio的結構
XScuGic INTCInst;															//宣告INTCInst為XScuGic的結構

//----------------------------------------------------
// PROTOTYPE FUNCTIONS (自己寫的函示)
//----------------------------------------------------

/*static : 限定函示只能在該文件使用，不能被其他程式調用*/
//1. UART-Send Data-Partial
static int	Uart_Init(void);
static int	Uart_SendData(void *InstancePtr);
//static int	Uart_SendData_test(void *InstancePtr);

//2. Button-interrupt-partial
static int 	InterruptSystemSetup(XScuGic *XScuGicInstancePtr);
static int 	IntcInitFunction_BTN(u16 DeviceId, XGpio *GpioInstancePtr);
static void	BTN_Intr_Handler(void *baseaddr_p);

//----------------------------------------------------
// 0. Main Function
//----------------------------------------------------
int main()
{
	init_platform();														//初始化UART、致能快取
	int status;																//設備設備初始化是否成功之變數

	//初始化地圖
	Initial_Map(map, init_map);												//自訂函式(當前地圖, 初始地圖)

    // 初始化LED
    status = XGpio_Initialize(&LEDInst, LEDS_DEVICE_ID);					//初始化XGpio (指向 GPIO 實例, 設備ID名稱)
    if (status != XST_SUCCESS)												//判斷 GPIO 初始化
    	return XST_FAILURE;													//失敗就結束程式

    // 初始化BTN
    status = XGpio_Initialize(&BTNInst, BTNS_DEVICE_ID);					//初始化XGpio (指向 GPIO 實例, 設備ID名稱)
    if (status != XST_SUCCESS)												//判斷 GPIO 初始化
    	return XST_FAILURE;													//失敗就結束程式

    //設定 GPIO 輸入/輸出
    XGpio_SetDataDirection(&LEDInst, 1, 0x00);								//設定 GPIO 輸入/輸出 (指向 GPIO 實例, GPIO的通道 , 方向設定 0:output 1:input)
    XGpio_SetDataDirection(&BTNInst, 1, 0xFF);								//設定 GPIO 輸入/輸出 (指向 GPIO 實例, GPIO的通道 , 方向設定 0:output 1:input)

    //初始化UART
    if(Uart_Init() == XST_FAILURE){											//判斷 UART 初始化
    	return XST_FAILURE;													//失敗就結束程式
	}
    else{
    	//printf("Uart Initialization Successful!\n");
    	Uart_SendData(&Uart_PS_1);										//UART 初始化成功後自動傳輸一次 UART 資料
	}

    //初始化中斷控制器
    status = IntcInitFunction_BTN(INTC_DEVICE_ID, &BTNInst);				//自訂函式 (設備ID ,GPIO 實例)
    if (status != XST_SUCCESS)												//判斷中斷控制器初始化
    	return XST_FAILURE;													//失敗就結束程式

    while(1){
    	if(isWinner(map)){													//自訂布林函式判斷是否通關 (當前地圖)
    		//printf("Winner!\n");
    		//printf("Do you wnat to play again?\n");
    		Remaining_Box(&Remaining_Box_led, map);							//自訂函式(剩餘目的地數量, 當前地圖)
    		XGpio_DiscreteWrite(&LEDInst, 1, Remaining_Box_led);			//將LED的資料寫進指定的GPIO通道的暫存器裡	(XGpio的實例, GPIO的通道, 要寫入暫存器的資料)

    		if(btn_value == 0)
    		    break;
    	}
    	else{
    		//判斷遊戲狀態
    		Judge_Game_State(&Sokoban_Game_State, map);								//自訂函式(遊戲狀態, 當前地圖)
    		//printf("Sokoban_Game_State is [%d]\n", Sokoban_Game_State);

    		//LED顯示剩餘擺放目的地數量
    		Remaining_Box(&Remaining_Box_led, map);						//自訂函式(剩餘目的地數量, 當前地圖)
    		XGpio_DiscreteWrite(&LEDInst, 1, Remaining_Box_led);			//將LED的資料寫進指定的GPIO通道的暫存器裡	(XGpio的實例, GPIO的通道, 要寫入暫存器的資料)
    		//printf("Remaining_Box_led = [%d]\n", Remaining_Box_led);

    		//尋找人物座標
    		Find_Person_Coordinates(&Person_X, &Person_Y, map);						//自訂函式(X軸, Y軸)
    		//printf("Person Coordinate is X[%d] Y[%d]\n", Person_X, Person_Y);

    		//usleep(500000);
    	}
    }
    cleanup_platform();															//關閉快取
    return 0;
}

//----------------------------------------------------
// 1. UART-Send Data-Partial
//----------------------------------------------------

//1.1 UART-Setup-initialization
int Uart_Init(void){
	int status = 0;

	/*1.在「xparameters.h」中尋找「是否有符合」的設備ID*/
	Config_1 = XUartPs_LookupConfig(XPAR_XUARTPS_0_DEVICE_ID);
	if (NULL == Config_1) {
		return XST_FAILURE;
	}

	/*UART初始化確認檢查*/
	status = XUartPs_CfgInitialize(&Uart_PS_1, Config_1, Config_1->BaseAddress);
	if (status != XST_SUCCESS) {
		return XST_FAILURE;
	}
	/*設置UART的BaudRate*/
	status = XUartPs_SetBaudRate(&Uart_PS_1,115200);
	if (status != XST_SUCCESS) {
		return XST_FAILURE;
	}
	/*設置UART的操作模式*/
	XUartPs_SetOperMode(&Uart_PS_1, XUARTPS_OPER_MODE_NORMAL);

	/*(簡單來說就是UART的FIFO儲存到多少bits(偏移量)會觸發動作，0x20U等於十進制的32
	 * 所以FIFO儲存到32bits(8 Bytes)的資料時會觸發動作)
	*/
	XUartPs_WriteReg(Uart_PS_1.Config.BaseAddress,XUARTPS_TXWM_OFFSET, 0x20U);

	return XST_SUCCESS;

}

//1.2 UART-Send-Data
int Uart_SendData(void *InstancePtr){
	MapToArray(TransmitBuffer, map, Sokoban_Game_State);
	//Print_Map_Data(TransmitBuffer);

	while (TotalByteSend < (sizeof(TransmitBuffer))) {
			ByteSend = XUartPs_Send(&Uart_PS_1,(u8*)&TransmitBuffer[TotalByteSend], 1);
			TotalByteSend += ByteSend;
	}
	TotalByteSend = 0;

	return TotalByteSend;
}

//----------------------------------------------------
// 2. Button-Interrupt-Partial
//----------------------------------------------------

//2.1 完成button interrupt的初始化設定
int InterruptSystemSetup(XScuGic *XScuGicInstancePtr) {
	// Enable interrupt
	XGpio_InterruptEnable(&BTNInst, BTN_INT);
	XGpio_InterruptGlobalEnable(&BTNInst);

	Xil_ExceptionRegisterHandler(XIL_EXCEPTION_ID_INT,
			(Xil_ExceptionHandler) XScuGic_InterruptHandler,
			XScuGicInstancePtr);
	Xil_ExceptionEnable();
	return XST_SUCCESS;
}

//2.2
int IntcInitFunction_BTN(u16 DeviceId, XGpio *GpioInstancePtr) {
	XScuGic_Config *IntcConfig;
	int status;

	// Interrupt controller initialisation
	IntcConfig = XScuGic_LookupConfig(DeviceId);
	status = XScuGic_CfgInitialize(&INTCInst, IntcConfig,IntcConfig->CpuBaseAddress);
	if (status != XST_SUCCESS)
		return XST_FAILURE;

	// Call to interrupt setup
	status = InterruptSystemSetup(&INTCInst);
	if (status != XST_SUCCESS)
		return XST_FAILURE;

	// Connect GPIO interrupt to handler
	status = XScuGic_Connect(&INTCInst, INTC_GPIO_INTERRUPT_ID,
			(Xil_ExceptionHandler) BTN_Intr_Handler, (void *) GpioInstancePtr);
	if (status != XST_SUCCESS)
		return XST_FAILURE;

	// Enable GPIO interrupts interrupt
	XGpio_InterruptEnable(GpioInstancePtr, 1);
	XGpio_InterruptGlobalEnable(GpioInstancePtr);

	// Enable GPIO and timer interrupts in the controller
	XScuGic_Enable(&INTCInst, INTC_GPIO_INTERRUPT_ID);

	return XST_SUCCESS;
}

//2.3 Button interrupt時會作的內容
void BTN_Intr_Handler(void *InstancePtr){

	// Disable GPIO interrupts
	XGpio_InterruptDisable(&BTNInst, BTN_INT);

	// Ignore additional button presses
	if ((XGpio_InterruptGetStatus(&BTNInst) & BTN_INT) != BTN_INT) {
		return;
	}

	btn_value = XGpio_DiscreteRead(&BTNInst, 1);

	// Increment counter based on button value
	// Reset if centre button pressed
	if(btn_value != 1){
		switch(btn_value){
			case 16:{
				if(Sokoban_Game_State == 0){
					Move_Up(map, Person_X, Person_Y);
					Judge_Game_State(&Sokoban_Game_State, map);
					Uart_SendData(&Uart_PS_1);
					//printf("\nMove UP : %d\n",btn_value);
					usleep(200000);
				}
				else
					Initial_Map(map, init_map);
			}break;

			case 2:{
				if(Sokoban_Game_State == 0){
					Move_Down(map, Person_X, Person_Y);
					Judge_Game_State(&Sokoban_Game_State, map);
					Uart_SendData(&Uart_PS_1);
					//printf("\nMove Down : %d\n",btn_value);
					usleep(200000);
				}
				else
					Initial_Map(map, init_map);
			}break;

			case 4:{
				if(Sokoban_Game_State == 0){
					Move_Left(map, Person_X, Person_Y);
					Judge_Game_State(&Sokoban_Game_State, map);
					Uart_SendData(&Uart_PS_1);
					//printf("\nMove Left : %d\n",btn_value);
					usleep(200000);
				}
				else
					Initial_Map(map, init_map);
			}break;

			case 8:{
				if(Sokoban_Game_State == 0){
					Move_Right(map, Person_X, Person_Y);
					Judge_Game_State(&Sokoban_Game_State, map);
					Uart_SendData(&Uart_PS_1);
					//printf("\nMove Right : %d\n",btn_value);
					usleep(200000);
				}
				else
					Initial_Map(map, init_map);
			}break;

			case 0:{
				if(Sokoban_Game_State == 0){
					Game_Reset(map, init_map);
					Judge_Game_State(&Sokoban_Game_State, map);
					Uart_SendData(&Uart_PS_1);
					//printf("\nReset Game : %d\n",btn_value);
					usleep(200000);
				}
				else{
				    //printf("\ngame over\n");
					break;
				}
			}break;

			default:{
				//printf("Error : Unknown btn_value\n");
			}break;
		}
	}
	(void) XGpio_InterruptClear(&BTNInst, BTN_INT);
	// Enable GPIO interrupts
	XGpio_InterruptEnable(&BTNInst, BTN_INT);
}
