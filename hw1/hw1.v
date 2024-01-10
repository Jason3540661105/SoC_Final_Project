`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date: 2023/09/21 15:22:27
// Design Name: 
// Module Name: hw1
// Project Name: 
// Target Devices: 
// Tool Versions: 
// Description: 
// 
// Dependencies: 
// 
// Revision:
// Revision 0.01 - File Created
// Additional Comments:
// 
//////////////////////////////////////////////////////////////////////////////////


module hw1(
    output reg [7:0] led_o,
    input clk,
    input rst
    );
    
    wire clk_div;
    
    always@(posedge clk_div, negedge rst)begin
        if(rst == 1'd0)begin
            led_o <= 8'd0;
        end
        else begin
            led_o <= led_o + 8'd1; 
        end
    end
    
    clk_div clk_cnt1
    (
        .clk_out(clk_div),
        .clk(clk),
        .rst(rst)
    );
endmodule

module clk_div(
		output  clk_out,
		input   clk,
		input   rst
	);
	
	assign clk_out = clk_cnt[24];
    
	reg [24:0]clk_cnt;
    
    always@(posedge clk,negedge rst)begin
        if(!rst)begin
            clk_cnt <= 25'd0;
        end
        else begin
            clk_cnt <= clk_cnt + 25'd1;
        end
    end
endmodule
