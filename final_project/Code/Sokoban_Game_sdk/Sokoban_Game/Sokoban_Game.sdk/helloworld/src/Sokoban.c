//initial head file
#include <stdio.h>
#include "platform.h"
#include "xil_printf.h"

#include<stdio.h>
#include<stdlib.h>
#include <stdbool.h>
#include <string.h>


//定義地圖長寬大小
#define Map1_HEIGHT 9
#define Map1_WIDTH	8

/*----------------------------------------------------
 * 	功能說明 : 遊戲機制的地圖，讀取初始的地圖資料
 *----------------------------------------------------
 * 	參數定義 :
 *	@param	Map_data	(輸出)	:要被存入初始地圖資料的參數
 *	@param	init_map	(輸入)	:初始地圖的資料參數
 */
void Initial_Map(int Map_data[Map1_HEIGHT][Map1_WIDTH], int init_map[Map1_HEIGHT][Map1_WIDTH]){
	for (int i = 0; i < Map1_HEIGHT; i++){
    	for (int j = 0; j < Map1_WIDTH; j++){
    		Map_data[i][j] = init_map[i][j];
   		}
	}
}

/*------------------------------------------------------
 * 	功能說明 : UART資料整合，將Map資料從二維陣列轉換為一維陣列並加上遊戲狀態
 *------------------------------------------------------
 * 	參數定義 :
 *	@param	byteArray	(輸出)	: 轉換後的「一維陣列」的地圖資料
 *	@param	Map_data	(輸入)	: 輸入的地圖資料
 *	@param	Game_State	(輸入)	: 輸入的遊戲狀態		|| 1.[0]:遊戲尚未完成  2.[1]:遊戲完成
 */
void MapToArray(u8 *byteArray, int Map_data[Map1_HEIGHT][Map1_WIDTH], int Game_State){

	u16 i,j;
	//u8 asicii_zero  = '0';								//數值方便偵錯時，用的ASICII轉換

	for (i = 0; i < Map1_HEIGHT; i++) {
        for (j = 0; j < Map1_WIDTH; j++) {
            byteArray[i * Map1_WIDTH + j] = Map_data[i][j];
        }
    }
    byteArray[Map1_HEIGHT * Map1_WIDTH] = Game_State;
}

/*----------------------------------------------------
 * 	功能說明 : 計算尚未被擺放箱子的剩餘數量
 *----------------------------------------------------
 * 	參數定義 :
 *	@param	cnt_data	(輸出)	: 計算目的地剩餘數量
 *	@param	Map_data	(輸入)	: 輸入的地圖資料(二維陣列的Map)
 */
void Remaining_Box(int *Remaining_Box_Total, int Map_data[Map1_HEIGHT][Map1_WIDTH]){
    u16 i,j;
    int cnt_data = 0;

    for(i=0; i<(Map1_HEIGHT); i++){
    	for(j=0; j<(Map1_WIDTH); j++){
    		if(Map_data[i][j] == 4){
    			cnt_data = cnt_data + 1;
    		}
    	}
    }
    *Remaining_Box_Total = cnt_data;
    //printf("\nBox : %d",cnt_data);
}

/*----------------------------------------------------
 * 	功能說明 : 判斷遊戲的目的地是否完全被擺放，並判斷遊戲是否通關
 *----------------------------------------------------
 * 	參數定義 :
 *	@param	true(1)		(輸出)	: 箱子全部擺放在目的地上
 *	@param	false(0)	(輸出)	: 箱子「尚未」全部擺放在目的地上
 *	@param	Map_data	(輸入)	: 輸入的地圖資料(二維陣列的Map)
 */
bool isWinner(int Map_data[Map1_HEIGHT][Map1_WIDTH]){
	for(int i = 0; i < Map1_HEIGHT; i++){
		for(int j = 0; j < Map1_WIDTH; j++){
			if(Map_data[i][j] == 4){
				return false;
			}
		}
	}
	return true;
}

/*----------------------------------------------------
 * 	功能說明 : 提供給UART傳輸的遊戲狀態判斷
 * 	功能缺失 : 遊戲通關後需要再次按下按鈕遊戲狀態才會改變成「1」
 *----------------------------------------------------
 * 	參數定義 :
 *	@param	Game_State	(輸出)	: 遊戲狀態判斷的結果		|| 1.[0]:遊戲尚未完成  2.[1]:遊戲完成
 *	@param	Map_data	(輸入)	: 輸入的地圖資料(二維陣列的Map)
 */
void Judge_Game_State(int *Sokoban_Game_State, int Map_data[Map1_HEIGHT][Map1_WIDTH]){
	u16 i,j;
	int State_Result = 0;

	for(i=0; i<Map1_HEIGHT; i++){
		for(j=0; j<Map1_WIDTH; j++){
			if(Map_data[i][j] == 4){
				State_Result = 1;
				break;
			}
		}
		if(State_Result){
			break;
		}
	}
	*Sokoban_Game_State = State_Result? 0 : 1;
}

/*----------------------------------------------------
 * 	在「二維陣列」的Map中尋找人物座標
 *----------------------------------------------------
 * 	參數定義 :
 *	@param	X			(輸出)	: 人物在的「Width」的位置
 *	@param	Y			(輸出)	: 人物在的「Height」的位置
 *	@param	Map_data	(輸入)	: 輸入的地圖資料(二維陣列的Map)
 */
void Find_Person_Coordinates(int *X, int *Y, int Map_data[Map1_HEIGHT][Map1_WIDTH]){
	u16 i,j;
	int X_Coordinate;
	int Y_Coordinate;

	for(i=0; i<Map1_HEIGHT; i++){
		for(j=0; j<Map1_WIDTH; j++){
			if(Map_data[i][j]==5 || Map_data[i][j]==8){
				X_Coordinate = j;
				Y_Coordinate = i;
				break;
			}
		}
	}
	*X = X_Coordinate;
	*Y = Y_Coordinate;
}

/*----------------------------------------------------
 *	按鈕功能->人物向「上」移動時，更新地圖資料
 *----------------------------------------------------
 * 	參數定義 :
 *  @param	Map_Updata	(輸入||輸出)	: 更新後的圖內容
 *	@param	Person_X	(輸入)		: 人物在的「Width」的位置
 *	@param	Person_Y	(輸入)		: 人物在的「Height」的位置
 */
void Move_Up(int Map_Update[Map1_HEIGHT][Map1_WIDTH], int Person_X, int Person_Y){
	if(Map_Update[Person_Y-1][Person_X]==0 || Map_Update[Person_Y-1][Person_X]==3){
		Map_Update[Person_Y-1][Person_X]+=5;
		Map_Update[Person_Y][Person_X]-=5;
	}
	else if((Map_Update[Person_Y-1][Person_X]==4 || Map_Update[Person_Y-1][Person_X]==7) &&
			(Map_Update[Person_Y-2][Person_X]!=1) && (Map_Update[Person_Y-2][Person_X]!=4)){
		Map_Update[Person_Y-2][Person_X]+=4;
		Map_Update[Person_Y-1][Person_X]+=1;
		Map_Update[Person_Y][Person_X]-=5;
	}
}

/*----------------------------------------------------
 *	按鈕功能->人物向「下」移動時，更新地圖資料
 *----------------------------------------------------
 * 	參數定義 :
 *  @param	Map_Updata	(輸入||輸出)	: 更新後的圖內容
 *	@param	Person_X	(輸入)		: 人物在的「Width」的位置
 *	@param	Person_Y	(輸入)		: 人物在的「Height」的位置
 */
void Move_Down(int Map_Update[Map1_HEIGHT][Map1_WIDTH], int Person_X, int Person_Y){
	if(Map_Update[Person_Y+1][Person_X]==0 || Map_Update[Person_Y+1][Person_X]==3){
		Map_Update[Person_Y+1][Person_X]+=5;
		Map_Update[Person_Y][Person_X]-=5;
	}else if((Map_Update[Person_Y+1][Person_X]==4 || Map_Update[Person_Y+1][Person_X]==7) &&
			 (Map_Update[Person_Y+2][Person_X]!=1) && (Map_Update[Person_Y+2][Person_X]!=4)){
		Map_Update[Person_Y+2][Person_X]+=4;
		Map_Update[Person_Y+1][Person_X]+=1;
		Map_Update[Person_Y][Person_X]-=5;
	}
}

/*----------------------------------------------------
 *	按鈕功能->人物向「右」移動時，更新地圖資料
 *----------------------------------------------------
 * 	參數定義 :
 *  @param	Map_Updata	(輸入||輸出)	: 更新後的圖內容
 *	@param	Person_X	(輸入)		: 人物在的「Width」的位置
 *	@param	Person_Y	(輸入)		: 人物在的「Height」的位置
 */
void Move_Right(int Map_Update[Map1_HEIGHT][Map1_WIDTH], int Person_X, int Person_Y){
	//printf("Person_X = %d  Person_Y = %d\n", Person_X, Person_Y);
	if(Map_Update[Person_Y][Person_X+1]==0 || Map_Update[Person_Y][Person_X+1]==3){
		Map_Update[Person_Y][Person_X+1]+=5;
		Map_Update[Person_Y][Person_X]-=5;
	}else if((Map_Update[Person_Y][Person_X+1]==4 || Map_Update[Person_Y][Person_X+1]==7) &&
			(Map_Update[Person_Y][Person_X+2]!=1) && (Map_Update[Person_Y][Person_X+2]!=4)){
		Map_Update[Person_Y][Person_X+2]+=4;
		Map_Update[Person_Y][Person_X+1]+=1;
		Map_Update[Person_Y][Person_X]-=5;
	}
}

/*----------------------------------------------------
 *	按鈕功能->人物向「左」移動時，更新地圖資料
 *----------------------------------------------------
 * 	參數定義 :
 *  @param	Map_Updata	(輸入||輸出)	: 更新後的圖內容
 *	@param	Person_X	(輸入)		: 人物在的「Width」的位置
 *	@param	Person_Y	(輸入)		: 人物在的「Height」的位置
 */
void Move_Left(int Map_Update[Map1_HEIGHT][Map1_WIDTH], int Person_X, int Person_Y){
	//printf("Person_X = %d  Person_Y = %d\n", Person_X, Person_Y);
	if(Map_Update[Person_Y][Person_X-1]==0||Map_Update[Person_Y][Person_X-1]==3){
		Map_Update[Person_Y][Person_X-1]+=5;
		Map_Update[Person_Y][Person_X]-=5;

	}else if((Map_Update[Person_Y][Person_X-1]==4||Map_Update[Person_Y][Person_X-1]==7) &&
			(Map_Update[Person_Y][Person_X-2]!=1)&&(Map_Update[Person_X-2][Person_X]!=4)){
		Map_Update[Person_Y][Person_X-2]+=4;
		Map_Update[Person_Y][Person_X-1]+=1;
		Map_Update[Person_Y][Person_X]-=5;
	}
}

/*----------------------------------------------------
 *	按鈕功能->重製遊戲初始化地圖
 *----------------------------------------------------
 * 	參數定義 :
 *	@param	Map_data	(輸出)	:要被存入初始地圖資料的參數
 *	@param	init_map	(輸入)	:初始地圖的資料參數
 */
void Game_Reset(int Map_data[Map1_HEIGHT][Map1_WIDTH], int init_map[Map1_HEIGHT][Map1_WIDTH]){
	for (int i = 0; i < Map1_HEIGHT; i++){
    	for (int j = 0; j < Map1_WIDTH; j++){
    		Map_data[i][j] = init_map[i][j];
   		}
	}
}

//----------------------------------------------------
//檢查UART傳輸的地圖資料-(最後需要刪除)
//----------------------------------------------------
void Print_Map_Data(u8 *Map_data){
	u16 i;
	for (i = 0; i < ((Map1_HEIGHT * Map1_WIDTH)+1); i++) {
		if(i == ((Map1_HEIGHT * Map1_WIDTH)+1)){
			printf("Over!");
		}else{
			printf("Map_data[%u]  Data:[%d]\n",(i), Map_data[i]);
		}
	}
}

